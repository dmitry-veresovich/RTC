$(function () {
    var getValidationSummaryErrors = function ($form) {
        var errorSummary = $form.find('#validSum');
        return errorSummary;
    };

    var displayErrors = function (form, errors) {
        var errorSummary = getValidationSummaryErrors(form);

        var items = $.map(errors, function (error) {
            return '<li>' + error + '</li>';
        }).join('');

        errorSummary.find('ul').empty().append(items);
        form.find("#submitBtn").button("reset");
    };

    var formSubmitHandler = function (e) {
        var $form = $(this);

        if (!$form.valid || $form.valid()) {
            $form.find("#submitBtn").button('loading');

            $.post($form.attr('action'), $form.serializeArray())
                .done(function (json) {
                    json = json || {};

                    if (json.success) {
                        window.location = json.redirect || location.href;
                    } else if (json.errors) {
                        displayErrors($form, json.errors);
                    }
                })
                .error(function () {
                    displayErrors($form, ['An unknown error happened. Try again.']);
                });
        }

        e.preventDefault();
    };



    $("#showSignUp").click(function () {
        $("#logInPartial").hide("slide", function () {
            $("#signUpPartial").show("slide", function () {
                $("#Name").focus();
                document.title = "Sign up for RTChat";
            });
        });
    });

    $("#showLogIn").click(function () {
        $("#signUpPartial").hide("slide", function () {
            $("#logInPartial").show("slide", function () {
                $("#LogInToken").focus();
                document.title = "Log in to RTChat";
            });
        });
    });


    $("#LogInToken").focus();
    $("#logInForm").submit(formSubmitHandler);
    $("#signUpForm").submit(formSubmitHandler);
});
