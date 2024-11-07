using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace Foodie.Admin
{
    public partial class Donation : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadDistricts();
                LoadCategories(); // Load categories based on AdminID
                InitializeOrganizationDropdown();
                InitializeProductDropdown();
            }
        }

        /// <summary>
        /// Event handler for when the selected district changes.
        /// </summary>
        protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedDistrict = ddlDistrict.SelectedValue;

            if (selectedDistrict != "0")
            {
                LoadOrganizationsByDistrict(selectedDistrict);
            }
            else
            {
                ResetOrganizationDropdown();
            }
        }

        /// <summary>
        /// Event handler for when the selected category changes.
        /// </summary>
        protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedCategory = ddlCategory.SelectedItem.Text;

            if (selectedCategory != "-- Select Category --")
            {
                LoadProductsByCategory(selectedCategory);
            }
            else
            {
                ResetProductDropdown();
            }
        }

        /// <summary>
        /// Handles the submission of the donation form.
        /// </summary>
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                string district = ddlDistrict.SelectedItem.Text;
                string organization = ddlOrganization.SelectedItem.Text;
                string description = txtDescription.Text.Trim();
                string category = ddlCategory.SelectedItem.Text;
                string product = ddlProduct.SelectedItem.Text;
                decimal donationAmount;

                if (!decimal.TryParse(txtDonationAmount.Text.Trim(), out donationAmount))
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "InvalidAmount", "alert('Please enter a valid donation amount.');", true);
                    return;
                }

                InsertDonationData(district, organization, description, category, product, donationAmount);
            }
        }

        /// <summary>
        /// Loads districts into the ddlDistrict dropdown using sp_get_Districts stored procedure.
        /// </summary>
        private void LoadDistricts()
        {
            string connString = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_get_Districts", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    try
                    {
                        conn.Open();
                        SqlDataReader reader = cmd.ExecuteReader();

                        if (reader.HasRows)
                        {
                            ddlDistrict.DataSource = reader;
                            ddlDistrict.DataTextField = "Location";
                            ddlDistrict.DataValueField = "Location"; // Use LocationID if available
                            ddlDistrict.DataBind();
                        }
                        reader.Close();
                    }
                    catch (Exception ex)
                    {
                        // Log the exception (implementation depends on your logging strategy)
                        ClientScript.RegisterStartupScript(this.GetType(), "Error", $"alert('An error occurred while loading districts: {ex.Message}');", true);
                    }
                }
            }
        }

        /// <summary>
        /// Initializes the Organization dropdown with the default placeholder.
        /// </summary>
        private void InitializeOrganizationDropdown()
        {
            ddlOrganization.Items.Clear();
            ddlOrganization.Items.Add(new ListItem("-- Select Organization --", "0"));
        }

        /// <summary>
        /// Initializes the Product dropdown with the default placeholder.
        /// </summary>
        private void InitializeProductDropdown()
        {
            ddlProduct.Items.Clear();
            ddlProduct.Items.Add(new ListItem("-- Select Product --", "0"));
        }

        /// <summary>
        /// Loads organizations into the ddlOrganization dropdown based on selected district.
        /// </summary>
        /// <param name="district">Selected district name.</param>
        private void LoadOrganizationsByDistrict(string district)
        {
            string connString = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_get_OrganizationsByDistrict", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@District", district);

                    try
                    {
                        conn.Open();
                        SqlDataReader reader = cmd.ExecuteReader();

                        ddlOrganization.Items.Clear();
                        ddlOrganization.Items.Add(new ListItem("-- Select Organization --", "0"));

                        if (reader.HasRows)
                        {
                            ddlOrganization.DataSource = reader;
                            ddlOrganization.DataTextField = "Name";
                            ddlOrganization.DataValueField = "Name"; // Use OrganizationID if available
                            ddlOrganization.DataBind();
                        }
                        reader.Close();
                    }
                    catch (Exception ex)
                    {
                        // Log the exception (implementation depends on your logging strategy)
                        ClientScript.RegisterStartupScript(this.GetType(), "Error", $"alert('An error occurred while loading organizations: {ex.Message}');", true);
                    }
                }
            }
        }

        /// <summary>
        /// Resets the Organization dropdown to its default state.
        /// </summary>
        private void ResetOrganizationDropdown()
        {
            ddlOrganization.Items.Clear();
            ddlOrganization.Items.Add(new ListItem("-- Select Organization --", "0"));
        }

        /// <summary>
        /// Loads categories into the ddlCategory dropdown using SP_GET_CategoryByUserID stored procedure.
        /// </summary>
        private void LoadCategories()
        {
            string connString = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;

            // Retrieve AdminID from session. Adjust the session key if different.
            string adminID = Session["Name"] as string;

            if (string.IsNullOrEmpty(adminID))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Error", $"alert('Admin ID not found. Please log in again.');", true);
                return;
            }

            using (SqlConnection conn = new SqlConnection(connString))
            {
                using (SqlCommand cmd = new SqlCommand("SP_GET_CategoryByUserID", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@AdminID", adminID);

                    try
                    {
                        conn.Open();
                        SqlDataReader reader = cmd.ExecuteReader();

                        ddlCategory.Items.Clear();
                        ddlCategory.Items.Add(new ListItem("-- Select Category --", "0"));

                        if (reader.HasRows)
                        {
                            ddlCategory.DataSource = reader;
                            ddlCategory.DataTextField = "Name";
                            ddlCategory.DataValueField = "Name"; // Assuming CategoryID is returned
                            ddlCategory.DataBind();
                        }
                        reader.Close();
                    }
                    catch (Exception ex)
                    {
                        // Log the exception (implementation depends on your logging strategy)
                        ClientScript.RegisterStartupScript(this.GetType(), "Error", $"alert('An error occurred while loading categories: {ex.Message}');", true);
                    }
                }
            }
        }

        /// <summary>
        /// Loads products into the ddlProduct dropdown based on selected category and AdminID.
        /// </summary>
        /// <param name="categoryName">Selected category name.</param>
        private void LoadProductsByCategory(string categoryName)
        {
            string connString = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;

            // Retrieve AdminID from session. Adjust the session key if different.
            string adminID = Session["Name"] as string;

            if (string.IsNullOrEmpty(adminID))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Error", $"alert('Admin ID not found. Please log in again.');", true);
                return;
            }

            using (SqlConnection conn = new SqlConnection(connString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_get_ProductsByCategory", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@AdminID", adminID);
                    cmd.Parameters.AddWithValue("@CategoryName", categoryName);

                    try
                    {
                        conn.Open();
                        SqlDataReader reader = cmd.ExecuteReader();

                        ddlProduct.Items.Clear();
                        ddlProduct.Items.Add(new ListItem("-- Select Product --", "0"));

                        if (reader.HasRows)
                        {
                            ddlProduct.DataSource = reader;
                            ddlProduct.DataTextField = "Name";
                            ddlProduct.DataValueField = "Name"; // Use ProductID if available
                            ddlProduct.DataBind();
                        }
                        reader.Close();
                    }
                    catch (Exception ex)
                    {
                        // Log the exception (implementation depends on your logging strategy)
                        ClientScript.RegisterStartupScript(this.GetType(), "Error", $"alert('An error occurred while loading products: {ex.Message}');", true);
                    }
                }
            }
        }

        /// <summary>
        /// Resets the Product dropdown to its default state.
        /// </summary>
        private void ResetProductDropdown()
        {
            ddlProduct.Items.Clear();
            ddlProduct.Items.Add(new ListItem("-- Select Product --", "0"));
        }

        /// <summary>
        /// Inserts the donation data into the database using the Save_DonationHistory stored procedure.
        /// </summary>
        /// <param name="district">Selected district.</param>
        /// <param name="organization">Selected organization.</param>
        /// <param name="description">Donation description.</param>
        /// <param name="category">Selected category.</param>
        /// <param name="product">Selected product.</param>
        /// <param name="donationAmount">Donation amount.</param>
        private void InsertDonationData(string district, string organization, string description, string category, string product, decimal donationAmount)
        {
            string connString = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connString))
            {
                using (SqlCommand cmd = new SqlCommand("Save_DonationHistory", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Set the parameters
                    cmd.Parameters.AddWithValue("@Name", organization);
                    cmd.Parameters.AddWithValue("@Location", district);
                    cmd.Parameters.AddWithValue("@Description", description);
                    cmd.Parameters.AddWithValue("@CreateBy", Session["Name"] ?? "Anonymous");
                    cmd.Parameters.AddWithValue("@Status", 1);
                    cmd.Parameters.AddWithValue("@Category", category);
                    cmd.Parameters.AddWithValue("@Product", product);
                    cmd.Parameters.AddWithValue("@Qty", donationAmount);

                    try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        // Log the exception (implementation depends on your logging strategy)
                        ClientScript.RegisterStartupScript(this.GetType(), "Error", $"alert('An error occurred while submitting your donation: {ex.Message}');", true);
                        return;
                    }
                }
            }

            ClearForm();

            ClientScript.RegisterStartupScript(this.GetType(), "Success", "alert('Donation successfully submitted.');", true);
        }

        /// <summary>
        /// Clears all form fields and resets dropdown selections to default.
        /// </summary>
        private void ClearForm()
        {
            txtDescription.Text = string.Empty;
            txtDonationAmount.Text = string.Empty;

            ddlDistrict.SelectedIndex = 0;
            ddlOrganization.SelectedIndex = 0;
            ddlCategory.SelectedIndex = 0;
            ddlProduct.SelectedIndex = 0;
        }
    }
}
