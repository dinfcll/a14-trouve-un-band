$(document).ready(function () {

    var NbStars = 0;
    var Rating = 0;
    var SkillsList = ["Débutant", "Initié", "Intermédiaire", "Avancé", "Légendaire"];
    var Elements = document.querySelectorAll(".star-rating");

    for (var i = 0; i < Elements.length; i++) {

        Elements[i].setAttribute("data-animation", "false");
        Elements[i].setAttribute("title", SkillsList[1]);
        Elements[i].setAttribute("data-toggle", "tooltip");
        NbStars = Elements[i].getAttribute("data-nb-stars");
        Rating = Elements[i].getAttribute("data-rating");

        for (var j = 0; j < NbStars; j++) {
            if (j < Rating) {
                Elements[i].innerHTML += '<i class="glyphicon glyphicon-star" data-prev-rating-class="glyphicon glyphicon-star"></i>';
            }
            else {
                Elements[i].innerHTML += '<i class="glyphicon glyphicon-star-empty" data-prev-rating-class="glyphicon glyphicon-star-empty"></i>';
            }
        }
    }

    $('.star-rating i').hover(function () {
        Rating = 0;
        $(this).prevAll().removeClass('glyphicon-star-empty').addClass('glyphicon-star');
        $(this).removeClass('glyphicon-star-empty').addClass('glyphicon-star');
        $(this).nextAll().addClass('glyphicon-star-empty').removeClass('glyphicon-star');

        $(this).siblings('i').each(function () {
            if ($(this).hasClass('glyphicon-star')) {
                Rating = Rating + 1;
            }
        });
        $(this).parent('.star-rating').attr('data-original-title', SkillsList[Rating]);

        $(this).parent('.star-rating').tooltip('hide')
        $(this).parent('.star-rating').tooltip('fixTitle')
        $(this).parent('.star-rating').tooltip('show')
    }, function () {
        
    });

    $('.star-rating').hover(function () {
        
    }, function () {
        Rating = 0;
        $(this).children('i').each(function () {
            $(this).removeClass('glyphicon-star-empty')
            $(this).attr('class', $(this).attr('data-prev-rating-class'));
            if ($(this).hasClass('glyphicon-star')) {
                Rating = Rating + 1;
            }
        });
        $(this).attr('data-original-title', SkillsList[Rating - 1]);
    });

    $('.star-rating').click(function () {
        
    }, function () {
        Rating = 0;
        $(this).children('i').each(function () {
            $(this).attr('data-prev-rating-class', $(this).attr('class'));
            if ($(this).hasClass('glyphicon-star')) {
                Rating = Rating + 1;
            }
        });
        $(this).attr('data-rating', Rating);
    });
});
