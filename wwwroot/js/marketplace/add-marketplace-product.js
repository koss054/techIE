$(function () {
    const phonesUrl = "https://i.imgur.com/MBPYVWU.jpeg";
    const laptopsUrl = "https://i.imgur.com/g0mVLnw.jpeg";
    const smartWatchesUrl = "https://i.imgur.com/L0UbSPc.jpeg";
    const customUrl = "https://i.imgur.com/DmgfDVd.jpeg";

    let imgUrl = $(".img-url");
    let productImg = $(".product-image");
    let category = $("#product-category");

    imgUrl.val(phonesUrl);

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
        console.log(category.children(":selected").attr("id"));
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
        let selectedCategory = category.children(":selected").attr("id");

        if (selectedCategory == "Phones") {
            img.attr("src", phonesUrl);
            setUrlOnCategoryChange(phonesUrl);
        } else if (selectedCategory == "Laptops") {
            img.attr("src", laptopsUrl);
            setUrlOnCategoryChange(laptopsUrl);
        } else if (selectedCategory == "Smart Watches") {
            img.attr("src", smartWatchesUrl);
            setUrlOnCategoryChange(smartWatchesUrl);
        } else {
            img.attr("src", customUrl);
            setUrlOnCategoryChange(customUrl);
        }
    }

    function setUrlOnCategoryChange(url) {
        imgUrl.val(url);
    }
})