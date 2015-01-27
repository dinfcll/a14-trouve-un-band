var randomImage = Math.floor((Math.random() * 3) + 1);

$("#home-photo").css("background-image", "url(/Photos/_StockPhotos/Home" + randomImage + ".jpg)");

$(".home-container").parent().removeClass("container");
