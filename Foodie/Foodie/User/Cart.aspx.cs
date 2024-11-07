using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using Foodie.Admin;

namespace Foodie.User
{
    public partial class Cart : System.Web.UI.Page
    {

        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter sda;
        DataTable dt;
        decimal grandTotal = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["userid"] == null)
            {
                Response.Redirect("Login.aspx");


            }
            else
            {
                getCartItems();
            }
        }

        void getCartItems()
        {
            con = new SqlConnection(Connection.GetConnectionString());
            cmd = new SqlCommand("Cart_Crud", con);
            cmd.Parameters.AddWithValue("@Action", "SELECT");
            cmd.Parameters.AddWithValue("@UserId", Session["userid"]);
            cmd.CommandType = CommandType.StoredProcedure;
            sda = new SqlDataAdapter(cmd);
            dt = new DataTable();
            sda.Fill(dt);
            rCartItem.DataSource = dt;
            if (dt.Rows.Count == 0)
            {
                rCartItem.FooterTemplate = null;
                rCartItem.FooterTemplate = new CustomTemplate(ListItemType.Footer);
            }
            rCartItem.DataBind();


        }
        protected void rCartItem_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            Utils utils = new Utils();
            if (e.CommandName == "remove")
            {
                con = new SqlConnection(Connection.GetConnectionString());
                cmd = new SqlCommand("Cart_Crud", con);
                cmd.Parameters.AddWithValue("@Action", "DELETE");
                cmd.Parameters.AddWithValue("@ProductId", e.CommandArgument);
                cmd.Parameters.AddWithValue("@UserId", Session["userId"]);
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    getCartItems();
                    Session["cartCount"] = utils.cartCount(Convert.ToInt32(Session["userId"]));
                }
                catch (Exception ex)
                {
                    Response.Write("<script>alert('Error - " + ex.Message + " '); <script>");
                }
                finally
                {
                    con.Close();
                }




            }



            //if (e.CommandName == "updateCart")
            //{
            //    bool isCartUpdated = false;
            //    for (int item = 0; item < rCartItem.Items.Count; item++)
            //    {
            //        if (rCartItem.Items[item].ItemType == ListItemType.Item || rCartItem.Items[item].ItemType == ListItemType.AlternatingItem)
            //        {
            //            TextBox quantity = rCartItem.Items[item].FindControl("txtQuantity") as TextBox;
            //            HiddenField _productId = rCartItem.Items[item].FindControl("hdnProductId") as HiddenField;
            //            HiddenField _quantity = rCartItem.Items[item].FindControl("hdnQuantity") as HiddenField;
            //            int quantityFromCart = Convert.ToInt32(quantity.Text);
            //            int ProductId = Convert.ToInt32(_productId.Value);
            //            int quantityFromDB = Convert.ToInt32(_quantity.Value);
            //            bool isTrue = false;
            //            int updatedQuantity = 1;
            //            if (quantityFromCart > quantityFromDB)
            //            {
            //                updatedQuantity = quantityFromCart;
            //                isTrue = true;
            //            }
            //            else if (quantityFromCart < quantityFromDB)
            //            {
            //                updatedQuantity = quantityFromCart;
            //                isTrue = true;

            //            }
            //            if (isTrue)
            //            {
            //                // Update cart item's quantity in DB.
            //                isCartUpdated = utils.updateCartQunatity(updatedQuantity, ProductId, Convert.ToInt32(Session["userId"]));
            //            }
            //        }
            //    }

            //    getCartItems();
            //}
            if (e.CommandName == "updateCart")
            {
                bool isCartUpdated = false;
                decimal newGrandTotal = 0; // Recalculate grand total

                for (int item = 0; item < rCartItem.Items.Count; item++)
                {
                    RepeaterItem repeaterItem = rCartItem.Items[item];
                    if (repeaterItem.ItemType == ListItemType.Item || repeaterItem.ItemType == ListItemType.AlternatingItem)
                    {
                        TextBox quantity = repeaterItem.FindControl("txtQuantity") as TextBox;
                        HiddenField _productId = repeaterItem.FindControl("hdnProductId") as HiddenField;
                        HiddenField _quantity = repeaterItem.FindControl("hdnQuantity") as HiddenField;
                        Label lblTotalPrice = repeaterItem.FindControl("lblTotalPrice") as Label;
                        Label lblPrice = repeaterItem.FindControl("lblPrice") as Label;

                        int quantityFromCart = Convert.ToInt32(quantity.Text);
                        int productId = Convert.ToInt32(_productId.Value);
                        int quantityFromDB = Convert.ToInt32(_quantity.Value);
                        bool isTrue = false;

                        if (quantityFromCart != quantityFromDB)
                        {
                            isTrue = true;
                        }

                        if (isTrue)
                        {
                            // Update cart item's quantity in DB.
                            isCartUpdated = utils.updateCartQunatity(quantityFromCart, productId, Convert.ToInt32(Session["userId"]));
                        }

                        // Recalculate total price for this item
                        decimal productPrice = Convert.ToDecimal(lblPrice.Text);
                        decimal totalPrice = productPrice * quantityFromCart;
                        lblTotalPrice.Text = "Rs. " + totalPrice.ToString("0.00");

                        // Update grand total
                        newGrandTotal += totalPrice;
                    }
                }

                // Update the grand total
                Session["grandTotalPrice"] = newGrandTotal;

                // Assuming you have a label for grand total in the footer or somewhere else
                // You need to ensure lblGrandTotal is accessible or use the appropriate control reference
                // lblGrandTotal.Text = "Rs. " + newGrandTotal.ToString("0.00");

                if (isCartUpdated)
                {
                    getCartItems(); // Rebind data to reflect changes
                }
            }


            if (e.CommandName == "checkout")
            {
                bool isTrue = false;
                string pName = string.Empty;
                // First will check item quantity
                for (int item = 0; item < rCartItem.Items.Count; item++)
                {
                    if (rCartItem.Items[item].ItemType == ListItemType.Item || rCartItem.Items[item].ItemType == ListItemType.AlternatingItem)
                    {
                        HiddenField _productId = rCartItem.Items[item].FindControl("hdnProductId") as HiddenField;
                        HiddenField _cartQuantity = rCartItem.Items[item].FindControl("hdnQuantity") as HiddenField;
                        HiddenField _productQuantity = rCartItem.Items[item].FindControl("hdnPrdQuantity") as HiddenField;
                        Label productName = rCartItem.Items[item].FindControl("lblName") as Label;
                        int productId = Convert.ToInt32(_productId.Value);
                        int cartQuantity = Convert.ToInt32(_cartQuantity.Value);
                        int productQuantity = Convert.ToInt32(_productQuantity.Value);

                        if (productQuantity >= cartQuantity && productQuantity > 1)
                        {
                            isTrue = true;
                        }
                        else
                        {
                            isTrue = false;
                            pName = productName.Text.ToString();
                            break;
                        }


                        
                    }
                }
                if (isTrue)
                {
                    Response.Redirect("Payment.aspx");
                }
                else
                {
                    lblMsg.Visible = true;
                    lblMsg.Text = "Item </b>'" + pName + "'</b> Out of stock!";
                    lblMsg.CssClass = "alert alert-warning";
                }
            }
        }

        protected void rCartItem_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            //if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem) 
            //{
            //    Label totalPrice = e.Item.FindControl("lblTotalPrice") as Label;
            //    Label productPrice = e.Item.FindControl("lblPrice") as Label;
            //    TextBox quantity = e.Item.FindControl("txtQuantity") as TextBox;
            //    decimal calTotalprice = Convert.ToDecimal(productPrice.Text) * Convert.ToDecimal(quantity.Text);
            //    totalPrice.Text = calTotalprice.ToString();
            //    grandTotal += calTotalprice;

            //}
            //Session["grandTotalPrice"] = grandTotal;





            // Check if the current item is a regular item or alternating item
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Label totalPrice = e.Item.FindControl("lblTotalPrice") as Label;
                Label productPrice = e.Item.FindControl("lblPrice") as Label;
                TextBox quantity = e.Item.FindControl("txtQuantity") as TextBox;

                if (productPrice != null && quantity != null && totalPrice != null)
                {
                    decimal calTotalPrice;
                    if (Decimal.TryParse(productPrice.Text, out decimal price) && Decimal.TryParse(quantity.Text, out decimal qty))
                    {
                        calTotalPrice = price * qty;
                        totalPrice.Text = "Rs. " + calTotalPrice.ToString("0.00");
                        grandTotal += calTotalPrice;
                    }
                    else
                    {
                        // Handle parsing error
                        totalPrice.Text = "Rs. 0.00";
                    }
                }
                else
                {
                    // Handle null control case
                    // You might want to log this error or handle it accordingly
                    System.Diagnostics.Debug.WriteLine("Control is null in ItemDataBound.");
                }
            }

            // Check if the current item is the footer
            if (e.Item.ItemType == ListItemType.Footer)
            {
                Label lblGrandTotal = e.Item.FindControl("lblGrandTotal") as Label;
                if (lblGrandTotal != null)
                {
                    lblGrandTotal.Text = "Rs. " + grandTotal.ToString("0.00");
                }
                else
                {
                    // Handle null control case
                    // You might want to log this error or handle it accordingly
                    System.Diagnostics.Debug.WriteLine("Footer control 'lblGrandTotal' is null.");
                }
            }

            // Update session grand total
            Session["grandTotalPrice"] = grandTotal;
        }

        private sealed class CustomTemplate : ITemplate
        {
            private ListItemType ListItemType { get; set; }
            
            public CustomTemplate(ListItemType type)
            {
                ListItemType = type;
            }

            public void InstantiateIn(Control container) 
            {
                if (ListItemType == ListItemType.Footer)
                {
                    var footer = new LiteralControl("<tr><td colspan='5'><b>Your Cart is empty.</b><a href='Menu.aspx' class='btn btn-info'><i class='fa fa-arrow-circle-left mr-2'></i>Continue Shopping</a></td></tr></tbody></table>");
                    container.Controls.Add(footer);
                }
            }
        }
    }
}