$(document).ready(function () {
    console.log(window.location);
});

function SetFocus(input) {
    $("#" + input).select();
}

$(".Next-Control").keyup(function (event) {
    var keycode = event.keyCode ? event.keyCode : event.which;
    if (keycode == "13") {
        $("#" + $(this).data("next-control")).select();
    }
});