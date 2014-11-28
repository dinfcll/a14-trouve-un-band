var bandMembers = [];

function AddBandMember(json) {
    bandMembers.push(json);
    alert(json["FirstName"]);
};

function RemoveBandMember(json) {
    bandMembers = $.grep(bandMembers, function (value) {
        return value != json;
    });
    alert(bandMembers);
};