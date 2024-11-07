//// to get current year
//function getYear() {
//    var currentDate = new Date();
//    var currentYear = currentDate.getFullYear();
//    document.querySelector("#displayYear").innerHTML = currentYear;
//}

//getYear();


//// isotope js
//$(window).on('load', function () {
//    $('.filters_menu li').click(function () {
//        $('.filters_menu li').removeClass('active');
//        $(this).addClass('active');

//        var data = $(this).attr('data-filter');
//        $grid.isotope({
//            filter: data
//        })
//    });

//    var $grid = $(".grid").isotope({
//        itemSelector: ".all",
//        percentPosition: false,
//        masonry: {
//            columnWidth: ".all"
//        }
//    })
//});

//// nice select
//$(document).ready(function() {
//    $('select').niceSelect();
//  });

///** google_map js **/
//function myMap() {
//    var mapProp = {
//        center: new google.maps.LatLng(40.712775, -74.005973),
//        zoom: 18,
//    };
//    var map = new google.maps.Map(document.getElementById("googleMap"), mapProp);
//}

//// client section owl carousel
//$(".client_owl-carousel").owlCarousel({
//    loop: true,
//    margin: 0,
//    dots: false,
//    nav: true,
//    navText: [],
//    autoplay: true,
//    autoplayHoverPause: true,
//    navText: [
//        '<i class="fa fa-angle-left" aria-hidden="true"></i>',
//        '<i class="fa fa-angle-right" aria-hidden="true"></i>'
//    ],
//    responsive: {
//        0: {
//            items: 1
//        },
//        768: {
//            items: 2
//        },
//        1000: {
//            items: 2
//        }
//    }
//});


// Get current year
function getYear() {
    var currentDate = new Date();
    var currentYear = currentDate.getFullYear();
    document.querySelector("#displayYear").innerHTML = currentYear;
}
getYear();

// Isotope JS
$(window).on('load', function () {
    var $grid = $(".grid").isotope({
        itemSelector: ".all",
        percentPosition: false,
        masonry: {
            columnWidth: ".all"
        }
    });

    $('.filters_menu li').click(function () {
        $('.filters_menu li').removeClass('active');
        $(this).addClass('active');

        var data = $(this).attr('data-filter');
        if (data == "*") {
            $grid.isotope({ filter: '*' });
        } else {
            $grid.isotope({ filter: data });
        }
    });
});

//$(window).on('load', function () {
//    var $grid = $(".grid").isotope({
//        itemSelector: ".all",
//        percentPosition: false,
//        masonry: {
//            columnWidth: ".all"
//        }
//    });

//    $('.filters_menu li').click(function () {
//        $('.filters_menu li').removeClass('active');
//        $(this).addClass('active');

//        var data = $(this).attr('data-filter');
//        $grid.isotope({
//            filter: data
//        });
//    });
//});

// Nice Select
$(document).ready(function () {
    $('select').niceSelect();
});

/** Google Map JS **/
function myMap() {
    var mapProp = {
        center: new google.maps.LatLng(40.712775, -74.005973),
        zoom: 18,
    };
    var map = new google.maps.Map(document.getElementById("googleMap"), mapProp);
}

// Client section Owl Carousel
$(".client_owl-carousel").owlCarousel({
    loop: true,
    margin: 0,
    dots: false,
    nav: true,
    navText: [
        '<i class="fa fa-angle-left" aria-hidden="true"></i>',
        '<i class="fa fa-angle-right" aria-hidden="true"></i>'
    ],
    autoplay: true,
    autoplayHoverPause: true,
    responsive: {
        0: {
            items: 1
        },
        768: {
            items: 2
        },
        1000: {
            items: 2
        }
    }
});

//(function ($) {


//    var proQty = $('.pro-qty');
//    proQty.prepend('<span class="dec qtybtn">-</span>'); /*style="color:white"*/
//    proQty.append('<span class="inc qtybtn">+</span>'); /*style="color:white"*/
//    proQty.on('click', '.qtybtn', function (){
//        var $button = $(this);
//    var oldValue = $button.parent().find('input').val();
//    if ($button.hasClass('inc')) {
//        //var newVal = parseFloat(oldValue) + 1;
//        if (oldValue >= 10) {
//            var newVal = parseFloat(oldValue);
//        } else {
//            newVal = parseFloat(oldValue) + 1;
//        }
//    } else {
//        // Don't allow decrementing below zero
//        if (oldValue > 1) {
//            var newVal = parseFloat(oldValue) - 1;
//        } else {
//            newVal = 1;
//            I
//        }
//    }
//    $button.parent().find('input').val(newVal);
//});
//})(jQuery);


