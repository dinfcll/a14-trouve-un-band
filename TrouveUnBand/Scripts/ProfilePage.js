var menuTopPosition;
var profileTabOffset = 70;

if ($("#profile-menu")[0]) {
    menuTopPosition = $("#profile-menu").offset().top;
}

$("#profile-menu ul > li").click(function (event) {
    var scrollSpy = $(this).attr("data-scroll-spy");

    $(this).siblings(".active").removeClass("active");
    $(this).addClass("active");
       
    $('html,body').animate({
        scrollTop: $("#" + scrollSpy).offset().top - profileTabOffset
    },
        'slow');

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

    if (chevron.hasClass("glyphicon-chevron-left")) {
        chevron.removeClass("glyphicon-chevron-left");
        chevron.addClass("glyphicon-chevron-down");
    }
    else {
        chevron.removeClass("glyphicon-chevron-down");
        chevron.addClass("glyphicon-chevron-left");
    }

    tabContent.slideToggle(250, function () {
    })
});

//Photo section

$("#profile-photo-carousel").carousel({
    interval: 0
});

$(".indicator-list img").click(function () {
    $(".indicator-list img").removeClass("active");
    $(this).addClass("active");
});
