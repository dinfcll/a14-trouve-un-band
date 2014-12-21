var monthNames = ["JANVIER", "FÉVRIER", "MARS", "AVRIL", "MAI", "JUIN",
    "JUILLET", "AOÛT", "SEPTEMBRE", "OCTOBRE", "NOVEMBRE", "DÉCEMBRE"];

$.fn.buildCalendar = function (options) {
    var calendar = '<table class="col-md-12">';
    calendar += createCalendarHead();
    calendar += createCalendarBody();
    this.html(calendar);
}

function createCalendarHead() {
    var currentMonth = monthNames[moment().month()];
    var tHead = '<div id="calender-head">' + currentMonth + ', ' +moment().year() + '</div>';

    return tHead;
}

function createCalendarBody() {
    var tBody = '<tbody>' +
                '<tr id="first-row"><td>DIM</td><td>LUN</td><td>MAR</td><td>MER</td><td>JEU</td><td>VEN</td><td>SAM</td></td>';

    var firstDayOfMonth = moment().date(1).day();
    var debuteDate = moment().date(1).subtract(firstDayOfMonth, 'days');

    for (i = 0; i < 6; i++)
    {
        tBody += '<tr>';
        for (j = 0; j < 7; j++) {
            tBody += '<td>' + debuteDate.date() + '</td>';
            debuteDate.add(1, 'days');
        }
        tBody += '</tr>';
    }

    tBody += '</tbody></table>';

    return tBody;
}
