// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

document.addEventListener("click", function (event) {
    var sidebar = document.getElementById("sidebarMenu");

    if (!sidebar) return;

    var isClickInsideSidebar = sidebar.contains(event.target);
    var isToggleButton = event.target.closest('[data-bs-toggle="collapse"]');

    if (!isClickInsideSidebar && !isToggleButton && sidebar.classList.contains("show")) {
        var bsCollapse = bootstrap.Collapse.getInstance(sidebar);
        bsCollapse.hide();
    }
});