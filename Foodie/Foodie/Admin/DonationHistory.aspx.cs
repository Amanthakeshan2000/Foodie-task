using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace Foodie.Admin
{
    public partial class DonationHistory : System.Web.UI.Page
    {
        // Connection string from Web.config
        private string connectionString = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;

        // Variables to handle sorting
        private string SortExpression
        {
            get { return ViewState["SortExpression"] as string ?? "CreateDate"; }
            set { ViewState["SortExpression"] = value; }
        }

        private string SortDirection
        {
            get { return ViewState["SortDirection"] as string ?? "ASC"; }
            set { ViewState["SortDirection"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadDonationHistory();
            }
        }

        /// <summary>
        /// Loads donation history data into the GridView.
        /// </summary>
        private void LoadDonationHistory()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_GetDonationHistory", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Optional: Pass parameters if needed
                    // cmd.Parameters.AddWithValue("@Status", 1);

                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        try
                        {
                            sda.Fill(dt);

                            // Apply sorting
                            if (!string.IsNullOrEmpty(SortExpression))
                            {
                                dt.DefaultView.Sort = $"{SortExpression} {SortDirection}";
                            }

                            gvDonationHistory.DataSource = dt;
                            gvDonationHistory.DataBind();
                        }
                        catch (Exception ex)
                        {
                            // Log the exception (implement logging as needed)
                            ClientScript.RegisterStartupScript(this.GetType(), "Error", $"alert('An error occurred while loading donation history: {ex.Message}');", true);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Handles paging for the GridView.
        /// </summary>
        protected void gvDonationHistory_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvDonationHistory.PageIndex = e.NewPageIndex;
            LoadDonationHistory();
        }

        /// <summary>
        /// Handles sorting for the GridView.
        /// </summary>
        protected void gvDonationHistory_Sorting(object sender, GridViewSortEventArgs e)
        {
            if (SortExpression == e.SortExpression)
            {
                // Toggle sort direction
                SortDirection = SortDirection == "ASC" ? "DESC" : "ASC";
            }
            else
            {
                SortExpression = e.SortExpression;
                SortDirection = "ASC";
            }

            LoadDonationHistory();
        }
    }
}
