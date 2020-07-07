$(document).ready(function () {
    $('[data-import-files-button]').change(function () {
        $(this).parent().submit();
    });

    $('[data-import-products-button]').change(function () {
        $(this).parent().submit();
    });
});