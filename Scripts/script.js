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

$(".menu-title").click(function () {
    $(".menu-title").find(".menu-down").hide();
    $(".menu-title").find(".menu-right").show();
    $(".menu-title").siblings().slideUp();

    if ($(this).siblings().is(":visible") == false) {
        $(this).find(".menu-down").show();
        $(this).find(".menu-right").hide();

        $(this).siblings().slideDown();
    }    
});