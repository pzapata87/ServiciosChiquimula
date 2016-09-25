function TestLogin() {
    test("Login deberia funcionar", function() {
        assertDone(FirstLogin());
    });
}

function TestResetPassword() {
    test("ResetPassword deberia funcionar", function () {

        assertDone(doAjax({
            url: urlBaseAuthentication + 'api/Login/ResetPassword',
            type: "POST",
            dataType: 'json',
            crossDomain: true,
            data: { '': 'Admin' },
            error: function (a) {
                alert(a.responseText);
            }
        }));
    });
}