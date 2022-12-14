$(function () {
    var pass = document.getElementById("pass");

    $(".show-pass-div").click(function () {
        $(".show-pass-div").css({ "display": "none" });
        $(".hide-pass-div").css({ "display": "flex" });
        pass.type = "text";
    });

    $(".hide-pass-div").click(function () {
        $(".hide-pass-div").css({ "display": "none" });
        $(".show-pass-div").css({ "display": "flex" });
        pass.type = "password";
    });
})

