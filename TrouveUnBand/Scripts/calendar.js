(function ($) {
    "use strict";

    var monthNames = ["JANVIER", "FÉVRIER", "MARS", "AVRIL", "MAI", "JUIN",
                      "JUILLET", "AOÛT", "SEPTEMBRE", "OCTOBRE", "NOVEMBRE", "DÉCEMBRE"];
    var parameters;

    var defaults =
    {
        currentObject: null,
        calendar: null,
        calendarMonth: moment(),
        changeMonthBy: 0,
        daysWithEvent: null
    }

    var filters =
    {
        date: "",
        keyword: ""
    }

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
            parameters.daysWithEvent = $("#DayWithEvent").val();

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
        },

        filterElementByKeyword: function (element) {
            var keywordString = filters.keyword.toUpperCase();
            var elementText = element.text().toUpperCase();

            if (elementText.indexOf(keywordString) > -1) {
                return true;
            }

            return false;
        },

        filterElementByDate: function (element) {
            var elementText = element.find(".day-and-month").text();
            elementText = elementText.slice(0, 2).trim();

            if (elementText === filters.date || filters.date === "") {
                return true;
            }

            return false;
        },

        filterEventView: function() {
            var selectedOption = $("#select-view").find(":selected").val();

            $(".page-display-div").removeClass("active");
            $("#page-section-" + selectedOption).addClass("active");
        },

        filterEvents: function () {
            $("[data-filter=true]").each(function (index, element) {
                if (methods.filterElementByKeyword($(element)) &&
                    methods.filterElementByDate($(element))) {
                    $(element).show();
                } else {
                    $(element).hide();
                }
            });

            methods.filterEventView();
        },

        displayEventsOnChangingMonth: function (callback) {
            $.ajax({
                url: "/Event/ChangeMonthOnCalendar",
                type: "GET",
                data: {
                    month: moment(parameters.calendarMonth).month() + 1,
                    year: moment(parameters.calendarMonth).year()
                },
                error: function () {
                    alert("Une erreur s'est produite.");
                }
            })
            .done(function (partialViewResult) {
                $("#event-display").html(partialViewResult);
                filters.date = "";
                callback();
            });
        },

        refreshCalendar: function () {
            methods.createNewCalendar();
            methods.displayEventsOnChangingMonth(function() {
                methods.printCalendar();
                methods.filterEvents();
            });
        }
    };

    var eventHandlers = {

        dayClick: function () {
            var $this = $(this);

            if ($this.hasClass("active")) {
                $this.removeClass("active");
                filters.date = "";

            } else {
                $("#calendar td").removeClass("active");
                $this.addClass("active");
                filters.date = $this.text();
            }

            methods.filterEvents();
        },

        changeMonthClick: function () {
            parameters.changeMonthBy = 1;

            if ($(this).hasClass("glyphicon-chevron-left")) {
                parameters.changeMonthBy = -1;
            }

            methods.refreshCalendar();
        },

        keyDown: function (event) {
            if($(event.target).is('input') || 
                $("#page-section-calendar").css('display') === 'none') {
                return;
            }

            var keyCode = event.keyCode;

            if (keyCode === 37 || keyCode === 39) {
                parameters.changeMonthBy = keyCode - 38;
                methods.refreshCalendar();
            }
        },

        keyUpForKeyword: function () {
            filters.keyword = $(this).val();
            methods.filterEvents();
        },

        changeViewOption: function () {
            methods.filterEventView();
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

        $("#filter-menu #input-keyword").keyup(eventHandlers.keyUpForKeyword);
        $("#filter-menu #select-view").change(eventHandlers.changeViewOption);

        $(document).keydown(eventHandlers.keyDown);

        return this;
    };

    $(window).on("load.bs.select.data-api", function () {
        $("#calendar").each(function () {
            $(this).buildCalendar();
        });
    });

})(jQuery);

//NOTE IMPORTANTE
//2. arranger le css