/*-------------------------------------------------------------BUSCAR CLIENTE-----------------------------------------------------------*/

$(document).ready(function () {
    $("#cboBuscarCliente").select2({
        ajax: {
            url: "/Venta/ObtenerClientes",
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
                            id: item.idCliente,

                            documentoCliente: item.numeroDocumento,
                            nombreCliente: item.nombres,
                            apellidoCliente: item.apellidos,
                            condicionTributaria: item.nombreCondicionTributaria
                        }
                    ))
                };
            }
        },
        language: "es",
        placeholder: 'Buscar Cliente...',
        minimumInputLength: 1,
        templateResult: formatoResultadosClientes
    });

})

function formatoResultadosClientes(data) {
    if (data.loading)
        return data.text;

    var contenedorCliente = $(
        `<table width="100%">
            <tr>
                <td>
                    <p style="font-weight: bolder;margin:2px">${data.nombreCliente}</p>
                    <p style="margin:2px">${data.nombreCliente}</p>
                    <p style="margin:2px">${data.apellidoCliente}</p>
                </td>
            </tr>
         </table>`
    );

    return contenedorCliente;
}


let ClienteParaVenta = [];
$("#cboBuscarCliente").on("select2:select", function (e) {
    const data = e.params.data;

    let cliente_encontrado = ClienteParaVenta.filter(c => c.idCliente == data.id)
    if (cliente_encontrado.length > 0) {
        $("#cboBuscarCliente").val("").trigger("change")
        toastr.warning("", "El Cliente ya fue agregado")
        return false
    }

    else {
        if (ClienteParaVenta.length > 0) {
            // Si ya hay un cliente agregado, eliminarlo
            ClienteParaVenta = [];
            // Limpiar la tabla de clientes
            $("#tbCliente tbody").html("");
            // Mensaje de advertencia
            toastr.warning("", "Se ha eliminado el cliente anterior");
        }

        let cliente = {
            idCliente: data.id,

            numeroDocumentoCliente: data.documentoCliente,
            nombresCliente: data.nombreCliente,
            apellidosCliente: data.apellidoCliente,
            nombreCondicionTributariaCliente: data.condicionTributaria,

        }

        ClienteParaVenta.push(cliente)
        console.log(cliente);
        mostrarCliente();
        mostrarTipoComprobante(data.condicionTributaria);
        $("#cboBuscarCliente").val("").trigger("change")
        swal.close()
    }

})

function mostrarCliente() {

    $("#tbCliente tbody").html("")

    ClienteParaVenta.forEach((item) => {

        $("#tbCliente tbody").append(
            $("<tr>").append(
                $("<td>").text(item.numeroDocumentoCliente),
                $("<td>").text(item.nombresCliente),
                $("<td>").text(item.apellidosCliente),
                $("<td>").text(item.nombreCondicionTributariaCliente),
            )
        )  


    }) 

}
let TipoComprobante;
function mostrarTipoComprobante(condicionTributaria) {

    fetch(`/Venta/ObtenerTipoComprobante?busqueda=${condicionTributaria}`)
        .then(response => {
            return response.ok ? response.json() : Promise.reject(response);
        })
        .then(responseJson => {

            if (responseJson) {
                const d = responseJson;
                $("#txtTipoComprobante").val(d.descripcion);
                TipoComprobante = d.idTipoComprobante;
                
            }
        })
        .catch(error => {

            console.error('Error al obtener tipo de comprobante:', error);
        });
}

/*-------------------------------------------------------------BUSCAR INVENTARIO--------------------------------------------------------*/
let ValorIVA = 0;
let ValorMG = 0;


