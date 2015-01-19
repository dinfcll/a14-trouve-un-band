$("#show-filter-btn").click(function() {
    $("#page-section-calendar").slideToggle("fast");
});

$("#filter-menu #select-view").change(function () {
    var selectedOption = $(this).find(":selected").val();

    $(".page-display-div").removeClass("active");
    $("#page-section-" + selectedOption).addClass("active");
});

$("#filter-menu #input-keyword").keyup(function () {
    var keywordString = $(this).val();

    $(".event-panel").each(function (index, element) {
        filterElementByKeyword($(element), keywordString);
    });

    $(".event-thumbnail").each(function (index, element) {
        filterElementByKeyword($(element), keywordString);
    });
});

function filterElementByKeyword(element, keywordString) {
    keywordString = keywordString.toUpperCase();
    var elementText = element.text().toUpperCase();

    if (elementText.indexOf(keywordString) > -1) {
        element.show();
    } else {
        element.hide();
    }
}

$("#calendar").on("click", "tr:not(#first-row) td:not(.outside-month)", function() {
    var dayOfMonth = $(this).text();

    $(".event-panel").each(function (index, element) {
        filterElementByDate($(element), dayOfMonth);
    });

    $(".event-thumbnail").each(function (index, element) {
        filterElementByDate($(element), dayOfMonth);
    });
});

function filterElementByDate(element, dayOfMonth) {
    var elementText = element.find(".day-and-month").text();
    elementText = elementText.slice(0, 2).trim();

    if (elementText === dayOfMonth) {
        element.show();
    } else {
        element.hide();
    }
}