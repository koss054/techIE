$(function () {
    $(".edit-modal").click(function () {
        $(".category-edit").toggleClass("hidden");
        $(".overlay").toggleClass("hidden");
    });

    $(".overlay").click(function () {
        $(".category-edit").toggleClass("hidden");
        $(".overlay").toggleClass("hidden");
    });
})