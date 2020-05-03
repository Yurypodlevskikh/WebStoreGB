﻿Cart = {
    _properties: {
        getCartViewLink: "",
        addToCartLink: ""
    },

    init: function (properties) {
        $.extend(Cart._properties, properies);

        $(".add-to-cart").click(addToCart);
    },

    addToCart: function (event) {
        event.preventDefault();

        var button = $(this);
        const id = button.data("id"); // data-id="..."

        $.get(Cart._properties.addToCartLink + "/" + id)
            .done(function () {
                Cart.showToolTip(button);
                Cart.refreshCartView();
            })
            .fail(function () { console.log("addToCart fail") });
    },

    showToolTip: function (button) {
        button.tooltip({ title: "Добавлено в корзину!" }).tooltip("show");
        setTimeout(function () {
            button.tooltip("destroy");
        }, 500);
    },

    refreshCartView: function () {
        var container = $("#cart-container");
        $.get(Cart._properties.getCartViewLink)
            .done(function (cartHtml) {
                container.html(cartHtml);
            })
            .fail(function () { console.log("refreshCartView fail") });
    }
}