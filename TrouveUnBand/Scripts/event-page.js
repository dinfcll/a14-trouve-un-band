$("#grid-selector button").click(function() {
    $("#grid-selector button").removeClass("active");
    $(this).addClass("active");

    var divId = $(this).attr("data-div-id");
    $(".page-display-div").removeClass("active");
    $("#" + divId).addClass("active");

    if (divId.contains("calendar")) {
        $("#calendar").fullCalendar({
        });
    }
});