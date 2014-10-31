function AddTable(row, cellid) {
    var cell = row.insertCell(0);
    var instcell = document.getElementById(cellid);
    cell.innerHTML = instcell.outerHTML;
    cell.className = document.getElementById("tablecell").className.toString();
}

function AddTableX(row, cellid, randomname) {
    var cell = row.insertCell(0);
    var newX = "<a id=\"Xfermer\" href=\"#\" onclick=\"DeleteInstrument('" + randomname + "')\">X</a>";
    cell.innerHTML = newX;
    cell.className = document.getElementById("tablecell").className.toString();
}

function AddInstrument() {
    var table = document.getElementById("InstrumentTable");
    var row = table.insertRow();
    row.id = "tablerow";
    row.className = document.getElementById("tablerow").className.toString();
    var randomname = makeid();
    row.setAttribute("name", randomname);
    AddTableX(row, "Xfermer", randomname);
    AddTable(row, "SkillsList");
    AddTable(row, "InstrumentList");
}

function DeleteInstrument(name) {
    var table = document.getElementById("InstrumentTable");
    var row = document.getElementsByName(name).item(0);
    table.deleteRow(row.rowIndex);
}

function makeid() {
    var text = "";
    var possible = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

    for (var i = 0; i < 5; i++)
        text += possible.charAt(Math.floor(Math.random() * possible.length));

    return text;
}

function annuler() {
    window.location.replace("/Home")
}

function hovertooltip() {
    $('#skillquestion').tooltip('show');
}

function outtooltip() {
    $('#skillquestion').tooltip('destroy');
}

$(function () {
    $('#datepicker').datetimepicker({
        dateFormat: "yy-mm-dd"
    });
});

$(function () {
    $('#datepickernotime').datepicker({
        dateFormat: "yy-mm-dd",
        changeYear: true,
        yearRange: '-90:+0'
    });
});


$(function () {
    $('#datepickernotime2').datepicker({
        dateFormat: "yy-mm-dd",
        changeYear: true,
        yearRange: '-90:+0'
    });
});

