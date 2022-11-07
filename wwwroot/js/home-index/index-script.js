﻿$(".right-arrow").click(function () {
    $(".right-arrow").css({ "display": "none" });
    $(".left-arrow").css({ "display": "flex"});
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
    $(".category-names.phone").fadeIn(300);
}, function () {
    hideCategoryText();
    $("#phone-img").removeClass("grow-category-img");
});

$(".category-index-image.laptop").hover(function () {
    $("#laptop-img").toggleClass("grow-category-img", true, 1000);
    $(".category-names.laptop").fadeIn(300);
}, function () {
    hideCategoryText();
    $("#laptop-img").removeClass("grow-category-img");
});

$(".category-index-image.smart-watch").hover(function () {
    $("#smart-watch-img").toggleClass("grow-category-img", true, 1000);
    $(".category-names.smart-watch").fadeIn(300);
}, function () {
    hideCategoryText();
    $("#smart-watch-img").removeClass("grow-category-img");
});

function hideCategoryText() {
    $(".category-names.phone").hide();
    $(".category-names.laptop").hide();
    $(".category-names.smart-watch").hide();
    console.log("hidden");
}