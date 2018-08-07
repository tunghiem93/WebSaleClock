/* js for multilanguage */
    function ap_multilang_translate_product(product_handle, class_selector_product, selector_modal) {
        $.ajax({
        url : 'http://' + Shopify.shop + '/products/' + product_handle,
        type : 'get',
        success : function(reponse) {
            var doc = document.implementation.createHTMLDocument('load product');
            doc.documentElement.innerHTML = reponse;
            var translate = doc.getElementsByClassName(class_selector_product)[0].innerHTML.trim();
            selector_modal.html(translate);
        }
      });
    }