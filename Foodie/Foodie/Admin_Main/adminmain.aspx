<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="adminmain.aspx.cs" Inherits="Foodie.Admin_Main.adminmain" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Admin Dashboard - Foodie</title>
    <style>
        /* GridView Header Styling */
        .grid-header {
            background-color: #004a99;
            color: #ffffff;
            font-weight: bold;
            font-size: 18px;
            padding: 15px;
            text-align: left;
            border-bottom: 2px solid #003366;
        }

        /* GridView Row Styling */
        .grid-row {
            background-color: #e1efff;
            color: #333;
            font-size: 16px;
            padding: 10px;
            border-bottom: 1px solid #cce1ff;
        }

        /* GridView Alternating Row Styling */
        .grid-alt-row {
            background-color: #cce1ff;
            color: #333;
            font-size: 16px;
            padding: 10px;
            border-bottom: 1px solid #b3d1ff;
        }

        /* Button Styles */
        .btn-accept, .btn-reject, .btn-view-details {
            color: #fff;
            border: none;
            padding: 8px 16px;
            border-radius: 5px;
            font-weight: 600;
            margin: 5px;
        }

        .btn-accept {
            background-color: #2a9d8f;
        }

        .btn-reject {
            background-color: #e76f51;
        }

        .btn-view-details {
            background-color: #0077b6;
        }
        
        /* Align button container */
        .btn-container {
            display: flex;
            justify-content: space-around;
            padding: 10px 0;
        }

        /* GridView Container Styling */
        .grid-container {
            max-width: 1200px;
            margin: 20px auto;
            box-shadow: 0 8px 16px rgba(0, 0, 0, 0.15);
            border-radius: 10px;
            overflow: hidden;
        }
    </style>
</head>
<body style="margin: 0; font-family: 'Segoe UI', sans-serif; background-color: #eef2f9;">

<!-- Navbar -->
<div style="background: linear-gradient(90deg, #0052cc, #0066ff); color: #ffffff; padding: 20px; position: fixed; top: 0; width: 100%; z-index: 1000; box-shadow: 0 4px 12px rgba(0, 0, 0, 0.15);">
    <div style="max-width: 1200px; margin: auto; display: flex; align-items: center; justify-content: space-between;">
        <h2 style="margin: 0; font-size: 28px; font-weight: bold;">KindKitchen Admin Dashboard</h2>
        <ul style="list-style-type: none; padding: 0; margin: 0; display: flex; gap: 20px;">
            <li>
                <a id="restaurantRequestBtn" href="#" runat="server" onserverclick="LoadRestaurantRequests"
                    style="color: #ffffff; text-decoration: none; font-weight: 500; padding: 10px 20px; border-radius: 6px; background-color: #004c99; transition: background 0.3s ease; cursor: pointer;">
                    Organization Requests
                </a>
            </li>
            <li>
                <a href="#" style="color: #ffffff; text-decoration: none; font-weight: 500; padding: 10px 20px; border-radius: 6px; transition: background 0.3s;">
                    Dashboard
                </a>
            </li>
            <li>
                <a href="../User/Login.aspx" style="color: #ffffff; text-decoration: none; font-weight: 500; padding: 10px 20px; border-radius: 6px; transition: background 0.3s;">
                    Logout
                </a>
            </li>
        </ul>
    </div>
</div>


    <!-- Main Content -->
    <form id="form1" runat="server" style="max-width: 1200px; margin: 120px auto 40px auto; padding: 20px;">
        <div style="text-align: center; margin-bottom: 30px;">
            <h1 style="font-size: 34px; color: #0052cc; font-weight: bold;">Welcome to the Admin Dashboard!</h1>
            <p style="font-size: 18px; color: #4a4a4a;">Manage your restaurant requests and other settings here.</p>
        </div>

        <!-- Styled GridView for Restaurant Requests -->
        <div class="grid-container">
            <asp:GridView ID="gvRestaurantRequests" runat="server" AutoGenerateColumns="False" Visible="false"
                OnRowCommand="gvRestaurantRequests_RowCommand"
                style="width: 100%; font-size: 16px; color: #333; border-radius: 10px; background-color: #ffffff;">
                <HeaderStyle CssClass="grid-header" />
                <RowStyle CssClass="grid-row" />
                <AlternatingRowStyle CssClass="grid-alt-row" />
                <Columns>
                    <asp:BoundField DataField="AdminID" HeaderText="ID" ItemStyle-Width="10%" />
                    <asp:BoundField DataField="Name" HeaderText="Name" ItemStyle-Width="30%" />
                    <asp:BoundField DataField="UserName" HeaderText="Username" ItemStyle-Width="30%" />
                    <asp:BoundField DataField="Status" HeaderText="Status" ItemStyle-Width="10%" />
                    <asp:TemplateField>
                        <ItemTemplate>
                            <div class="btn-container">
                                <asp:Button ID="btnAccept" runat="server" Text="Accept" CommandName="Accept" CommandArgument='<%# Eval("AdminID") %>'
                                            CssClass="btn-accept" />
                                <asp:Button ID="btnReject" runat="server" Text="Reject" CommandName="Reject" CommandArgument='<%# Eval("AdminID") %>'
                                            CssClass="btn-reject" />
                                <asp:Button ID="btnViewDetails" runat="server" Text="View Details" CommandName="ViewDetails" CommandArgument='<%# Eval("UserName") %>'
                                            CssClass="btn-view-details" />
                            </div>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </form>
</body>
</html>
