using System;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Net.Mail;
using System.Web.UI;

namespace Foodie.User
{
    public partial class Payment : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["userId"] == null)
                {
                    Response.Redirect("Login.aspx");
                }
            }
        }

        protected void LbCodSubmit_Click(object sender, EventArgs e)
        {
            string paymentMode = "cod";

            if (Session["userId"] != null)
            {
            }
            else
            {
                Response.Redirect("Login.aspx");
            }
        }

        private void ProcessOrder(string address, string paymentMode)
        {
            int paymentId;
            DataTable orderDetailsTable = new DataTable();
            orderDetailsTable.Columns.AddRange(new DataColumn[]
            {
                new DataColumn("ProductName", typeof(string)),
                new DataColumn("Quantity", typeof(int)),
                new DataColumn("TotalPrice", typeof(decimal))
            });

            using (SqlConnection con = new SqlConnection(Connection.GetConnectionString()))
            {
                con.Open();
                SqlTransaction transaction = con.BeginTransaction();

                try
                {
                    string customerEmail = GetCustomerEmail(Convert.ToInt32(Session["userId"]), con, transaction);

                    paymentId = SavePaymentDetails(address, paymentMode, con, transaction);

                    RetrieveAndProcessCartItems(orderDetailsTable, con, transaction);

                    transaction.Commit();

                    bool emailSent = SendOrderConfirmationEmail(customerEmail, orderDetailsTable);

                    lblMsg.Visible = true;
                    lblMsg.Text = emailSent
                        ? "Your order was successfully placed and a confirmation email has been sent!"
                        : "Order placed, but there was an error sending the confirmation email.";
                    lblMsg.CssClass = emailSent ? "alert alert-success" : "alert alert-warning";

                    Response.AddHeader("REFRESH", "2;URL=Invoice.aspx?id=" + paymentId);
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    lblMsg.Visible = true;
                    lblMsg.Text = "Error: " + ex.Message;
                    lblMsg.CssClass = "alert alert-danger";
                }
            }
        }

        private string GetCustomerEmail(int userId, SqlConnection con, SqlTransaction transaction)
        {
            using (SqlCommand cmd = new SqlCommand("SELECT Email FROM Users WHERE UserId = @UserId", con, transaction))
            {
                cmd.Parameters.AddWithValue("@UserId", userId);
                return cmd.ExecuteScalar()?.ToString();
            }
        }

        private int SavePaymentDetails(string address, string paymentMode, SqlConnection con, SqlTransaction transaction)
        {
            using (SqlCommand cmd = new SqlCommand("Save_Payment", con, transaction))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Name", "name");
                cmd.Parameters.AddWithValue("@Address", address);
                cmd.Parameters.AddWithValue("@PaymentMode", paymentMode);
                SqlParameter outputId = new SqlParameter("@InsertedId", SqlDbType.Int) { Direction = ParameterDirection.Output };
                cmd.Parameters.Add(outputId);
                cmd.ExecuteNonQuery();
                return Convert.ToInt32(outputId.Value);
            }
        }

        private void RetrieveAndProcessCartItems(DataTable orderDetailsTable, SqlConnection con, SqlTransaction transaction)
        {
            using (SqlCommand cmd = new SqlCommand("Cart_Crud", con, transaction))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "SELECT");
                cmd.Parameters.AddWithValue("@UserId", Session["userId"]);

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        string productName = dr["Name"].ToString();
                        int quantity = Convert.ToInt32(dr["Quantity"]);
                        decimal price = Convert.ToDecimal(dr["Price"]) * quantity;

                        orderDetailsTable.Rows.Add(productName, quantity, price);

                        UpdateProductQuantity(Convert.ToInt32(dr["ProductId"]), quantity, transaction, con);
                        DeleteCartItem(Convert.ToInt32(dr["ProductId"]), transaction, con);
                    }
                }
            }
        }

        private bool SendOrderConfirmationEmail(string customerEmail, DataTable orderDetails)
        {
            try
            {
                string subject = "Your Order Confirmation - Foodie";
                string body = "Thank you for your order! Here are the details:<br/><br/>";
                body += "<table border='1' cellpadding='5' cellspacing='0'><tr><th>Product</th><th>Quantity</th><th>Total Price</th></tr>";

                foreach (DataRow row in orderDetails.Rows)
                {
                    body += $"<tr><td>{row["ProductName"]}</td><td>{row["Quantity"]}</td><td>Rs. {row["TotalPrice"]:0.00}</td></tr>";
                }
                body += "</table><br/>We hope you enjoy your purchase!";

                MailMessage mail = new MailMessage
                {
                    From = new MailAddress("pdakdhanujaya@gmail.com"),
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true
                };
                mail.To.Add(customerEmail);

                using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                {
                    smtp.Credentials = new NetworkCredential("pdakdhanujaya@gmail.com", "ecqkggccmajtqxzh");
                    smtp.EnableSsl = true;
                    smtp.Send(mail);
                }

                return true;
            }
            catch (SmtpException smtpEx)
            {
                System.Diagnostics.Debug.WriteLine("SMTP error: " + smtpEx.Message);
                System.Diagnostics.Debug.WriteLine("SMTP stack trace: " + smtpEx.StackTrace);
                lblMsg.Visible = true;
                lblMsg.Text = "There was a problem sending the email. Please try again later.";
                lblMsg.CssClass = "alert alert-danger";
                return false;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("General error: " + ex.Message);
                System.Diagnostics.Debug.WriteLine("General stack trace: " + ex.StackTrace);
                lblMsg.Visible = true;
                lblMsg.Text = "An unexpected error occurred while sending the email.";
                lblMsg.CssClass = "alert alert-danger";
                return false;
            }
        }

        private void UpdateProductQuantity(int productId, int quantity, SqlTransaction transaction, SqlConnection con)
        {
            using (SqlCommand cmd = new SqlCommand("Product_Crud", con, transaction))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("Action", "QTYUPDATE");
                cmd.Parameters.AddWithValue("Quantity", quantity);
                cmd.Parameters.AddWithValue("ProductId", productId);
                cmd.ExecuteNonQuery();
            }
        }

        private void DeleteCartItem(int productId, SqlTransaction transaction, SqlConnection con)
        {
            using (SqlCommand cmd = new SqlCommand("Cart_Crud", con, transaction))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "DELETE");
                cmd.Parameters.AddWithValue("@ProductId", productId);
                cmd.Parameters.AddWithValue("@UserId", Session["userId"]);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
