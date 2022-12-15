$(function () {
    // Add a color to the product's color.
    // If it's colorful, apply a gradient.
    const productColor = $("#product-color");
    const colorText = productColor.text().toLowerCase();

    if (colorText === "colorful") {
        productColor.css({
            "background": "linear-gradient(to bottom right, #ff98f6, #b5a2ff)",
            "-webkit-background-clip": "text",
            "text-fill-color": "transparent"
        });
    } else if (colorText == "white") {
        productColor.css({
            "color": "white",
            "text-shadow": "-1px -1px 0 #000, 1px -1px 0 #000, -1px 1px 0 #000, 1px 1px 0 #000"
        });
    } else {
        productColor.css({ "color": colorText });
    }

    // Add new lines to product description.
    // Otherwise the text is on one line.
    let productDescription = $(".details-description");
    let newDescription = productDescription.text().replace(/\n/g, '<br />');
    productDescription.html(newDescription);
});