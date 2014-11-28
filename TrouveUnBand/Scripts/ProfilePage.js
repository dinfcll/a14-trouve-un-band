//Menu
//var menuTopPosition;

//if ($(".profile-tab-menu")[0]) {
//    menuTopPosition = $(".profile-tab-menu").offset().top;
//}


//$(".profile-tab-menu>div.list-group>a").click(function (event) {
//    var index = $(this).index();

//    $(this).siblings(".active").removeClass("active");
//    $(this).addClass("active");
//    $(".profile-tab-content").removeClass("active");
//    $(".profile-tab-content").eq(index).addClass("active");
//    return false;
//});

//$(window).scroll(function () {
//    var scrollPosition = $(document).scrollTop();
//    var isOnTop = scrollPosition >= menuTopPosition - 50;

//    if (isOnTop) {
//        var newMenuTopPosition = scrollPosition - menuTopPosition + 50;
//        $(".profile-tab-menu").css("top", newMenuTopPosition);
//        $(".back-to-top").fadeIn();
//    }
//    else {
//        $('.profile-tab-menu').css("top", 0);
//        $(".back-to-top").fadeOut();
//    }

//});

////Tab description/

//$(".profile-info-row").click(function () {
//    var chevron = $(this).children("i");
//    var panelId = $(this).attr("data-panel-id");
//    var panel = $("#" + panelId);

//    panel.slideToggle(400, function () {
//        if (panel.is(":visible")) {
//            chevron.removeClass("glyphicon-chevron-down");
//            chevron.addClass("glyphicon-chevron-up");
//        }
//        else {
//            chevron.removeClass("glyphicon-chevron-up");
//            chevron.addClass("glyphicon-chevron-down");
//        }
//    })
//});

////Tab Pictures
//var photoList = document.querySelectorAll(".profile-photo-list li");

//if (photoList.length > 4) {
//    for(var i=4;i<photoList.length;i++)
//    {
//        photoList[i].style.display = "none";
//    }
//}

//$("#profile-photo-carousel").carousel({
//    interval: 0
//});

//$(".profile-photo-list li img").click(function () {
//    $(".profile-photo-list li img").removeClass("active");
//    $(this).addClass("active");
//});

//$(".photo-arrow-down").click(function () {
//    if (photoList.length > 4) {
//        photoList = document.querySelectorAll(".profile-photo-list li");

//        photoList[photoList.length - 1].parentNode.appendChild(photoList[0]);
//        photoList[0].style.display = "none";
//        photoList[4].style.display = "block";
//    }
//});

//$(".photo-arrow-up").click(function () {
//    if (photoList.length > 4) {
//        photoList = document.querySelectorAll(".profile-photo-list li");

//        $(photoList[0]).parent().prepend(photoList[photoList.length - 1]);
//        photoList[3].style.display = "none";
//        photoList[photoList.length - 1].style.display = "block";
//    }
//});

//$("#profile-photo-carousel").on("slide.bs.carousel", function (event) {
//    $(".profile-photo-list").find("img.active").removeClass("active");

//    var nextSlide = $(event.relatedTarget).index();

//    var selectorString = "[data-slide-to='" +
//                         nextSlide
//                         + "']";

//    var newActiveSlide = document.querySelector(selectorString);
//    $(newActiveSlide).addClass("active");
//});


//$(".back-to-top").click(function () {
//    $("html, body").animate({ scrollTop: 0 }, "slow");
//    return false;
//});

var menuTopPosition;

if ($(".profile-menu")[0]) {
    menuTopPosition = $(".profile-menu").offset().top;
}

$(".profile-content > ul > li").click(function (event) {
        var index = $(this).index();

        $(this).siblings(".active").removeClass("active");
        $(this).addClass("active");

        //Go to section
        //$(".profile-tab-content").removeClass("active");
        //$(".profile-tab-content").eq(index).addClass("active");
        return false;
});

$(window).scroll(function () {

    var scrollPosition = $(document).scrollTop();
    var isOnTop = scrollPosition >= menuTopPosition - 70;

    if (isOnTop) {
        var newMenuTopPosition = scrollPosition - menuTopPosition + 70;
        $(".profile-menu").css("top", newMenuTopPosition);
        $(".profile-quickinfo-tab").css("top", newMenuTopPosition);
    }
    else {
        $('.profile-menu').css("top", 0);
        $(".profile-quickinfo-tab").css("top", 0);
    }
});


//position pour le always-on-screen ~20px