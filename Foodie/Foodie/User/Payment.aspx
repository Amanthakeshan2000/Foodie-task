<%@ Page Title="" Language="C#" MasterPageFile="~/User/User.Master" AutoEventWireup="true" CodeBehind="Payment.aspx.cs" Inherits="Foodie.User.Payment" %>

<%@ Import Namespace="Foodie" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script>

        window.onload = function () {
            var seconds = 5;
            setTimeout(function () {
                document.getElementById("<%=lblMsg.ClientID %>").style.display = "none";
            }, seconds * 1000);
        };
        /*For tooltip*/
        $(function () {
            $('[data-toggle="tooltip"]').tooltip()
        })

    </script>
    <script type="text/javascript">
        function DisableBackButton() {
            window.history.forward()
        }
        DisableBackButton();
        window.onload = DisableBackButton;
        window.onpageshow = function (evt) { if (evt.persisted) DisableBackButton() }
        window.onunload = function () { void (0) }
    </script>
    <style>
        .rounded {
            border-radius: 1rem
        }

        .nav-pills .nav-link {
            color: #555
        }

       .nav-pills .nav-link.active {
                color: white
            }

        .bold {
            font-weight: bold
        }

        .card {
            padding: 40px 50px;
            border-radius: 20px;
            border: none;
            box-shadow: 1px 5px 10px 1px rgba(0, 0, 0, 0.2)
        }

     

    </style>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <section class="book_section" style="background-image: url('../Images/payment-bg.png'); width: 100%; height: 100%; background-repeat: no-repeat; background-size: auto; background-attachment: fixed; background-position: left;">
    <div class="container py-5">
        <div class="heading_container">
            <div class="align-self-end">
                <asp:Label ID="lblMsg" runat="server" Visible="false"></asp:Label>
            </div>
        </div>
        <!-- For demo purpose -->
        <div class="row mb-4">
            <div class="col-lg-8 mx-auto text-center">
                <h1 class="display-6">Summery</h1>
            </div>
        </div>
        <!-- End -->
        <div class="row">
            <div class="col-lg-6 mx-auto">
                <div class="card">
                    <div class="card-header">
                      
                        <!-- Cash On Delivery info -->
                       <%-- <div id="paypal" class="tab-pane fade pt-3">
                            <div class="form-group">
                                <h6>Delivery Address</h6>
                                <label for="txtCODAddress">Delivery Address</label>
                                <asp:TextBox ID="txtCODAddress" runat="server" CssClass="form-control" placeholder="Delivery Address" TextMode="Multiline"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvCODAddress" runat="server" ErrorMessage="Address is required" ForeColor="Red" ControlToValidate="txtCODAddress" Display="Dynamic" SetFocusOnError="true" ValidationGroup="cod" Font-Names="Segoe Script"></asp:RequiredFieldValidator>
                            </div>
                            <p>
                                <asp:LinkButton ID="LbCodSubmit" runat="server" CssClass="btn btn-info" ValidationGroup="cod" OnClick="LbCodSubmit_Click">
                                    <i class="fa fa-cart-arrow-down mr-2"></i>Confirm Order
                                </asp:LinkButton>
                            </p>
                            <p class="text-muted">
                                Note: At the time of receiving your order, you need to do full payment.
                                After completing the payment process, you can check your updated order status.
                            </p>
                        </div>--%>
                        
                        <p>
                             Note: At the time of receiving your order, you need to do full payment.
                                After completing the payment process, you can check your updated order status.
                        </p>
                        <asp:Button ID="LbCodSubmit" runat="server" Text="Ok" OnClick="LbCodSubmit_Click" class="btn btn-primary"/>
                        <br />
                        <br />
                        <!-- End -->
                        <div class="card-footer">
                            <b class="badge badge-success badge-pill shadow-sm">Order Total: <% Response.Write(Session["grandTotalPrice"]); %></b>
                            <div class="pt-1">
                                <asp:ValidationSummary ID="ValidationSummary2" runat="server" ForeColor="Red" ValidationGroup="card" HeaderText="Fix the following errors" Font-Names="Segoe Script"/>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div> 
    </div>
    <!-- Embedded SVG -->
   
</section>


</asp:Content>
