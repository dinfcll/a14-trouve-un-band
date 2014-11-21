$('[data-toggle="tooltip"]').hover(function () {
    $(this).tooltip('toggle');
});

$('[data-toggle="popover"]').popover({
    trigger: 'focus'
});