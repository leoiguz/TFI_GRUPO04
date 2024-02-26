const MODELO_BASE_PAGO = {
    idPago: 0,
    monto: 0,
    idTipoPago: 0
}

$(document).ready(function () {

    fetch("/TipoPago/Lista")
        .then(response => {
            return response.ok ? response.json() : Promise.reject(response);
        })
        .then(responseJson => {
            if (responseJson.data.length > 0) {
                responseJson.data.forEach((item) => {
                    $("#cboTipoPago").append(
                        $("<option>").val(item.idTipoPago).text(item.descripcion)
                    )
                })
            }
        })
})

function mostrarModal(modelo = MODELO_BASE_PAGO) {
    $("#txtId").val(modelo.idPago)
    $("#txtMonto").val(modelo.monto)
    $("#cboTipoPago").val(modelo.idTipoPago == 0 ? $("#cboTipoPago option:first").val() : modelo.idTipoPago)
    $("#modalPago").modal("show")
}

$("#btnPagoVenta").click(function () {
    if (InventariosParaVenta.length < 1) {
        toastr.warning("", "Debe ingresar inventarios")
        return;
    }

    if (ClienteParaVenta.length < 1) {
        toastr.warning("", "Debe ingresar un cliente")
        return;
    }
    mostrarModal()
})