/*
*
* 2016 Apollo Theme
*
*/
var loadIcon = "//cdn.shopify.com/s/files/1/1359/9953/t/2/assets/loading.gif?949329912612580167";
var productQuickview = {};
$(document).ready(function () {
    initApollo();
    initQuickView();
    initWishlist();
    //initSelectors();
    productImage();
    responsiveProductZoom();
    $(window).resize(responsiveProductZoom);
    backtotop();
    panelTool();
    linkDropdown();
    if (!isMobileFunc()) {
        new WOW().init({ offset: 50 });
    }
});
function isMobileFunc() { var userAgent = navigator.userAgent.toLowerCase(); var phone = (/iphone|android|ipod|blackberry|opera mini|opera mobi|skyfire|maemo|windows phone|palm|iemobile|symbian|symbianos|fennec/i.test(userAgent)); var tablet = (/ipad|sch-i800|playbook|tablet|kindle|gt-p1000|sgh-t849|shw-m180s|a510|a511|a100|dell streak|silk/i.test(userAgent)); return phone || tablet; }
function initApollo() { $(".swatch :radio").change(function () { var t = $(this).closest(".swatch").attr("data-option-index"); var n = $(this).val(); $(this).closest("form").find(".single-option-selector").eq(t).val(n).trigger("change") }); $(".hover-bimg").mouseenter(function () { $(this).closest('.product-image-container').find('.product_img_link').first().find('img').first().attr('src', $(this).data('bimg')); }); $(".nav-verticalmenu .caret").click(function (e) { $(this).closest("li").toggleClass("menu-open"); e.stopPropagation(); }); }
function updatePricing() { var regex = /([0-9]+[.|,][0-9]+[.|,][0-9]+)/g; var unitPriceTextMatch = $('#ProductPrice span').text().match(regex); if (!unitPriceTextMatch) { regex = /([0-9]+[.|,][0-9]+)/g; unitPriceTextMatch = $('#ProductPrice span').text().match(regex); } if (unitPriceTextMatch) { var unitPriceText = unitPriceTextMatch[0]; var unitPrice = unitPriceText.replace(/[.|,]/g, ''); var quantity = parseInt($('#Quantity').val()); var totalPrice = unitPrice * quantity; var totalPriceText = Shopify.formatMoney(totalPrice, window.money_format); var textPrice = totalPriceText.match(regex); totalPriceText = (textPrice && textPrice.length > 0) ? textPrice[0] : "0"; var regInput = new RegExp(unitPriceText, "g"); var totalPriceHtml = $('#ProductPrice span').html().replace(regInput, totalPriceText); $('.product .total-price span').html(totalPriceHtml); } }
function updatePricingQuickView() {
    var regex = /([0-9]+[.|,][0-9]+[.|,][0-9]+)/g;
    var unitPriceTextMatch = jQuery('.quick-view-product .price').text().match(regex);
    if (!unitPriceTextMatch) {
        regex = /([0-9]+[.|,][0-9]+)/g;
        unitPriceTextMatch = jQuery('.quick-view-product .price').text().match(regex);
    }
    if (unitPriceTextMatch) {
        var unitPriceText = unitPriceTextMatch[0];
        var unitPrice = unitPriceText.replace(/[.|,]/g, '');
        var quantity = parseInt(jQuery('.quick-view-product input[name=quantity]').val());
        var totalPrice = unitPrice * quantity;
        var totalPriceText = Shopify.formatMoney(totalPrice, window.money_format);
        var textPrice = totalPriceText.match(regex);
        totalPriceText = (textPrice && textPrice.length > 0) ? textPrice[0] : "0";
        var regInput = new RegExp(unitPriceText, "g");
        var totalPriceHtml = jQuery('.quick-view-product .price').html().replace(regInput, totalPriceText);
        var priceVariant = jQuery('.quick-view-product [name="id"]').val();
        if (priceVariant > 0) {
            var price = getPriceByVariant(priceVariant);
            var totalPrice = price * quantity;
        }
        var newPriceHtml = Shopify.formatMoney(totalPrice, window.money_format);
        jQuery('.quick-view-product .total-price span').html(newPriceHtml);
    }

    Currency.convertAll(window.shop_currency, $("#currencies a.selected").data("currency"), "span.money", "money_format")

}
function getPriceByVariant(variantId) {
    var len = productQuickview.variants.length;
    var price = 0;
    for (var i = 0; i < len; i++) {
        var variant = productQuickview.variants[i];
        if (variant.id == variantId) {
            return variant.price;
        }
    }
    return price;
}
function updateOptionsInSelector(t, sel) { var from = (sel != "quickview") ? "" : ".quick-view-product "; switch (t) { case 0: var n = "root"; var r = $(from + ".single-option-selector:eq(0)"); break; case 1: var n = $(from + ".single-option-selector:eq(0)").val(); var r = $(from + ".single-option-selector:eq(1)"); break; case 2: var n = $(from + ".single-option-selector:eq(0)").val(); n += " / " + $(from + ".single-option-selector:eq(1)").val(); var r = $(from + ".single-option-selector:eq(2)") }var i = r.val(); r.empty(); var s = (sel != "quickview") ? Shopify.optionsMap[n] : Shopify.optionsMapQuickview[n]; if (typeof s != "undefined") { for (var o = 0; o < s.length; o++) { var u = s[o]; var a = $("<option></option>").val(u).html(u); r.append(a) } } $(from + '.swatch[data-option-index="' + t + '"] .swatch-element').each(function () { if ($.inArray($(this).attr("data-value"), s) !== -1) { $(this).removeClass("soldout").show().find(":radio").removeAttr("disabled", "disabled"); } else { } }); if ($.inArray(i, s) !== -1) { r.val(i) } r.trigger("change") }

