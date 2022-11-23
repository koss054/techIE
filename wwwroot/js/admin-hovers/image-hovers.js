$(function () {
    $(".panel-img.category").hover(function () {
        $("#category-img").toggleClass("grow-img", true, 1000);
        $(".category-text.category").fadeIn(300);
    }, function () {
        hideText();
        $("#category-img").removeClass("grow-img");
    });

    $(".panel-img.product").hover(function () {
        $("#product-img").toggleClass("grow-img", true, 1000);
        $(".category-text.product").fadeIn(300);
    }, function () {
        hideText();
        $("#product-img").removeClass("grow-img");
    });

    function hideText() {
        $(".category-text").hide();
    }
});