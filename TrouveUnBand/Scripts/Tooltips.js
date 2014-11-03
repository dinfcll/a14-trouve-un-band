$('[data-toggle="tooltip"]').hover(function () {
    $(this).tooltip('show');
}, function () {

});

$('[data-toggle="tooltip"]').hover(function () {

}, function () {
    $(this).tooltip('destroy');
});
