var bandMembers = [];
var TrId = 0;

function AddBandMember(json) {
    bandMembers.push(json);
    var div = document.getElementById('band-members');
    $('#band-members').append('<input id="'+ json["User_ID"] +'" type="button" value="' + json["FirstName"] + '" onclick="RemoveBandMember(' + json["User_ID"] + ')" />');
    TrId++;
    alert(bandMembers);
};

function RemoveBandMember(i) {
    bandMembers = $.grep(bandMembers, function (value) {
        return value.User_ID != i;
    });
    $('#' + i).remove();
};