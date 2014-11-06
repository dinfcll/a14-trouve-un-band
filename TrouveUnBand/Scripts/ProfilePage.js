//Menu
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
var photoList = document.querySelectorAll(".profile-photo-list li");
var photoListIndex = 0;

if (photoList.length > 4) {
    for(var i=4;i<photoList.length;i++)
    {
        photoList[i].style.display = "none";
    }
}

$("#profile-photo-carousel").carousel({
    interval: 0
});

$(".profile-photo-list li img").click(function () {
    $(".profile-photo-list li img").removeClass("active");
    $(this).addClass("active");
});

$(".photo-arrow-down").click(function () {
    if (photoList.length > 4 && photoListIndex + 4 < photoList.length) {
        
            photoList[photoListIndex].style.display = "none";
            photoList[photoListIndex + 4].style.display = "block";
            photoListIndex++;
    }
});

$(".photo-arrow-up").click(function () {
    if (photoList.length > 4 && photoListIndex > 0) {

        photoList[photoListIndex+3].style.display = "none";
        photoList[photoListIndex - 1].style.display = "block";
        photoListIndex--;
    }
});

function setVisiblePhotoList() {

    for (var i = 0; i < 4; i++) {
        visiblePhotoList[i] = photoList[i + photoListIndex];
    }

}


$(".back-to-top").click(function () {
    $("html, body").animate({ scrollTop: 0 }, "slow");
    return false;
});