function linkOptionSelectorsQuickview(t) { for (var n = 0; n < t.variants.length; n++) { var r = t.variants[n]; if (r.available) { Shopify.optionsMapQuickview["root"] = Shopify.optionsMapQuickview["root"] || []; Shopify.optionsMapQuickview["root"].push(r.option1); Shopify.optionsMapQuickview["root"] = Shopify.uniq(Shopify.optionsMapQuickview["root"]); if (t.options.length > 1) { var i = r.option1; Shopify.optionsMapQuickview[i] = Shopify.optionsMapQuickview[i] || []; Shopify.optionsMapQuickview[i].push(r.option2); Shopify.optionsMapQuickview[i] = Shopify.uniq(Shopify.optionsMapQuickview[i]) } if (t.options.length === 3) { var i = r.option1 + " / " + r.option2; Shopify.optionsMapQuickview[i] = Shopify.optionsMapQuickview[i] || []; Shopify.optionsMapQuickview[i].push(r.option3); Shopify.optionsMapQuickview[i] = Shopify.uniq(Shopify.optionsMapQuickview[i]) } } } updateOptionsInSelector(0, "quickview"); if (t.options.length > 1) updateOptionsInSelector(1, "quickview"); if (t.options.length === 3) updateOptionsInSelector(2, "quickview"); $(".single-option-selector:eq(0)").change(function () { updateOptionsInSelector(1, "quickview"); if (t.options.length === 3) updateOptionsInSelector(2, "quickview"); return true }); $(".single-option-selector:eq(1)").change(function () { if (t.options.length === 3) updateOptionsInSelector(2, "quickview"); return true }); }
function initWishlist() {
    if (window.wishlist_enable) {
        $(".wishlist button.btn-wishlist").click(function (e) {
            e.preventDefault();
            var d = $(this).parent();
            $.ajax({
                type: "POST",
                url: "/contact",
                data: d.serialize(),
                beforeSend: function () {

                },
                success: function (n) {
                    d.html('<a class="btn btn-outline-inverse btn-wishlist added" href="' + window.wishlist_url + '"><i class="fa fa-heart"></i><span>Go to Wishlist</span></a>');
                    if (!!$.prototype.fancybox)
                        $.fancybox.open([{
                            type: 'inline',
                            autoScale: true,
                            minHeight: 30,
                            content: '<p class="fancybox-error">' + 'Added to your wishlist.' + '</p>'
                        }], {
                                padding: 0
                            });
                    else
                        alert('Added to your wishlist.');
                },
                error: function () {

                }
            })
        });
    }
}
var product = {};
var currentLinkQuickView = '';
var option1 = '';
var option2 = '';
function validateQty(qty) { if ((parseFloat(qty) == parseInt(qty)) && !isNaN(qty)) { } else { qty = 1; } return qty; }
function initQuickView() {
    if (window.quickview_enable) {
        $(document).on("click", "#thumblist_quickview li", function () {
            changeImageQuickView($(this).find("img:first-child"), "#product-featured-image-quickview");
        });
        $(document).on('click', '.quick-view', function (e) {
            e.preventDefault();
            var producthandle = $(this).data("handle");
            currentLinkQuickView = $(this);
            Shopify.getProduct(producthandle, function (product) {
                productQuickview = product;
                var qvhtml = $("#quickview-modal").html();
                $(".quick-view-product").html(qvhtml);
                var quickview = $(".quick-view-product");
                var productdes = product.description.replace(/(<([^>]+)>)/ig, "");
                var featured_image = product.featured_image;
                productdes = productdes.split(" ").splice(0, 30).join(" ") + "...";
                quickview.find(".view_full_size img").attr("src", featured_image);
                quickview.find(".view_full_size img").attr("alt", product.title);
                quickview.find(".view_full_size a").attr("title", product.title);
                quickview.find(".view_full_size a").attr("href", product.url);

                quickview.find(".price").html(Shopify.formatMoney(product.price, window.money_format));
                quickview.find(".product-item").attr("id", "product-" + product.id);
                quickview.find(".variants").attr("id", "product-actions-" + product.id);
                console.log(quickview.find(".variants select").length);
                quickview.find(".variants select").attr("id", "product-select-" + product.id);

                quickview.find(".product-info .qwp-name").text(product.title);
                quickview.find(".product-info .brand").append("<span>Vendor: </span>" + product.vendor);
                if (product.available) {
                    quickview.find(".product-info .availability").append("<p class=\"available instock\">Available</p>");
                } else {
                    quickview.find(".product-info .availability").append("<p class=\"available outstock\">Unavailable</p>");
                }
                quickview.find(".product-info .product-sku").append("<p>Product Code: <span>" + product.variants[0].sku + "</span></p>");
                quickview.find(".product-description").text(productdes);
                if (product.compare_at_price > product.price) {
                    quickview.find(".compare-price").html(Shopify.formatMoney(product.compare_at_price_max, window.money_format)).show();
                    quickview.find(".price").addClass("on-sale")
                }
                else {
                    quickview.find(".compare-price").html("");
                    quickview.find(".price").removeClass("on-sale")
                }
                if (!product.available) {
                    quickview.find("select, input, .dec, .inc, .variants label").remove();
                    quickview.find(".add_to_cart_detail").text("Sold Out").addClass("disabled").attr("disabled", "disabled");
                    $(".quantity_wanted_p").css("display", "none");
                }
                else {
                    quickViewVariantsSwatch(product, quickview);
                }
                $("#quick-view-product").fadeIn(500);

                $(".view_scroll_spacer").removeClass("hidden");
                loadQuickViewSlider(product, quickview);

                $(".quick-view").fadeIn(500);
                if ($(".quick-view .total-price").length > 0) {
                    $(".quick-view input[name=quantity]").on("change", updatePricingQuickView)
                }
                updatePricingQuickView();
                $(".apQtyAdjust").on("click", function () {
                    var el = $(this),
                        id = el.data("id"),
                        qtySelector = el.siblings(".apQtyNum"),
                        qty = parseInt(qtySelector.val().replace(/\D/g, ''));
                    var qty = validateQty(qty);
                    if (el.hasClass("apQtyAdjustPlus")) {
                        qty = qty + 1;
                    } else {
                        qty = qty - 1;
                        if (qty <= 1) qty = 1;
                    }
                    qtySelector.val(qty);
                    updatePricingQuickView();
                });
                $(".apQtyNum").on("change", function () {
                    updatePricingQuickView();
                });
            });
            return false;
        });
    }
}
function loadQuickViewSlider(n, r) {
    productImage();
    var loadingImgQuickView = $('.loading-imgquickview');
    var s = Shopify.resizeImage(n.featured_image, "grande");
    r.find(".quickview-featured-image").append('<a href="' + n.url + '"><img src="' + s + '" title="' + n.title + '"/><div style="height: 100%; width: 100%; top:0; left:0 z-index: 2000; position: absolute; display: none; background: url(' + window.loading_url + ') 50% 50% no-repeat;"></div></a>');
    if (n.images.length > 0) {
        var o = r.find(".more-view-wrapper ul");
        for (i in n.images) {
            var u = Shopify.resizeImage(n.images[i], "grande");
            var a = Shopify.resizeImage(n.images[i], "compact");
            var f = '<li><a href="javascript:void(0)" data-imageid="' + n.id + '" data-image="' + n.images[i] + '" data-zoom-image="' + u + '" ><img src="' + a + '" alt="Proimage" /></a></li>';
            o.append(f)
        }
        o.find("a").click(function () {
            var t = r.find("#product-featured-image-quickview");
            if (t.attr("src") != $(this).attr("data-image")) {
                t.attr("src", $(this).attr("data-image"));
                loadingImgQuickView.show();
                t.load(function (t) {
                    loadingImgQuickView.hide();
                    $(this).unbind("load");
                    loadingImgQuickView.hide()
                })
            }
        });
        o.owlCarousel({
            navigation: true,
            items: 3,
            itemsDesktop: [1199, 3],
            itemsDesktopSmall: [979, 3],
            itemsTablet: [768, 3],
            itemsTabletSmall: [540, 3],
            itemsMobile: [360, 3]
        }).css("visibility", "visible")
    } else {
        r.find(".quickview-more-views").remove();
        r.find(".quickview-more-view-wrapper-jcarousel").remove()
    }
}
var convertToSlug = function (e) { return e.toLowerCase().replace(/[^a-z0-9 -]/g, "").replace(/\s+/g, "-").replace(/-+/g, "-") }
var selectCallbackQuickView = function (variant, selector) { };
function quickViewVariantsSwatch(t, quickview) {
    if (t.variants.length > 1) {
        for (var r = 0; r < t.variants.length; r++) {
            var i = t.variants[r];
            var s = '<option value="' + i.id + '">' + i.title + "</option>";
            quickview.find("form.variants > select").append(s)
        }
        var ps = "product-select-" + t.id;
        new Shopify.OptionSelectors(ps, {
            product: t,
            onVariantSelected: selectCallbackQuickView
        });
        if (t.options.length == 1) {
            $(".selector-wrapper:eq(0)").prepend("<label>" + t.options[0].name + "</label>")
        }
        quickview.find("form.variants .selector-wrapper label").each(function (n, r) {
            $(this).html(t.options[n].name)
        })

    }
    else {
        quickview.find("form.variants > select").remove();
        var v = '<input type="hidden" name="id" value="' + t.variants[0].id + '">';
        quickview.find("form.variants").append(v)
    }
}
function addCheckedSwatch() { $('.swatch .color label').on('click', function () { $('.swatch .color').each(function () { $(this).find('label').removeClass('checkedBox'); }); $(this).addClass('checkedBox'); }); }
$(document).on('click', '.quickview-close, #quick-view-product .quickview-overlay', function (e) { $("#quick-view-product").fadeOut(500); });
function initSelectors() { var numInputs = $('input[type="number"]'); numInputs.length && (numInputs.each(function () { var a = $(this), b = a.val(), c = a.attr("name"), d = a.attr("id"), e = b + 1, f = b - 1, g = b, h = $("#apQty").html(), i = Handlebars.compile(h), j = { id: a.data("id"), itemQty: g, itemAdd: e, itemMinus: f, inputName: c, inputId: d }; a.after(i(j)).remove() }), $(".apQtyAdjust").on("click", function () { var a = $(this), c = (a.data("id"), a.siblings(".apQtyNum")), d = parseInt(c.val().replace(/\D/g, "")), d = validateQty(d); a.hasClass("apQtyAdjustPlus") ? d += 1 : (d -= 1, d <= 1 && (d = 1)), c.val(d), updatePricing() }), $(".apQtyNum").on("change", function () { updatePricing() })); }
function productImage() {
    $('#thumbs_list .owl-carousel').owlCarousel({ navigation: true, items: 4, itemsDesktop: [1199, 4], itemsDesktopSmall: [979, 4], itemsTablet: [768, 4], itemsTabletSmall: [540, 4], itemsMobile: [360, 4] });
    //$("#thumblist").height(parseInt($("#thumblist >li").outerHeight(!0)*$("#thumblist >li").height)+"px"),$("#thumbs_list").serialScroll({items:"li:visible",prev:"#view_scroll_left",next:"#view_scroll_right",axis:"y",start:0,stop:!0,duration:700,step:2,lazy:!0,lock:!1,force:!1,cycle:!1});
    $('.thumbs_list_frame').height(parseInt($('.thumbs_list_frame >li').outerHeight(true) * $('.thumbs_list_frame >li').height) + 'px');
    $('.thumbs_list').serialScroll({ items: 'li:visible', prev: '.view_scroll_left', next: '.view_scroll_right', axis: 'y', start: 0, stop: true, duration: 700, step: 2, lazy: true, lock: false, force: false, cycle: false });
    if (!!$.prototype.fancybox) { $('li:visible .fancybox, .fancybox.shown').fancybox({ 'hideOnContentClick': true, 'openEffect': 'elastic', 'closeEffect': 'elastic' }); }
}
function responsiveProductZoom() { if (($(window).width()) >= 992) { productZoom('enable'); } else if (($(window).width()) <= 991) { productZoom('disable'); } }

