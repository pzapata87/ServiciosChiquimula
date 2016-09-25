var urlBaseAuthentication = 'http://localhost:4855/';
//var urlBaseAuthentication = 'http://190.187.158.158:8080/VipacAuthentication/';
//var urlBaseAuthentication = 'http://ACOAzure.cloudapp.net:8080/VipacAuthentication/';
//var urlBaseAuthentication = 'http://vipacauthentication.azurewebsites.net/';

var urlBaseServices = 'http://localhost:34013/';
//var urlBaseServices = 'http://localhost:8080/';
//var urlBaseServices = 'http://190.187.158.158:8080/VipacServices/';
//var urlBaseServices = 'http://ACOAzure.cloudapp.net:8080/VipacServices/';
//var urlBaseServices = 'http://vipacservices.azurewebsites.net/';

function doAjax(settings) {
    settings = $.extend({

    }, settings);

    return $.ajax(settings);
}

function assertDone(promise, msg) {
    msg = msg || "Se esperaba una respuesta SUCCESS en la invocacion AJAX. ";
    promise.done(function (response) {
        if (response.Success)
            ok(true, msg + response.Message);
        else {
            ok(false, msg + response.Message);
        }
    });
    promise.fail(function (response) {
        ok(false, response.responseText);
    });
    return promise;
}

function assertDoneOta(promise, msg) {
    msg = msg || "Se esperaba una respuesta SUCCESS en la invocacion AJAX. ";
    promise.done(function (response) {
        if (!response.Items[0].hasOwnProperty('Error'))
            ok(true, msg + response.Message);
        else {
            ok(false, msg + response.Message);
        }
    });
    promise.fail(function (response) {
        ok(false, response.responseText);
    });
    return promise;
}

function assertFail(promise, msg) {
    msg = msg || "expected error Ajax response";
    promise.done(function () {
        ok(false, msg);
    });
    promise.fail(function () {
        ok(true, msg);
    });
    return promise;
}

function FirstLogin(callbackFunction) {
    var objetoInicioSesion = {
        UserName: "admin",
        Password: "1234"
    };

    return doAjax({
        url: urlBaseAuthentication + 'api/Login/Login',
        type: "POST",
        dataType: 'json',
        contentType: 'application/json',
        crossDomain: true,
        data: JSON.stringify(objetoInicioSesion),
        error: function (a) {
            alert(a.responseText);
        },
        success: function (response) {
            if (callbackFunction != null && typeof callbackFunction == "function")
                callbackFunction(response);
        }
    });
}
