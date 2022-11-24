$(function () {
    $(".right-arrow").click(function () {
        $(".right-arrow").css({ "display": "none" });
        $(".left-arrow").css({ "display": "flex" });
        $(".hero-first").css({ "display": "none" });
        $(".hero-second").css({ "display": "flex" });
    });

    $(".left-arrow").click(function () {
        $(".left-arrow").css({ "display": "none" });
        $(".right-arrow").css({ "display": "flex" });
        $(".hero-second").css({ "display": "none" });
        $(".hero-first").css({ "display": "flex" });
    });

    $(".category-index-image.phone").hover(function () {
        $("#phone-img").toggleClass("grow-category-img", true, 1000);
        $(".category-names.phone").fadeIn(50);
    }, function () {
        $("#phone-img").toggleClass("grow-category-img");
        $(".category-names.phone").fadeOut(5);
    });

    $(".category-index-image.laptop").hover(function () {
        $("#laptop-img").toggleClass("grow-category-img", true, 1000);
        $(".category-names.laptop").fadeIn(50);
    }, function () {
        $("#laptop-img").toggleClass("grow-category-img");
        $(".category-names.laptop").fadeOut(5);
    });

    $(".category-index-image.smart-watch").hover(function () {
        $("#smart-watch-img").toggleClass("grow-category-img", true, 1000);
        $(".category-names.smart-watch").fadeIn(50);
    }, function () {
        $("#smart-watch-img").toggleClass("grow-category-img");
        $(".category-names.smart-watch").fadeOut(5);
    });

    $(".marketplace-index-image.explore").hover(function () {
        $("#explore-img").toggleClass("grow-category-img", true, 1000);
        $(".marketplace-names.explore").fadeIn(50);
    }, function () {
        $("#explore-img").toggleClass("grow-category-img");
        $(".marketplace-names.explore").fadeOut(5);
    });

    $(".marketplace-index-image.create").hover(function () {
        $("#create-img").toggleClass("grow-category-img", true, 1000);
        $(".marketplace-names.create").fadeIn(50);
    }, function () {
        $("#create-img").toggleClass("grow-category-img");
        $(".marketplace-names.create").fadeOut(5);
    });
});