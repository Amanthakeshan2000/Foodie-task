<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="Product.aspx.cs" Inherits="Foodie.Admin.Product" %>

<%@ Import Namespace="Foodie" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script>
        window.onload = function () {
            var seconds = 5;
            setTimeout(function () {
                document.getElementById("<%=lblMsg.ClientID %>").style.display = "none";
            }, seconds * 1000);

            // Get user's location
            getLocation();
        };

        // Function to get the user's current location
        function getLocation() {
            if (navigator.geolocation) {
                navigator.geolocation.getCurrentPosition(showPosition, showError);
            } else {
                alert("Geolocation is not supported by this browser.");
            }
        }

        // Function to store location in hidden fields
        function showPosition(position) {
            document.getElementById("<%=hdnLatitude.ClientID %>").value = position.coords.latitude;
            document.getElementById("<%=hdnLongitude.ClientID %>").value = position.coords.longitude;
        }

        // Function to handle geolocation errors
        function showError(error) {
            switch (error.code) {
                case error.PERMISSION_DENIED:
                    alert("User denied the request for Geolocation.");
                    break;
                case error.POSITION_UNAVAILABLE:
                    alert("Location information is unavailable.");
                    break;
                case error.TIMEOUT:
                    alert("The request to get user location timed out.");
                    break;
                case error.UNKNOWN_ERROR:
                    alert("An unknown error occurred.");
                    break;
            }
        }

        // Image preview function
        function ImagePreview(input) {
            if (input.files && input.files[0]) {
                var reader = new FileReader();
                reader.onload = function (e) {
                    $('#<%=imgProduct.ClientID %>').prop('src', e.target.result)
                        .width(200)
                        .height(200);
                };
                reader.readAsDataURL(input.files[0]);
            }
        }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="pcoded-inner-content pt-0">
        <div class="align-align-self-end">
            <asp:Label runat="server" Text="Label" ID="lblMsg" Visible="false"></asp:Label>
        </div>
        <div class="main-body">
            <div class="page-wrapper">
                <div class="page-body">
                    <div class="row">
                        <div class="col-sm-12">
                            <div class="card">
                                <div class="card-header"></div>
                                <div class="card-block">
                                    <div class="row">
                                        <div class="col-sm-6 col-md-4 col-lg-4">
                                            <h4 class="sub-title">Product</h4>

                                            <!-- Form fields for Product details -->
                                            <div class="form-group">
                                                <label>Product Name</label>
                                                <div>
                                                    <asp:TextBox runat="server" ID="txtName" CssClass="form-control" placeholder="Enter Product name" required></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Name is Required" ForeColor="Red" Display="Dynamic" SetFocusOnError="true" ControlToValidate="txtName"></asp:RequiredFieldValidator>
                                                    <asp:HiddenField runat="server" ID="hdnId" Value="0"></asp:HiddenField>
                                                </div>
                                            </div>

                                            <div class="form-group">
                                                <label>Product Description</label>
                                                <div>
                                                    <asp:TextBox runat="server" ID="txtDescription" CssClass="form-control" placeholder="Enter Product Description" TextMode="MultiLine" required></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Description is Required" ForeColor="Red" Display="Dynamic" SetFocusOnError="true" ControlToValidate="txtDescription"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>

                                            <div class="form-group">
                                                <label>Product Price(Rs.)</label>
                                                <div>
                                                    <asp:TextBox runat="server" ID="txtPrice" CssClass="form-control" placeholder="Enter Product Price" required></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Price is Required" ForeColor="Red" Display="Dynamic" SetFocusOnError="true" ControlToValidate="txtPrice"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Price must be in decimal" ForeColor="Red" Display="Dynamic" SetFocusOnError="true" ControlToValidate="txtPrice" ValidationExpression="^\d{0,8}(\.\d{1,4})?$"></asp:RegularExpressionValidator>
                                                </div>
                                            </div>

                                            <div class="form-group">
                                                <label>Product Quantity</label>
                                                <div>
                                                    <asp:TextBox runat="server" ID="txtQuantity" CssClass="form-control" placeholder="Enter Product Quantity" required></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Quantity is Required" ForeColor="Red" Display="Dynamic" SetFocusOnError="true" ControlToValidate="txtQuantity"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="Quantity must be non-negative" ForeColor="Red" Display="Dynamic" SetFocusOnError="true" ControlToValidate="txtQuantity" ValidationExpression="^([1-9]\d*|0)$"></asp:RegularExpressionValidator>
                                                </div>
                                            </div>

                                            <div class="form-group">
                                                <label>Product Image</label>
                                                <div>
                                                    <asp:FileUpload runat="server" ID="fuProductImage" CssClass="form-control" onchange="ImagePreview(this);"></asp:FileUpload>
                                                </div>
                                            </div>

                                            <div class="form-group">
                                                <label>Product Category</label>
                                                <div>
                                                    <asp:DropDownList ID="ddlCategories" runat="server" CssClass="form-control" DataSourceID="SqlDataSource1" DataTextField="Name" DataValueField="CategoryID" AppendDataBoundItems="true">
                                                        <asp:ListItem runat="server" Value="0">Select Category</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Category is Required" ForeColor="Red" Display="Dynamic" SetFocusOnError="true" ControlToValidate="ddlCategories"></asp:RequiredFieldValidator>
                                                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:cs %>" SelectCommand="SELECT [CategoryID], [Name] FROM [Categories]"></asp:SqlDataSource>
                                                </div>
                                            </div>

                                            <div class="form-check pl-4">
                                                <asp:CheckBox runat="server" ID="cbIsActive" Text="&nbsp; IsActive" CssClass="form-check-input"></asp:CheckBox>
                                            </div>

                                            <!-- Hidden fields for storing location -->
                                            <asp:HiddenField ID="hdnLatitude" runat="server" />
                                            <asp:HiddenField ID="hdnLongitude" runat="server" />

                                            <!-- Add/Update and Clear buttons -->
                                            <div class="pb-5">
                                                <asp:Button runat="server" ID="btnAddOrUpdate" Text="Add" CssClass="btn btn-primary" OnClick="btnAddOrUpdate_Click" />
                                                &nbsp;
                                                <asp:Button runat="server" Text="Clear" ID="btnClear" CssClass="btn btn-primary" CausesValidation="false" OnClick="btnClear_Click" />
                                            </div>
                                            <div>
                                                <asp:Image runat="server" ID="imgProduct" CssClass="img-thumbnail"></asp:Image>
                                            </div>
                                        </div>

                                        <div class="col-sm-6 col-md-8 col-lg-8 mobile-inputs">
                                            <h4 class="sub-title">Product Lists</h4>
                                            <div class="card-block table-border-style">
                                                <div class="table-responsive">
                                                    <asp:Repeater runat="server" ID="rProduct" OnItemCommand="rProduct_ItemCommand" OnItemDataBound="rProduct_ItemDataBound">
    <HeaderTemplate>
        <table class="table data-table-export table-hover nowrap">
            <thead>
                <tr>
                    <th class="table-plus">Name</th>
                    <th>Image</th>
                    <th>Price (Rs.)</th>
                    <th>Qty</th>
                    <th>Category</th>
                    <th>IsActive</th>
                    <th>Description</th>
                    <th>CreatedDate</th>
                    <th class="datatable-nosort">Action</th>
                </tr>
            </thead>
            <tbody>
    </HeaderTemplate>
    <ItemTemplate>
        <tr>
            <td class="table-plus"><%# Eval("Name") %></td>
            <td>
                <img alt="" width="40" src="<%# Eval("ImageUrl") %>" />
            </td>
            <td><%# Eval("Price") %></td>
            <td><asp:Label ID="lblQuantity" runat="server" Text='<%# Eval("Quantity") %>'></asp:Label></td>
            <td><%# Eval("CategoryName") %></td>
            <td><asp:Label ID="lblIsActive" runat="server" Text='<%# Eval("IsActive") %>'></asp:Label></td>
            <td><%# Eval("Description") %></td>
            <td><%# Eval("CreatedDate") %></td>
            <td>
                <asp:LinkButton ID="lnkEdit" runat="server" Text="Edit" CommandName="edit" CssClass="badge bg-primary" CommandArgument='<%# Eval("ProductId") %>'></asp:LinkButton>
                <asp:LinkButton ID="lnkDelete" runat="server" Text="Delete" CommandName="delete" CssClass="badge bg-danger" CommandArgument='<%# Eval("ProductId") %>'></asp:LinkButton>
            </td>
        </tr>
    </ItemTemplate>
    <FooterTemplate>
        </tbody>
        </table>
    </FooterTemplate>
</asp:Repeater>

                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
