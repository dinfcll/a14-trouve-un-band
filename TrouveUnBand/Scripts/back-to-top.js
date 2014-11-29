$(document).ready(function () {
    $("body").append('<a id="back-to-top" href="#" class="btn back-to-top" role="button"'+
                        'title="Retour en haut de la page." data-toggle="tooltip" data-placement="left">'+
                        '<span class="glyphicon glyphicon-chevron-up"></span>' +
                     '</a>');

    var backToTopBtn = document.getElementById("back-to-top");
    backToTopBtn.style.display = "none";

    $(window).scroll(function () {
        if ($(this).scrollTop() > 100) {
            $('#back-to-top').fadeIn();
        } else {
            $('#back-to-top').fadeOut();
        }
    });

    $(".back-to-top").click(function () {
        $("html, body").animate({ scrollTop: 0 }, 800);
        $("#back-to-top").tooltip("hide");
        return false;
    });

    $("#back-to-top").tooltip("show");
});