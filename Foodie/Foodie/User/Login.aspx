<%@ Page Title="" Language="C#" MasterPageFile="~/User/User.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Foodie.User.Login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        window.onload = function () {
            var seconds = 5;
            setTimeout(function () {
                document.getElementById("<%=lblMsg.ClientID %>").style.display = "none";
            }, seconds * 1000)
        };
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   <section class="book_section layout_padding" style="background: #f7f7f7; padding: 50px 0;">
        <div class="container" style="max-width: 800px; background: white; border-radius: 15px; box-shadow: 0 4px 10px rgba(0, 0, 0, 0.1); padding: 30px;">
            <div class="heading_container" style="text-align: center;">
                <asp:Label runat="server" ID="lblMsg" Visible="false" style="color: red;"></asp:Label>
                <h2 style="font-family: 'Arial', sans-serif; color: #333; margin-bottom: 30px;">Login</h2>
            </div>
            <div class="row">
                <!-- Image Section -->
                <div class="col-md-6" style="padding-right: 15px;">
                    <div class="form_container" style="text-align: center;">
                        <asp:Image ID="userLogin" runat="server" CssClass="img-thumbnail" src="../Images/Login.jpg" alt="Login Image" 
                                   style="width: 100%; border-radius: 10px;"/>
                    </div>
                </div>

                <!-- Form Section -->
                <div class="col-md-6" style="padding-left: 15px;">
                <!-- Dropdown List for User Type -->
<div class="form_container" style="margin-bottom: 15px;">
    <asp:DropDownList ID="ddlUserType" runat="server" CssClass="form-control" 
                      style="width: 100%; background-color: #f0f0f0; border: 2px solid #ddd; 
                             padding: 10px; border-radius: 5px; color: #333; 
                             font-size: 16px; transition: all 0.3s ease; 
                             box-shadow: 0 2px 5px rgba(0,0,0,0.1);">
        <asp:ListItem Text="Select User Type" Value=""></asp:ListItem>
        <asp:ListItem Text="Customer" Value="User"></asp:ListItem>
        <asp:ListItem Text="Seller" Value="Seller"></asp:ListItem>
        <asp:ListItem Text="Organization" Value="Organization"></asp:ListItem>
        <asp:ListItem Text="Admin" Value="Admin"></asp:ListItem>

    </asp:DropDownList>
</div>

<br />

<!-- Username Field -->
<div class="form_container" style="margin-bottom: 15px;">
    <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control" placeholder="Enter Username"
                 style="width: 100%; background-color: #f0f0f0; border: 2px solid #ddd; 
                        padding: 10px; border-radius: 5px; color: #333; 
                        font-size: 16px; transition: all 0.3s ease; 
                        box-shadow: 0 2px 5px rgba(0,0,0,0.1);">
    </asp:TextBox>
    <asp:RequiredFieldValidator ID="rfvUsername" runat="server" ErrorMessage="Username is required" 
                                ControlToValidate="txtUsername" ForeColor="Red" Display="Dynamic" 
                                SetFocusOnError="true" Font-Size="Small"></asp:RequiredFieldValidator>
</div>

<style>
    /* Add some hover effects */
    #ddlUserType:hover, #txtUsername:hover {
        border-color: #007bff; /* Change border color on hover */
        box-shadow: 0 4px 8px rgba(0, 123, 255, 0.2); /* Enhance shadow */
    }
</style>


                    <!-- Password Field -->
                    <div class="form_container" style="margin-bottom: 20px;">
                        <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" placeholder="Enter Password" TextMode="Password"
                                     style="width:100%; background-color: #f0f0f0; border: 2px solid #ddd; padding: 10px; border-radius: 5px; color: #333;"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvPassword" runat="server" ErrorMessage="Password is required" ControlToValidate="txtPassword"
                            ForeColor="Red" Display="Dynamic" SetFocusOnError="true" Font-Size="Small"></asp:RequiredFieldValidator>
                    </div>

                    <!-- Login Button -->
                    <div class="btn_box" style="text-align: center;">
                        <asp:Button ID="btnLogin" runat="server" Text="Login" CssClass="btn btn-success" 
            OnClick="btnLogin_Click" 
            style="background-color: #28a745; border: none; padding: 10px 30px; 
                   font-size: 16px; color: white; border-radius: 50px; 
                   cursor: pointer; transition: background-color 0.3s ease;" 
            onmouseover="this.style.backgroundColor='#218838';" 
            onmouseout="this.style.backgroundColor='#28a745';" />

                        <br/>
                        <span class="text-info" style="display: block; margin-top: 15px;">
                            New User? <a href="Registration.aspx" class="badge badge-info" style="text-decoration: none; color: #17a2b8;">Register Here...</a>
                        </span>
                    </div>
                </div>
            </div>
        </div>
   </section>
</asp:Content>
