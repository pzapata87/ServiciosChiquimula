function TestListarCotizacion() {
    test("ListarCotizacion deberia funcionar", function () {
        var filter = {
            ColumnOrder: 'FechaApertura',
            OrderType: 'Desc',
            CurrentPage: 1,
            AmountRows: 10,
            WhereRule: {
                GroupOp: "AND",
                Rules: [
                    //{ Field: 'ClienteId', Value: '1', Operator: 'eq' },
                    //{ Field: 'NumCotizacion', Value: '000000001W', Operator: 'cn' }
                    { Field: 'FechaApertura', Value: '2014-10-24', Operator: 'gt' },
                    { Field: 'FechaApertura', Value: '2014-10-24 23:59:59', Operator: 'lt' }
                ]
            }
        };

        FirstLogin(function (respuesta) {
            assertDone(doAjax({
                url: urlBaseServices + 'api/File/ListarCotizacion',
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

function TestObtenerFile() {
    test("ObtenerFile deberia funcionar", function () {
        FirstLogin(function (respuesta) {
            assertDone(doAjax({
                url: urlBaseServices + 'api/File/ObtenerFile',
                type: "POST",
                dataType: 'json',
                beforeSend: function (xhr) {
                    xhr.setRequestHeader("Authorization-Token", respuesta.Data.Token);
                },
                crossDomain: true,
                data: { '': '2014-000000025W-RC' },
                error: function (a) {
                    alert(a.responseText);
                }
            }));
        });
    });
}

function TestObtenerCotizacion() {
    test("ObtenerCotizacion deberia funcionar", function () {
        FirstLogin(function (respuesta) {
            assertDone(doAjax({
                url: urlBaseServices + 'api/File/ObtenerCotizacion',
                type: "POST",
                dataType: 'json',
                beforeSend: function (xhr) {
                    xhr.setRequestHeader("Authorization-Token", respuesta.Data.Token);
                },
                crossDomain: true,
                data: { '': '2014-000000015W-RC' },
                error: function (a) {
                    alert(a.responseText);
                }
            }));
        });
    });
}

function TestCrearCotizacionCircuito() {
    var cotizacion = {
        NombreFile: 'Ximena x2',
        ClienteId: 'ESGLO',
        NumAdultos: 2,
        NumChild: 0,
        CircuitoFilterList: [
            {
                CircuitoId: '2014-ALL-CACS-RC',
                FechaInicio: '14/12/2014',
                NumRoomSg: 0,
                NumRoomDb: 1,
                NumRoomTp: 0,
                ClaseHotelId: 'L',
                ServIndividualList: [],
                HotelIndividualList: []
            }
        ]
    };

    test("CrearCotizacion - Circuito deberia funcionar", function() {
        FirstLogin(function(respuesta) {
            assertDone(doAjax({
                url: urlBaseServices + 'api/File/CrearCotizacion',
                type: "POST",
                dataType: 'json',
                contentType: 'application/json',
                beforeSend: function(xhr) {
                    xhr.setRequestHeader("Authorization-Token", respuesta.Data.Token);
                },
                crossDomain: true,
                data: JSON.stringify(cotizacion),
                error: function(a) {
                    alert(a.responseText);
                }
            }));
        });
    });
}

function TestCrearFileCircuito() {
    var file = {
        NombreFile: 'Ximena x2',
        ClienteId: 'ESGLO',
        NumAdultos: 2,
        NumChild: 0,
        CircuitoFilterList: [
            {
                CircuitoId: '2014-ALL-CACS-RC',
                FechaInicio: '12/02/2015',
                NumRoomSg: 0,
                NumRoomDb: 1,
                NumRoomTp: 0,
                ClaseHotelId: 'L',
                ServIndividualList: [],
                HotelIndividualList: []
            }
        ]
    };

    test("CrearFile - Circuito deberia funcionar", function () {
        FirstLogin(function (respuesta) {
            assertDone(doAjax({
                url: urlBaseServices + 'api/File/CrearFile',
                type: "POST",
                dataType: 'json',
                contentType: 'application/json',
                beforeSend: function (xhr) {
                    xhr.setRequestHeader("Authorization-Token", respuesta.Data.Token);
                },
                crossDomain: true,
                data: JSON.stringify(file),
                error: function (a) {
                    alert(a.responseText);
                }
            }));
        });
    });
}

function TestModificarCotizacionCircuito() {
    //var cotizacion = {
    //    CotizacionId: '2014-000000061W-RC',
    //    NombreFile: 'Ximena x3',
    //    ClienteId: 'AROTO',
    //    NumAdulto: 3,
    //    NumChild: 0,
    //    CircuitoFilterList: [
    //        {
    //            CircuitoId: '2014-CLV-RC',
    //            FechaInicio: '14/08/2014',
    //            NumRoomSg: 1,
    //            NumRoomDb: 1,
    //            NumRoomTp: 0,
    //            ClaseHotelId: 'L',
    //            ServIndividualList: [],
    //            HotelIndividualList: []
    //        }
    //    ]
    //};

    var cotizacion = {
        Id: "2014-000000001W-RC",
        NombreFile: "Cotización x1, modificada",
        NumAdultos: 5,
        NumChild: 0,
        NumChildRoom: 0,
        CircuitoFilterList: [
            {
                CircuitoId: "2014-ALL-CACSE-RC",
                FechaInicio: "2014-10-12",
                NumRoomSg: 1,
                NumRoomDb: 2,
                NumRoomTp: 0,
                ClaseHotelId: "E"
            }
        ],
        ServIndividualList: [],
        HotelIndividualList: [],
        PaxList: []
    };

    test("ModificarCotizacion - Circuito deberia funcionar", function () {
        FirstLogin(function (respuesta) {
            assertDone(doAjax({
                url: urlBaseServices + 'api/File/ModificarCotizacion',
                type: "POST",
                dataType: 'json',
                contentType: 'application/json',
                beforeSend: function (xhr) {
                    xhr.setRequestHeader("Authorization-Token", respuesta.Data.Token);
                },
                crossDomain: true,
                data: JSON.stringify(cotizacion),
                error: function (a) {
                    alert(a.responseText);
                }
            }));
        });
    });
}

function TestCrearCotizacionHotel() {
    var cotizacion = {
        NombreFile: 'Ximena x2',
        ClienteId: 'AROTO',
        NumAdultos: 2,
        NumChild: 0,
        HotelIndividualList: [
            {
                HotelRoomId: 'SRZYOT-SRZYOT',
                NumNoches: 4,
                NumRoomSg: 0,
                NumRoomDb: 1,
                NumRoomTp: 0,
                FechaInicio: '09/12/2014'
            }
        ]
    };

    test("CrearCotizacion - Hotel deberia funcionar", function () {
        FirstLogin(function (respuesta) {
            assertDone(doAjax({
                url: urlBaseServices + 'api/File/CrearCotizacion',
                type: "POST",
                dataType: 'json',
                contentType: 'application/json',
                beforeSend: function (xhr) {
                    xhr.setRequestHeader("Authorization-Token", respuesta.Data.Token);
                },
                crossDomain: true,
                data: JSON.stringify(cotizacion),
                error: function (a) {
                    alert(a.responseText);
                }
            }));
        });
    });
}

function TestCrearFileHotel() {
    var cotizacion = {
        NombreFile: 'Ximena x2',
        ClienteId: 'AROTO',
        NumAdultos: 2,
        NumChild: 0,
        HotelIndividualList: [
            {
                HotelRoomId: 'SRZYOT-SRZYOT',
                NumNoches: 4,
                NumRoomSg: 0,
                NumRoomDb: 1,
                NumRoomTp: 0,
                FechaInicio: '12/14/2014'
            }
        ]
    };

    test("CrearFile - Hotel deberia funcionar", function () {
        FirstLogin(function (respuesta) {
            assertDone(doAjax({
                url: urlBaseServices + 'api/File/CrearFile',
                type: "POST",
                dataType: 'json',
                contentType: 'application/json',
                beforeSend: function (xhr) {
                    xhr.setRequestHeader("Authorization-Token", respuesta.Data.Token);
                },
                crossDomain: true,
                data: JSON.stringify(cotizacion),
                error: function (a) {
                    alert(a.responseText);
                }
            }));
        });
    });
}

function TestCrearCotizacionServicio() {
    var cotizacion = {
        NombreFile: 'Ximena x2',
        ClienteId: 'AROTO',
        NumAdulto: 2,
        NumChild: 0,
        ServIndividualList: [
            {
                ServicioId: 'AQPVCF1',
                FechaInicio: '09/08/2014',
                EsPrivado: true
            }
        ]
    };

    test("CrearCotizacion - Servicio deberia funcionar", function () {
        FirstLogin(function (respuesta) {
            assertDone(doAjax({
                url: urlBaseServices + 'api/File/CrearCotizacion',
                type: "POST",
                dataType: 'json',
                contentType: 'application/json',
                beforeSend: function (xhr) {
                    xhr.setRequestHeader("Authorization-Token", respuesta.Data.Token);
                },
                crossDomain: true,
                data: JSON.stringify(cotizacion),
                error: function (a) {
                    alert(a.responseText);
                }
            }));
        });
    });
}

function TestCrearFileServicio() {
    var cotizacion = {
        NombreFile: 'Ximena x2',
        ClienteId: 'AROTO',
        NumAdultos: 2,
        NumChild: 0,
        ServIndividualList: [
            {
                ServicioId: 'AGCCHMC',
                FechaInicio: '16/12/2014',
                EsPrivado: true
            }
        ]
    };

    test("CrearFile - Servicio deberia funcionar", function () {
        FirstLogin(function (respuesta) {
            assertDone(doAjax({
                url: urlBaseServices + 'api/File/CrearFile',
                type: "POST",
                dataType: 'json',
                contentType: 'application/json',
                beforeSend: function (xhr) {
                    xhr.setRequestHeader("Authorization-Token", respuesta.Data.Token);
                },
                crossDomain: true,
                data: JSON.stringify(cotizacion),
                error: function (a) {
                    alert(a.responseText);
                }
            }));
        });
    });
}

function TestListarFile() {
    test("ListarFile deberia funcionar", function () {
        var filter = {
            ColumnOrder: 'FechaApertura',
            OrderType: 'Desc',
            CurrentPage: 1,
            AmountRows: 10,
            WhereRule: {
                GroupOp: "AND",
                Rules: [
                    //{ Field: 'ClienteId', Value: '1', Operator: 'eq' },
                    //{ Field: 'NumFile', Value: '000000001W', Operator: 'cn' }
                ]
            }
        };

        FirstLogin(function (respuesta) {
            assertDone(doAjax({
                url: urlBaseServices + 'api/File/ListarFile',
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

function TestCrearFileDeCotizacion() {
    var cotizacion = {
        Id: '2014-000000001W-RC',
        NombreFile: 'Ximena x2',
        ClienteId: 'ESGLO',
        NumAdultos: 2,
        NumChild: 0,
        CircuitoFilterList: [
            {
                CircuitoId: '2014-ALL-CACS-RC',
                FechaInicio: '17/10/2014',
                NumRoomSg: 0,
                NumRoomDb: 1,
                NumRoomTp: 0,
                ClaseHotelId: 'L',
                ServIndividualList: [],
                HotelIndividualList: []
            }
        ]
        //HotelIndividualList: [
        //    {
        //        HotelRoomId: 'LIMCAM-LIMCAM',
        //        NumNoches: 4,
        //        NumRoomSg: 0,
        //        NumRoomDb: 1,
        //        NumRoomTp: 0,
        //        FechaInicio: '09/08/2014'                
        //    }
        //]
    };

    test("CrearFileDeCotizacion deberia funcionar", function () {
        FirstLogin(function (respuesta) {
            assertDone(doAjax({
                url: urlBaseServices + 'api/File/CrearFile',
                type: "POST",
                dataType: 'json',
                contentType: 'application/json',
                beforeSend: function (xhr) {
                    xhr.setRequestHeader("Authorization-Token", respuesta.Data.Token);
                },
                crossDomain: true,
                data: JSON.stringify(cotizacion),
                error: function (a) {
                    alert(a.responseText);
                }
            }));
        });
    });
}

function TestReservasEjecutadas() {
    test("ReservasEjecutadas deberia funcionar", function () {
        FirstLogin(function (respuesta) {
            assertDone(doAjax({
                url: urlBaseServices + 'api/File/ReservasEjecutadas',
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

function TestReservasPendientes() {
    test("ReservasPendientes deberia funcionar", function () {
        FirstLogin(function (respuesta) {
            assertDone(doAjax({
                url: urlBaseServices + 'api/File/ReservasPendientes',
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

function TestReservasAnuladas() {
    test("ReservasAnuladas deberia funcionar", function () {
        FirstLogin(function (respuesta) {
            assertDone(doAjax({
                url: urlBaseServices + 'api/File/ReservasAnuladas',
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

function TestActualizarDatosPax() {
    var pax = {
        Id: '2014-000000025W-RC',
        PaxList: [
            {
                NumItem: '001',
                Nombre: 'test',
                Apellido: 'test',
                DocIdentidad: '123456789',
                FechaNacimiento: '10/04/1988',
                Nacionalidad: 'Peruanna',
                EsNiño: true,
                EsExtranjero: true,
                CodReservaAereo: 'test',
                Habitacion: 'doble',
                NotaHabitacion: 'nota'
            },
            {
                NumItem: '002',
                Nombre: 'test2',
                Apellido: 'test2',
                DocIdentidad: '123456789',
                FechaNacimiento: '10/04/1988',
                Nacionalidad: 'Peruanna',
                EsNiño: true,
                EsExtranjero: false,
                CodReservaAereo: 'test2',
                Habitacion: 'doble',
                NotaHabitacion: 'nota'
            }
        ]
    };

    test("ActualizarDatosPax deberia funcionar", function () {
        FirstLogin(function (respuesta) {
            assertDone(doAjax({
                url: urlBaseServices + 'api/File/ActualizarDatosPax',
                type: "POST",
                dataType: 'json',
                contentType: 'application/json',
                beforeSend: function (xhr) {
                    xhr.setRequestHeader("Authorization-Token", respuesta.Data.Token);
                },
                crossDomain: true,
                data: JSON.stringify(pax),
                error: function (a) {
                    alert(a.responseText);
                }
            }));
        });
    });
}