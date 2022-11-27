$(function () {
    $(".edit-modal").click(function () {
        $(".category-edit").toggleClass("hidden");
        $(".overlay").toggleClass("hidden");
        console.log("edit m");
    });

    $(".overlay").click(function () {
        $(".category-edit").toggleClass("hidden");
        $(".overlay").toggleClass("hidden");
        console.log("edit o");
    });
})