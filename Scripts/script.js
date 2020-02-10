//$(document).ready(function () {
//    console.log(window.location);
//});

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

$(".ResetUserPassowrd").click(function () {
    var user_id = $(this).data("userid");

    $.ajax({
        url: "/pages_dynamic/users/view_all_users.aspx/ResetPassword",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: "{'user_id': '" + user_id + "'}",
        success: function (response) {
            var success = response.d;
            if (success) {
                alert("Password has been reset. Email details not set up for demo to send out email.");
            }
            else {
                alert("Error resetting password");
            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            console.log(XMLHttpRequest);
            console.log(textStatus);
            console.log(errorThrown);
            alert(response.d);
        },
        failure: function (response) {
            alert(response.d);
        }
    });
});

$(".UnlockUser").click(function () {
    var user_id = $(this).data("userid");
    var btn = $(this);

    $.ajax({
        url: "/pages_dynamic/users/view_all_users.aspx/UnlockAccount",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: "{'user_id': '" + user_id + "'}",
        success: function (response) {
            var success = response.d;
            if (success) {
                $(btn).remove();
            }
            else {
                alert("Error unlocking account");
            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            console.log(XMLHttpRequest);
            console.log(textStatus);
            console.log(errorThrown);
            alert(response.d);
        },
        failure: function (response) {
            alert(response.d);
        }
    });
});