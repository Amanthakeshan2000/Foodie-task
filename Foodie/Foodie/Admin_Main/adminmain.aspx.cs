using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Mail;


namespace Foodie.Admin_Main
{
    public partial class adminmain : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                gvRestaurantRequests.Visible = false;
            }
        }

        protected void LoadRestaurantRequests(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(Connection.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("GetAdminsByStatus", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                gvRestaurantRequests.DataSource = dt;
                gvRestaurantRequests.DataBind();
                gvRestaurantRequests.Visible = true;
            }
        }

        protected void gvRestaurantRequests_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Accept" || e.CommandName == "Reject")
            {
                string adminID = e.CommandArgument.ToString();
                int newStatus = e.CommandName == "Accept" ? 1 : 99;

                UpdateRestaurantStatus(adminID, newStatus);
                LoadRestaurantRequests(sender, e); 
            }
            else if (e.CommandName == "ViewDetails")
            {
                string adminID = e.CommandArgument.ToString();
                DataTable adminDetails = GetAdminDetailsById(adminID);

                if (adminDetails.Rows.Count > 0)
                {
                    DataRow row = adminDetails.Rows[0];
                    string adminName = row["Name"].ToString();
                    string adminStatus = row["Status"].ToString();
                    string adminUserName = row["UserName"].ToString();

                    string script = $"showModal('{adminID}', '{adminName}', '{adminStatus}', '{adminUserName}');";
                    ScriptManager.RegisterStartupScript(this, GetType(), "showModal", script, true);
                }
            }
        }

        private void UpdateRestaurantStatus(string adminID, int status)
        {
            using (SqlConnection conn = new SqlConnection(Connection.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("UpdateAdminStatusTo1", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@AdminID", adminID);
                cmd.Parameters.AddWithValue("@Status", status);

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }

            // Determine the status message
            string statusMessage = status == 1 ? "accepted" : "rejected";

            // Send email notification
            SendEmailNotification(adminID, statusMessage);
        }
        private void SendEmailNotification(string adminID, string statusMessage)
        {
            // Define the sender and receiver email addresses
            string senderEmail = "pdakdhanujaya@gmail.com";
            string receiverEmail = "ganeshikavindya2517@gmail.com";
            string subject = $"Request {statusMessage}";
            string body = $"Dear Admin,\n\nThe request with Admin ID {adminID} has been {statusMessage}.\n\nBest regards,\nFoodie Admin Team";

            // Configure the SmtpClient
            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587)
            {
                Credentials = new System.Net.NetworkCredential("pdakdhanujaya@gmail.com", "dxpe hljl tsii gcoo"), // Using the app password
                EnableSsl = true
            };

            try
            {
                // Create and configure the email message
                MailMessage mailMessage = new MailMessage(senderEmail, receiverEmail)
                {
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = false
                };

                // Send the email
                smtpClient.Send(mailMessage);
            }
            catch (Exception ex)
            {
                // Log or handle the error if needed
                Console.WriteLine("Error sending email: " + ex.Message);
            }
        }


        private DataTable GetAdminDetailsById(string adminID)
        {
            DataTable dt = new DataTable();

            using (SqlConnection conn = new SqlConnection(Connection.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("GetRestaurantDetailsByUsername", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserName", adminID);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dt);
            }

            return dt;
        }
    }
}
