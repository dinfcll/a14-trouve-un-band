function PlaySomeSound(genre) {
    console.log("bonjour");
    SC.get('/tracks',
    {
        genres: genre,
        bpm:
        {
            from: 100
        }
    }, function(tracks) {
        var random = Math.floor(Math.random() * 49);
        console.log(random);
        console.log(tracks[random]);
        console.log(tracks[random].uri);
        SC.oEmbed(tracks[random].uri, {
            auto_play: true
        }, document.getElementById('target'));
    });
}

window.onload = function() {
    SC.initialize({
        client_id: '96fcd16e1df481197be64f94a6d0f35a'
    });

    var menuLinks = document.getElementsByClassName('genre');

    for (var i = 0; i < menuLinks.length; i++) {
        console.log(menuLinks[i]);
        var menuLink = menuLinks[i];
        menuLinks[i].onclick = function(e) {
            e.preventDefault();
            PlaySomeSound(menuLink.innerHTML);
            console.log(menuLink.innerHTML);
        };
    }
};