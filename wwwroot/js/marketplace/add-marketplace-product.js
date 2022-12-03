$(function () {
    let imgUrl = $(".img-url");
    let productImg = $(".product-image");
    let category = $("#product-category");

    imgUrl.change(function () {
        let url = imgUrl.val();

        if (url != "") {
            checkImage(productImg, url);
        } else {
            setCategoryImage(productImg, url);
        }
    })

    category.change(function () {
        setCategoryImage(productImg, imgUrl.val());
        console.log(category.val());
    })

    function checkImage(productImg, url) {
        var request = new XMLHttpRequest();

        request.open("GET", url, true);
        request.send();
        request.onload = function () {
            status = request.status;
            if (request.status == 200) {
                productImg.attr("src", url.toString());
            } else {
                setCategoryImage(productImg, url);
            }
        }
    }

    function setCategoryImage(img, url) {
        if (url == "") {
            if (category.val() == "Phones") {
                img.attr("src", "https://i.imgur.com/MBPYVWU.jpeg");
            } else if (category.val() == "Laptops") {
                img.attr("src", "https://i.imgur.com/g0mVLnw.jpeg");
            } else if (category.val() == "Smart Watches") {
                img.attr("src", "https://i.imgur.com/L0UbSPc.jpeg");
            } else {
                img.attr("src", "https://i.imgur.com/DmgfDVd.jpeg");
            }
        }
    }
})