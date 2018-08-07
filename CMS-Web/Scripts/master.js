/*
*
* 2016 Apollo Theme Customize
*
*/
var loadIcon = "//cdn.shopify.com/s/files/1/1359/9953/t/2/assets/loading.gif?6880591645775336984";
$(document).ready(function() {
  	floatHeader();
	floatTopbar();
  	offCanvas();
  	responsiveAccordion();
	$(window).resize(responsiveAccordion);
});
function processFloatHeader(headerAdd, scroolAction){
	if(headerAdd){
		$("#header").addClass( "navbar-fixed-top" );
	    $("#page").css( "padding-top", $("#header").height());
	    var hideheight =  $("#header").height()+120;
	    var pos = $(window).scrollTop();
	    if( scroolAction && pos >= hideheight ){
	        $("#topbar").addClass('hide-bar');
	        $(".hide-bar").css( "margin-top", - $("#topbar").height() );
	    }else {
	        $("#topbar").css( "margin-top", 0 );
	    }
	}else{
		$("#header").removeClass( "navbar-fixed-top" );
	    $("#page").css( "padding-top", '');
        $("#header-main").removeClass('hidden');
        $("#apollo-menu").removeClass('hidden');
        $("#topbar").css( "margin-top", 0 );

	}
	
}
function processFloatTopbar(topbarAdd, scroolAction){
	if(topbarAdd){
		$("#header").addClass( "navbar-fixed-top" );
	    $("#page").css( "padding-top", $("#header").height());
	    var hideheightBar =  $("#header").height()+120;
	    var pos = $(window).scrollTop();
	    if( scroolAction && pos >= hideheightBar ){
	        $("#header-main").addClass('hidden');
			$("#apollo-menu").addClass('hidden');
	    }else {
	        $("#header-main").removeClass('hidden');
	        $("#apollo-menu").removeClass('hidden');
	    }
	}else{
		$("#header").removeClass("navbar-fixed-top");
	    $("#page").css( "padding-top", '');
	    $("#header-main").removeClass('hidden');
	    $("#apollo-menu").removeClass('hidden');
        $("#topbar").css( "margin-top", 0 );
	}

}
function floatHeader(){
	$(window).resize(function(){
		if (!$("body").hasClass("keep-header") || $(window).width() <= 990){
			return;
		}
		if ($(window).width() <= 990)
		{
			processFloatHeader(0,0);
		}
		else if ($(window).width() > 990)
		{
			if ($("body").hasClass("keep-header"))
				processFloatHeader(1,1);
		}
	});
    $(window).scroll(function() {
    	if (!$("body").hasClass("keep-header")) return;
    	if($(window).width() > 990){
	         processFloatHeader(1,1);

    	}
    });
}
function floatTopbar(){
	$(window).resize(function(){
		if (!$("body").hasClass("keep-topbar") || $(window).width() <= 990){
			return;
		}
		if ($(window).width() <= 990)
		{
			processFloatTopbar(0,0);
		}
		else if ($(window).width() > 990)
		{
			if ($("body").hasClass("keep-topbar"))
				processFloatTopbar(1,1);
		}
	});
    $(window).scroll(function() {
    	if (!$("body").hasClass("keep-topbar")) return;
    	if($(window).width() > 990){
	         processFloatTopbar(1,1);

    	}
    });
}
function offCanvas(){
	$('[data-toggle="offcanvas"]').click(function (event) {
        var menuCanvas = $(this).data('target');
        $(this).toggleClass('open');
        $(menuCanvas).toggleClass('active');
        $('body').toggleClass('off-canvas-active');
        var heightCanvas = $(window).height();
        $(menuCanvas).css({'min-height' : heightCanvas});
        event.stopPropagation();
    });
  	$('.off-canvas-nav').click(function () {
      	$('[data-toggle="offcanvas"]').click();
    });
  	$('#page').click(function(){
      	if ($('body').hasClass('off-canvas-active')){
          	$('[data-toggle="offcanvas"]').click();
        }
    });
}
function scrollCompensate() 
{
    var inner = document.createElement('p');
    inner.style.width = "100%";
    inner.style.height = "200px";
    var outer = document.createElement('div');
    outer.style.position = "absolute";
    outer.style.top = "0px";
    outer.style.left = "0px";
    outer.style.visibility = "hidden";
    outer.style.width = "200px";
    outer.style.height = "150px";
    outer.style.overflow = "hidden";
    outer.appendChild(inner);
    document.body.appendChild(outer);
    var w1 = inner.offsetWidth;
    outer.style.overflow = 'scroll';
    var w2 = inner.offsetWidth;
    if (w1 == w2) w2 = outer.clientWidth;
    document.body.removeChild(outer);
    return (w1 - w2);
}
function responsiveAccordion()
{
	compensante = scrollCompensate();
	if (($(window).width()+scrollCompensate()) <= 767)
	{
      	accordion('enable');
	    accordionFooter('enable');
	}
	else if (($(window).width()+scrollCompensate()) >= 768)
	{
		accordion('disable');
		accordionFooter('disable');
	}
}
function accordion(status)
{
 	leftColumnBlocks = $('#left_column');
 	if(status == 'enable')
 	{
      	if(!$('#left_column').hasClass('accordion')){
        	$('#left_column .block .title_block').on('click', function(){
   				$(this).toggleClass('active').parent().find('.block_content').stop().slideToggle('medium');
  			});
      	}
      	if(!$('#right_column').hasClass('accordion')){
        	$('#right_column .block .title_block').on('click', function(){
   				$(this).toggleClass('active').parent().find('.block_content').stop().slideToggle('medium');
  			});
      	}
      	$('#left_column').addClass('accordion').find('.block .block_content').slideUp('fast');
  		$('#right_column').addClass('accordion').find('.block .block_content').slideUp('fast');
 	}
 	else
 	{
  		$('#right_column .block .title_block, #left_column .block .title_block').removeClass('active').off().parent().find('.block_content').removeAttr('style').slideDown('fast');
  		$('#left_column, #right_column').removeClass('accordion');
 	}
}
function accordionFooter(status){
 	if(status == 'enable') {
       	if(!$('.footerAccordion').hasClass('accordion')){
          	$('.footerAccordion h4').on('click', function(){
              	$(this).toggleClass('active').parent().find('.block_content').stop().slideToggle('medium');
          	})
        }
  		$('.footerAccordion').addClass('accordion').find('.block_content').slideUp('fast');
 	}
 	else {
  		$('.footerAccordion h4').removeClass('active').off().parent().find('.block_content').removeAttr('style').slideDown('fast');
  		$('.footerAccordion').removeClass('accordion');
 	}
}
function accordionNewletter(status){
 	if(status == 'enable') {
       	if(!$('#newsletter_block').hasClass('accordion')){
          	$('#newsletter_block .title_block').on('click', function(){
              	$(this).toggleClass('active').parent().find('.block_content').stop().slideToggle('medium');
          	})
        }
  		$('#newsletter_block').addClass('accordion').find('.block_content').slideUp('fast');
 	}
 	else {
  		$('#newsletter_block .title_block').removeClass('active').off().parent().find('.block_content').removeAttr('style').slideDown('fast');
  		$('#newsletter_block').removeClass('accordion');
 	}
}
/*START LEFT- RIGHT MENU*/
function setLeftColumn() {
  	if($("#left_column").length > 0) {
        if($(".over-cover").length == 0) {
            $('body').append($('<div class="over-cover"></div><div class="drag-target-left"><div class="drag-panel-left"></div></div>'));
            $('body').append($('<a href="javascript:;" data-activates="slide-out" class="button-collapse-left"><i class="mdi-navigation-menu"></i></a>'));
        }

        $(".button-collapse-left").click(function() {
            $(".drag-target-left, .button-collapse-left").addClass("hide");
          	$("body").addClass("move-left-to-right");
            $('#left_column').css("left", "-400px");
            setTimeout(function() {
                $("#left_column").parent().addClass("side-nav-container-left");
                $("#left_column").addClass("side-nav-left");
            }, 200);
        });
        $(".over-cover").click(function() {
          	$(".drag-target-left, .button-collapse-left").removeClass("hide");
            $("#left_column").parent().removeClass("side-nav-container-left");
            $("#left_column").removeClass("side-nav-left");
            $("body").removeClass("move-left-to-right");
            $(".box-left-menu").addClass("menu-close");
            $('#left_column').css("left", "");
        });
        $(".drag-target-left").click(function() {
            console.log("drag-target click");
            $(".button-collapse-left").trigger("click");
        });
    }
}
function setRightColumn() {
  	if($("#right_column").length > 0) {
        if($(".over-cover").length == 0) {
            $('body').append($('<div class="over-cover"></div><div class="drag-target-right"><div class="drag-panel-right"></div></div>'));
            $('body').append($('<a href="javascript:;" data-activates="slide-out" class="button-collapse-right"><i class="mdi-navigation-menu"></i></a>'));
        }

        $(".button-collapse-right").click(function() {
            $(".drag-target-right, .button-collapse-right").addClass("hide");
          	$("body").addClass("move-right-to-left");
            $('#right_column').css("right", "-400px");
            setTimeout(function() {
                $("#right_column").parent().addClass("side-nav-container-right");
                $("#right_column").addClass("side-nav-right");
            }, 200);
        });
        $(".over-cover").click(function() {
          	$(".drag-target-right, .button-collapse-right").removeClass("hide");
            $("#right_column").parent().removeClass("side-nav-container-right");
            $("#right_column").removeClass("side-nav-right");
            $("body").removeClass("move-right-to-left");
            $(".box-right-menu").addClass("menu-close");
            $('#right_column').css("right", "");
        });
        $(".drag-target-right").click(function() {
            console.log("drag-target click");
            $(".button-collapse-right").trigger("click");
        });
    }
}
/*END LEFT- RIGHT MENU*/