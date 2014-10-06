$(document).ready(function () {
    if ($('#DDLCategories option[value=4]')) {
        $('#DDLGenres').prop('disabled', true);
    } else {
        $('#DDLGenres').prop('disabled', false);
    }
});