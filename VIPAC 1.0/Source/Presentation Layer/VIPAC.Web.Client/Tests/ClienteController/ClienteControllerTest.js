function TestListarCliente() {
    test("ListarCliente deberia funcionar", function () {
        var filter = {
            ColumnOrder: 'Nombre',
            OrderType: 'Asc',
            CurrentPage: 1,
            AmountRows: 10,
            WhereRule: {
                GroupOp: "AND",
                Rules: [
                    //{ Field: 'Nombre', Value: 'AROTO', Operator: 'cn' }
                    //{ Field: 'CodUsuarioReserva', Value: 'CALVAREZ', Operator: 'eq' }
                ]
            }
        };

        FirstLogin(function (respuesta) {
            assertDone(doAjax({
                url: urlBaseServices + 'api/Cliente/ListarCliente',
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

function TestListarContacto() {
    test("ListarContacto deberia funcionar", function () {
        var filter = {
            ColumnOrder: 'Nombre',
            OrderType: 'Asc',
            CurrentPage: 1,
            AmountRows: 10,
            WhereRule: {
                GroupOp: "AND",
                Rules: [
                    //{ Field: 'Nombre', Value: 'pedro', Operator: 'cn' },
                    //{ Field: 'ClienteId', Value: 'AROTO', Operator: 'eq' }
                ]
            }
        };

        FirstLogin(function (respuesta) {
            assertDone(doAjax({
                url: urlBaseServices + 'api/Cliente/ListarContacto',
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

function ObtenerClientePorId() {
    test("ObtenerClientePorId deberia funcionar", function () {

        FirstLogin(function (respuesta) {
            assertDone(doAjax({
                url: urlBaseServices + 'api/Cliente/ObtenerCliente',
                type: "POST",
                dataType: 'json',
                beforeSend: function (xhr) {
                    xhr.setRequestHeader("Authorization-Token", respuesta.Data.Token);
                },
                crossDomain: true,
                data: { '': 'CUZAM1' },//ESGLO
                error: function (a) {
                    alert(a.responseText);
                }
            }));
        });
    });
}

function MovimientosEstadoCuenta() {
    test("MovimientosEstadoCuenta deberia funcionar", function () {
        var filter = {
            ColumnOrder: 'FechaOperacion',
            OrderType: 'Desc',
            CurrentPage: 1,
            AmountRows: 10,
            WhereRule: {
                GroupOp: "AND",
                Rules: [
                    { Field: 'ClienteId', Value: '123', Operator: 'eq' },
                    { Field: 'FechaOperacion', Value: '11/03/2014 00:00:00', Operator: 'gt' },
                    { Field: 'FechaOperacion', Value: '11/03/2014 23:59:59', Operator: 'lt' }
                ]
            }
        };

        FirstLogin(function (respuesta) {
            assertDone(doAjax({
                url: urlBaseServices + 'api/Cliente/MovimientosEstadoCuenta',
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

function ActualizarEstadoCuenta() {
    var movimiento = {
        ClienteId: '123',
        Descripcion: 'Actualización de saldo',
        Importe: 300.00
    };
    
    test("ActualizarEstadoCuenta deberia funcionar", function () {
        FirstLogin(function (respuesta) {
            assertDone(doAjax({
                url: urlBaseServices + 'api/Cliente/ActualizarEstadoCuenta',
                type: "POST",
                dataType: 'json',
                contentType: 'application/json',
                beforeSend: function (xhr) {
                    xhr.setRequestHeader("Authorization-Token", respuesta.Data.Token);
                },
                crossDomain: true,
                data: JSON.stringify(movimiento),
                error: function (a) {
                    alert(a.responseText);
                }
            }));
        });
    }); 
}

function ObtenerContactoPorId() {
    var contacto = {
        ClienteId: "AROTO",
        NumItem: "02"
    };

    test("ObtenerContactoPorId deberia funcionar", function () {
        FirstLogin(function (respuesta) {
            assertDone(doAjax({
                url: urlBaseServices + 'api/Cliente/ObtenerContacto',
                type: "POST",
                dataType: 'json',
                contentType: 'application/json',
                beforeSend: function (xhr) {
                    xhr.setRequestHeader("Authorization-Token", respuesta.Data.Token);
                },
                crossDomain: true,
                data: JSON.stringify(contacto),
                error: function (a) {
                    alert(a.responseText);
                }
            }));
        });
    });
}

function ModificarCliente() {
    var cliente = {
        Id: 'ESGLO',
        //Nombre: 'Millennium Travel',
        //Ciudad: "Rio de Janeiro",
        //Calle: "Av. Rio Branco, Brasil.",
        //Telefono: "12345677",
        Fax: "12345678",
        Email: "test@GMAIL.COM",
        PaginaWeb:"www.sigcomt.com"
    };

    test("ActualizarCliente deberia funcionar", function () {
        FirstLogin(function (respuesta) {
            assertDone(doAjax({
                url: urlBaseServices + 'api/Cliente/ModificarCliente',
                type: "POST",
                dataType: 'json',
                contentType: 'application/json',
                beforeSend: function (xhr) {
                    xhr.setRequestHeader("Authorization-Token", respuesta.Data.Token);
                },
                crossDomain: true,
                data: JSON.stringify(cliente),
                error: function (a) {
                    alert(a.responseText);
                }
            }));
        });
    });
}

function CrearContacto() {
    var contacto = {
        ClienteId: "ESGLO",
        Cargo: 'DESARROLLADOR',
        Nombre: "pedro",
        Apellido: "zapata",
        Telefono: "123456789",
        Movil: "123456789",
        Email: "pedroze2009@gmail.com",
        ActivarUsuario: false,
        RolList: [{ Id: 2 }]
    };

    test("CrearContacto deberia funcionar", function () {
        FirstLogin(function (respuesta) {
            assertDone(doAjax({
                url: urlBaseServices + 'api/Cliente/CrearContacto',
                type: "POST",
                dataType: 'json',
                contentType: 'application/json',
                beforeSend: function (xhr) {
                    xhr.setRequestHeader("Authorization-Token", respuesta.Data.Token);
                },
                crossDomain: true,
                data: JSON.stringify(contacto),
                error: function (a) {
                    alert(a.responseText);
                }
            }));
        });
    });
}

function ModificarContacto() {
    var contacto = {
        ClienteId: "AROTO",
        NumItem: "01",
        Cargo: 'DESARROLLADOR01',
        Nombre: "OMAR",
        //Apellido: "CUSI",
        //Telefono: "21542154",
        //Movil: "12345677",
        //Email: "CUSIOMAR@GMAIL.COM"
    };

    test("ModificarContacto deberia funcionar", function () {
        FirstLogin(function (respuesta) {
            assertDone(doAjax({
                url: urlBaseServices + 'api/Cliente/ModificarContacto',
                type: "POST",
                dataType: 'json',
                contentType: 'application/json',
                beforeSend: function (xhr) {
                    xhr.setRequestHeader("Authorization-Token", respuesta.Data.Token);
                },
                crossDomain: true,
                data: JSON.stringify(contacto),
                error: function (a) {
                    alert(a.responseText);
                }
            }));
        });
    });
}

function EliminarContacto() {
    var contacto = {
        ClienteId: "AROTO",
        NumItem: "01"
    };
    
    test("EliminarContacto deberia funcionar", function () {
        FirstLogin(function (respuesta) {
            assertDone(doAjax({
                url: urlBaseServices + 'api/Cliente/EliminarContacto',
                type: "POST",
                dataType: 'json',
                contentType: 'application/json',
                beforeSend: function (xhr) {
                    xhr.setRequestHeader("Authorization-Token", respuesta.Data.Token);
                },
                crossDomain: true,
                data: JSON.stringify(contacto),
                error: function (a) {
                    alert(a.responseText);
                }
            }));
        });
    });
}
