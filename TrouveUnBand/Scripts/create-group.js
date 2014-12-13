﻿$(document).ready(function () {

    var band = new Object();

    $('#btnTerminer').click(function () {
        band.name = $('#Name').val();
        band.location = $('#Location').val();
        band.description = $('#Description').val();
        band.members = getMembers();
        band.genres = $('#MultiSelect').val();

        fillInfos(band);
        fillGenres(band);
        fillMembers(band);
        $('#openModal').click();
    })

    $('#btnConfirmation').click(function () {
        var json = JSON.stringify(band);

        $.ajax({
            url: "Create",
            type: "POST",
            data: { json: json },
            error: function () {
                alert("Error while sending data to server");
            },
            success: function (data) {
                window.location.pathname = "/group";
            }
        });
    })

    $('#btnConfirmationBack').click(function () {
        $('#confirmation-body').html("");
    })

    function getMembers() {
        var membersList = new Array();
        $('#band-members tr').each(function () {
            var member = new Object();
            if ($(this).find(".hidden").html() !== undefined) {
                member.id = $(this).find(".hidden").html();
                member.name = $(this).find(".name-cell").html();
                member.location = $(this).find(".location-cell").html();
                membersList.push(member);
            }
        });

        return membersList;
    }

    function fillInfos(band) {
        html = "";
        html += "<label class='col-md-6'>Nom du groupe</label>";
        html += "<span class='col-md-6'>" + band.name + "</span>";
        html += "<label class='col-md-6'>Ville</label>";
        html += "<span class='col-md-6'>" + band.location + "</span>";
        html += "<label class='col-md-12'>Description</label>";
        html += "<span class='col-md-12'>" + band.description + "</span>";
        $('#confirmation-body').append(html);
    }

    function fillGenres(band) {
        html = "<table class='table'><tr><th>Genres</th></tr>";
        for (var i = 0; i < band.genres.length; i++)
        {
            html += "<tr><td>" + band.genres[i] + "</td></tr>";
        }
        html += "</table>";
        $('#confirmation-body').append(html);
    }

    function fillMembers(band) {
        html = "<table class='table'><tr><th>Nom du membre</th><th>Ville</th></tr>";
        for (var i = 0; i < band.members.length; i++) {
            html += "<tr><td class='hidden'>" + band.members[i]["id"] + "</td>";
            html += "<td>" + band.members[i]["name"] + "</td>";
            html += "<td>" + band.members[i]["location"] + "</td></tr>";
        }
        html += "</table>";
        $('#confirmation-body').append(html);
    }
})
