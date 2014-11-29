var bandMembers = [];
var TrId = 0;

function AddBandMember(json) {
    var testResult = $.grep(bandMembers, function(value) {
        return value != json;
    });
    if (testResult.length === 0) 
    {
        bandMembers.push(json);
        var div = document.getElementById('band-members');
        $('#band-members').append('<input id="' + json["User_ID"] + '" type="button" value="' + json["FirstName"] + '" onclick="RemoveBandMember(' + json["User_ID"] + ')" />');
    }
};

function RemoveBandMember(i) {
    bandMembers = $.grep(bandMembers, function (value) {
        return value.User_ID != i;
    });
    $('#' + i).remove();
};