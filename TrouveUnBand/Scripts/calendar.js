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
    var tHead = '<thead id="calender-head">' + currentMonth + '</thead>';

    return tHead;
}

function createCalendarBody() {
    var tBody = '<tbody>' +
                '<tr><th>DIM</th><th>LUN</th><th>MAR</th><th>MER</th><th>JEU</th><th>VEN</th><th>SAM</th></tr>';



    for (i = 0; i < 6; i++)
    {
       tBody += '<tr><td>1</td><td>2</td><td>3</td><td>4</td><td>5</td><td>6</td><td>7</td></tr>';
    }

    return tBody;
}
