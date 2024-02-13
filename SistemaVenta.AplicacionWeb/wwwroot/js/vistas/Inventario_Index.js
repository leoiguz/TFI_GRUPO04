const MODELO_BASE = {
    idInventario: 0,
    cantidad: 0,
    idArticulo: 0,
    idColor: 0,
    idTalle: 0,
    esActivo: 1

}

let tablaData;

$(document).ready(function () {

    fetch("/Articulo/Lista")
        .then(response => {
            return response.ok ? response.json() : Promise.reject(response);
        })
        .then(responseJson => {
            if (responseJson.data.length > 0) {
                responseJson.data.forEach((item) => {
                    $("#cboArticulo").append(
                        $("<option>").val(item.idArticulo).text(item.descripcion)
                    )
                })
            }
        })
    fetch("/Color/Lista")
        .then(response => {
            return response.ok ? response.json() : Promise.reject(response);
        })
        .then(responseJson => {
            if (responseJson.data.length > 0) {
                responseJson.data.forEach((item) => {
                    $("#cboColor").append(
                        $("<option>").val(item.idColor).text(item.descripcion)
                    )
                })
            }
        })
    fetch("/Talle/Lista")
        .then(response => {
            return response.ok ? response.json() : Promise.reject(response);
        })
        .then(responseJson => {
            if (responseJson.data.length > 0) {
                responseJson.data.forEach((item) => {
                    $("#cboTalle").append(
                        $("<option>").val(item.idTalle).text(item.descripcion)
                    )
                })
            }
        })

    tablaData = $('#tbdata').DataTable({
        responsive: true,
        "ajax": {
            "url": '/Inventario/Lista',
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "idInventario", "visible": false, "searchable": false },
            { "data": "cantidad" },
            { "data": "nombreArticulo" },
            { "data": "nombreColor" },
            { "data": "nombreTalle" },            
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
                filename: 'Reporte Inventarios',
                exportOptions: {
                    columns: [1, 2, 3, 4]
                }
            }, 'pageLength'
        ],
        language: {
            url: "https://cdn.datatables.net/plug-ins/1.11.5/i18n/es-ES.json"
        },
    });
})

function mostrarModal(modelo = MODELO_BASE) {
    $("#txtId").val(modelo.idInventario)
    $("#cboArticulo").val(modelo.idArticulo == 0 ? $("#cboArticulo option:first").val() : modelo.idArticulo)
    $("#cboColor").val(modelo.idColor == 0 ? $("#cboColor option:first").val() : modelo.idColor)
    $("#cboTalle").val(modelo.idTalle == 0 ? $("#cboTalle option:first").val() : modelo.idTalle)
    $("#txtCantidad").val(modelo.cantidad)
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
    modelo["idInventario"] = parseInt($("#txtId").val())
    modelo["idArticulo"] = $("#cboArticulo").val()
    modelo["idColor"] = $("#cboColor").val()
    modelo["idTalle"] = $("#cboTalle").val()
    modelo["cantidad"] = $("#txtCantidad").val()
    modelo["esActivo"] = $("#cboEstado").val()

    const formData = new FormData();

    formData.append("modelo", JSON.stringify(modelo))

    $("#modalData").find("div.modal-content").LoadingOverlay("show");

    if (modelo.idInventario == 0) {
        fetch("/Inventario/Crear", {
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
                    swal("Listo!", "El inventario fue creado", "success")
                } else {
                    swal("Lo sentimos", responseJson.mensaje, "error")
                }
            })
    } else {
        fetch("/Inventario/Editar", {
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
                    swal("Listo!", "El inventario fue modificado", "success")
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
        text: `Eliminar el inventario"`,
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

                fetch(`/Inventario/Eliminar?IdInventario=${data.idInventario}`, {
                    method: "DELETE",
                })
                    .then(response => {
                        $(".showSweetAlert").LoadingOverlay("hide");
                        return response.ok ? response.json() : Promise.reject(response);
                    })
                    .then(responseJson => {
                        if (responseJson.estado) {
                            tablaData.row(fila).remove().draw()
                            swal("Listo!", "El inventario fue eliminado", "success")
                        } else {
                            swal("Lo sentimos", responseJson.mensaje, "error")
                        }
                    })
            }
        }
    )

})