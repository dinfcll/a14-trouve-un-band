(function ($) {
    var monthNames = ["JANVIER", "FÉVRIER", "MARS", "AVRIL", "MAI", "JUIN",
            "JUILLET", "AOÛT", "SEPTEMBRE", "OCTOBRE", "NOVEMBRE", "DÉCEMBRE"];

    function createCalendarHead(calendarMonth) {

        var month = monthNames[moment(calendarMonth).month()];
        var year = moment(calendarMonth).year();

        var tHead = '<div id="calendar-head" class="col-md-12">'
                    + '<span class="glyphicon glyphicon-chevron-left col-md-1 col-md-offset-2"></span>'
                    + '<span id="calendar-title" class="col-md-6">' + month + ', ' + year + '</span>'
                    + '<span class="glyphicon glyphicon-chevron-right col-md-1"></span>'
                    + '</div>';

        return tHead;
    }

    function createCalendarBody(calendarMonth) {
        var tBody = '<tbody>' +
                    '<tr id="first-row">' +
                    '<td>DIM</td><td>LUN</td>' +
                    '<td>MAR</td><td>MER</td>' +
                    '<td>JEU</td><td>VEN</td><td>SAM</td></td>';

        var daysBeforeFirstDay = moment(calendarMonth).date(1).day();
        var dayIndex = moment(calendarMonth).date(1).subtract(daysBeforeFirstDay, 'days');;
        var isSameMonth;
        var i = 0;
        var j = 0;

        while (i < 6) {

            tBody += '<tr>';

            while (j < 7) {
                isSameMonth = moment(calendarMonth).isSame(dayIndex, 'month');

                if (isSameMonth === true) {
                    tBody += '<td>' + dayIndex.date() + '</td>';
                } else {
                    tBody += '<td class="outside-month">' + dayIndex.date() + '</td>';
                }

                dayIndex.add(1, 'days');
                j++;
            }

            tBody += '</tr>';
            i++;
            j = 0;
        }

        tBody += '</tbody></table>';

        return tBody;
    }

    function setTodayClass(calendarMonth) {
        if (moment().isSame(calendarMonth, 'month')) {
            $("tr td:contains('" + moment().date() + "')").addClass("today");
        }
    }

    function changeMonth(nextMonthPosition) {
        var calendarTitle = $("#calendar-title").text().split(",");

        var currentCalendarMonth = $.trim(calendarTitle[0]);
        currentCalendarMonth = monthNames.indexOf(currentCalendarMonth);

        var currentCalendarYear = $.trim(calendarTitle[1]);

        $("#calendar").buildCalendar({
            month: currentCalendarMonth,
            year: currentCalendarYear,
            changeMonthBy: nextMonthPosition
        });
    }

    $("#calendar").on("click", "tr:not(#first-row) td:not(.outside-month)", function () {
        $("#calendar td").removeClass("active");
        $(this).addClass("active");
    });

    $("#calendar").on("click", "#calendar-head .glyphicon", function () {
        if ($(this).hasClass("glyphicon-chevron-left")) {
            changeMonth(-1);
        } else {
            changeMonth(1);
        }
    });

    $.fn.buildCalendar = function (options) {

        var defaults =
        {
            month: moment().month(),
            year: moment().year(),
            changeMonthBy: 0
        }

        var parameters = $.extend(defaults, options);

        var calendarMonth = moment()
            .set('month', parameters.month)
            .set('year', parameters.year)
            .add(parameters.changeMonthBy, 'months');

        var calendar = '<table class="col-md-12">';
        calendar += createCalendarHead(calendarMonth);
        calendar += createCalendarBody(calendarMonth);
        this.html(calendar);
        setTodayClass(calendarMonth);
    };

})(jQuery);
