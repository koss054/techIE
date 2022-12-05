$(function () {
    // Add a color to the product's color.
    // If it's colorful, apply a gradient.
    const productColor = $("#product-color");
    const colorText = productColor.text().toLowerCase();

    if (colorText !== "colorful") {
        $("#product-color").css({ "color": colorText });
    } else {
        productColor.css({
            "background": "linear-gradient(to bottom right, #ff98f6, #b5a2ff)",
            "-webkit-background-clip": "text",
            "text-fill-color": "transparent"
        });
    }

    // Add new lines to product description.
    // Otherwise the text is on one line.
    let productDescription = $(".details-description");
    let newDescription = productDescription.text().replace(/\n/g, '<br />');
    productDescription.html(newDescription);
});