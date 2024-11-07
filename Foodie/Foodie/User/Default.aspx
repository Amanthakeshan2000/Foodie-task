<%@ Page Title="" Language="C#" MasterPageFile="~/User/User.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Foodie.User.Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<br /><br /><br />

<div class="heading_container" style="text-align: center; display: flex; justify-content: center; align-items: center;">
  <h2 style="margin: 0; font-size: 2.5em; font-family: 'Poppins', sans-serif;">
    We Are KindKitchen
  </h2>
</div>


 <!-- offer section -->   
<section class="offer_section" style="padding-bottom: 50px; font-family: 'Poppins', sans-serif;">
  <div class="offer_container" style="margin: 0 auto; width: 100%;">
    <div class="container" style="display: flex; justify-content: center;">
      <div class="row" style="display: flex; justify-content: space-between; width: 100%; flex-wrap: wrap;">
        <asp:Repeater ID="rCategory" runat="server">
          <ItemTemplate>
            <div class="col-lg-15" style="flex: 1 1 30%; max-width: 30%; height: 270px; margin-bottom: 30px; display: flex; justify-content: center; position: relative;">
              <div class="box" style="border: none; border-radius: 20px; overflow: hidden; box-shadow: 0 12px 25px rgba(0, 0, 0, 0.2); transition: transform 0.4s ease, box-shadow 0.4s ease; background: linear-gradient(145deg, #2a2a2a, #595959); transform: scale(1); cursor: pointer;">
                <div class="img-box" style="width: 100%; height: 100%; overflow: hidden; position: relative;">
                  <a href="Menu.aspx?id=<%# Eval("CategoryId") %>">
                    <img src="<%# Foodie.Utils.GetImageUrl(Eval("ImageUrl")) %>" alt="" style="width: 100%; height: 100%; object-fit: cover; transition: transform 0.5s ease, filter 0.5s ease;">
                  </a>
                  <div class="img-overlay" style="position: absolute; top: 0; left: 0; width: 100%; height: 100%; background: rgba(0, 0, 0, 0.5); opacity: 0; transition: opacity 0.5s ease;">
                  </div>
                </div>
                <div class="detail-box" style="padding: 15px; text-align: center; color: white;">
                  <h5 style="font-size: 1.6em; margin: 15px 0; font-weight: bold; letter-spacing: 1px;">
                    <%# Eval("Name") %>
                  </h5>
                  <a href="Menu.aspx?id=<%# Eval("CategoryId") %>" style="color: white; background: rgba(255, 255, 255, 0.15); padding: 12px 35px; text-decoration: none; border-radius: 25px; font-weight: bold; display: inline-block; transition: background 0.4s ease, transform 0.4s ease; text-transform: uppercase;">
                    >
                  </a>
                </div>
              </div>
            </div>
          </ItemTemplate>
        </asp:Repeater>
      </div>
    </div>
  </div>
</section>

<%--<style>
  .box:hover {
    transform: scale(1.05);
    box-shadow: 0 15px 30px rgba(0, 0, 0, 0.3);
  }
  .box:hover .img-box img {
    transform: scale(1.1);
    filter: brightness(60%);
  }
  .box:hover .img-overlay {
    opacity: 1;
  }
  .box:hover .detail-box a {
    background-color: rgba(255, 255, 255, 0.3);
    transform: translateY(-5px);
  }
</style>--%>


<style>
  @import url('https://fonts.googleapis.com/css2?family=Poppins:wght@400;600&display=swap');
</style>


<style>
  @import url('https://fonts.googleapis.com/css2?family=Poppins:wght@400;600&display=swap');
</style>



<!-- end offer section -->

    <br /> <br /> <br />

<!-- about section -->

  <section class="about_section layout_padding-bottom">
    <div class="container  ">

      <div class="row">
        <div class="col-md-6 "><br />
            <br />
         <br /><div class="img-box">
   <img src="../TemplateFiles/images/about-img.png" alt="" width="50%" height="50%">
    </div>

        </div>
        <div class="col-md-6">
          <div class="detail-box">
          <div class="heading_container"  display: flex; justify-content: center; align-items: center;">
  <h2 style="margin: 0; font-size: 2.5em; font-family: 'Poppins', sans-serif;">
    We Are KindKitchen
  </h2>
</div>
            <p>
             Welcome to The Flavor Junction, where every meal is an adventure! Our restaurant is dedicated to serving up bold, fresh, and unforgettable flavors, blending the best of fast food convenience with gourmet quality. From sizzling, juicy burgers to hand-crafted sides and decadent desserts, each dish is made with the finest ingredients, packed with love and creativity. 
            </p>
            <a href="">
              Read More
            </a>
          </div>
        </div>
      </div>
    </div>
  </section>

<!-- end about section -->

<!-- client section -->
    <br />
  <br /><br /><section class="client_section layout_padding-bottom">
    <div class="container">
    

                  <div class="heading_container" style="text-align: center;  display: flex; justify-content: center; align-items: center;">
  <h2 style="margin: 0; font-size: 2.5em; font-family: 'Poppins', sans-serif;">
    What Says Our Customers
  </h2>
</div>
      <div class="carousel-wrap row ">
        <div class="owl-carousel client_owl-carousel">
          <div class="item">
            <div class="box">
              <div class="detail-box">
                <p>
                  Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam
                </p>
                <h6>
                  Moana Michell
                </h6>
                <p>
                  magna aliqua
                </p>
              </div>
              <div class="img-box">
                <img src="../TemplateFiles/images/client1.jpg" alt="" class="box-img">
              </div>
            </div>
          </div>
          <div class="item">
            <div class="box">
              <div class="detail-box">
                <p>
                  Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam
                </p>
                <h6>
                  Mike Hamell
                </h6>
                <p>
                  magna aliqua
                </p>
              </div>
              <div class="img-box">
                <img src="../TemplateFiles/images/client2.jpg" alt="" class="box-img">
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </section>

  <!-- end client section -->

</asp:Content>
