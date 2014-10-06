$(document).ready(function () {

    var NbStars = 0;
    var SkillsList = ["Débutant", "Initié", "Intermédiaire", "Avancé", "Légendaire"];

    $('.star-rating i').hover(function () {
        NbStars=0;
        $(this).prevAll().removeClass('glyphicon-star-empty').addClass('glyphicon-star');
        $(this).removeClass('glyphicon-star-empty').addClass('glyphicon-star');
        $(this).nextAll().addClass('glyphicon-star-empty').removeClass('glyphicon-star');

        $(this).siblings('i').each(function () {
            if ($(this).hasClass('glyphicon-star')) {
                NbStars = NbStars + 1;
            }
        });
        $(this).parent('.star-rating').attr('data-original-title', SkillsList[NbStars]);

        $(this).parent('.star-rating').tooltip('hide')
        $(this).parent('.star-rating').tooltip('fixTitle')
        $(this).parent('.star-rating').tooltip('show')
    }, function () {
        
    });

    $('.star-rating').hover(function () {
        
    }, function () {
        NbStars = 0;
        $(this).children('i').each(function () {
            $(this).removeClass('glyphicon-star-empty')
            $(this).attr('class', $(this).attr('data-prev-rating-class'));
            if ($(this).hasClass('glyphicon-star')) {
                NbStars = NbStars + 1;
            }
        });
        $(this).attr('data-original-title', SkillsList[NbStars-1]);
    });

    $('.star-rating').click(function () {
        
    }, function () {
        NbStars=0;
        $(this).children('i').each(function () {
            $(this).attr('data-prev-rating-class', $(this).attr('class'));
            if ($(this).hasClass('glyphicon-star')) {
                NbStars=NbStars+1;
            }
        });
        $(this).attr('data-nb-stars', NbStars);
    });
});
