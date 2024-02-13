const MODELO_BASE = {
    idTalle: 0,
    descripcion: "",
    idTipoTalle: 0,
    esActivo: 1

}

let tablaData;

$(document).ready(function () {

    fetch("/TipoTalle/Lista")
        .then(response => {
            return response.ok ? response.json() : Promise.reject(response);
        })
        .then(responseJson => {
            if (responseJson.data.length > 0) {
                responseJson.data.forEach((item) => {
                    $("#cboTipoTalle").append(
                        $("<option>").val(item.idTipoTalle).text(item.descripcion)
                    )
                })
            }
        })

    tablaData = $('#tbdata').DataTable({
        responsive: true,
        "ajax": {
            "url": '/Talle/Lista',
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "idTalle", "visible": false, "searchable": false },
            { "data": "descripcion" },
            { "data": "nombreTipoTalle" },    
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
                filename: 'Reporte Talles',
                exportOptions: {
                    columns: [1, 2]
                }
            }, 'pageLength'
        ],
        language: {
            url: "https://cdn.datatables.net/plug-ins/1.11.5/i18n/es-ES.json"
        },
    });
})

function mostrarModal(modelo = MODELO_BASE) {
    $("#txtId").val(modelo.idTalle)
    $("#txtDescripcion").val(modelo.descripcion)
    $("#cboTipoTalle").val(modelo.idTipoTalle == 0 ? $("#cboTipoTalle option:first").val() : modelo.idTipoTalle)
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

    const modelo = structuredClone(MODELO_BASE);
    modelo["idTalle"] = parseInt($("#txtId").val())
    modelo["descripcion"] = $("#txtDescripcion").val()
    modelo["idTipoTalle"] = $("#cboTipoTalle").val()
    modelo["esActivo"] = $("#cboEstado").val()

    const formData = new FormData();

    formData.append("modelo", JSON.stringify(modelo))

    $("#modalData").find("div.modal-content").LoadingOverlay("show");

    if (modelo.idTalle == 0) {
        fetch("/Talle/Crear", {
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
                    swal("Listo!", "El Talle fue creado", "success")
                } else {
                    swal("Lo sentimos", responseJson.mensaje, "error")
                }
            })
    } else {
        fetch("/Talle/Editar", {
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
                    swal("Listo!", "El talle fue modificado", "success")
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
        text: `Eliminar el talle "${data.descripcion}"`,
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

                fetch(`/Talle/Eliminar?IdTalle=${data.idTalle}`, {
                    method: "DELETE",
                })
                    .then(response => {
                        $(".showSweetAlert").LoadingOverlay("hide");
                        return response.ok ? response.json() : Promise.reject(response);
                    })
                    .then(responseJson => {
                        if (responseJson.estado) {
                            tablaData.row(fila).remove().draw()
                            swal("Listo!", "El talle fue eliminado", "success")
                        } else {
                            swal("Lo sentimos", responseJson.mensaje, "error")
                        }
                    })
            }
        }
    )

})