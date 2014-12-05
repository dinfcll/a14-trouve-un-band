$(document).ready(function () {
    var currentPage = window.location.pathname;

    $("[data-path='"+currentPage+"']").addClass("active");
});