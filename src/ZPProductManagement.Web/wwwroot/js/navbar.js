$(document).ready(function () {
    $("body").click(function () {
        if (!$("[data-navbar-sign-out]").hasClass("navbar__sign-out--visible")) {
            return;
        }

        $("[data-navbar-sign-out]").removeClass("navbar__sign-out--visible");
    });

    $("[data-navbar-avatar]").click(function (e) {
        e.stopPropagation();

        $("[data-navbar-sign-out]").toggleClass("navbar__sign-out--visible");
    });
});