$(document).ready(function () {

    fetch("/Sucursal/Obtener")
        .then(response => {
            return response.ok ? response.json() : Promise.reject(response);
        })
        .then(responseJson => {

            if (responseJson.estado) {

                const d = responseJson.objeto;

                $("#inputGroupSubTotal").text(`Sub total - ${d.simboloMoneda}`)
                $("#inputGroupIva").text(`IVA(${d.iva}%) - ${d.simboloMoneda}`)
                $("#inputGroupMG").text(`MG(${d.margenGanancia}%) - ${d.simboloMoneda}`)
                $("#inputGroupTotal").text(`Total - ${d.simboloMoneda}`)

                ValorIVA = d.iva
                ValorMG = d.margenGanancia
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
                            precio: item.precioArticulo
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
                nombreArticulo: data.articulo,
                colorInventario: data.color,
                talleInventario: data.talle,
                cantidad: parseInt(valor),
                precio: data.precio.toString(),
                total: (parseFloat(valor) * data.precio).toString()
            }

            console.log(inventario);

            InventariosParaVenta.push(inventario)

            mostrarInventario_Precios();
            $("#cboBuscarInventario").val("").trigger("change")
            swal.close()
        }
    )

})

let impuestosTotales = 0;
function mostrarInventario_Precios() {

    let totalIVA = 0;
    let IVA = 0;
    let MG = 0;
    let subtotal = 0;
    let totalFinal = 0;

    $("#tbInventario tbody").html("")

    InventariosParaVenta.forEach((item) => {

        subtotal = subtotal + parseFloat(item.total)

        $("#tbInventario tbody").append(
            $("<tr>").append(
                $("<td>").append(
                    $("<button>").addClass("btn btn-danger btn-eliminar btn-sm").append(
                        $("<i>").addClass("fas fa-trash-alt")
                    ).data("idInventario", item.idInventario)
                ),
                $("<td>").text(item.cantidad),               
                $("<td>").text(item.nombreArticulo),
                $("<td>").text(item.colorInventario),
                $("<td>").text(item.talleInventario),
                $("<td>").text(item.cantidad),
                $("<td>").text(item.precio),
                $("<td>").text(item.total)
            )
        )
    })

    totalIVA = subtotal * (1 + (ValorIVA)/100);
    IVA = totalIVA - subtotal;
    totalFinal = totalIVA + (totalIVA * (ValorMG) / 100)
    MG = subtotal * (1 + (ValorMG) / 100) - subtotal

    $("#txtSubTotal").val(subtotal)
    $("#txtIVA").val(IVA)
    $("#txtMG").val(MG)
    $("#txtTotal").val(totalFinal)

    impuestosTotales = IVA + MG;

}

$(document).on("click", "button.btn-eliminar", function () {

    const _idinventario = $(this).data("idInventario")

    InventariosParaVenta = InventariosParaVenta.filter(p => p.idInventario != _idinventario);

    mostrarInventario_Precios();
})

/*-------------------------------------------------------------TERMINAR VENTA--------------------------------------------------------*/
$("#btnTerminarVenta").click(function () {

    if (InventariosParaVenta.length < 1) {
        toastr.warning("", "Debe ingresar inventarios")
        return;
    }

    if (ClienteParaVenta.length < 1) {
        toastr.warning("", "Debe ingresar un cliente")
        return;
    }

    const vmDetalleVenta = InventariosParaVenta;

    const ultimoCliente = ClienteParaVenta[ClienteParaVenta.length - 1];

    // Construir el objeto de venta con el nombre del cliente
    const venta = {
        idTipoComprobante: TipoComprobante.toString(),
        idCliente: ultimoCliente.idCliente.toString(),
        subTotal: $("#txtSubTotal").val(),
        impuestoTotal: impuestosTotales.toString(),
        total: $("#txtTotal").val(),
        detalleVenta: vmDetalleVenta
    };

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
                ClienteParaVenta = [];
                mostrarCliente();

                $("#txtTipoComprobante").val($("#txtTipoComprobante option:first").val())

                swal("Registrado!", `Numero Venta : ${responseJson.objeto.numeroVenta}`, "success")
            } else {
                swal("Lo sentimos!", "No se pudo registrar la venta", "error")
            }
        })

})