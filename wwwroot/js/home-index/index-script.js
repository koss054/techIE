$(".right-arrow").click(function () {
    $(".right-arrow").css({ "display": "none" });
    $(".left-arrow").css({ "display": "block"});
    $(".hero-first").css({ "display": "none" });
    $(".hero-second").css({ "display": "block" });
});

$(".left-arrow").click(function () {
    $(".left-arrow").css({ "display": "none" });
    $(".right-arrow").css({ "display": "block" });
    $(".hero-second").css({ "display": "none" });
    $(".hero-first").css({ "display": "block" });
});