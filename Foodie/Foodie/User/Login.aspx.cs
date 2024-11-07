using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using Foodie.Admin;

namespace Foodie.User
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["userId"] != null)
            {
                Response.Redirect("../User/Default.aspx");
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();
            string userType = ddlUserType.SelectedValue;

            // Validate inputs
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(userType))
            {
                ShowErrorMessage("Username, Password, and User Type cannot be NULL.");
                return;
            }

            // Check if the user is an admin with hardcoded credentials
            if (userType == "Admin")
            {
                if (username == "Admin" && password == "123")
                {
                    // Redirect to admin page if the credentials are correct
                    Response.Redirect("../Admin_Main/adminmain.aspx", false);
                    Context.ApplicationInstance.CompleteRequest();
                    return;
                }
                else
                {
                    ShowErrorMessage("Invalid Admin credentials.");
                    return;
                }
            }

            // Database connection and query execution for other user types
            using (SqlConnection con = new SqlConnection(Connection.GetConnectionString()))
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;
                    SqlDataAdapter sda;
                    DataTable dt;

                    if (userType == "Seller")
                    {
                        cmd.CommandText = "SP_GET_AdminByUserNameAndPassword";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Action", "SELECTADMINLOGIN");
                        cmd.Parameters.AddWithValue("@Username", username);
                        cmd.Parameters.AddWithValue("@Password", password);
                    }
                    else if (userType == "User")
                    {
                        cmd.CommandText = "User_Crud";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Action", "SELECT4LOGIN");
                        cmd.Parameters.AddWithValue("@Username", username);
                        cmd.Parameters.AddWithValue("@Password", password);
                    }
                    else if (userType == "Organization")
                    {
                        cmd.CommandText = "Organization_Crud";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Action", "SELECT4LOGIN");
                        cmd.Parameters.AddWithValue("@Name", username);
                        cmd.Parameters.AddWithValue("@Password", password);
                    }
                    else
                    {
                        ShowErrorMessage("Invalid User Type selected.");
                        return;
                    }

                    sda = new SqlDataAdapter(cmd);
                    dt = new DataTable();
                    sda.Fill(dt);

                    if (dt.Rows.Count >= 1)
                    {
                        if (userType == "Seller")
                        {
                            Session["Name"] = dt.Rows[0]["Name"].ToString();
                            Response.Redirect("../Admin/Dashboard.aspx", false);
                        }
                        else if (userType == "User")
                        {
                            Session["userId"] = dt.Rows[0]["userId"].ToString();
                            Response.Redirect("../User/Default.aspx", false);
                        }
                        else if (userType == "Organization")
                        {
                            Session["Organization"] = username;
                            Response.Redirect("../Organization/Default.aspx", false);
                        }
                        Context.ApplicationInstance.CompleteRequest();
                    }
                    else
                    {
                        ShowErrorMessage("Invalid Credentials..!");
                    }
                }
                catch (Exception ex)
                {
                    // Log the exception if necessary
                    ShowErrorMessage("An error occurred. Please try again later.");
                }
            }
        }

        private void ShowErrorMessage(string message)
        {
            lblMsg.Visible = true;
            lblMsg.Text = message;
            lblMsg.CssClass = "alert alert-danger";
        }
    }
}
