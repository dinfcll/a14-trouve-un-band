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
    var tHead = '<div id="calender-head">'
                + '<span class="glyphicon glyphicon-chevron-left"></span>'
                + '<span>' + currentMonth + ', ' + moment().year() + '</span>'
                + '<span class="glyphicon glyphicon-chevron-right"></span>'
                + '</div>';

    return tHead;
}

function createCalendarBody() {
    var tBody = '<tbody>' +
                '<tr id="first-row"><td>DIM</td><td>LUN</td><td>MAR</td><td>MER</td><td>JEU</td><td>VEN</td><td>SAM</td></td>';

    var debuteDate = moment().date(1);
    var isSameMonth = moment().isSame(debuteDate, 'month');
    var i = 0;

    while (isSameMonth === true) {

        tBody += '<tr>';

        while (i < 7 && isSameMonth === true) {
            tBody += '<td>' + debuteDate.date() + '</td>';
            debuteDate.add(1, 'days');
            isSameMonth = moment().isSame(debuteDate, 'month');
            i++;
        }

        tBody += '</tr>';
        i = 0;
    }

    tBody += '</tbody></table>';

    return tBody;
}
