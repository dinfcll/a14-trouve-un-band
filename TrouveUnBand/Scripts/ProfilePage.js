﻿//Menu
var menuTopPosition = $(".profile-tab-menu").offset().top;

$(".profile-tab-menu>div.list-group>a").click(function (event) {
    var index = $(this).index();

    event.preventDefault();

    $(this).siblings(".active").removeClass("active");
    $(this).addClass("active");
    $(".profile-tab-content").removeClass("active");
    $(".profile-tab-content").eq(index).addClass("active");
});

$(window).scroll(function () {
    var scrollPosition = $(document).scrollTop();
    var isOnTop = scrollPosition >= menuTopPosition - 50;

    if (isOnTop) {
        var newMenuTopPosition = scrollPosition - menuTopPosition + 50;
        $(".profile-tab-menu").css("top", newMenuTopPosition);
        $(".back-to-top").fadeIn();
    }
    else {
        $('.profile-tab-menu').css("top", 0);
        $(".back-to-top").fadeOut();
    }

});

//Tab description/

$(".profile-info-row").click(function () {
    var chevron = $(this).children("i");
    var panelId = $(this).attr("data-panel-id");
    var panel = $("#" + panelId);

    panel.slideToggle(400, function () {
        if (panel.is(":visible")) {
            chevron.removeClass("glyphicon-chevron-down");
            chevron.addClass("glyphicon-chevron-up");
        }
        else {
            chevron.removeClass("glyphicon-chevron-up");
            chevron.addClass("glyphicon-chevron-down");
        }
    })
});

//Tab Pictures

$("#profile-photo-carousel").carousel({
    interval: 0
});

$(".profile-photo-list li").click(function () {
    $(".profile-photo-list li").removeClass("active");
    $(this).addClass("active");
});

$("#profile-photo-carousel").on("slide.bs.carousel", function (event) {
    $(".profile-photo-list").find("li.active").removeClass("active");

    var nextSlide = $(event.relatedTarget).index();

    var selectorString = "[data-slide-to='" +
                         nextSlide
                         + "']";

    var newActiveSlide = document.querySelector(selectorString);
    $(newActiveSlide).addClass("active");

    var divPos = $(".profile-photo-list").offset();

    $(".profile-photo-list").animate({
        scrollTop: $(newActiveSlide).offset().top - divPos.top
    }, 1000);
});

$(".back-to-top").click(function() {
     $("html, body").animate({ scrollTop: 0 }, "slow");
    return false;
});
