using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Foodie.Admin
{
    public partial class Dashboard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["breadCrum"] = "";
                if (Session["Name"] == null)
                {
                    Response.Redirect("../User/Login.aspx");
                }
                else
                {
                    DashboardCount dashboard = new DashboardCount();
                    Session["category"] = dashboard.Count("CATEGORY","admin_A");
                    Session["product"] = dashboard.Count("PRODUCT", "admin_A");
                    Session["order"] = dashboard.Count("ORDER", "admin_A");
                    Session["delivered"] = dashboard.Count("DELIVERED", "admin_A");
                    Session["pending"] = dashboard.Count("PENDING", "admin_A");
                    Session["user"] = dashboard.Count("USER", "admin_A");
                    Session["soldAmount"] = dashboard.Count("SOLDAMOUNT", "admin_A");
                    Session["contact"] = dashboard.Count("CONTACT", "admin_A");
                }
            }
        }
    }
}