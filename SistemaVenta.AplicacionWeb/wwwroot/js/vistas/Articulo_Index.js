const MODELO_BASE = {
    idArticulo: 0,
    codigoBarra: "",
    idMarca: 0,
    idTipoTalle: 0,
    descripcion: "",
    idCategoria: 0,
    costo: "",
    iva: "",
    margenGanancia: "",
    esActivo: 1

}

let tablaData;

$(document).ready(function () {

    fetch("/Categoria/Lista")
        .then(response => {
            return response.ok ? response.json() : Promise.reject(response);
        })
        .then(responseJson => {
            if (responseJson.data.length > 0) {
                responseJson.data.forEach((item) => {
                    $("#cboCategoria").append(
                        $("<option>").val(item.idCategoria).text(item.descripcion)
                    )
                })
            }
        })
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
    fetch("/Marca/Lista")
        .then(response => {
            return response.ok ? response.json() : Promise.reject(response);
        })
        .then(responseJson => {
            if (responseJson.data.length > 0) {
                responseJson.data.forEach((item) => {
                    $("#cboMarca").append(
                        $("<option>").val(item.idMarca).text(item.descripcion)
                    )
                })
            }
        })

    tablaData = $('#tbdata').DataTable({
        responsive: true,
        "ajax": {
            "url": '/Articulo/Lista',
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "idArticulo", "visible": false, "searchable": false },
            { "data": "codigoBarra" },
            { "data": "nombreMarca" },
            { "data": "nombreTipoTalle" },
            { "data": "descripcion" },
            { "data": "nombreCategoria" },
            {
                "data": null, "render": function (data, type, row) {
                    // Calcular el precio de venta
                    var costo = parseFloat(row.costo);
                    var margenGanancia = parseFloat(row.margenGanancia);
                    var iva = parseFloat(row.iva);
                    var netoGravado = costo + (costo * (margenGanancia / 100));
                    var precioVenta = netoGravado + (netoGravado * (iva / 100));

                    // Devolver el precio de venta formateado como moneda
                    return '$' + precioVenta.toFixed(2);
                }
            },
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
                filename: 'Reporte Articulos',
                exportOptions: {
                    columns: [2, 3, 4, 5, 6]
                }
            }, 'pageLength'
        ],
        language: {
            url: "https://cdn.datatables.net/plug-ins/1.11.5/i18n/es-ES.json"
        },
    });
})

function mostrarModal(modelo = MODELO_BASE) {
    $("#txtId").val(modelo.idArticulo)
    $("#txtCodigoBarra").val(modelo.codigoBarra)
    $("#cboMarca").val(modelo.idMarca == 0 ? $("#cboMarca option:first").val() : modelo.idMarca)
    $("#cboTipoTalle").val(modelo.idTipoTalle == 0 ? $("#cboTipoTalle option:first").val() : modelo.idTipoTalle)
    $("#txtDescripcion").val(modelo.descripcion)
    $("#cboCategoria").val(modelo.idCategoria == 0 ? $("#cboCategoria option:first").val() : modelo.idCategoria)
    $("#txtCosto").val(modelo.costo)
    $("#txtIva").val(modelo.iva)
    $("#txtMargenGanancia").val(modelo.margenGanancia)
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
    modelo["idArticulo"] = parseInt($("#txtId").val())
    modelo["codigoBarra"] = $("#txtCodigoBarra").val()
    modelo["idMarca"] = $("#cboMarca").val()
    modelo["idTipoTalle"] = $("#cboTipoTalle").val()
    modelo["descripcion"] = $("#txtDescripcion").val()
    modelo["idCategoria"] = $("#cboCategoria").val()
    modelo["costo"] = $("#txtCosto").val()
    modelo["iva"] = $("#txtIva").val()
    modelo["margenGanancia"] = $("#txtMargenGanancia").val()
    modelo["esActivo"] = $("#cboEstado").val()

    const formData = new FormData();

    formData.append("modelo", JSON.stringify(modelo))

    $("#modalData").find("div.modal-content").LoadingOverlay("show");

    if (modelo.idArticulo == 0) {
        fetch("/Articulo/Crear", {
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
                    swal("Listo!", "El articulo fue creado", "success")
                } else {
                    swal("Lo sentimos", responseJson.mensaje, "error")
                }
            })
    } else {
        fetch("/Articulo/Editar", {
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
                    swal("Listo!", "El articulo fue modificado", "success")
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
        text: `Eliminar el articulo "${data.descripcion}"`,
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

                fetch(`/Articulo/Eliminar?IdArticulo=${data.idArticulo}`, {
                    method: "DELETE",
                })
                    .then(response => {
                        $(".showSweetAlert").LoadingOverlay("hide");
                        return response.ok ? response.json() : Promise.reject(response);
                    })
                    .then(responseJson => {
                        if (responseJson.estado) {
                            tablaData.row(fila).remove().draw()
                            swal("Listo!", "El articulo fue eliminado", "success")
                        } else {
                            swal("Lo sentimos", responseJson.mensaje, "error")
                        }
                    })
            }
        }
    )

})