(function($) {
	
	'use strict';
	
	var Flatland = {
		
		// Initialization the functions
		init: function() {
			Flatland.AffixMenu();
			Flatland.ScrollSpy();
			Flatland.SmoothScroll();
			Flatland.FitVids();
			Flatland.PlaceHolder();
			Flatland.Carousel();
			Flatland.Lightbox();
			Flatland.CounterUp();
			Flatland.Parallax();
			Flatland.ImgHover();
			Flatland.Isotope();
			Flatland.Form();
			Flatland.Animated();
		},
		
		// Navigation menu affix
		AffixMenu: function() {
			var navMenu	= '<nav id="navigation_affix">';
			navMenu		+= '<div class="container">';
			navMenu		+= '<div class="navbar-brand">';
			//navMenu += '<a href="index.html"><img src="images/logo_affix.png" alt="Logo" /></a>';
			navMenu     += '<h1>Unitcandy</h1>';
			navMenu		+= '</div>';
			navMenu		+= '<ul class="nav navbar-nav">';
			navMenu		+= $('#navigation .nav.navbar-nav').html();
			navMenu		+= '</ul>';
			navMenu		+= '</div>';
			navMenu		+= '</nav>';
			
			$('#header').before(navMenu);
			
			$('#navigation').waypoint(function() {
				$('#navigation_affix').removeClass('show');
			}, {
				offset: -120
			});
			
			$('#navigation').waypoint(function() {
				$('#navigation_affix').addClass('show');
			}, {
				offset: -121
			});
		},
		
		// Navigation menu scrollspy to anchor section
		ScrollSpy: function() {
			$('body').scrollspy({
				target: '#navigation_affix',
				offset: parseInt($('#navigation_affix').height(), 0)
			});
		},
		
		// Smooth scrolling to anchor section
		SmoothScroll: function() {
			$('a.smooth-scroll').on('click', function(event) {
				var $anchor		= $(this);
				var offsetTop	= '';
				var elemHeight	= parseInt($('#navigation_affix').height() - 1, 0);
				
				if (window.Response.band(768)) {
					offsetTop = parseInt($($anchor.attr('href')).offset().top - elemHeight, 0);
				} else {
				    offsetTop = parseInt($($anchor.attr('href')).offset().top, 0);
				}

				offsetTop = offsetTop - 50;
				if (offsetTop < 0) {
				    offsetTop = 0;
				}
				
				$('html, body').stop().animate({
					scrollTop: offsetTop
				}, 1500,'easeInOutExpo');
				
				event.preventDefault();
			});
		},
		
		// Responsive video size
		FitVids: function() {
			$('body').fitVids();
		},
		
		// Placeholder compatibility for IE8
		PlaceHolder: function() {
			$('input, textarea').placeholder();
		},
		
		// Slider with Slick carousel
		Carousel: function() {
			// Header slider
			$('#header .carousel-slider').each(function() {
				$(this).slick({
					arrows: false,
					dots: true,
					autoplay: true,
					autoplaySpeed: 5000,
					draggable: false,
					responsive: [{
						breakpoint: 768,
						settings: {
							draggable: true
						}
					}]
				});
			});
			
			// Gallery slider
			$('.carousel-slider.gallery-slider').each(function() {
				$(this).slick({
					arrows: false,
					dots: true,
					slidesToShow: 4,
					slidesToScroll: 1,
					draggable: false,
					responsive: [
						{
							breakpoint: 992,
							settings: {
								slidesToShow: 3
							}
						},
						{
							breakpoint: 768,
							settings: {
								slidesToShow: 2,
								draggable: true
							}
						},
						{
							breakpoint: 400,
							settings: {
								arrows: true,
								dots: false,
								slidesToShow: 1,
								slidesToScroll: 1
							}
						}
					]
				});
			});
			
			// General slider
			$('.carousel-slider.general-slider').each(function() {
				$(this).slick({
					dots: true,
					speed: 300,
					adaptiveHeight: true,
					draggable: false,
					responsive: [{
						breakpoint: 768,
						settings: {
							draggable: true
						}
					}]
				});
			}).on('afterChange', function() {
				$(window).trigger('resize.px.parallax');
			});
		},
		
		// Preview images popup gallery with Fancybox
		Lightbox: function() {
			$('.fancybox').fancybox({
				loop: false
			});
		},
		
		// Number counter ticker animation
		CounterUp: function() {
			$('.affa-counter > h4 > span').counterUp({
				delay: 10,
				time: 3000
			});
		},
		
		// Background with parallax effect
		Parallax: function() {
			$(window).resize(function() {
				setTimeout(function() {
					$(window).trigger('resize.px.parallax');
				}, 100);
			});
			
			$('#navigation .navbar-toggle').on('click', function() {
				setTimeout(function() {
					$(window).trigger('resize.px.parallax');
				}, 300);
			});
		},
		
		// Image on hover animation effect
		ImgHover: function() {
			$('.img-overlay').hover(
				function() {
					var $elem = $(this);
					
					$elem.find('.overlay-masked').fadeIn(300);
					
					setTimeout(function() {
						$elem.find('.overlay-masked .overlay-icon').animate({
							top: '50%',
							opacity: 1
						}, 200);
						
						$elem.find('.overlay-masked p').fadeIn(200);
					}, 100);
				},
				function() {
					var $elem = $(this);
					
					$elem.find('.overlay-masked .overlay-icon').animate({
						top: '25%',
						opacity: 0
					}, 200);
					
					$elem.find('.overlay-masked p').fadeOut(200).parents('.overlay-masked').fadeOut(300);
				}
			);
		},
		
		// Filter elements with jQuery Isotope
		Isotope: function() {
			var $filter		= $('.isotope-menu');
			var $container	= $('.isotope-row');
			
			// Initialize
			$(window).load(function() {
				$container.isotope({
					filter			 : '*',
					layoutMode		 : 'fitRows',
					animationOptions : {
						duration: 400
					}
				}).on('arrangeComplete', function() {
					$(window).trigger('resize.px.parallax');
				}).on('layoutComplete', function() {
					$(window).trigger('resize.px.parallax');
				});
			});
			
			// Trigger item lists filter when link clicked
			$filter.find('a').click(function() {
				var selector = $(this).attr('data-filter');
				$filter.find('a').removeClass('active');
				$(this).addClass('active');
				$container.isotope({
					filter			 : selector,
					animationOptions : {
						animationDuration : 400,
						queue : false
					}
				});
				
				return false;
			});
		},
		
		// Form submit function
		Form: function() {
			var pattern = /^((([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+(\.([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+)*)|((\x22)((((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(([\x01-\x08\x0b\x0c\x0e-\x1f\x7f]|\x21|[\x23-\x5b]|[\x5d-\x7e]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(\\([\x01-\x09\x0b\x0c\x0d-\x7f]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]))))*(((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(\x22)))@((([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.)+(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))$/i;
			
			// Checking form input when focus and keypress event
			$('.affa-form-contact input[type="text"], .affa-form-contact input[type="email"], .affa-form-contact textarea').on('focus keypress', function() {
				var $input = $(this);
				
				if ($input.hasClass('error')) {
					$input.removeClass('error');
				}
			});
			
			// Contact form when submit button clicked
			$('.affa-form-contact').submit(function() {
				var $form		= $(this);
				var submitData	= $form.serialize();
				var $name		= $form.find('input[name="name"]');
				var $email		= $form.find('input[name="email"]');
				var $message	= $form.find('textarea[name="message"]');
				var $submit		= $form.find('input[name="submit"]');
				var status		= true;
				
				if ($email.val() === '' || pattern.test($email.val()) === false) {
					$email.addClass('error');
					status = false;
				}
				if ($message.val() === '') {
					$message.addClass('error');
					status = false;
				}
				
				if (status) {
					$name.attr('disabled', 'disabled');
					$email.attr('disabled', 'disabled');
					$message.attr('disabled', 'disabled');
					$submit.attr('disabled', 'disabled');
					
					$.ajax({
						type: 'POST',
						url: 'process-contact.php',
						data: submitData + '&action=add',
						dataType: 'html',
						success: function(msg) {
							if (parseInt(msg, 0) !== 0) {
								var msg_split = msg.split('|');
								if (msg_split[0] === 'success') {
									$name.val('').removeAttr('disabled').removeClass('error');
									$email.val('').removeAttr('disabled').removeClass('error');
									$message.val('').removeAttr('disabled').removeClass('error');
									$submit.removeAttr('disabled');
									$form.find('.submit-status').html('<div class="submit-status-text"><span class="success"><i class="fa fa-check-circle"></i> ' + msg_split[1] + '</span></div>').fadeIn(300).delay(3000).fadeOut(300);
								} else {
									$name.removeAttr('disabled').removeClass('error');
									$email.removeAttr('disabled').removeClass('error');
									$message.removeAttr('disabled').removeClass('error');
									$submit.removeAttr('disabled').removeClass('error');
									$form.find('.submit-status').html('<div class="submit-status-text"><span class="error"><i class="fa fa-exclamation-circle"></i> ' + msg_split[1] + '</span></div>').fadeIn(300).delay(3000).fadeOut(300);
								}
							}
						}
					});
				}
				
				status = true;
				
				return false;
			});
		},
		
		// Embed animation effects to HTML elements with CSS3
		Animated: function() {
			$(window).on('load', function() {
				$('img.parallax-slider').imgpreload({
					all: function() {
						$('img.parallax-slider').addClass('loaded');
					}
				});
				
				$('.animation, .animation-visible').each(function() {
					var $element = $(this);
					$element.waypoint(function() {
						var delay = 0;
						if ($element.attr('data-delay')) delay = parseInt($element.attr('data-delay'), 0);
						if (!$element.hasClass('animated')) {
							setTimeout(function() {
								$element.addClass('animated ' + $element.attr('data-animation'));
							}, delay);
						}
						delay = 0;
					}, {
						offset: '90%'
					});
				});
			});
		}
		
	};
	
	// Run the main function
	$(function() {
		Flatland.init();
	});
	
})(window.jQuery);
