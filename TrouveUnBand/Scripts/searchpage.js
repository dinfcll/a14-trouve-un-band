$(document).ready(function () {
    $(".search-manage-overflow").dotdotdot({
        ellipsis: '... ',
        wrap: 'word',
        fallbackToLetter: true,
        after: null,
        watch: false,
        height: null,
        tolerance: 0,
        lastCharacter: {
            remove: [' ', ',', ';', '.', '!', '?'],
            noEllipsis: []
        }
    });
});
