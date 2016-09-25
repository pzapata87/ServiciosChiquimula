function TestListarHotel() {
    test("ListarHotel deberia funcionar", function () {
        var filter = {
            ColumnOrder: 'Nombre',
            OrderType: 'Desc',
            CurrentPage: 1,
            AmountRows: 10,
            WhereRule: {
                GroupOp: "AND",
                Rules: [
                    //{ Field: 'Nombre', Value: 'Pa', Operator: 'cn' },
                    { Field: 'ClaseHotelId', Value: 'E', Operator: 'eq' }
                    //{ Field: 'ClaseHotel.Descripcion', Value: 'Pa', Operator: 'cn' }
                ]
                //Groups: [
                //    {
                //        GroupOp: "OR",
                //        Rules: [
                //            { Field: 'Nombre', Value: 'Pa', Operator: 'cn' },
                //            { Field: 'Nombre', Value: 'Lim', Operator: 'cn' }
                //        ]
                //    }
                //]
            }
        };

        FirstLogin(function (respuesta) {
            assertDone(doAjax({
                url: urlBaseServices + 'api/Hotel/ListarHotel',
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

function TestObtenerHotel() {
    test("ObtenerHotel deberia funcionar", function () {
        FirstLogin(function (respuesta) {
            assertDone(doAjax({
                url: urlBaseServices + 'api/Hotel/ObtenerHotel',
                type: "POST",
                dataType: 'json',
                beforeSend: function (xhr) {
                    xhr.setRequestHeader("Authorization-Token", respuesta.Data.Token);
                },
                crossDomain: true,
                data: { '': 'SRZYOT' },
                error: function (a) {
                    alert(a.responseText);
                }
            }));
        });
    });
}

function TestObtenerClaseHotel() {
    test("ObtenerClaseHotel deberia funcionar", function () {
        FirstLogin(function (respuesta) {
            assertDone(doAjax({
                url: urlBaseServices + 'api/Hotel/ObtenerClaseHotel',
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

function TestObtenerPrecio() {
    var hotelFilter = {
        ClienteId: "ESGLO",
        NumAdultos: 1,
        NumChildRoom: 1,
        HotelId: "SRZYOT",
        HotelRoomId: "SRZYOT-SRZYOT",
        NumNoches:2,
        NumRoomSg:0,
        NumRoomDb:1,
        NumRoomTp:0
    };

    test("ObtenerPrecio deberia funcionar", function () {
        FirstLogin(function (respuesta) {
            assertDone(doAjax({
                url: urlBaseServices + 'api/Hotel/ObtenerPrecioHotel',
                type: "POST",
                dataType: 'json',
                contentType: 'application/json',
                beforeSend: function (xhr) {
                    xhr.setRequestHeader("Authorization-Token", respuesta.Data.Token);
                },
                crossDomain: true,
                data: JSON.stringify(hotelFilter),
                error: function (a) {
                    alert(a.responseText);
                }
            }));
        });
    });
}

function TestDisponibilidadHotel() {
    var hotelFilter = {
        ClienteId: "ALL",
        NumAdultos: 2,
        NumChildRoom: 0,
        HotelId: "CUZJOS",
        HotelRoomId: "",
        FechaInicio: "04/11/2014",
        NumNoches: 1,
        NumRoomSg: 2,
        NumRoomDb: 0,
        NumRoomTp: 0
    };

    test("DisponibilidadHotel deberia funcionar", function () {
        FirstLogin(function (respuesta) {
            assertDone(doAjax({
                url: urlBaseServices + 'api/Hotel/DisponibilidadHotel',
                type: "POST",
                dataType: 'json',
                contentType: 'application/json',
                beforeSend: function (xhr) {
                    xhr.setRequestHeader("Authorization-Token", respuesta.Data.Token);
                },
                crossDomain: true,
                data: JSON.stringify(hotelFilter),
                error: function (a) {
                    alert(a.responseText);
                }
            }));
        });
    });
}