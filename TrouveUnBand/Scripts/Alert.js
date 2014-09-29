$(function () {
    //Close alert
    $('.page-alert .close').click(function (e) {
        e.preventDefault();
        $(this).closest('.page-alert').slideUp();
    });
});