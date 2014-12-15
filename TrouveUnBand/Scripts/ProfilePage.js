var menuTopPosition;
var profileTabOffset = 70;

if ($("#profile-menu")[0]) {
    menuTopPosition = $("#profile-menu").offset().top;
}

$("#profile-menu ul > li").click(function () {
    var scrollSpy = $(this).attr("data-scroll-spy");

    var tabContent = $("#" + scrollSpy).children(".profile-tab-content");
    tabContent.slideDown("fast", function() {
    });

    $("#" + scrollSpy).find("i").switchClass("glyphicon-plus", "glyphicon-minus");

    $(this).siblings(".active").removeClass("active");
    $(this).addClass("active");

    if (scrollSpy === "information") {
        scrollSpy = "profile-main-tab";
    }

    $('html,body').animate({
        scrollTop: $("#" + scrollSpy).offset().top - profileTabOffset
    },'fast');

    return false;
});

$(window).scroll(function () {

    var scrollPosition = $(document).scrollTop();
    var isOnTop = scrollPosition >= menuTopPosition - profileTabOffset;

    if (isOnTop) {
        var newMenuTopPosition = scrollPosition - menuTopPosition + profileTabOffset;
        $("#profile-menu").css("top", newMenuTopPosition);
        $(".profile-quickinfo-tab").css("top", newMenuTopPosition);
    }
    else {
        $("#profile-menu").css("top", 0);
        $(".profile-quickinfo-tab").css("top", 0);
    }
});

$(".profile-tab-header").click(function () {
    var tabContent = $(this).siblings(".profile-tab-content");
    var chevron = $(this).children("i");

    if (chevron.hasClass("glyphicon-plus")) {
        chevron.removeClass("glyphicon-plus");
        chevron.addClass("glyphicon-minus");
    }
    else {
        chevron.removeClass("glyphicon-minus");
        chevron.addClass("glyphicon-plus");
    }

    tabContent.slideToggle("fast", function() {
    });
});

//Photo section

$("#profile-photo-carousel").carousel({
    interval: 0
});

$(".indicator-list img").click(function () {
    $(".indicator-list img").removeClass("active");
    $(this).addClass("active");
});

$(".profile-photo-rightarrow").click(function () {
    photoList = document.querySelectorAll(".indicator-list li");
    
    if (photoList.length > 5) {
        photoList[photoList.length - 1].parentNode.appendChild(photoList[0]);
        photoList[0].style.display = "none";
        photoList[5].style.display = "inline-block";
    }
});

$(".profile-photo-leftarrow").click(function () {
    photoList = document.querySelectorAll(".indicator-list li");

    if (photoList.length > 5) {
        $(photoList[0]).parent().prepend(photoList[photoList.length - 1]);
        photoList[4].style.display = "none";
        photoList[photoList.length - 1].style.display = "inline-block";
    }
});

$("#profile-photo-carousel").on("slide.bs.carousel", function (event) {
    $(".indicator-list").find("img.active").removeClass("active");

    var nextSlide = $(event.relatedTarget).index();
    var selectorString = "[data-slide-to='" + nextSlide + "']";
    var newActiveSlide = document.querySelector(selectorString);
    $(newActiveSlide).addClass("active");
});

