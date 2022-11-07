$(".right-arrow").click(function () {
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