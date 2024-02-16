let ValorImpuesto = 0;

$(document).ready(function () {


    fetch("/Venta/ListaTipoComprobante")
        .then(response => {
            return response.ok ? response.json() : Promise.reject(response);
        })
        .then(responseJson => {
            if (responseJson.length > 0) {
                responseJson.forEach((item) => {
                    $("#cboTipoComprobante").append(
                        $("<option>").val(item.idTipoComprobante).text(item.descripcion)
                    )
                })
            }
        })


    fetch("/Sucursal/Obtener")
        .then(response => {
            return response.ok ? response.json() : Promise.reject(response);
        })
        .then(responseJson => {

            if (responseJson.estado) {

                const d = responseJson.objeto;

                console.log(d)

                $("#inputGroupSubTotal").text(`Sub total - ${d.simboloMoneda}`)
                $("#inputGroupIGV").text(`IGV(${d.porcentajeImpuesto}%) - ${d.simboloMoneda}`)
                $("#inputGroupTotal").text(`Total - ${d.simboloMoneda}`)

                ValorImpuesto = parseFloat(d.porcentajeImpuesto)
            }

        })

    $("#cboBuscarInventario").select2({
        ajax: {
            url: "/Venta/ObtenerInventarios",
            dataType: 'json',
            contentType: "application/json; charset=utf-8",
            delay: 250,
            data: function (params) {
                return {
                    busqueda: params.term
                };
            },
            processResults: function (data,) {

                return {
                    results: data.map((item) => (
                        {
                            id: item.idInventario,

                            articulo: item.nombreArticulo,
                            color: item.nombreColor,
                            talle: item.nombreTalle,
                            precio: parseFloat(item.precioInventario)
                        }
                    ))
                };
            }
        },
        language: "es",
        placeholder: 'Buscar Inventario...',
        minimumInputLength: 1,
        templateResult: formatoResultados
    });

})

function formatoResultados(data) {
    if (data.loading)
        return data.text;

    var contenedor = $(
        `<table width="100%">
            <tr>
                <td>
                    <p style="font-weight: bolder;margin:2px">${data.articulo}</p>
                    <p style="margin:2px">${data.color}</p>
                    <p style="margin:2px">${data.talle}</p>
                </td>
            </tr>
         </table>`
    );

    return contenedor;
}

$(document).on("select2:open", function () {
    document.querySelector(".select2-search__field").focus();
})

let InventariosParaVenta = [];
$("#cboBuscarInventario").on("select2:select", function (e) {
    const data = e.params.data;

    let inventario_encontrado = InventariosParaVenta.filter(p => p.idInventario == data.id)
    if (inventario_encontrado.length > 0) {
        $("#cboBuscarInventario").val("").trigger("change")
        toastr.warning("", "El inventario ya fue agregado")
        return false
    }

    swal({
        title: data.articulo,
        type: "input",
        showCancelButton: true,
        closeOnConfirm: false,
        inputPlaceholder: "Ingrese Cantidad"
    },
        function (valor) {

            if (valor === false) return false;

            if (valor === "") {
                toastr.warning("", "Necesita ingresar la cantidad")
                return false;
            }
            if (isNaN(parseInt(valor))) {
                toastr.warning("", "Debe ingresar un valor númerico")
                return false;
            }

            let inventario = {
                idInventario: data.id,  
                articuloInventario: data.articulo,
                colorInventario: data.color,
                talleInventario: data.talle,
                cantidad: parseInt(valor),
                precio: data.precio.toString(),
                total: (parseFloat(valor) * data.precio).toString()
            }

            InventariosParaVenta.push(inventario)

            mostrarInventario_Precios();
            $("#cboBuscarInventario").val("").trigger("change")
            swal.close()
        }
    )

})

function mostrarInventario_Precios() {

    let total = 0;
    let igv = 0;
    let subtotal = 0;
    let porcentaje = ValorImpuesto / 100;

    $("#tbInventario tbody").html("")

    InventariosParaVenta.forEach((item) => {

        total = total + parseFloat(item.total)

        $("#tbInventario tbody").append(
            $("<tr>").append(
                $("<td>").append(
                    $("<button>").addClass("btn btn-danger btn-eliminar btn-sm").append(
                        $("<i>").addClass("fas fa-trash-alt")
                    ).data("idInventario", item.idInventario)
                ),
                $("<td>").text(item.cantidad),               
                $("<td>").text(item.articuloInventario),
                $("<td>").text(item.colorInventario),
                $("<td>").text(item.talleInventario),
                $("<td>").text(item.precio),
                $("<td>").text(item.total)
            )
        )
    })

    subtotal = total / (1 + porcentaje);
    igv = total - subtotal;

    $("#txtSubTotal").val(subtotal.toFixed(2))
    $("#txtIGV").val(igv.toFixed(2))
    $("#txtTotal").val(total.toFixed(2))


}

$(document).on("click", "button.btn-eliminar", function () {

    const _idinventario = $(this).data("idInventario")

    InventariosParaVenta = InventariosParaVenta.filter(p => p.idInventario != _idinventario);

    mostrarInventario_Precios();
})


$("#btnTerminarVenta").click(function () {

    if (InventariosParaVenta.length < 1) {
        toastr.warning("", "Debe ingresar inventarios")
        return;
    }

    const vmDetalleVenta = InventariosParaVenta;

    const venta = {
        idTipoComprobante: $("#cboTipoComprobante").val(),
        //documentoCliente: $("#txtDocumentoCliente").val(),
        //nombreCliente: $("#txtNombreCliente").val(),
        subTotal: $("#txtSubTotal").val(),
        /*impuestoTotal: $("#txtIGV").val(),*/
        total: $("#txtTotal").val(),
        DetalleVenta: vmDetalleVenta
    }

    $("#btnTerminarVenta").LoadingOverlay("show");

    fetch("/Venta/RegistrarVenta", {
        method: "POST",
        headers: { "Content-Type": "application/json; charset=utf-8" },
        body: JSON.stringify(venta)
    })
        .then(response => {
            $("#btnTerminarVenta").LoadingOverlay("hide");
            return response.ok ? response.json() : Promise.reject(response);
        })
        .then(responseJson => {

            if (responseJson.estado) {
                InventariosParaVenta = [];
                mostrarInventario_Precios();

                $("#txtDocumentoCliente").val("")
                $("#txtNombreCliente").val("")
                $("#cboTipoComprobante").val($("#cboTipoComprobante option:first").val())

                swal("Registrado!", `Numero Venta : ${responseJson.objeto.numeroVenta}`, "success")
            } else {
                swal("Lo sentimos!", "No se pudo registrar la venta", "error")
            }
        })

})