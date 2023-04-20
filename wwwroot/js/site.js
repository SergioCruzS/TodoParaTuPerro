// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

/* Sidebar toggle */
function toggleMenu() {
    var menu = document.getElementById("sidebar");
    if (menu.style.left == "-300px") {
        menu.style.left = "0px";
    } else {
        menu.style.left = "-300px"
    }
}