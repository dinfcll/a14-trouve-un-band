SC.initialize({
    client_id: "96fcd16e1df481197be64f94a6d0f35a",
    redirect_uri: "http://localhost:13035/SocialMedia/soundcloud-callback.html"
});

  
$("#connect").live("click", function(){
    SC.connect(function(){
      SC.get("/me", function(me){
        $("#username").text(me.username);
        $("#description").val(me.description);
      });
    });
  });