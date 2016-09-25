function TestListarServicio() {
    test("ListarServicio deberia funcionar", function () {
        var filter = {
            ColumnOrder: 'Descripcion',
            OrderType: 'Desc',
            CurrentPage: 1,
            AmountRows: 10,
            WhereRule: {
                GroupOp: "AND",
                Rules: [
                    //{ Field: 'Descripcion', Value: 'Pa', Operator: 'cn' }
                ]
            }
        };

        FirstLogin(function (respuesta) {
            assertDone(doAjax({
                url: urlBaseServices + 'api/Servicio/ListarServicio',
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

function TestObtenerServicio() {
    test("ObtenerServicio deberia funcionar", function () {
        FirstLogin(function (respuesta) {
            assertDone(doAjax({
                url: urlBaseServices + 'api/Servicio/ObtenerServicio',
                type: "POST",
                dataType: 'json',
                beforeSend: function (xhr) {
                    xhr.setRequestHeader("Authorization-Token", respuesta.Data.Token);
                },
                crossDomain: true,
                data: { '': 'AQPZMG1' },
                error: function (a) {
                    alert(a.responseText);
                }
            }));
        });
    });
}

function TestObtenerPrecio() {
    var servicioFilter = {
        ClienteId: "ESGLO",
        NumAdultos: 1,
        NumChild: 1,
        ServicioId: "AQPZMG1",
        EsPrivado: true
    };

    test("ObtenerPrecio deberia funcionar", function () {
        FirstLogin(function (respuesta) {
            assertDone(doAjax({
                url: urlBaseServices + 'api/Servicio/ObtenerPrecioServicio',
                type: "POST",
                dataType: 'json',
                contentType: 'application/json',
                beforeSend: function (xhr) {
                    xhr.setRequestHeader("Authorization-Token", respuesta.Data.Token);
                },
                crossDomain: true,
                data: JSON.stringify(servicioFilter),
                error: function (a) {
                    alert(a.responseText);
                }
            }));
        });
    });
}

function TestDisponibilidadServicio() {
    var servicioFilter = {
        ServicioId: "AQPZMG1",
        FechaInicio: "20/10/2014",
        EsPrivado: true
    };

    test("DisponibilidadServicio deberia funcionar", function () {
        FirstLogin(function (respuesta) {
            assertDone(doAjax({
                url: urlBaseServices + 'api/Servicio/DisponibilidadServicio',
                type: "POST",
                dataType: 'json',
                contentType: 'application/json',
                beforeSend: function (xhr) {
                    xhr.setRequestHeader("Authorization-Token", respuesta.Data.Token);
                },
                crossDomain: true,
                data: JSON.stringify(servicioFilter),
                error: function (a) {
                    alert(a.responseText);
                }
            }));
        });
    });
}