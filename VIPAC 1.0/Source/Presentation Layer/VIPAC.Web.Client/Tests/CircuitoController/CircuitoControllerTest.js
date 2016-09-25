function TestListarCircuito() {
    test("ListarCircuito deberia funcionar", function () {
        var filter = {
            ColumnOrder: 'Descripcion',
            OrderType: 'Asc',
            CurrentPage: 1,
            AmountRows: 10,
            WhereRule: {
                GroupOp: "AND",
                Rules: [
                    //{ Field: 'Descripcion', Value: 'Lima', Operator: 'cn' }
                    //{ Field: 'CodCircuito', Value: 'CACS1', Operator: 'cn' }
                ]//,
                //Groups: [
                //    {
                //        GroupOp: "OR",
                //        Rules: [
                //            { Field: 'ClienteId', Value: 'AROTO', Operator: 'eq' },
                //            { Field: 'Cliente.EsGenerico', Value: 'True', Operator: 'eq' }
                //        ]
                //    }
                //]
            },
            DestinoList: ['CUZCO', 'LIMA']
        };

        FirstLogin(function (respuesta) {
            assertDone(doAjax({
                url: urlBaseServices + 'api/Circuito/ListarCircuito',
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

function TestObtenerCircuito() {
    test("ObtenerCircuito deberia funcionar", function () {
        FirstLogin(function (respuesta) {
            assertDone(doAjax({
                url: urlBaseServices + 'api/Circuito/ObtenerCircuito',
                type: "POST",
                dataType: 'json',
                beforeSend: function (xhr) {
                    xhr.setRequestHeader("Authorization-Token", respuesta.Data.Token);
                },
                crossDomain: true,
                data: { '': '2014-ALL-CACS1-RC' },
                error: function (a) {
                    alert(a.responseText);
                }
            }));
        });
    });
}

function TestObtenerPrecioCircuito() {
    var circuitoFilter = {
        CircuitoId: "2014-ALL-USCA-LCPCS-RC",//2014-ALL-CACS1-RC
        ClienteId: "ESGLO",//AROTO
        NumAdultos: 1,
        NumChild: 0,
        NumChildRoom: 0,
        NumRoomSg: 1,
        NumRoomDb: 0,
        NumRoomTp: 0,
        ClaseHotelId: "E",
        ServAdicionalIncluidoList: []
    };

    test("ObtenerPrecioCircuito deberia funcionar", function () {
        FirstLogin(function (respuesta) {
            assertDone(doAjax({
                url: urlBaseServices + 'api/Circuito/ObtenerPrecioCircuito',
                type: "POST",
                dataType: 'json',
                contentType: 'application/json',
                beforeSend: function (xhr) {
                    xhr.setRequestHeader("Authorization-Token", respuesta.Data.Token);
                },
                crossDomain: true,
                data: JSON.stringify(circuitoFilter),
                error: function (a) {
                    alert(a.responseText);
                }
            }));
        });
    });
}

function TestDisponibilidadCircuito() {
    var circuitoFilter = {
        CircuitoId: "2014-ALL-CACS-RC",
        FechaInicio: '30/11/2014',
        ClienteId: "ESGLO",
        NumAdultos: 1,
        NumChild: 0,
        NumChildRoom: 0,
        NumRoomSg: 1,
        NumRoomDb: 0,
        NumRoomTp: 0,
        ClaseHotelId: "L",
        ServAdicionalIncluidoList: []
    };

    test("DisponibilidadCircuito deberia funcionar", function () {
        FirstLogin(function (respuesta) {
            assertDone(doAjax({
                url: urlBaseServices + 'api/Circuito/DisponibilidadCircuito',
                type: "POST",
                dataType: 'json',
                contentType: 'application/json',
                beforeSend: function (xhr) {
                    xhr.setRequestHeader("Authorization-Token", respuesta.Data.Token);
                },
                crossDomain: true,
                data: JSON.stringify(circuitoFilter),
                error: function (a) {
                    alert(a.responseText);
                }
            }));
        });
    });
}

function TestObtenerCircuitoOta() {
    test("ObtenerCircuitoOta deberia funcionar", function () {
        var pkgRequest = {
            PackageRequest: {
                ID: '2',
                //ShortDescription: 'cuzco',
                //TourCode: 'CUZC',
                DateRange: {
                    Start: '31/03/2014',
                    End: '20/04/2014'
                }
            }
        };

        FirstLogin(function (respuesta) {
            assertDoneOta(doAjax({
                url: urlBaseServices + 'api/Circuito/ObtenerCircuitoOta',
                type: "POST",
                dataType: 'json',
                contentType: 'application/json',
                beforeSend: function (xhr) {
                    xhr.setRequestHeader("Authorization-Token", respuesta.Data.Token);
                },
                crossDomain: true,
                data: JSON.stringify(pkgRequest),
                error: function (a) {
                    alert(a.responseText);
                }
            }));
        });
    });
}

function ObtenerHotel() {
    test("ObtenerHotel deberia funcionar", function () {
        var hotelAvailRQ = {
            AvailRequestSegments: {
                AvailRequestSegment: [{
                    HotelSearchCriteria: {
                        Criterion: [{
                            HotelRef: [{
                                //  HotelName: "11",
                                HotelCode: "1",
                            }]
                        }]
                    },

                }]
            }
        };

        FirstLogin(function (respuesta) {
            assertDoneOta(doAjax({
                url: urlBaseServices + 'api/Hotel/ObtenerHotelOta',
                type: "POST",
                dataType: 'json',
                contentType: 'application/json',
                beforeSend: function (xhr) {
                    xhr.setRequestHeader("Authorization-Token", respuesta.Data.Token);
                },
                crossDomain: true,
                data: JSON.stringify(hotelAvailRQ),
                error: function (a) {
                    alert(a.responseText);
                }
            }));
        });
    });
}

