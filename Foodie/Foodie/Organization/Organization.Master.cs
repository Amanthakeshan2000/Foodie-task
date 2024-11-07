using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace Foodie.Organization
{
    public partial class Organization : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["Organization"] != null)
                {
                    lbLoginOrLogout.Text = "Logout";
                    Utils utils = new Utils();
                    Session["cartCount"] = utils.cartCount(Convert.ToInt32(Session["userId"])).ToString();
                }
                else
                {
                    lbLoginOrLogout.Text = "Login";
                    Session["cartCount"] = "0";
                }
            }

            // Postback to load donation history
            if (Request["__EVENTTARGET"] == "ShowDonationHistory")
            {
                LoadDonationHistory();
            }
        }

        // Method to load donation history from the stored procedure
        private void LoadDonationHistory()
        {
            string connString = System.Configuration.ConfigurationManager.ConnectionStrings["cs"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_GetDonationHistory", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Open connection and fill GridView
                    conn.Open();
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);

                    gvDonationHistory.DataSource = dt;
                    gvDonationHistory.DataBind();
                }
            }
        }

        // Handle Accept/Reject button clicks
        protected void gvDonationHistory_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Accept" || e.CommandName == "Reject")
            {
                int donationId = Convert.ToInt32(e.CommandArgument);
                string status = e.CommandName == "Accept" ? "2" : "99";
                UpdateDonationStatus(donationId, status);

                // Reload data after status change
                LoadDonationHistory();
            }
        }

        // Method to update donation request status using the stored procedure
        private void UpdateDonationStatus(int id, string status)
        {
            string connString = System.Configuration.ConfigurationManager.ConnectionStrings["cs"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connString))
            {
                using (SqlCommand cmd = new SqlCommand("SP_UPDATE_DonationRequestStatus", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID", id);
                    cmd.Parameters.AddWithValue("@Status", status);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        protected void lbLoginOrLogout_Click(object sender, EventArgs e)
        {
            if (Session["Name"] == null)
            {
                Response.Redirect("../User/Login.aspx");
            }
            else
            {
                Session.Abandon();
                Response.Redirect("Login.aspx");
            }
        }

        // Handle GridView paging
        protected void gvDonationHistory_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvDonationHistory.PageIndex = e.NewPageIndex;
            LoadDonationHistory();
        }
    }
}
