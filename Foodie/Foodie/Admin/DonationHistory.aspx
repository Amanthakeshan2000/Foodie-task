<%@ Page Title="Donation History" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="DonationHistory.aspx.cs" Inherits="Foodie.Admin.DonationHistory" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <!-- You can add page-specific head content here if needed -->
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2 style="text-align: center; color: #333;">Donation History</h2>
    <asp:Panel ID="pnlDonationHistory" runat="server" Style="margin: 20px;">
        <asp:GridView 
            ID="gvDonationHistory" 
            runat="server" 
            AutoGenerateColumns="False" 
            CssClass="table table-striped table-bordered"
            AllowPaging="True" 
            PageSize="10" 
            AllowSorting="True"
            OnPageIndexChanging="gvDonationHistory_PageIndexChanging"
            OnSorting="gvDonationHistory_Sorting">
            <Columns>
                <asp:BoundField DataField="ID" HeaderText="ID" SortExpression="ID" />
                <asp:BoundField DataField="Name" HeaderText="Organization Name" SortExpression="Name" />
                <asp:BoundField DataField="Location" HeaderText="District" SortExpression="Location" />
                <asp:BoundField DataField="Description" HeaderText="Description" SortExpression="Description" />
                <asp:BoundField DataField="CreateDate" HeaderText="Donation Date" DataFormatString="{0:MM/dd/yyyy HH:mm:ss}" HtmlEncode="false" SortExpression="CreateDate" />
<%--                <asp:BoundField DataField="CreateBy" HeaderText="Created By" SortExpression="CreateBy" />--%>
                <asp:BoundField DataField="Category" HeaderText="Category" SortExpression="Category" />
                <asp:BoundField DataField="Product" HeaderText="Product" SortExpression="Product" />
                <asp:BoundField DataField="Amount" HeaderText="Amount" DataFormatString="{0:C}" HtmlEncode="false" SortExpression="Amount" />
                <asp:BoundField DataField="StatusText" HeaderText="Status" SortExpression="StatusText" />
            </Columns>
            <PagerStyle CssClass="pager" />
            <HeaderStyle CssClass="header" />
        </asp:GridView>
    </asp:Panel>
</asp:Content>
