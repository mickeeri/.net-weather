$(document).ready(function () {
    //$('#LocationSearchQuery').focus();
    $('#LocationSearchQuery').select();

    $('form').submit(function () {
        $('.btn').val("Söker...");
    });
});