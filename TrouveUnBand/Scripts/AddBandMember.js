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
        url: '<%=Url.Action("Group", "Create")%>',
        type: 'POST',
        data: { bandJSON: ArrayToComplexJson() },
        success: function(data) {
            return data;
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

function getInfo() {
    var obj = new Object();


    return JSON.stringify(obj);
};

function UpdateModal() {
    var objInfo = getInfo();
    $('#modalName').innerHTML = FormatOutput(objInfo["Name"]);
    $('#modalLocation').innerHTML = FormatOutput(objInfo["Location"]);
    $('#modalDescription').innerHTML = FormatOutput(objInfo["Description"]);

    var objGenres = $('#MultiSelect').val();
    objGenres.each(function(i) {
        $element('#modalGenres').append(FormatOutput(i));
    });

    BandMembers.each(function (i) {
        $element('#modalMusicians').innerHTML(FormatOutput(i["FirstName"]));
        $element('#modalMusicians').innerHTML(FormatOutput(i["LastName"]));

    });

};

function FormatOutput(data) {
    return '@Html.Display("' + data + '")';
};