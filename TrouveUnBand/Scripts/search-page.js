$(document).ready(function () {
    $('#advanced-filter').hide();

    /**
    * This function hides the 'Genres' dropdown list when 'Utilisateur' is selected
    * in the 'Categorie' dropdown list.
    */
    $('#ddlCategories').change(function () {
        var selectedItem = $('#ddlCategories').val()
        if (selectedItem == 'option_user') {
            $('#ddlGenres').hide();
            $('label[for="ddlGenres"]').hide();
        } else {
            $('#ddlGenres').show();
            $('label[for="ddlGenres"]').show();
        }
    });

    /**
    * This function show/hide the advanced search section.
    */
    $('#toggle-filter').click(function () {
        var text = $(this).text();
        if (text == 'Filtre avancée') {
            $(this).text('Filtre simple');
        }
        else {
            $(this).text('Filtre avancée');
        }
        $('#basic-filter').toggle();
        $('#advanced-filter').toggle();
    });

    $('li.searchfilter').on('click', function () {
        var genre = $(this).children('span').text();
        var label = '<li id="tags" class="label label-default">' + genre + '<span class="glyphicon-remove"></span></li>';
        $('#search-parameters').append(label);
    });

    $('#search-parameters').on('click', function () {
        alert("yo");
    });
});