function productZoom(status) {
    if (status == 'enable') {
        $("#proimage").elevateZoom({

            zoomType: "window",
            cursor: 'pointer',

            responsive: true, gallery: 'thumbs_list', galleryActiveClass: 'active', imageCrossfade: true, scrollZoom: true, onImageSwapComplete: function () { $(".zoomWrapper div").hide() }, loadingIcon: loadIcon
        });
        $("#proimage").bind("click", function (e) { var ez = $('#proimage').data('elevateZoom'); $.fancybox(ez.getGalleryList()); return false; });
    }
    else { $(document).on('click', '#thumbs_list .thumb_item a', function () { if ($(this).attr('href')) { var new_src = $(this).data('image'); var new_title = $(this).attr('title'); var new_href = $(this).attr('href'); if ($('#proimage').attr('src') != new_src) { $('#proimage').attr({ 'src': new_src, 'alt': new_title, 'title': new_title }); } } }); }
}
function backtotop() { $("#back-top").hide(); $(function () { $(window).scroll(function () { if ($(this).scrollTop() > 100) { $('#back-top').fadeIn(); } else { $('#back-top').fadeOut(); } }); $('#back-top a').click(function () { $('body,html').animate({ scrollTop: 0 }, 800); return false; }); }); }
//---------- PANELTOOLS ----------//
function panelTool() { $('#paneltool .panelbutton').click(function () { $('#paneltool .paneltool').toggleClass('active'); }); changeFloatHeader(); changeLayoutMode(); changeHeaderStyle(); }
function changeFloatHeader() { $('#floatHeader').click(function () { if ($('body').hasClass('keep-header')) { $('body').removeClass('keep-header'); processFloatHeader(0, 0); } else { processFloatTopbar(0, 0); $('body').addClass('keep-header'); $('body').removeClass('keep-topbar'); $("#floatTopbar").prop("checked", false); }; }); $('#floatTopbar').click(function () { if ($('body').hasClass('keep-topbar')) { $('body').removeClass('keep-topbar'); processFloatTopbar(0, 0); } else { processFloatHeader(0, 0); $('body').addClass('keep-topbar'); $('body').removeClass('keep-header'); $("#floatHeader").prop("checked", false); }; }); }
function changeLayoutMode() { $('.dynamic-update-layout').click(function () { var currentLayoutMode = $('.dynamic-update-layout.selected').data('layout-mode'); if (!$(this).hasClass('selected')) { $('.dynamic-update-layout').removeClass('selected'); $(this).addClass('selected'); selectedLayout = $(this).data('layout-mode'); $('body').removeClass(currentLayoutMode); $('body').addClass(selectedLayout); } }); }
function changeHeaderStyle() { $('.dynamic-update-header').click(function () { var currentHeaderMode = $('.dynamic-update-header.selected').data('header-style'); if (!$(this).hasClass('selected')) { $('.dynamic-update-header').removeClass('selected'); $(this).addClass('selected'); selectedHeaderStyle = $(this).data('header-style'); $('body').removeClass(currentHeaderMode); $('body').addClass(selectedHeaderStyle); } }); }
//---------- OWLCAROUSEL Add class First, Last ----------//
function OwlLoaded(el) { el.removeClass('owl-loading').addClass('owl-loaded'); };
function SetOwlCarouselFirstLast(el) { el.find(".owl-item").removeClass("first"); el.find(".owl-item.active").first().addClass("first"); el.find(".owl-item").removeClass("last"); el.find(".owl-item.active").last().addClass("last"); };
//---------- Fixed Click Link Dropdown ----------//
function linkDropdown() { $('body').on('touchstart.dropdown', '.dropdown-menu', function (e) { e.stopPropagation(); }); $(document.body).on('click', '[data-toggle="dropdown"]', function () { if (!$(this).parent().hasClass('open') && this.href && this.href != '#') { window.location.href = this.href; } }); }
//---------- AJAX Filter Sidebar + FORM Account ----------//
function callBackAjaxCart() {
    ajaxifyShopify.init({
        method: 'modal',
        wrapperClass: 'wrapper',
        formSelector: '.form-ajaxtocart',
        addToCartSelector: '.ajax_addtocart',
        cartCountSelector: '#CartCount',
        cartCostSelector: '#CartCost',
        toggleCartButton: '#CartToggle',
        useCartTemplate: true,
        btnClass: 'btn',
        moneyFormat: null,
        disableAjaxCart: false,
        enableQtySelectors: true,
        prependDrawerTo: 'body'
    });
}
(function ($) {
    var getHash = function () { return window.location.hash; };
    var updateHash = function (hash) { window.location.hash = '#' + hash; $('#' + hash).attr('tabindex', -1).focus(); };
    $(document).ready(function () {
        if ($(".template-collection")) { History.Adapter.bind(window, 'statechange', function () { var State = History.getState(); if (!apollo.isFilterAjaxClick) { apollo.filterParams(); var newUrl = apollo.filterCreateUrl(); apollo.filterGetContent(newUrl); apollo.reloadFilter(); } apollo.isFilterAjaxClick = false; }); }
        apollo.init();
    });
    var apollo = {
        isFilterAjaxClick: false,
        init: function () {
            this.loginForms();
            this.initFilter();

            this.ajaxSearch();

        },
        loginForms: function () { function showRecoverPasswordForm() { $('#RecoverPasswordForm').show(); $('#CustomerLoginForm').hide(); } function hideRecoverPasswordForm() { $('#RecoverPasswordForm').hide(); $('#CustomerLoginForm').show(); } $('#RecoverPassword').on('click', function (evt) { evt.preventDefault(); showRecoverPasswordForm(); }); $('#HideRecoverPasswordLink').on('click', function (evt) { evt.preventDefault(); hideRecoverPasswordForm(); }); if (getHash() == '#recover') { showRecoverPasswordForm(); } },
        resetPasswordSuccess: function () { $('#ResetSuccess').show(); },
        showLoading: function () { $('#loading').show(); },
        hideLoading: function () { $('#loading').hide(); },
        initFilter: function () { apollo.filterParams(); apollo.filterSortby(); apollo.filterMapSidebar(); },
        filterParams: function () { Shopify.queryParams = {}; if (location.search.length) { for (var aKeyValue, i = 0, aCouples = location.search.substr(1).split('&'); i < aCouples.length; i++) { aKeyValue = aCouples[i].split('='); if (aKeyValue.length > 1) { Shopify.queryParams[decodeURIComponent(aKeyValue[0])] = decodeURIComponent(aKeyValue[1]); } } } },
        filterSortby: function () { if (Shopify.queryParams.sort_by) { var sortby = Shopify.queryParams.sort_by; $("#SortBy").val(sortby); } },
        filterCreateUrl: function (baseLink) { var newQuery = $.param(Shopify.queryParams).replace(/%2B/g, '+'); if (baseLink) { if (newQuery != "") { return baseLink + "?" + newQuery; } else { return baseLink; } } return location.pathname + "?" + newQuery; },
        filterMapData: function (data) { $('#product_list').html($("#product_list", data.responseText).html()); $('#pagination').remove(); if ($(".content_sortPagiBar", data.responseText).length && $(".content_sortPagiBar", data.responseText).html() != '') { $('.content_sortPagiBar').html($(".content_sortPagiBar", data.responseText).html()); } $('#catalog_block').html($("#catalog_block", data.responseText).html()); var viewCurrent = $(".change-view.change-view--active").data('view'); $('#product_list').removeClass('list').removeClass('grid').addClass(viewCurrent); },
        filterAjaxClick: function (baseLink) { delete Shopify.queryParams.page; var newurl = apollo.filterCreateUrl(baseLink); console.log('Link: ' + newurl); apollo.isFilterAjaxClick = true; History.pushState({ param: Shopify.queryParams }, newurl, newurl); apollo.filterGetContent(newurl); },
        filterGetContent: function (newurl) { $.ajax({ type: 'GET', url: newurl, data: {}, beforeSend: function () { apollo.showLoading(); }, complete: function (data) { apollo.filterMapData(data); apollo.hideLoading(); apollo.filterMapPaging(); apollo.sidebarMapTagEvents(); callBackAjaxCart(); }, error: function (xhr, text) { apollo.hideLoading(); $('#errorJs .modal-content').text($.parseJSON(xhr.responseText).description); $('#errorJs').modal('show'); } }); },
        sidebarMapTagEvents: function () { $('.advanced-filter a').click(function (event) { event.preventDefault(); var currentTags = []; if (Shopify.queryParams.constraint) { currentTags = Shopify.queryParams.constraint.split('+'); } if (!window.multiple_filter_sidebar_enable && !$(this).parent().hasClass("active-filter")) { console.log('No multichoise'); var otherTag = $(this).parents('.catalog_filter_ul').find("li.active-filter"); if (otherTag.length > 0) { var tagName = otherTag.data("handle"); if (tagName) { var tagPos = currentTags.indexOf(tagName); if (tagPos >= 0) { currentTags.splice(tagPos, 1); } } } } var dataHandle = $(this).parent().data("handle"); if (dataHandle) { var tagPos = currentTags.indexOf(dataHandle); if (tagPos >= 0) { currentTags.splice(tagPos, 1); } else { currentTags.push(dataHandle); } } if (currentTags.length) { Shopify.queryParams.constraint = currentTags.join('+'); } else { delete Shopify.queryParams.constraint; } apollo.filterAjaxClick(); }); },
        sidebarMapTypeEvents: function () { },
        sidebarMapVendorEvents: function () { },
        filterMapView: function () { $(".change-view").click(function (event) { event.preventDefault(); if (!$(this).hasClass("change-view--active")) { if ($(this).data('view') == 'list') { Shopify.queryParams.view = "list"; } else { Shopify.queryParams.view = "grid"; } $(".change-view").removeClass('change-view--active'); $(this).addClass('change-view--active'); apollo.filterAjaxClick(); } }); },
        filterMapSorting: function () { $("#SortBy").change(function (event) { Shopify.queryParams.sort_by = $(this).val(); apollo.filterAjaxClick(); }); },
        filterMapPaging: function () { $('.content_sortPagiBar .pagination a').click(function (event) { event.preventDefault(); var linkPage = $(this).attr("href").match(/page=\d+/g); if (linkPage) { Shopify.queryParams.page = parseInt(linkPage[0].match(/\d+/g)); if (Shopify.queryParams.page) { var newurl = apollo.filterCreateUrl(); apollo.isFilterAjaxClick = true; History.pushState({ param: Shopify.queryParams }, newurl, newurl); console.log('Link: ' + newurl); apollo.filterGetContent(newurl); $('body,html').animate({ scrollTop: 500 }, 600); } } }); },
        filterMapSidebar: function () { apollo.sidebarMapTagEvents(); apollo.sidebarMapTypeEvents(); apollo.sidebarMapVendorEvents(); apollo.filterMapView(); apollo.filterMapSorting(); apollo.filterMapPaging(); },
        reloadFilter: function () { $('.advanced-filter.active-filter').removeClass('active-filter'); if (Shopify.queryParams.view) { $('.change-view.change-view--active').removeClass('change-view--active'); var view = Shopify.queryParams.view; if (view.indexOf('list') >= 0) { $('.change-view').data('view', 'list').addClass('change-view--active'); } else { $('.change-view').data('view', 'grid').addClass('change-view--active'); } } },
        ajaxProductItems: function () {
            var result = new Array();
            var search_url = '/collections/all?view=ajax';
            $.ajax({
                type: 'GET',
                url: search_url,
                success: function (data) {
                    data = '<div>' + data + '</div>';
                    data = data.trim();
                    var elements = $(data).find('.aps-ajax');
                    if (0 < elements.length) {
                        elements.each(function () {
                            var title = $.trim(this.getAttribute('data-title'));
                            var price = $.trim(this.getAttribute('data-price'));
                            var handle = $.trim(this.getAttribute('data-handle'));
                            var image = $.trim(this.getAttribute('data-img'));
                            var item = new Object();
                            item.title = title;
                            item.price = price;
                            item.handle = handle;
                            item.featured_image = image;
                            result.push(item);
                        });
                    } else {
                        //todo : return not found here
                    }
                },
                dataType: 'html'
            });
            return result;
        },
        ajaxSearch: function () { var products = apollo.ajaxProductItems(); $("#search_query_top").keyup(function () { var $this = $(this); var keyword = $this.val().toLowerCase(); $('#ap-ajax-search').hide(); if (keyword.length >= 2) { $(this).removeClass('error warning valid').addClass('valid'); var result = $('#ap-ajax-search .aps-results').empty(); var j = 0; for (var i = 0; i < products.length; i++) { var item = products[i]; var title = item.title; var price = item.price; var handle = item.handle; var image = item.featured_image; if (title.toLowerCase().indexOf(keyword) > -1) { var j = j + 1; var markedString = title.replace(new RegExp('(' + keyword + ')', 'gi'), '<span class="marked">$1</span>'); var template = '<li class="product-block"><a class="product_img_link" href="/products/' + handle + '">' + '<img style="max-width: 80px; float: left;margin-right: 5px" src="' + image + '" />' + '</a><a class="product-name" href="/products/' + handle + '">' + markedString + '</a><div class="content_price"><span class="price product-price">' + price + '</span></div></li>'; if (j <= 5) { result.append(template); } } } if ($('#ap-ajax-search .aps-results li').length < 1) { result.append('<li><p>No result found for your search.</p></li>') } if ($('#ap-ajax-search .aps-results li').length) { $('#ap-ajax-search').show(); } } else { if (keyword.length == 1) { $(this).removeClass('error warning valid').addClass('error'); var text = '<li><p>You must enter at least 2 characters.</p></li>'; var result = $('#ap-ajax-search .aps-results').empty(); result.append(text); $('#ap-ajax-search').show(); } else { $('#ap-ajax-search').hide(); } } }); $(document).on('click', '#page_content', function (e) { $('#ap-ajax-search').hide(); }); }
    }

})(jQuery);
//---------- AJAX Search ----------//