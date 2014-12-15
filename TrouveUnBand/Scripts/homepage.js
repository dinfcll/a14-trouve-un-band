var randomImage = Math.floor((Math.random() * 3) + 1);

$("#homeImage").attr("src", "/Photos/_StockPhotos/Home" + randomImage + ".jpg");

var tt = $(".home-container").parent().removeClass("container");
