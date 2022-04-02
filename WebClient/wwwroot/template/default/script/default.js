// popup
var popupStatus=0;function loadPopup(){if(popupStatus==0){$("#backgroundPopup").css({opacity:"0.8"});$("#backgroundPopup").fadeIn("slow");$("#popupContact").fadeIn("slow");popupStatus=1}}function disablePopup(){if(popupStatus==1){$("#backgroundPopup").fadeOut("slow");$("#popupContact").fadeOut("slow");popupStatus=0}}function centerPopup(){var a=document.documentElement.clientWidth;var d=document.documentElement.clientHeight;var c=$("#popupContact").height();var b=$("#popupContact").width();$("#popupContact").css({position:"fixed",top:d/2-c/2,left:a/2-b/2});$("#backgroundPopup").css({height:d})}$(window).load(function(){centerPopup();loadPopup();$("#button").click(function(){centerPopup();loadPopup()});$("#popupContactClose").click(function(){disablePopup()});$("#backgroundPopup").click(function(){disablePopup()});$(document).keypress(function(a){if(a.keyCode==27&&popupStatus==1){disablePopup()}})});

$(document).ready(function(){
	//// login register
 //   $('#login').click( function(event){        
 //       event.stopPropagation();        
 //       $('.form-login').css('display','block');
	//	$('.form-register').css('display','none');
        
 //   });
	//$('#register').click( function(event){        
 //       event.stopPropagation();        
 //       $('.form-login').css('display','none');
	//	$('.form-register').css('display','block');        
 //   });
	//$('.log-in').click(function(){
	//	$('#register').removeAttr('checked');
	//	$('#login').click();
		
	//});
	//$('.register').click(function(){
	//	$('#login').removeAttr('checked');
	//	$('#register').click();
		
	//});
	// filters
	$('.filters-top-content a').click( function(event){        
        event.stopPropagation();        
        $('.filters-bottom-content').slideToggle("fast",function(){
            if($('.filters-bottom-content').css('display') == 'block')
             {
              $('.filters-top-content a').css('background-position','0 -34px')
            }else {
              $('.filters-top-content a').css('background-position','0 4px')
            }
        });        
	});	
	// slide mobi menu
	$('.mob-top-menu').click( function(){
		$('#menu').slideToggle("fast");
        $('#search').slideUp("fast");
		$('#topWishlistContent').slideUp("fast");
		$('#topCartContent').slideUp("fast");		
    });
	$('.mob-top-search').click( function(){
		$('#search').slideToggle("fast");
        $('#menu').slideUp("fast");
		$('#topWishlistContent').slideUp("fast");
		$('#topCartContent').slideUp("fast");		
    });
	$('.mob-top-wishlist').click( function(){
		$('#topWishlistContent').slideToggle("fast");
        $('#menu').slideUp("fast");
		$('#search').slideUp("fast");
		$('#topCartContent').slideUp("fast");		
    });
	$('.mob-top-cart').click( function(){
		$('#topCartContent').slideToggle("fast");
        $('#menu').slideUp("fast");
		$('#search').slideUp("fast");
		$('#topWishlistContent').slideUp("fast");		
    });
	// picture in detail page
	$('.small-pic img').click(function(){
		var $this = $(this);
		var src = $this.attr('data-img');
 		if(!$this.hasClass("active")){
			$('.big-pic img').attr('src',src);
			$('.small-pic img').removeClass('active');
			$this.addClass('active');
		}
	});
	// send img Button
	$('.gh').click(function(){
		$('#upload-image').trigger('click');
	});
	// Plus - Minius Qty
	$('.mipl').click(function(){
		var current = $('#qty').val();
		if($(this).hasClass('plus-btn')) {
			current = eval(current) + 1;
		}else if(current>0){
			current = current - 1;
		}
		$('#qty').val(current);
      	$('#qty-mobile').val(current);
	});
	// top-fixed
	$('.cwsm').click(function(){
		var $this = $(this);
		
		$this.siblings().each(function(){
		var margin = 49-$(this).width();
		$(this).css('margin-right',margin+'px');
		});
		
		$this.css('margin-right','0');
		$this.find('.cwsm-container').slideDown('fast');
		
	});
	$('.ut-close-icon').click(function(event){
		event.stopPropagation();		
		var $parent = $(this).closest('li');
		var margin = 49-$parent.width();		
		$parent.find('.cwsm-container').slideUp("fast");
		$parent.delay(4000).css('margin-right',margin+'px');
	});
	// change class footer mobi
	var footer = $('footer').html()
	$(window).on('load resize', function(){		
	if( $( window ).width()<=767){	
	$('.ft-mobi-1').append($('.ft-mobi-2'));
	$('.ft-mobi-1').append($('.ft-mobi-3'));
	$('.ft-mobi-1').append($('.ft-mobi-4'));
	$('.ft-mobi-1').append($('.ft-mobi-5'));
	$('.ft-mobi-4').addClass('clearfix')
	}else{
	
	$('footer').html(footer);
	}	
});
});