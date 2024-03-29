﻿const MODELO_BASE = {
    idCliente: 0,
    numeroDocumento: "",
    nombres: "",
    apellidos: "",
    telefono: "",
    domicilio: "",
    idCondicionTributaria: 0,
    esActivo: 1

}

let tablaData;

$(document).ready(function () {

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

    tablaData = $('#tbdata').DataTable({
        responsive: true,
        "ajax": {
            "url": '/Cliente/Lista',
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "idCliente", "visible": false, "searchable": false },
            { "data": "numeroDocumento" },
            { "data": "nombres" },
            { "data": "apellidos" },
            { "data": "telefono" },
            { "data": "domicilio" },
            { "data": "nombreCondicionTributaria" },
            { "data": "esActivo", render: function (data) { if (data == 1) return '<span class="badge badge-info">Activo</span>'; else return '<span class="badge badge-danger">No Activo</span>'; } },
            {
                "defaultContent": '<button class="btn btn-primary btn-editar btn-sm mr-2"><i class="fas fa-pencil-alt"></i></button>' +
                    '<button class="btn btn-danger btn-eliminar btn-sm"><i class="fas fa-trash-alt"></i></button>',
                "orderable": false,
                "searchable": false,
                "width": "80px"
            }
        ],
        order: [[0, "desc"]],
        dom: "Bfrtip",
        buttons: [
            {
                text: 'Exportar Excel',
                extend: 'excelHtml5',
                title: '',
                filename: 'Reporte Clientes',
                exportOptions: {
                    columns: [1, 2, 3, 4, 5]
                }
            }, 'pageLength'
        ],
        language: {
            url: "https://cdn.datatables.net/plug-ins/1.11.5/i18n/es-ES.json"
        },
    });
})

function mostrarModal(modelo = MODELO_BASE) {
    $("#txtId").val(modelo.idCliente)
    $("#txtNumeroDocumento").val(modelo.numeroDocumento)
    $("#txtNombres").val(modelo.nombres)
    $("#txtApellidos").val(modelo.apellidos)
    $("#txtTelefono").val(modelo.telefono)
    $("#txtDomicilio").val(modelo.domicilio)
    $("#cboCondicionTributaria").val(modelo.idCondicionTributaria == 0 ? $("#cboCondicionTributaria option:first").val() : modelo.idCondicionTributaria)
    $("#cboEstado").val(modelo.esActivo)
    $("#modalData").modal("show")
}

$("#btnNuevo").click(function () {
    mostrarModal()
})

$("#btnGuardar").click(function () {

    const inputs = $("input.input-validar").serializeArray();
    const inputs_sin_valor = inputs.filter((item) => item.value.trim() == "")

    if (inputs_sin_valor.length > 0) {
        const mensaje = `Debe completar el campo : "${inputs_sin_valor[0].name}"`;
        toastr.warning("", mensaje)
        $(`input[name="${inputs_sin_valor[0].name}"]`).focus()
        return;
    }

    const condicionTributariaValidacion = $("#cboCondicionTributaria").val();
    if (condicionTributariaValidacion === '8') {
        const numeroDocumento = $("#txtNumeroDocumento").val();
        if (numeroDocumento.length !== 7 && numeroDocumento.length !== 8) {
            toastr.error("", "El número de documento debe tener 7 u 8 dígitos para la condición tributaria DNI");
            $("#txtNumeroDocumento").focus();
            return;
        }
    }  else if (condicionTributariaValidacion === '6' || condicionTributariaValidacion === '7') {
        const numeroDocumento = $("#txtNumeroDocumento").val();
        if (numeroDocumento.length !== 11) {
            toastr.error("", "El número de documento debe tener 11 dígitos para la condición tributaria CUIT/CUIL");
            $("#txtNumeroDocumento").focus();
            return;
        }
    }



    const modelo = structuredClone(MODELO_BASE);
    modelo["idCliente"] = parseInt($("#txtId").val())
    modelo["numeroDocumento"] = $("#txtNumeroDocumento").val()
    modelo["nombres"] = $("#txtNombres").val()
    modelo["apellidos"] = $("#txtApellidos").val()
    modelo["telefono"] = $("#txtTelefono").val()
    modelo["domicilio"] = $("#txtDomicilio").val()
    modelo["idCondicionTributaria"] = $("#cboCondicionTributaria").val()
    modelo["esActivo"] = $("#cboEstado").val()

    const formData = new FormData();

    formData.append("modelo", JSON.stringify(modelo))

    $("#modalData").find("div.modal-content").LoadingOverlay("show");

    if (modelo.idCliente == 0) {
        fetch("/Cliente/Crear", {
            method: "POST",
            body: formData
        })
            .then(response => {
                $("#modalData").find("div.modal-content").LoadingOverlay("hide");
                return response.ok ? response.json() : Promise.reject(response);
            })
            .then(responseJson => {
                if (responseJson.estado) {
                    tablaData.row.add(responseJson.objeto).draw(false)
                    $("#modalData").modal("hide")
                    swal("Listo!", "El cliente fue creado", "success")
                } else {
                    swal("Lo sentimos", responseJson.mensaje, "error")
                }
            })
    } else {
        fetch("/Cliente/Editar", {
            method: "PUT",
            body: formData
        })
            .then(response => {
                $("#modalData").find("div.modal-content").LoadingOverlay("hide");
                return response.ok ? response.json() : Promise.reject(response);
            })
            .then(responseJson => {
                if (responseJson.estado) {
                    tablaData.row(filaSeleccionada).data(responseJson.objeto).draw(false)
                    filaSeleccionada = null;
                    $("#modalData").modal("hide")
                    swal("Listo!", "El cliente fue modificado", "success")
                } else {
                    swal("Lo sentimos", responseJson.mensaje, "error")
                }
            })
    }

})

let filaSeleccionada;

$("#tbdata tbody").on("click", ".btn-editar", function () {

    if ($(this).closest("tr").hasClass("child")) {
        filaSeleccionada = $(this).closest("tr").prev()
    } else {
        filaSeleccionada = $(this).closest("tr");
    }

    const data = tablaData.row(filaSeleccionada).data();

    mostrarModal(data);

})

$("#tbdata tbody").on("click", ".btn-eliminar", function () {

    let fila;

    if ($(this).closest("tr").hasClass("child")) {
        fila = $(this).closest("tr").prev()
    } else {
        fila = $(this).closest("tr");
    }

    const data = tablaData.row(fila).data();

    swal({
        title: "¿Esta seguro?",
        text: `Eliminar el cliente "${data.numeroDocumento}"`,
        type: "warning",
        showCancelButton: true,
        confirmButtonClass: "btn-danger",
        confirmButtonText: "Si, eliminar",
        cancelButtonText: "No, cancelar",
        closeOnConfirm: false,
        closeOnCancel: true
    },
        function (respuesta) {
            if (respuesta) {
                $(".showSweetAlert").LoadingOverlay("show");

                fetch(`/Cliente/Eliminar?IdCliente=${data.idCliente}`, {
                    method: "DELETE",
                })
                    .then(response => {
                        $(".showSweetAlert").LoadingOverlay("hide");
                        return response.ok ? response.json() : Promise.reject(response);
                    })
                    .then(responseJson => {
                        if (responseJson.estado) {
                            tablaData.row(fila).remove().draw()
                            swal("Listo!", "El cliente fue eliminado", "success")
                        } else {
                            swal("Lo sentimos", responseJson.mensaje, "error")
                        }
                    })
            }
        }
    )

})