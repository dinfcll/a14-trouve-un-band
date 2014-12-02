var BandMembers = [];
var TrId = 0;

function AddBandMember(json) {

    if (!containsObject(json)) 
    {
        BandMembers.push(json);
        var div = document.getElementById('band-members');
        $('#band-members').append('<input id="' + json["User_ID"] + '" type="button" value="' + json["FirstName"] + '" onclick="RemoveBandMember(' + json["User_ID"] + ')" />');
    }
};

function RemoveBandMember(i) {
    BandMembers = $.grep(BandMembers, function (value) {
        return value.User_ID != i;
    });
    $('#' + i).remove();
};

function containsObject(obj) {
    var i;
    for (i = 0; i < BandMembers.length; i++) {
        if (BandMembers[i]["User_ID"] == obj["User_ID"]) {
            return true;
        }
    }
    return false;
}

function SendData() {

    $.ajax({
        url: 'Create',
        type: 'POST',
        data: { bandJSON: ArrayToComplexJson() },
        success: function (data) {
            $('#ConfirmForm').html(data);
        },
        error: function(request, error) {
            console.log(arguments);
        }
    });
};

function ArrayToComplexJson() {
    var obj = new Object();
    obj.BandMembers = BandMembers;
    obj.Genres = $('#MultiSelect').val();
    obj.Name = $('#BandName').val();
    obj.Location = $('#BandLocation').val();
    obj.Description = $('#BandDescription').val();

    return JSON.stringify(obj);
};

$('#btnTerminer').click(function () {
        $("#ConfirmForm").slideDown("slow");
        $("#MainForm").hide();
        SendData();
});

function Cancel() {
        $("#MainForm").slideDown("slow");
        $("#ConfirmForm").hide();
};

