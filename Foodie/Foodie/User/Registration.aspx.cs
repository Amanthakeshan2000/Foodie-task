using System;
using System.Data.SqlClient;
using System.Data;
using System.IO;

namespace Foodie.User
{
    public partial class Registration : System.Web.UI.Page
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter sda;
        DataTable dt;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Redirect if user is already logged in
                if (Session["userId"] != null)
                {
                    Response.Redirect("Default.aspx");
                }
            }
        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            string userType = ddlUserType.SelectedValue;
            string imagePath = string.Empty, fileExtension = string.Empty;
            bool isValidToExecute = false;

            con = new SqlConnection(Connection.GetConnectionString());
            cmd = new SqlCommand("User_Crud", con);

            // Handle registration based on the user type (Customer, Seller, Organization)
            if (userType == "Customer")
            {
                cmd.Parameters.AddWithValue("@Action", "INSERT_CUSTOMER");
                cmd.Parameters.AddWithValue("@Name", txtName.Text.Trim());
                cmd.Parameters.AddWithValue("@UserName", txtUserName.Text.Trim());
                cmd.Parameters.AddWithValue("@Mobile", txtMobile.Text.Trim());
                cmd.Parameters.AddWithValue("@Email", txtEmail.Text.Trim());
                cmd.Parameters.AddWithValue("@Address", txtAddress.Text.Trim());
                cmd.Parameters.AddWithValue("@Password", txtPassword.Text.Trim());

            }
            else if (userType == "Seller")
            {
                cmd.Parameters.AddWithValue("@Action", "INSERT_SELLER");
                cmd.Parameters.AddWithValue("@ShopName1", txtShopName1.Text.Trim());
                cmd.Parameters.AddWithValue("@Location1", txtLocation1.Text.Trim());
                cmd.Parameters.AddWithValue("@Description1", txtDescription1.Text.Trim());
                cmd.Parameters.AddWithValue("@Address1", txtAddress1.Text.Trim());
                cmd.Parameters.AddWithValue("@Number1", txtNumber1.Text.Trim());
                cmd.Parameters.AddWithValue("@Password1", txtPassword1.Text.Trim());
                cmd.Parameters.AddWithValue("@UserName1", txtUserName1.Text.Trim());
            }
            else if (userType == "Organization")
            {
                cmd.Parameters.AddWithValue("@Action", "INSERT_ORGANIZATION");
                cmd.Parameters.AddWithValue("@OrganizationName2", txtOrganizationName2.Text.Trim());
                cmd.Parameters.AddWithValue("@Location2", txtLocation2.Text.Trim());
                cmd.Parameters.AddWithValue("@Description2", txtDescription2.Text.Trim());
                cmd.Parameters.AddWithValue("@Address2", txtAddress2.Text.Trim());
                cmd.Parameters.AddWithValue("@Number2", txtNumber2.Text.Trim());
                cmd.Parameters.AddWithValue("@Email2", txtEmail2.Text.Trim());
                cmd.Parameters.AddWithValue("@Password2", txtPassword2.Text.Trim());
                cmd.Parameters.AddWithValue("@UserName2", txtUserName2.Text.Trim());
            }

            // Handle image upload
            if (fuUserImage.HasFile)
            {
                if (Utils.IsValidExtension(fuUserImage.FileName))
                {
                    Guid obj = Guid.NewGuid();
                    fileExtension = Path.GetExtension(fuUserImage.FileName);
                    imagePath = "Images/User/" + obj.ToString() + fileExtension;
                    fuUserImage.PostedFile.SaveAs(Server.MapPath("../Images/User/") + obj.ToString() + fileExtension);
                    cmd.Parameters.AddWithValue("@ImageUrl", imagePath);
                    isValidToExecute = true;
                }
                else
                {
                    lblMsg.Visible = true;
                    lblMsg.Text = "Please select a valid image file (jpg, jpeg, png)";
                    lblMsg.CssClass = "alert alert-danger";
                }
            }
            else
            {
                isValidToExecute = true; // Image is optional
            }

            if (isValidToExecute)
            {
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    lblMsg.Visible = true;
                    lblMsg.Text = "Registration successful!";
                    lblMsg.CssClass = "alert alert-success";
                    clear();
                }
                catch (SqlException ex)
                {
                    lblMsg.Visible = true;
                    lblMsg.Text = "Error: " + ex.Message;
                    lblMsg.CssClass = "alert alert-danger";
                }
                finally
                {
                    con.Close();
                }
            }
        }

        private void clear()
        {
            // Clear form fields
            txtName.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtMobile.Text = string.Empty;
            txtAddress.Text = string.Empty;
            txtShopName1.Text = string.Empty;
            txtAddress1.Text = string.Empty;
    
            imgUser.ImageUrl = string.Empty;
        }
    }
}
