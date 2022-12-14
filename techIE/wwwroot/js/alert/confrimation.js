$(function () {
    $('[data-confirm]').click(function (e) {
        if (!confirm(jQuery(this).attr("data-confirm"))) {
            e.preventDefault();
        }
    });
})