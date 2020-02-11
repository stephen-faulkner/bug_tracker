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

$("#btnEditProjectDescr").click(function () {
    $("#dProjectDescr").removeClass("d-flex");
    $("#dProjectDescr").addClass("d-none");
    $("#dEditDescr").removeClass("d-none");
    $("#dEditDescr").addClass("d-flex");
});

$("#btnSaveProjectDescr").click(function () {
    var new_descr = $("#txtProjectDescr").val();
    $.ajax({
        url: "/pages_dynamic/projects/projects_edit.aspx/SaveProjectDescr",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: "{'project_descr': '" + new_descr + "'}",
        success: function (response) {
            var updated = response.d;

            if (updated == true) {
                $("#lblProjectDescr").html(new_descr);

                $("#dProjectDescr").addClass("d-flex");
                $("#dProjectDescr").removeClass("d-none");
                $("#dEditDescr").addClass("d-none");
                $("#dEditDescr").removeClass("d-flex");
            }
            else {
                alert("Unexpected error updating project description");
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

$("#btnAddProjectDev").click(function () {
    var user_id = $("#ddlAddProjectDeveloper").val();

    $.ajax({
        url: "/pages_dynamic/projects/projects_edit.aspx/AddDevToProject",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: "{'user_id': '" + user_id + "'}",
        success: function (response) {
            var added = response.d;

            if (added == false) {
                alert("Unexpected error adding developer to project");
            }
            else {
                $("#ddlAddProjectDeveloper option[value='" + user_id + "']").remove();
                LoadProjectDevelopers();
            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            console.log(XMLHttpRequest);
            console.log(textStatus);
            console.log(errorThrown);
        },
        failure: function (response) {
            alert(response.d);
        }
    });
});



$("#btnAddProjectUser").click(function () {
    var user_id = $("#ddlAddProjectUser").val();

    $.ajax({
        url: "/pages_dynamic/projects/projects_edit.aspx/AddUserToProject",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: "{'user_id': '" + user_id + "'}",
        success: function (response) {
            var added = response.d;

            if (added == false) {
                alert("Unexpected error adding developer to project");
            }
            else {
                $("#ddlAddProjectUser option[value='" + user_id + "']").remove();
                LoadProjectUsers();
            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            console.log(XMLHttpRequest);
            console.log(textStatus);
            console.log(errorThrown);
        },
        failure: function (response) {
            alert(response.d);
        }
    });
});

function RemoveUserFromProject(btn) {
    if (confirm("Are you sure you want to remove this user from this project?")) {
        var user_id = $(btn).data("user");
        var user_row = $(btn).closest("tr");
        $.ajax({
            url: "/pages_dynamic/projects/projects_edit.aspx/RemoveUserFromProject",
            type: "POST",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: "{'user_id': '" + user_id + "'}",
            success: function (response) {
                var removed = response.d;

                if (removed) {
                    $(user_row).remove();

                    var devTable = $("#tblUsers tbody");
                    $("#sUserCount").html("Users: " + devTable.rows().count().toString());
                }
                else {
                    alert("Unexpected error removing user from project.");
                }
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                console.log(XMLHttpRequest);
                console.log(textStatus);
                console.log(errorThrown);
            },
            failure: function (response) {
                alert(response.d);
            }
        });
    }    
}

function LoadProjectDevelopers() {
    $.ajax({
        url: "/pages_dynamic/projects/projects_edit.aspx/LoadProjectDevelopers",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            var data = response.d;
            var devs = data[0];
            var devCount = data[1];            

            $("#tblDevelopers tbody tr").remove();            
            var table = $("#tblDevelopers").DataTable();
            $.each(devs, function (index, value) {
                table.row.add([
                    value.Username, value.Ticket_count, value.Remove_button
                ]).draw();
            });

            $("#sDevCount").html("Developers: " + devCount.toString());
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            console.log(XMLHttpRequest);
            console.log(textStatus);
            console.log(errorThrown);
        },
        failure: function (response) {
            alert(response.d);
        }
    });
}

function LoadProjectUsers() {
    $.ajax({
        url: "/pages_dynamic/projects/projects_edit.aspx/LoadProjectUsers",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            var data = response.d;
            var users = data[0];
            var userCount = data[1];

            $("#tblUsers tbody tr").remove();
            var table = $("#tblUsers").DataTable();
            $.each(users, function (index, value) {
                table.row.add([
                    value.Username, value.Ticket_count, value.Remove_button
                ]).draw();
            });

            $("#sUserCount").html("Users: " + userCount.toString());
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            console.log(XMLHttpRequest);
            console.log(textStatus);
            console.log(errorThrown);
        },
        failure: function (response) {
            alert(response.d);
        }
    });
}

function ReInitliazeTable(tbl) {
    $('#' + tbl).DataTable().destroy();
    $('#' + tbl).DataTable();
}