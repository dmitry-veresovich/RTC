$("#logInPartial").ready(function () {
    $("#submit").submit(submitLogIn);
    $("#signUpBtn").click(signUp);
});

function submitLogIn(event) {
    event.preventDefault();
    var data = $(this).serialize();
    var url = $(this).attr("action");
    $.post(url, data, function (response) {
        $("#body").empty().append(response);
    });
}

function signUp() {
    $.post("/Account/SignUpPartial", null, function (response) {
        $("#body").empty().append(response);
    });
}
