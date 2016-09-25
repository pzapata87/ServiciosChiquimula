function TestActivarUsuarioCliente() {
    test("ActivarUsuarioCliente deberia funcionar", function () {

        var usuario = {
            ClienteId: "ESGLO",
            NumItem: "09",
            RolList: [{ Id: 2 }]
        };

        FirstLogin(function (respuesta) {
            assertDone(doAjax({
                url: urlBaseServices + 'api/Usuario/ActivarUsuarioCliente',
                type: "POST",
                dataType: 'json',
                contentType: 'application/json',
                beforeSend: function (xhr) {
                    xhr.setRequestHeader("Authorization-Token", respuesta.Data.Token);
                },
                crossDomain: true,
                data: JSON.stringify(usuario),
                error: function (a) {
                    alert(a.responseText);
                }
            }));
        });
    });
}

function TestActivarUsuarioVipac() {
    test("ActivarUsuarioVipac deberia funcionar", function () {

        var usuario = {
            Id: 2,
            RolList: [{ Id: 1 }]
        };

        FirstLogin(function (respuesta) {
            assertDone(doAjax({
                url: urlBaseServices + 'api/Usuario/ActivarUsuarioVipac',
                type: "POST",
                dataType: 'json',
                contentType: 'application/json',
                beforeSend: function (xhr) {
                    xhr.setRequestHeader("Authorization-Token", respuesta.Data.Token);
                },
                crossDomain: true,
                data: JSON.stringify(usuario),
                error: function (a) {
                    alert(a.responseText);
                }
            }));
        });
    });
}

function TestDesactivarUsuario() {
    test("DesactivarUsuario deberia funcionar", function () {

        FirstLogin(function (respuesta) {
            assertDone(doAjax({
                url: urlBaseServices + 'api/Usuario/DesactivarUsuario',
                type: "POST",
                dataType: 'json',
                beforeSend: function (xhr) {
                    xhr.setRequestHeader("Authorization-Token", respuesta.Data.Token);
                },
                crossDomain: true,
                data: { '': 2 },
                error: function (a) {
                    alert(a.responseText);
                }
            }));
        });
    });
}

function TestCambiarContrasenia() {
    test("CambiarContraseñaUsuario deberia funcionar", function () {

        var changePassword = {            
            Id: 1,
            PasswordOld: "1234",
            PasswordNew: "1234"
        };

        FirstLogin(function (respuesta) {
            assertDone(doAjax({
                url: urlBaseServices + 'api/Usuario/CambiarContrasenia',
                type: "POST",
                dataType: 'json',
                contentType: 'application/json',
                beforeSend: function (xhr) {
                    xhr.setRequestHeader("Authorization-Token", respuesta.Data.Token);
                },
                crossDomain: true,
                data: JSON.stringify(changePassword),
                error: function (a) {
                    alert(a.responseText);
                }
            }));
        });
    });
}

function TestAsignarRolesUsuario() {
    test("AsignarRolesUsuario deberia funcionar", function () {

        var usuario = {
            Id: 3,
            RolList: [{Id: 1}, {Id: 3}]
        };

        FirstLogin(function (respuesta) {
            assertDone(doAjax({
                url: urlBaseServices + 'api/Usuario/AsignarRolesUsuario',
                type: "POST",
                dataType: 'json',
                contentType: 'application/json',
                beforeSend: function (xhr) {
                    xhr.setRequestHeader("Authorization-Token", respuesta.Data.Token);
                },
                crossDomain: true,
                data: JSON.stringify(usuario),
                error: function (a) {
                    alert(a.responseText);
                }
            }));
        });
    });
}

function TestObtenerRoles() {
    test("ObtenerRoles deberia funcionar", function () {

        FirstLogin(function (respuesta) {
            assertDone(doAjax({
                url: urlBaseServices + 'api/Usuario/ObtenerRoles',
                type: "POST",
                dataType: 'json',
                beforeSend: function (xhr) {
                    xhr.setRequestHeader("Authorization-Token", respuesta.Data.Token);
                },
                crossDomain: true,
                data:  { '': 1 },
                error: function (a) {
                    alert(a.responseText);
                }
            }));
        });
    });
}

function TestObtenerUsuario() {
    test("ObtenerUsuario deberia funcionar", function () {

        FirstLogin(function (respuesta) {
            assertDone(doAjax({
                url: urlBaseServices + 'api/Usuario/ObtenerUsuario',
                type: "POST",
                dataType: 'json',
                beforeSend: function (xhr) {
                    xhr.setRequestHeader("Authorization-Token", respuesta.Data.Token);
                },
                crossDomain: true,
                data: { '': 2 },
                error: function (a) {
                    alert(a.responseText);
                }
            }));
        });
    });
}

function TestListarUsuarioVipac() {
    test("ListarUsuarioVipac deberia funcionar", function () {
        var filter = {
            ColumnOrder: 'UserName',
            OrderType: 'Desc',
            CurrentPage: 1,
            AmountRows: 10,
            WhereRule: {
                GroupOp: "AND",
                Rules: [
                    //{ Field: 'UserName', Value: 'admin', Operator: 'cn' }
                    //{ Field: 'Estado', Value: '1', Operator: 'eq' }
                ]
            }
        };
        
        FirstLogin(function (respuesta) {
            assertDone(doAjax({
                url: urlBaseServices + 'api/Usuario/ListarUsuarioVipac',
                type: "POST",
                dataType: 'json',
                contentType: 'application/json',
                beforeSend: function (xhr) {
                    xhr.setRequestHeader("Authorization-Token", respuesta.Data.Token);
                },
                crossDomain: true,
                data: JSON.stringify(filter),
                error: function (a) {
                    alert(a.responseText);
                }
            }));
        });
    });
}

function TestListarUsuarioCliente() {
    test("ListarUsuarioCliente deberia funcionar", function () {
        var filter = {
            ColumnOrder: 'Usuario.UserName',
            OrderType: 'Desc',
            CurrentPage: 1,
            AmountRows: 10,
            WhereRule: {
                GroupOp: "AND",
                Rules: [
                    //{ Field: 'Usuario.UserName', Value: 'abc', Operator: 'cn' }
                    { Field: 'Usuario.Estado', Value: '1', Operator: 'eq' }
                ]
            }
        };

        FirstLogin(function (respuesta) {
            assertDone(doAjax({
                url: urlBaseServices + 'api/Usuario/ListarUsuarioCliente',
                type: "POST",
                dataType: 'json',
                contentType: 'application/json',
                beforeSend: function (xhr) {
                    xhr.setRequestHeader("Authorization-Token", respuesta.Data.Token);
                },
                crossDomain: true,
                data: JSON.stringify(filter),
                error: function (a) {
                    alert(a.responseText);
                }
            }));
        });
    });
}

function TestVerificarUsuario() {
    test("VerificarUsuario deberia funcionar", function () {

        FirstLogin(function (respuesta) {
            assertDone(doAjax({
                url: urlBaseServices + 'api/Usuario/VerificarUsuario',
                type: "POST",
                dataType: 'json',
                beforeSend: function (xhr) {
                    xhr.setRequestHeader("Authorization-Token", respuesta.Data.Token);
                },
                crossDomain: true,
                data: { '': 2 },
                error: function (a) {
                    alert(a.responseText);
                }
            }));
        });
    });
}