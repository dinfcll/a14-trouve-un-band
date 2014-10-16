function GetCurrentPageName() { 
    var PageURL = document.location.href; 
    var PageName = PageURL.substring(PageURL.lastIndexOf('/') + 1); 
 
    if (PageName.length<1)
    {
        PageName="index"

    }
    return PageName.toLowerCase() ;
}

$(document).ready(function () {
    var CurrPage = GetCurrentPageName();

    $(".navbar-nav").find(".active").removeClass("active");
    $("#" + CurrPage).addClass("active");
});
