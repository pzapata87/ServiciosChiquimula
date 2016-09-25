function TestGuardarDataTemporal() {
    test("GuardarDataTemporal deberia funcionar", function () {
        var data = {
            Categoria: 'FILE',
            Data: 'file1 file2 file3 file4'
        };

        FirstLogin(function (respuesta) {
            assertDone(doAjax({
                url: urlBaseServices + 'api/Temporal/GuardarDataTemporal',
                type: "POST",
                dataType: 'json',
                contentType: 'application/json',
                beforeSend: function (xhr) {
                    xhr.setRequestHeader("Authorization-Token", respuesta.Data.Token);
                },
                crossDomain: true,
                data: JSON.stringify(data),
                error: function (a) {
                    alert(a.responseText);
                }
            }));
        });
    });
}

function TestObtenerDataTemporal() {
    test("ObtenerDataTemporal deberia funcionar", function () {
        FirstLogin(function (respuesta) {
            assertDone(doAjax({
                url: urlBaseServices + 'api/Temporal/ObtenerDataTemporal',
                type: "POST",
                dataType: 'json',
                beforeSend: function (xhr) {
                    xhr.setRequestHeader("Authorization-Token", respuesta.Data.Token);
                },
                crossDomain: true,
                data: { '': 'FILE' },
                error: function (a) {
                    alert(a.responseText);
                }
            }));
        });
    });
}