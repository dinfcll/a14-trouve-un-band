(function ($) {
    "use strict";

    var monthNames = ["JANVIER", "FÉVRIER", "MARS", "AVRIL", "MAI", "JUIN",
                      "JUILLET", "AOÛT", "SEPTEMBRE", "OCTOBRE", "NOVEMBRE", "DÉCEMBRE"];

    var defaults =
    {
        currentObject: null,
        calendar: null,
        calendarMonth: moment(),
        changeMonthBy: 0,
        daysWithEvent: null
    }

    var parameters;

    var methods = {

        createNewCalendar: function () {
            methods.setCalendarMonth();

            var calendar = methods.createCalendarHead(parameters.calendarMonth);
            calendar += methods.createCalendarBody(parameters.calendarMonth);

            parameters.calendar = calendar;
        },

        createCalendarHead: function (calendarMonth) {

            var month = monthNames[moment(calendarMonth).month()];
            var year = moment(calendarMonth).year();

            var tHead = '<table class="col-md-12">'
                        + '<div id="calendar-head" class="col-md-12">'
                        + '<span class="glyphicon glyphicon-chevron-left col-md-1 col-md-offset-2"></span>'
                        + '<span id="calendar-title" class="col-md-6">' + month + ', ' + year + '</span>'
                        + '<span class="glyphicon glyphicon-chevron-right col-md-1"></span>'
                        + '</div>';

            return tHead;
        },

        createCalendarBody: function (calendarMonth) {
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
        },

        setCalendarMonth: function () {
            var newMonth = moment(parameters.calendarMonth)
                .add(parameters.changeMonthBy, 'months');

            parameters.calendarMonth = newMonth;
        },

        printCalendar: function () {
            parameters.currentObject.html(parameters.calendar);
            methods.setTodayClass(parameters.calendarMonth);
            methods.setDaysWithEventClass();
        },

        setTodayClass: function (calendarMonth) {
            if (moment().isSame(calendarMonth, 'month')) {
                $("tr td:not(.outside-month)").filter(function () {
                    return $(this).text() == moment().date();
                }).addClass("today");
            }
        },

        setDaysWithEventClass: function () {
            if (parameters.daysWithEvent != null) {
                var daysWithEventArray = parameters.daysWithEvent.split("-");
                var i;

                for (i = 0; i < daysWithEventArray.length; ++i) {
                    var day = daysWithEventArray[i];

                    $("tr td:not(.outside-month)").filter(function () {
                        return $(this).text() === day;
                    }).addClass("dayWithEvent");
                }
            }
        }

    };

    var eventHandlers = {

        dayClick: function () {
            var $this = $(this);

            if ($this.hasClass("active")) {
                $this.removeClass("active");
                return;
            }

            $("#calendar td").removeClass("active");
            $this.addClass("active");
        },

        changeMonthClick: function () {
            var changeMonthBy = 1;

            if ($(this).hasClass("glyphicon-chevron-left")) {
                changeMonthBy = -1;
            }

            parameters.changeMonthBy = changeMonthBy;
            methods.createNewCalendar();
            methods.printCalendar();
        },

        keyDown: function (event) {
            if ($(event.target).is('input')) {
                return;
            }

            var keyCode = event.keyCode;

            if (keyCode === 37 || keyCode === 39) {
                parameters.changeMonthBy = keyCode - 38;
                methods.createNewCalendar();
                methods.printCalendar();
            }
        }
    };

    $.fn.buildCalendar = function (options) {
        parameters = $.extend(defaults, options);
        parameters.currentObject = this;

        methods.createNewCalendar();
        methods.printCalendar();

        $("#calendar")
            .on("click", "tr:not(#first-row) td:not(.outside-month)", eventHandlers.dayClick)
            .on("click", "#calendar-head .glyphicon", eventHandlers.changeMonthClick);

        $(document).keydown(eventHandlers.keyDown);
    };

})(jQuery);

//TODO
// Faire en sorte d'éviter les conflits sur un changement d'onglet par exemple
//Garder dernier mois vue vs le  mois courant