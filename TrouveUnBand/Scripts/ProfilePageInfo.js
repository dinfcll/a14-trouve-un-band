
$(".profile-info-panel").hide();

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
