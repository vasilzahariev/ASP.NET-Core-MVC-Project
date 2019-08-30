// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

jQuery('.dropdown-toggle').on('click', function (e) {
    $(this).next().toggle();
});
jQuery('.dropdown-menu.keep-open').on('click', function (e) {
    e.stopPropagation();
});

if (1) {
    $('body').attr('tabindex', '0');
}
else {
    alertify.confirm().set({ 'reverseButtons': true });
    alertify.prompt().set({ 'reverseButtons': true });
}


$('input[type=radio].submiter').on('change', function () {
    $("form").submit();
});