$('.carusel').slick({
    autoplay: true,
    autoplaySpeed: 3000,
    speed: 2000,
    cssEase: 'ease-out',
    dots: true,
    arrows: false,
    pauseOnHover: false
});

$('.partners').slick({
    slidesToShow: 4,
    slidesToScroll: 1,
    autoplay: true,
    autoplaySpeed: 2000,
    arrows: false,
    pauseOnHover: false
});

$(function() {
    $(document).scroll(function() {
        var $nav = $(".header-bottom");
        $nav.toggleClass('scrolled', $(this).scrollTop() > $nav.height());
    });
});

const selectElement = (element) => document.querySelector(element);

selectElement('.menu-icons').addEventListener('click', () => {
    selectElement('nav').classList.toggle('active');
});
