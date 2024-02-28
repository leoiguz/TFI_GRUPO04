$(document).ready(function () {

    $(".card-body").LoadingOverlay("show");

    fetch("/CondicionTributaria/Lista")
        .then(response => {
            return response.ok ? response.json() : Promise.reject(response);
        })
        .then(responseJson => {
            if (responseJson.data.length > 0) {
                responseJson.data.forEach((item) => {
                    $("#cboCondicionTributaria").append(
                        $("<option>").val(item.idCondicionTributaria).text(item.nombre)
                    )
                })
            }
        })

    fetch("/Sucursal/Obtener")
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
                $("#cboCondicionTributaria").val(d.idCondicionTributaria == 0 ? $("#cboCondicionTributaria option:first").val() : d.idCondicionTributaria)
       
            } else {
                swal("Lo sentimos!", responseJson.mensaje, "error")
            }
        })
})

$("#btnGuardarCambios").click(function () {

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
        idCondicionTributaria: $("#cboCondicionTributaria").val()
    }

    console.log(modelo);
    
    const formData = new FormData()
    formData.append("modelo", JSON.stringify(modelo))

    $(".card-body").LoadingOverlay("show");
    fetch("/Sucursal/GuardarCambios", {
        method: "POST",
        body: formData
    })
        .then(response => {
            $(".card-body").LoadingOverlay("hide");
            return response.ok ? response.json() : Promise.reject(response);
        })
        .then(responseJson => {

            if (responseJson.estado) {
                const d = responseJson.objeto
                             
            } else {
                swal("Lo sentimos!", responseJson.mensaje, "error")
            }
        })

})