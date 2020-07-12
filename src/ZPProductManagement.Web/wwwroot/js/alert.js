$(document).ready(function () {
    $('[data-alert-close]').click(function () {
        $(this).parent().fadeOut(500);
    });

    setTimeout(function () {
        $('[data-alert]').fadeOut(500);
    }, 3500);
});