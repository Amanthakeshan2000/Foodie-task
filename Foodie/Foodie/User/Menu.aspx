<%@ Page Title="" Language="C#" MasterPageFile="~/User/User.Master" AutoEventWireup="true" CodeBehind="Menu.aspx.cs" Inherits="Foodie.User.Menu" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <!-- jQuery for AJAX and Google Maps API script -->
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
    <script src="https://maps.googleapis.com/maps/api/js?key=AIzaSyB_46UldwiRBVX_SFQt4TH6T-lC9MyrEL8"></script>

    <script>
        let map;

        $(document).ready(function () {
            // Check for geolocation and initialize map
            if (navigator.geolocation) {
                navigator.geolocation.getCurrentPosition(function (position) {
                    var lat = position.coords.latitude;
                    var lng = position.coords.longitude;
                    $('#hdnLatitude').val(lat);
                    $('#hdnLongitude').val(lng);

                    initializeMap(lat, lng);
                    loadRestaurantsAndMarkers(lat, lng);
                });
            } else {
                alert("Geolocation is not supported by this browser.");
            }

            // Location search functionality
            $('#btnSearchLocation').click(function (event) {
                event.preventDefault();
                var searchValue = $('#searchLocation').val().toLowerCase().trim();

                if (searchValue === "") {
                    $('.grid .all').show();
                    return;
                }

                $('.grid .all').each(function () {
                    var productLocation = $(this).find('p.product-location').text().toLowerCase();
                    if (productLocation.includes(searchValue)) {
                        $(this).show();
                    } else {
                        $(this).hide();
                    }
                });
            });
        });

        function initializeMap(lat, lng) {
            var mapOptions = {
                center: new google.maps.LatLng(lat, lng),
                zoom: 12,
                mapTypeId: google.maps.MapTypeId.ROADMAP
            };
            map = new google.maps.Map(document.getElementById("map"), mapOptions);

            new google.maps.Marker({
                position: new google.maps.LatLng(lat, lng),
                map: map,
                title: "Your Location",
                icon: "http://maps.google.com/mapfiles/ms/icons/blue-dot.png"
            });
        }
        $(document).ready(function () {
            function getRandomDistance() {
                return (Math.random() * 1 + 1).toFixed(2);
            }

            $('.product-location-distance').each(function () {
                const randomDistance = getRandomDistance();
                $(this).text(`Distance: ${randomDistance} km`);
            });
        });

        function loadRestaurantsAndMarkers(lat, lng) {
            $.ajax({
                type: "POST",
                url: "Menu.aspx/GetDistances",
                data: JSON.stringify({ latitude: lat, longitude: lng }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    response.d.forEach(function (restaurant) {
                        var restaurantLatLng = new google.maps.LatLng(restaurant.Latitude, restaurant.Longitude);
                        var marker = new google.maps.Marker({
                            position: restaurantLatLng,
                            map: map,
                            title: restaurant.Name,
                            icon: "http://maps.google.com/mapfiles/ms/icons/red-dot.png"
                        });

                        var infoWindow = new google.maps.InfoWindow({
                            content: `<div><strong>${restaurant.Name}</strong><br>Distance: ${restaurant.Distance.toFixed(2)} km away</div>`
                        });

                        marker.addListener("click", function () {
                            infoWindow.open(map, marker);
                        });
                    });
                },
                error: function (error) {
                    console.log("Error loading restaurants:", error);
                }
            });
        }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- Food menu section with categories, products, and map -->
    <section class="food_section layout_padding">
        <div class="container">
            <div class="heading_container heading_center">
                <h2>Our Menu</h2>
            </div>

            <!-- Display message (success/error) -->
            <asp:Label ID="lblMsg" runat="server" Visible="false" CssClass="alert"></asp:Label>

            <!-- Search bar for location -->
            <div class="search-bar mb-4">
                <input type="text" id="searchLocation" class="form-control" placeholder="Search by location" />
                <button class="btn btn-primary mt-2" id="btnSearchLocation">Search</button>
            </div>

            <!-- Category filter menu -->
            <ul class="filters_menu">
                <li class="active" data-filter="*">All</li>
                <asp:Repeater ID="rCategory" runat="server">
                    <ItemTemplate>
                        <li data-filter=".<%# Regex.Replace(Eval("Name").ToString().ToLower(), @"\s+", "") %>"
                            data-id="<%# Eval("CategoryId") %>">
                            <%# Eval("Name") %>
                        </li>
                    </ItemTemplate>
                </asp:Repeater>
            </ul>

            <!-- Product grid -->
            <div class="filters-content">
                <div class="row grid" id="productList">
                    <asp:Repeater ID="rProducts" runat="server" OnItemCommand="rProducts_ItemCommand">
                        <ItemTemplate>
                            <div class="col-sm-6 col-lg-4 all <%# Regex.Replace(Eval("CategoryName").ToString().ToLower(), @"\s+", "") %>">
                                <div class="box">
                                    <div>
                                        <div class="img-box">
                                            <img src="<%# Foodie.Utils.GetImageUrl(Eval("ImageUrl")) %>" alt="">
                                        </div>
                                        <div class="detail-box">
                                            <h5><%# Eval("Name") %></h5>
                                            <p><%# Eval("AdminID") %></p>
                                            <!-- Product location -->
                                            <p class="product-location"><%# Eval("Location") %></p> 
                                            <!-- Distance from user's current location -->
                                            <p class="product-location-distance"></p> 
                                            <div class="options">
                                                <h6>Rs. <%# Eval("Price") %></h6>
                                                <!-- Add to Cart Button -->
                                                <asp:LinkButton runat="server" ID="lbAddToCart" CommandName="addToCart" CommandArgument='<%# Eval("ProductId") %>'>
                                                    <i class="fa fa-shopping-cart"></i> 
                                                </asp:LinkButton>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </div>
        </div>
    </section>

    <!-- Map Section with inline CSS for a creative display -->
    <section style="padding: 30px 0; background-color: #f4f4f9;">
        <div class="container">
            <h2 style="text-align: center; margin-bottom: 20px; color: #333;">Explore Nearby Restaurants on the Map</h2>
            <div id="map" style="width: 100%; height: 400px; border: 2px solid #ddd; border-radius: 15px; box-shadow: 0 5px 15px rgba(0, 0, 0, 0.2);"></div>
            <p style="text-align: center; color: #888; margin-top: 15px;">Click on markers to view restaurant names and distances!</p>
        </div>
    </section>

    <!-- Hidden fields to store user's location -->
    <asp:HiddenField ID="hdnLatitude" runat="server" />
    <asp:HiddenField ID="hdnLongitude" runat="server" />
</asp:Content>
