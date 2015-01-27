$(document).ready(function () {
    $('#changeNameForm').submit(onSubmitFactory("btnChangeName"));
    $('#changeEmailForm').submit(onSubmitFactory("btnChangeEmail"));
    //$('#changePhotoForm').submit(onSubmitFactory("btnChangePhoto"));
    $('#changePhoneNumberForm').submit(onSubmitFactory("btnChangePhoneNumber"));
    $('#changePasswordForm').submit(function(event) {
        event.preventDefault();
        var data = $(this).serialize();
        var url = $(this).attr("action");
        $.post(url, data, function(response) {
            showModal(response.toString());
            if (response == "Password changed!") {
                $("#CurrentPassword").val('');
                $("#Password").val('');
                $("#ConfirmPassword").val('');
            }
        });
    });
});

function onSubmitFactory(btn) {
    function onSubmitForm(event) {
        event.preventDefault();
        var data = $(this).serialize();
        var url = $(this).attr("action");
        $.post(url, data, function (response) {
            if (response == "") {
                document.getElementById(btn).disabled = true;
            } else {
                showModal(response.toString());
            }
        });
    }
    return onSubmitForm;
}

function onChange(btn) {
    document.getElementById(btn).disabled = false;
}

function showModal(text) {
    $("#modalText").html(text);
    $("#modal").modal('show');
}
