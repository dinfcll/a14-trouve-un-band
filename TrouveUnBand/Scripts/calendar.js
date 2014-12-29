(function ($) {
    var monthNames = ["JANVIER", "FÉVRIER", "MARS", "AVRIL", "MAI", "JUIN",
            "JUILLET", "AOÛT", "SEPTEMBRE", "OCTOBRE", "NOVEMBRE", "DÉCEMBRE"];

    function createCalendarHead(calendarMonth) {

        var month = monthNames[moment(calendarMonth).month()];
        var year = moment(calendarMonth).year();

        var tHead = '<div id="calendar-head" class="col-md-12">'
                    + '<span class="glyphicon glyphicon-chevron-left col-md-1"></span>'
                    + '<span id="calendar-title" class="col-md-5">' + month + ', ' + year + '</span>'
                    + '<span class="glyphicon glyphicon-chevron-right col-md-1"></span>'
                    + '</div>';

        return tHead;
    }

    function createCalendarBody(calendarMonth) {
        var tBody = '<tbody>' +
                    '<tr id="first-row"><td>DIM</td><td>LUN</td><td>MAR</td><td>MER</td><td>JEU</td><td>VEN</td><td>SAM</td></td>';

        var debuteDate = moment(calendarMonth).date(1);
        var isSameMonth = moment(calendarMonth).isSame(debuteDate, 'month');
        var i = 0;

        while (isSameMonth === true) {

            tBody += '<tr>';

            while (i < 7 && isSameMonth === true) {
                tBody += '<td>' + debuteDate.date() + '</td>';
                debuteDate.add(1, 'days');
                isSameMonth = moment(calendarMonth).isSame(debuteDate, 'month');
                i++;
            }

            tBody += '</tr>';
            i = 0;
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

    $("#calendar").on("click", "tr:not(#first-row) td", function () {
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