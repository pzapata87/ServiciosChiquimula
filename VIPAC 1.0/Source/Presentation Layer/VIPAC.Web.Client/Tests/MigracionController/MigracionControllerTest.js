function TestListarConfiguracion() {
    test("ListarConfiguracion deberia funcionar", function () {
        var filter = {
            ColumnOrder: 'Id',
            OrderType: 'Asc',
            CurrentPage: 1,
            AmountRows: 10,
            WhereRule: {
                GroupOp: "AND",
                Rules: []
            }
        };

        FirstLogin(function (respuesta) {
            assertDone(doAjax({
                url: urlBaseServices + 'api/Migracion/ListarConfiguracion',
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

function TestObtenerConfiguracionPorId() {
    test("ObtenerConfiguracion deberia funcionar", function () {

        FirstLogin(function (respuesta) {
            assertDone(doAjax({
                url: urlBaseServices + 'api/Migracion/ObtenerConfiguracion',
                type: "POST",
                dataType: 'json',
                beforeSend: function (xhr) {
                    xhr.setRequestHeader("Authorization-Token", respuesta.Data.Token);
                },
                crossDomain: true,
                data: { '': 1 },
                error: function (a) {
                    alert(a.responseText);
                }
            }));
        });
    });
}

function TestObtenerFrecuencias() {
    test("ObtenerFrecuencias deberia funcionar", function () {

        FirstLogin(function (respuesta) {
            assertDone(doAjax({
                url: urlBaseServices + 'api/Migracion/ObtenerFrecuencias',
                type: "POST",
                dataType: 'json',
                beforeSend: function (xhr) {
                    xhr.setRequestHeader("Authorization-Token", respuesta.Data.Token);
                },
                crossDomain: true,
                error: function (a) {
                    alert(a.responseText);
                }
            }));
        });
    });
}

function TestObtenerConceptos() {
    test("ObtenerConceptos deberia funcionar", function () {

        FirstLogin(function (respuesta) {
            assertDone(doAjax({
                url: urlBaseServices + 'api/Migracion/ObtenerConceptos',
                type: "POST",
                dataType: 'json',
                beforeSend: function (xhr) {
                    xhr.setRequestHeader("Authorization-Token", respuesta.Data.Token);
                },
                crossDomain: true,
                error: function (a) {
                    alert(a.responseText);
                }
            }));
        });
    });
}

function TestEjecutarConceptosManual() {
    var conceptos = [5];

    test("EjecutarConceptosManual deberia funcionar", function () {
        
        FirstLogin(function (respuesta) {
            assertDone(doAjax({
                url: urlBaseServices + 'api/Migracion/EjecutarConceptosManual',
                type: "POST",
                dataType: 'json',
                contentType: 'application/json',
                beforeSend: function (xhr) {
                    xhr.setRequestHeader("Authorization-Token", respuesta.Data.Token);
                },
                crossDomain: true,
                data: JSON.stringify(conceptos),
                error: function (a) {
                    alert(a.responseText);
                }
            }));
        });
    });
}

function TestActivarMigracion() {
    var configuracionDiaria = {
        ConceptoId: '1',
        FechaInicio: '08/09/2014 22:30',
        FrecuenciaId: '1',
        IntervaloFrecuencia: '1'
    };
    
    var configuracionSemanal = {
        ConceptoId: '1',
        FechaInicio: '08/09/2014 22:30',
        FrecuenciaId: '2',
        IntervaloFrecuencia: '1',
        DiasRepeticion: 'L,M'
    };
    
    var configuracionPersonalizada = {
        ConceptoId: '1',
        FrecuenciaId: '3',
        ExpresionCron: '0 59 23 ? * MON-FRI *'
    };

    test("ActivarMigracion deberia funcionar", function () {
        
        FirstLogin(function (respuesta) {
            assertDone(doAjax({
                url: urlBaseServices + 'api/Migracion/ActivarMigracion',
                type: "POST",
                dataType: 'json',
                contentType: 'application/json',
                beforeSend: function (xhr) {
                    xhr.setRequestHeader("Authorization-Token", respuesta.Data.Token);
                },
                crossDomain: true,
                data: JSON.stringify(configuracionSemanal),
                error: function (a) {
                    alert(a.responseText);
                }
            }));
        });
    });
}

function TestDesactivarMigracion() {
    test("DesactivarMigracion deberia funcionar", function () {
        
        FirstLogin(function (respuesta) {
            assertDone(doAjax({
                url: urlBaseServices + 'api/Migracion/DesactivarMigracion',
                type: "POST",
                dataType: 'json',
                beforeSend: function (xhr) {
                    xhr.setRequestHeader("Authorization-Token", respuesta.Data.Token);
                },
                crossDomain: true,
                data: { '': 1 },
                error: function (a) {
                    alert(a.responseText);
                }
            }));
        });
    });
}