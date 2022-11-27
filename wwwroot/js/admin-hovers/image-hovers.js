$(function () {
    $(".panel-img.category").hover(function () {
        $("#category-img").toggleClass("grow-img", true, 1500);
        $(".category-text.category").fadeIn(50);
    }, function () {
        $("#category-img").toggleClass("grow-img");
        $(".category-text.category").fadeOut(5);
    });

    $(".panel-img.product").hover(function () {
        $("#product-img").toggleClass("grow-img", true, 1500);
        $(".category-text.product").fadeIn(50);
    }, function () {
        $("#product-img").toggleClass("grow-img");
        $(".category-text.product").fadeOut(5);
    });
});