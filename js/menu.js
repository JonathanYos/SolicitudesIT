$(document).ready(function () {

    $('.desplegar').click(function (event) {
        var elem = $(this).next();
        var $divs = $(".desplegar").toArray().length;

        var elem2 = $('.desplegar').next();
        if ($(this).hasClass('activado')) {

            $(this).removeClass('activado')
            elem.slideToggle();
        } else {
            $(this).addClass('activado');
            elem2.addClass('mst');
            $('.desplegar').removeClass('activado');
            elem.slideToggle();
        }
    });
});