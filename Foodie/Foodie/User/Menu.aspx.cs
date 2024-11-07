using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Foodie.User
{
    public partial class Menu : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                getCategories();
                getProducts();
            }
        }

        // Method to get categories from the database
        private void getCategories()
        {
            SqlConnection con = new SqlConnection(Connection.GetConnectionString());
            SqlCommand cmd = new SqlCommand("Category_Crud", con);
            cmd.Parameters.AddWithValue("@Action", "ACTIVECAT");
            cmd.Parameters.AddWithValue("@AdminID", 2000);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            rCategory.DataSource = dt;
            rCategory.DataBind();
        }

        // Method to get products from the database
        private void getProducts()
        {
            SqlConnection con = new SqlConnection(Connection.GetConnectionString());
            SqlCommand cmd = new SqlCommand("Product_Crud", con);
            cmd.Parameters.AddWithValue("@Action", "ACTIVEPRO");
            cmd.Parameters.AddWithValue("@AdminID", 2000);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            rProducts.DataSource = dt;
            rProducts.DataBind();
        }

        // Handle item commands from the Repeater (e.g., "Add to Cart")
        protected void rProducts_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "addToCart")
            {
                if (Session["userId"] != null)
                {
                    int productId = Convert.ToInt32(e.CommandArgument);
                    addItemToCart(productId);
                }
                else
                {
                    // If user is not logged in, redirect to login page
                    Response.Redirect("Login.aspx");
                }
            }
        }

        // Web method to retrieve restaurant distances
        [WebMethod]
        public static List<Restaurant> GetDistances(double latitude, double longitude)
        {
            var restaurants = GetRestaurantLocations(latitude, longitude);
            foreach (var restaurant in restaurants)
            {
                restaurant.Distance = CalculateDistance(latitude, longitude, restaurant.Latitude, restaurant.Longitude);
            }

            return restaurants; // Return list of Restaurant objects with names, coordinates, and distances
        }

        // Calculate distance between two latitude/longitude points in kilometers
        private static double CalculateDistance(double lat1, double lon1, double lat2, double lon2)
        {
            const double R = 6371; // Radius of the Earth in km
            var lat = ToRadians(lat2 - lat1);
            var lon = ToRadians(lon2 - lon1);
            var a = Math.Sin(lat / 2) * Math.Sin(lat / 2) +
                    Math.Cos(ToRadians(lat1)) * Math.Cos(ToRadians(lat2)) *
                    Math.Sin(lon / 2) * Math.Sin(lon / 2);
            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            return R * c;
        }

        // Convert degrees to radians
        private static double ToRadians(double angle)
        {
            return angle * Math.PI / 180.0;
        }

        // Dynamically generate nearby restaurant locations based on the user’s current location
        private static List<Restaurant> GetRestaurantLocations(double userLat, double userLng)
        {
            List<Restaurant> nearbyRestaurants = new List<Restaurant>();

            // Define the connection string and command for database access
            using (SqlConnection con = new SqlConnection(Connection.GetConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand("Restaurant_Locations", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;


                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);


                    foreach (DataRow row in dt.Rows)
                    {
                        nearbyRestaurants.Add(new Restaurant
                        {
                            Name = row["AdminID"].ToString(),
                            Latitude = Convert.ToDouble(row["Latitude"]),
                            Longitude = Convert.ToDouble(row["Longitude"]),
                            
                        });
                    }
                }
            }

            return nearbyRestaurants;
        }

        // Class to represent Restaurant data
        public class Restaurant
        {
            public string Name { get; set; }
            public double Latitude { get; set; }
            public double Longitude { get; set; }
            public double Distance { get; set; } // Distance in kilometers from the user location
        }

        // Method to add the product to the cart
        private void addItemToCart(int productId)
        {
            SqlConnection con = new SqlConnection(Connection.GetConnectionString());
            SqlCommand cmd = new SqlCommand("Cart_Crud", con);
            cmd.Parameters.AddWithValue("@Action", "INSERT");
            cmd.Parameters.AddWithValue("@ProductId", productId);
            cmd.Parameters.AddWithValue("@Quantity", 1);
            cmd.Parameters.AddWithValue("@UserId", Session["userId"]);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                lblMsg.Visible = true;
                lblMsg.Text = "Item added to cart!";
                lblMsg.CssClass = "alert alert-success";
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

            // Optionally, refresh or redirect the user to the cart page
            Response.AddHeader("REFRESH", "1;URL=Cart.aspx");
        }
    }
}
