using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using Newtonsoft.Json.Linq;


namespace Foodie.Admin
{
    public partial class Product : System.Web.UI.Page
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter sda;
        DataTable dt;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["breadCrum"] = "Product";
                if (Session["Name"] == null)
                {
                    Response.Redirect("../User/Login.aspx");
                }
                else
                {
                    getProducts();
                }
            }

            lblMsg.Visible = false;
        }

        //protected void btnAddOrUpdate_Click(object sender, EventArgs e)
        //{
        //    string actionName = string.Empty, imagePath = string.Empty, fileExtension = string.Empty;
        //    bool isValidToExecute = false;
        //    int productId = Convert.ToInt32(hdnId.Value);

        //    // Get user's latitude and longitude from hidden fields
        //    string latitude = hdnLatitude.Value;
        //    string longitude = hdnLongitude.Value;

        //    // Ensure the connection string is set correctly.
        //    con = new SqlConnection(Connection.GetConnectionString());
        //    cmd = new SqlCommand("Product_Crud", con);
        //    cmd.Parameters.AddWithValue("@Action", productId == 0 ? "INSERT" : "UPDATE");
        //    cmd.Parameters.AddWithValue("@ProductId", productId);
        //    cmd.Parameters.AddWithValue("@Name", txtName.Text.Trim());
        //    cmd.Parameters.AddWithValue("@Description", txtDescription.Text.Trim());
        //    cmd.Parameters.AddWithValue("@Price", txtPrice.Text.Trim());
        //    cmd.Parameters.AddWithValue("@Quantity", txtQuantity.Text.Trim());
        //    cmd.Parameters.AddWithValue("@CategoryId", ddlCategories.SelectedValue);
        //    cmd.Parameters.AddWithValue("@IsActive", cbIsActive.Checked);
        //    cmd.Parameters.AddWithValue("@AdminID", Session["Name"]);
        //    cmd.Parameters.AddWithValue("@Location", (Nearest address here));
        //    // Add latitude and longitude to the SQL command
        //    cmd.Parameters.AddWithValue("@Latitude", latitude);
        //    cmd.Parameters.AddWithValue("@Longitude", longitude);

        //    if (fuProductImage.HasFile)
        //    {
        //        if (Utils.IsValidExtension(fuProductImage.FileName))
        //        {
        //            Guid obj = Guid.NewGuid();
        //            fileExtension = Path.GetExtension(fuProductImage.FileName);
        //            imagePath = "Images/Product/" + obj.ToString() + fileExtension;
        //            fuProductImage.PostedFile.SaveAs(Server.MapPath("../Images/Product/") + obj.ToString() + fileExtension);
        //            cmd.Parameters.AddWithValue("@ImageUrl", imagePath);
        //            isValidToExecute = true;
        //        }
        //        else
        //        {
        //            lblMsg.Visible = true;
        //            lblMsg.Text = "Please select .jpg, .jpeg, or .png image";
        //            lblMsg.CssClass = "alert alert-danger";
        //            isValidToExecute = false;
        //        }
        //    }
        //    else
        //    {
        //        isValidToExecute = true;
        //    }

        //    if (isValidToExecute)
        //    {
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        try
        //        {
        //            con.Open();
        //            cmd.ExecuteNonQuery();
        //            actionName = productId == 0 ? "inserted" : "updated";
        //            lblMsg.Visible = true;
        //            lblMsg.Text = "Product " + actionName + " Successfully!";
        //            lblMsg.CssClass = "alert alert-success";
        //            getProducts();
        //            clear();
        //        }
        //        catch (Exception ex)
        //        {
        //            lblMsg.Visible = true;
        //            lblMsg.Text = "Error: " + ex.Message;
        //            lblMsg.CssClass = "alert alert-danger";
        //        }
        //        finally
        //        {
        //            con.Close();
        //        }
        //    }
        //}


        protected void btnAddOrUpdate_Click(object sender, EventArgs e)
        {
            string actionName = string.Empty, imagePath = string.Empty, fileExtension = string.Empty;
            bool isValidToExecute = false;
            int productId = Convert.ToInt32(hdnId.Value);

            // Get user's latitude and longitude from hidden fields
            string latitude = hdnLatitude.Value;
            string longitude = hdnLongitude.Value;

            // Get nearest address using latitude and longitude
            string nearestAddress = GetAddressFromLatLng(latitude, longitude);

            if (string.IsNullOrEmpty(nearestAddress))
            {
                lblMsg.Visible = true;
                lblMsg.Text = "Unable to retrieve address from location. Please check your input.";
                lblMsg.CssClass = "alert alert-danger";
                return;
            }

            // Ensure the connection string is set correctly.
            con = new SqlConnection(Connection.GetConnectionString());
            cmd = new SqlCommand("Product_Crud", con);
            cmd.Parameters.AddWithValue("@Action", productId == 0 ? "INSERT" : "UPDATE");
            cmd.Parameters.AddWithValue("@ProductId", productId);
            cmd.Parameters.AddWithValue("@Name", txtName.Text.Trim());
            cmd.Parameters.AddWithValue("@Description", txtDescription.Text.Trim());
            cmd.Parameters.AddWithValue("@Price", txtPrice.Text.Trim());
            cmd.Parameters.AddWithValue("@Quantity", txtQuantity.Text.Trim());
            cmd.Parameters.AddWithValue("@CategoryId", ddlCategories.SelectedValue);
            cmd.Parameters.AddWithValue("@IsActive", cbIsActive.Checked);
            cmd.Parameters.AddWithValue("@AdminID", Session["Name"]);
            cmd.Parameters.AddWithValue("@Location", nearestAddress);  
            cmd.Parameters.AddWithValue("@Latitude", latitude);        
            cmd.Parameters.AddWithValue("@Longitude", longitude);      

            if (fuProductImage.HasFile)
            {
                if (Utils.IsValidExtension(fuProductImage.FileName))
                {
                    Guid obj = Guid.NewGuid();
                    fileExtension = Path.GetExtension(fuProductImage.FileName);
                    imagePath = "Images/Product/" + obj.ToString() + fileExtension;
                    fuProductImage.PostedFile.SaveAs(Server.MapPath("../Images/Product/") + obj.ToString() + fileExtension);
                    cmd.Parameters.AddWithValue("@ImageUrl", imagePath);
                    isValidToExecute = true;
                }
                else
                {
                    lblMsg.Visible = true;
                    lblMsg.Text = "Please select .jpg, .jpeg, or .png image";
                    lblMsg.CssClass = "alert alert-danger";
                    isValidToExecute = false;
                }
            }
            else
            {
                isValidToExecute = true;
            }

            if (isValidToExecute)
            {
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    actionName = productId == 0 ? "inserted" : "updated";
                    lblMsg.Visible = true;
                    lblMsg.Text = "Product " + actionName + " Successfully!";
                    lblMsg.CssClass = "alert alert-success";
                    getProducts();
                    clear();
                }
                catch (Exception ex)
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

        // Function to get the address from latitude and longitude using Google Geocoding API
        private string GetAddressFromLatLng(string lat, string lng)
        {
            string apiKey = "AIzaSyB_46UldwiRBVX_SFQt4TH6T-lC9MyrEL8";  // Your Google API Key
            string url = $"https://maps.googleapis.com/maps/api/geocode/json?latlng={lat},{lng}&key={apiKey}";

            try
            {
                using (WebClient wc = new WebClient())
                {
                    string json = wc.DownloadString(url);
                    JObject obj = JObject.Parse(json);
                    string status = (string)obj["status"];
                    if (status == "OK")
                    {
                        return (string)obj["results"][0]["formatted_address"];  // Extract the formatted address
                    }
                    else
                    {
                        return null;  // Handle cases where no address was found
                    }
                }
            }
            catch (Exception ex)
            {
                // Log or handle exception as needed
                return null;
            }
        }
        private void getProducts()
        {
            con = new SqlConnection(Connection.GetConnectionString());
            cmd = new SqlCommand("Product_Crud", con);
            cmd.Parameters.AddWithValue("@Action", "SELECT");
            cmd.Parameters.AddWithValue("@AdminID", Session["Name"]);
            cmd.CommandType = CommandType.StoredProcedure;
            sda = new SqlDataAdapter(cmd);
            dt = new DataTable();
            sda.Fill(dt);
            rProduct.DataSource = dt;
            rProduct.DataBind();
        }

        private void clear()
        {
            txtName.Text = string.Empty;
            txtDescription.Text = string.Empty;
            txtPrice.Text = string.Empty;
            txtQuantity.Text = string.Empty;
            cbIsActive.Checked = false;
            hdnId.Value = "0";
            btnAddOrUpdate.Text = "Add";
            imgProduct.ImageUrl = string.Empty;
            ddlCategories.ClearSelection();
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            clear();
        }

        protected void rProduct_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            lblMsg.Visible = false;
            if (e.CommandName == "edit")
            {
                con = new SqlConnection(Connection.GetConnectionString());
                cmd = new SqlCommand("Product_Crud", con);
                cmd.Parameters.AddWithValue("@Action", "GETBYID");
                cmd.Parameters.AddWithValue("@ProductId", e.CommandArgument);
                cmd.CommandType = CommandType.StoredProcedure;
                sda = new SqlDataAdapter(cmd);
                dt = new DataTable();
                sda.Fill(dt);
                txtName.Text = dt.Rows[0]["Name"].ToString();
                txtDescription.Text = dt.Rows[0]["Description"].ToString();
                txtPrice.Text = dt.Rows[0]["Price"].ToString();
                txtQuantity.Text = dt.Rows[0]["Quantity"].ToString();
                ddlCategories.SelectedValue = dt.Rows[0]["CategoryId"].ToString();
                cbIsActive.Checked = Convert.ToBoolean(dt.Rows[0]["IsActive"]);
                imgProduct.ImageUrl = string.IsNullOrEmpty(dt.Rows[0]["ImageUrl"].ToString()) ? "../Images/No_image.png" : "../" + dt.Rows[0]["ImageUrl"].ToString();
                imgProduct.Width = 200;
                imgProduct.Height = 200;
                hdnId.Value = dt.Rows[0]["ProductId"].ToString();
                btnAddOrUpdate.Text = "Update";
                LinkButton btn = e.Item.FindControl("lnkEdit") as LinkButton;
                btn.CssClass = "badge badge-warning";
            }
            else if (e.CommandName == "delete")
            {
                con = new SqlConnection(Connection.GetConnectionString());
                cmd = new SqlCommand("Product_Crud", con);
                cmd.Parameters.AddWithValue("@Action", "DELETE");
                cmd.Parameters.AddWithValue("@ProductId", e.CommandArgument);
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();

                    lblMsg.Visible = true;
                    lblMsg.Text = "Product deleted successfully!";
                    lblMsg.CssClass = "alert alert-success";
                    getProducts();
                }
                catch (Exception ex)
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

        protected void rProduct_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Label lblIsActive = e.Item.FindControl("lblIsActive") as Label;
                Label lblQuantity = e.Item.FindControl("lblQuantity") as Label;

                if (lblIsActive.Text == "True")
                {
                    lblIsActive.Text = "Active";
                    lblIsActive.CssClass = "badge badge-success";
                }
                else
                {
                    lblIsActive.Text = "In-Active";
                    lblIsActive.CssClass = "badge badge-danger";
                }

                if (Convert.ToInt32(lblQuantity.Text) <= 1)
                {
                    lblQuantity.CssClass = "badge badge-danger";
                    lblQuantity.ToolTip = "Item about to be 'Out of stock'!";
                }
            }
        }
    }
}
