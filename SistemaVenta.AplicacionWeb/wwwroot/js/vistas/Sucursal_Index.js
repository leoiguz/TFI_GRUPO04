$(document).ready(function () {

    $(".card-body").LoadingOverlay("show");

    fetch("Sucursal/Obtener")
        .then(response => {
            $(".card-body").LoadingOverlay("hide");
            return response.ok ? response.json() : Promise.reject(response);
        })
        .then(responseJson => {
            if (responseJson.estado) {
                const d = responseJson.objeto

                $("#txtNumeroDocumento").val(d.numeroDocumento)
                $("#txtRazonSocial").val(d.nombre)
                $("#txtCorreo").val(d.correo)
                $("#txtDomicilio").val(d.domicilio)
                $("#txtCiudad").val(d.ciudad)
                $("#txTelefono").val(d.telefono)
                $("#txtIva").val(d.iva)
                $("#txtMargenGanancia").val(d.margenGanancia)
                $("#txtSimboloMoneda").val(d.simboloMoneda)
       
            } else {
                swal("Lo sentimos!", responseJson.mensaje, "error")
            }
        })
})

$("#btnGuardarCambios").click(function () {

    ;
    const inputs = $("input.input-validar").serializeArray();
    const inputs_sin_valor = inputs.filter((item) => item.value.trim() == "") //devuelve todos los inputs q no tenga valor

    if (inputs_sin_valor.length > 0) {
        const mensaje = `Debe completar el campo: "${inputs_sin_valor[0].name}"`;
        toastr.warning("", mensaje)
        $(`input[name="${inputs_sin_valor[0].name}"]`).focus()
        return;
    }

    const modelo = {
        numeroDocumento: $("#txtNumeroDocumento").val(),
        nombre: $("#txtRazonSocial").val(),
        correo: $("#txtCorreo").val(),
        domicilio: $("#txtDomicilio").val(),
        ciudad: $("#txtCiudad").val(),
        telefono: $("#txTelefono").val(),
        iva: $("#txtIva").val(),
        margenGanancia: $("#txtMargenGanancia").val(),
        simboloMoneda: $("#txtSimboloMoneda").val()

    }
    

    $(".card-body").LoadingOverlay("show");
    fetch("Sucursal/GuardarCambios", {
        method: "POST",
        headers: { "Content-Type": "application/json; charset=utf-8" },
        body: JSON.stringify(modelo)
    })
        .then(response => {
            $(".card-body").LoadingOverlay("hide");
            return response.ok ? response.json() : Promise.reject(response);
        })
        .then(responseJson => {

            if (responseJson.estado) {
                const d = responseJson.objeto
                swal("Listo!", "Se guardaron los datos!", "success")
                             
            } else {
                swal("Lo sentimos!", responseJson.mensaje, "error")
            }
        })

})