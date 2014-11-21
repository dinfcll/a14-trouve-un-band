
    /**
    * This function hides the 'Genres' dropdown list when 'Utilisateur' is selected
    * in the 'Categorie' dropdown list.
    */
    $('#DDLCategories').change(function () {
        var selectedItem = $('#DDLCategories').val()
        if (selectedItem == 'option_user') {
            $('#DDLGenres').hide();
            $('label[for="DDLGenres"]').hide();
        } else {
            $('#DDLGenres').show();
            $('label[for="DDLGenres"]').show();
        }
    });

    /**
    * This function show/hide the advanced search section.
    */
    $('#advanced-search-link').click(function () {
        $('#advanced-search').toggle();
    });
