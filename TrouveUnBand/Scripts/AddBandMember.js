var bandMembers = [];
var TrId = 0;

function AddBandMember(json) {
    bandMembers.push(json);
    var div = document.getElementById('band-members');
    $('#band-members').html('<input id="btnRmvMember" type="button" value="Enlever au groupe" onclick="RemoveBandMember()" />');
    TrId++;
    alert(bandMembers);
};

function RemoveBandMember(json) {
    bandMembers = $.grep(bandMembers, function (value) {
        return value != json;
    });
    $("#" + TrId).remove();
};