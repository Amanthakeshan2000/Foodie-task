<%@ Page Title="Food Donation" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="Donation.aspx.cs" Inherits="Foodie.Admin.Donation" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="width: 100%; max-width: 600px; margin: 0 auto; padding: 20px; background-color: #f9f9f9; border-radius: 10px;">
        <h2 style="text-align: center; color: #333;">Food Donation</h2>

        <!-- District -->
        <div style="margin-bottom: 15px;">
            <label for="ddlDistrict" style="display: block; margin-bottom: 5px; font-weight: bold; color: #555;">Select District</label>
            <asp:DropDownList 
                ID="ddlDistrict" 
                runat="server" 
                AutoPostBack="true" 
                OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged" 
                AppendDataBoundItems="true"
                style="width: 100%; padding: 10px; border: 1px solid #ccc; border-radius: 5px;">
                <asp:ListItem Text="-- Select District --" Value="0"></asp:ListItem>
            </asp:DropDownList>
            <asp:RequiredFieldValidator 
                ID="rfvDistrict" 
                runat="server" 
                ControlToValidate="ddlDistrict"
                InitialValue="0" 
                ErrorMessage="Please select a district." 
                ForeColor="Red" />
        </div>

        <!-- Organization -->
        <div style="margin-bottom: 15px;">
            <label for="ddlOrganization" style="display: block; margin-bottom: 5px; font-weight: bold; color: #555;">Select Organization</label>
            <asp:DropDownList 
                ID="ddlOrganization" 
                runat="server" 
                AppendDataBoundItems="true"
                style="width: 100%; padding: 10px; border: 1px solid #ccc; border-radius: 5px;">
                <asp:ListItem Text="-- Select Organization --" Value="0"></asp:ListItem>
            </asp:DropDownList>
            <asp:RequiredFieldValidator 
                ID="rfvOrganization" 
                runat="server" 
                ControlToValidate="ddlOrganization"
                InitialValue="0" 
                ErrorMessage="Please select an organization." 
                ForeColor="Red" />
        </div>

        <!-- Description -->
        <div style="margin-bottom: 15px;">
            <label for="txtDescription" style="display: block; margin-bottom: 5px; font-weight: bold; color: #555;">Description</label>
            <asp:TextBox 
                ID="txtDescription" 
                runat="server" 
                placeholder="Enter Description"
                style="width: 100%; padding: 10px; border: 1px solid #ccc; border-radius: 5px;">
            </asp:TextBox>
            <asp:RequiredFieldValidator 
                ID="rfvDescription" 
                runat="server" 
                ControlToValidate="txtDescription"
                ErrorMessage="Description is required." 
                ForeColor="Red" />
        </div>

        <!-- Category -->
        <div style="margin-bottom: 15px;">
            <label for="ddlCategory" style="display: block; margin-bottom: 5px; font-weight: bold; color: #555;">Select Category</label>
            <asp:DropDownList 
                ID="ddlCategory" 
                runat="server" 
                AutoPostBack="true" 
                OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged"
                AppendDataBoundItems="true"
                style="width: 100%; padding: 10px; border: 1px solid #ccc; border-radius: 5px;">
                <asp:ListItem Text="-- Select Category --" Value="0"></asp:ListItem>
            </asp:DropDownList>
            <asp:RequiredFieldValidator 
                ID="rfvCategory" 
                runat="server" 
                ControlToValidate="ddlCategory"
                InitialValue="0" 
                ErrorMessage="Please select a category." 
                ForeColor="Red" />
        </div>

        <!-- Product -->
        <div style="margin-bottom: 15px;">
            <label for="ddlProduct" style="display: block; margin-bottom: 5px; font-weight: bold; color: #555;">Select Product</label>
            <asp:DropDownList 
                ID="ddlProduct" 
                runat="server" 
                AppendDataBoundItems="true"
                style="width: 100%; padding: 10px; border: 1px solid #ccc; border-radius: 5px;">
                <asp:ListItem Text="-- Select Product --" Value="0"></asp:ListItem>
            </asp:DropDownList>
            <asp:RequiredFieldValidator 
                ID="rfvProduct" 
                runat="server" 
                ControlToValidate="ddlProduct"
                InitialValue="0" 
                ErrorMessage="Please select a product." 
                ForeColor="Red" />
        </div>

        <!-- Donation Amount -->
        <div style="margin-bottom: 15px;">
            <label for="txtDonationAmount" style="display: block; margin-bottom: 5px; font-weight: bold; color: #555;">Donation Amount</label>
            <asp:TextBox 
                ID="txtDonationAmount" 
                runat="server" 
                placeholder="Enter amount"
                style="width: 100%; padding: 10px; border: 1px solid #ccc; border-radius: 5px;">
            </asp:TextBox>
            <asp:RequiredFieldValidator 
                ID="rfvDonationAmount" 
                runat="server" 
                ControlToValidate="txtDonationAmount"
                ErrorMessage="Please enter a donation amount." 
                ForeColor="Red" />
            <asp:RegularExpressionValidator 
                ID="revDonationAmount" 
                runat="server" 
                ControlToValidate="txtDonationAmount"
                ErrorMessage="Please enter a valid donation amount."
                ValidationExpression="^\d+(\.\d{1,2})?$" 
                ForeColor="Red" />
        </div>

        <!-- Submit Button -->
        <div style="text-align: center;">
            <asp:Button 
                ID="btnSubmit" 
                runat="server" 
                Text="Submit Donation" 
                OnClick="btnSubmit_Click"
                OnClientClick="return confirm('Are you sure you want to submit this donation?');"
                style="background-color: #4CAF50; color: white; padding: 10px 20px; border: none; border-radius: 5px; cursor: pointer;" />
        </div>
    </div>
</asp:Content>
