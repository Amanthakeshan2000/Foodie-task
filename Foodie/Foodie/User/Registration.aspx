<%@ Page Title="" Language="C#" MasterPageFile="~/User/User.Master" AutoEventWireup="true" CodeBehind="Registration.aspx.cs" Inherits="Foodie.User.Registration" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        // Function to show or hide fields based on user type selection without reloading the page
        function showFields() {
            var selectedRole = document.getElementById("<%=ddlUserType.ClientID %>").value;

            // References to different field sections
            var customerFields = document.getElementById("customerFields");
            var sellerFields = document.getElementById("sellerFields");
            var organizationFields = document.getElementById("organizationFields");
            var imageUploadSection = document.getElementById("imageUploadSection");

            // Hide all fields by default
            customerFields.style.display = "none";
            sellerFields.style.display = "none";
            organizationFields.style.display = "none";
            imageUploadSection.style.display = "none";

            // Show the relevant fields and image upload based on selected role
            if (selectedRole === "Customer") {
                customerFields.style.display = "block";
                imageUploadSection.style.display = "block";
            } else if (selectedRole === "Seller") {
                sellerFields.style.display = "block";
                imageUploadSection.style.display = "block";
            } else if (selectedRole === "Organization") {
                organizationFields.style.display = "block";
                imageUploadSection.style.display = "block";
            }
        }

        // Image preview functionality
        function ImagePreview(input) {
            if (input.files && input.files[0]) {
                var reader = new FileReader();
                reader.onload = function (e) {
                    document.getElementById('<%=imgUser.ClientID %>').src = e.target.result;
                    document.getElementById('<%=imgUser.ClientID %>').width = 200;
                    document.getElementById('<%=imgUser.ClientID %>').height = 200;
                };
                reader.readAsDataURL(input.files[0]);
            }
        }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section class="book_section layout_padding">
        <div class="container">
            <div class="heading_container">
                <asp:Label runat="server" ID="lblMsg" Visible="false"></asp:Label>
                <asp:Label runat="server" ID="lblHeaderMsg" Text="<h2>User Registration</h2>"></asp:Label>
            </div>

            <!-- Role Selection -->
            <div class="row">
                <div class="col-md-6">
                    <div class="form_container">
                        <!-- User Type Dropdown -->
                        <div>
                            <asp:DropDownList ID="ddlUserType" runat="server" CssClass="form-control" AutoPostBack="false" OnChange="showFields()">
                                <asp:ListItem Text="Select User Type" Value=""></asp:ListItem>
                                <asp:ListItem Text="Customer" Value="Customer"></asp:ListItem>
                                <asp:ListItem Text="Seller" Value="Seller"></asp:ListItem>
                                <asp:ListItem Text="Organization" Value="Organization"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
            </div>

            <!-- Customer Fields -->
            <div id="customerFields" style="display:none;">
                <div class="row">
                    <div class="col-md-6">
                        <div class="form_container">
                            <div>
                                <asp:TextBox ID="txtName" runat="server" CssClass="form-control" placeholder="Enter Full Name" />
                            </div>
                             <div>
                                 <asp:TextBox ID="txtUserName" runat="server" CssClass="form-control" placeholder="Enter User Name" />
                             </div>
                             <div>
                                 <asp:TextBox ID="txtMobile" runat="server" CssClass="form-control" placeholder="Enter Mobile Number" />
                             </div>
                            <div>
                                <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" placeholder="Enter Email" />
                            </div>
                            <div>
                                <asp:TextBox ID="txtAddress" runat="server" CssClass="form-control" placeholder="Enter Address" />
                            </div>
                            <div>
                                <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" placeholder="Enter Password" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <!-- Seller Fields -->
            <div id="sellerFields" style="display:none;">
                <div class="row">
                    <div class="col-md-6">
                        <div class="form_container">
                                                                    <div>
        <asp:TextBox ID="txtUserName1" runat="server" CssClass="form-control" placeholder="Enter User Name" />
    </div>
                            <div>
                                <asp:TextBox ID="txtShopName1" runat="server" CssClass="form-control" placeholder="Enter Restuarant" />
                            </div>
                                                        <div>
                                <asp:TextBox ID="txtLocation1" runat="server" CssClass="form-control" placeholder="Enter Location" />
                            </div>
                                                        <div>
                                <asp:TextBox ID="txtDescription1" runat="server" CssClass="form-control" placeholder="Enter Description" />
                            </div>
                                                        <div>
                                <asp:TextBox ID="txtAddress1" runat="server" CssClass="form-control" placeholder="Enter Address" />
                            </div>
                                                        <div>
                                <asp:TextBox ID="txtNumber1" runat="server" CssClass="form-control" placeholder="Enter Number" />
                            </div>
                                                               <div>
                               <asp:TextBox ID="txtPassword1" runat="server" CssClass="form-control" placeholder="Enter Password" />
                           </div>

                        </div>
                    </div>
                </div>
            </div>

            <!-- Organization Fields -->
            <div id="organizationFields" style="display:none;">
                <div class="row">
                    <div class="col-md-6">
                        <div class="form_container">
                            <div>
                                <asp:TextBox ID="txtOrganizationName2" runat="server" CssClass="form-control" placeholder="Enter Organization Name" />
                            </div>
                            <div>
                                <asp:TextBox ID="txtLocation2" runat="server" CssClass="form-control" placeholder="Enter Location" />
                            </div>
                              <div>
                                  <asp:TextBox ID="txtDescription2" runat="server" CssClass="form-control" placeholder="Enter Description" />
                              </div>
                              <div>
                                  <asp:TextBox ID="txtAddress2" runat="server" CssClass="form-control" placeholder="Enter Address" />
                              </div>
                              <div>
                                  <asp:TextBox ID="txtNumber2" runat="server" CssClass="form-control" placeholder="Enter Number" />
                              </div>
                            <div>
                                  <asp:TextBox ID="txtEmail2" runat="server" CssClass="form-control" placeholder="Enter Email" />
                              </div>
                            <div>
                                 <asp:TextBox ID="txtPassword2" runat="server" CssClass="form-control" placeholder="Enter Password" />
          </div>
                                               <div>
                        <asp:TextBox ID="txtUserName2" runat="server" CssClass="form-control" placeholder="Enter User Name" />
 </div>
                        </div>
                    </div>
                </div>
            </div>

            <!-- Image Upload Section -->
            <div id="imageUploadSection" style="display:none;">
                <div class="row">
                    <div class="col-md-6">
                        <div class="form_container">
                            <div>
                                <asp:FileUpload ID="fuUserImage" runat="server" CssClass="form-control" onchange="ImagePreview(this);" />
                            </div>
                            <div>
                                <asp:Image ID="imgUser" runat="server" CssClass="img-thumbnail" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <!-- Registration Button -->
            <div class="row">
                <div class="col-md-6">
                    <div class="form_container">
                        <asp:Button ID="btnRegister" runat="server" Text="Register" CssClass="btn btn-success" OnClick="btnRegister_Click" />
                    </div>
                </div>
            </div>
        </div>
    </section>
</asp:Content>
