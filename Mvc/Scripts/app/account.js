$(function () {


    $("#LogInToken").focus();

    $("#showSignUp").click(function () {
        $("#logInPartial").hide("slide", function () {
            $("#signUpPartial").show("slide", function () {
                $("#Name").focus();
            });
        });
    });

    $("#showLogIn").click(function () {
        $("#signUpPartial").hide("slide", function () {
            $("#logInPartial").show("slide", function () {
                $("#LogInToken").focus();
            });
        });
    });
});
