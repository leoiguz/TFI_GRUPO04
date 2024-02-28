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
                            condicionTributaria: item.nombreCondicionTributaria,
                            condicionTributariaCodigo: item.codigoCondicionTributaria
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
            codigoCondicionTributariaCliente: data.condicionTributariaCodigo

        }

        ClienteParaVenta.push(cliente)
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
let CodigoTipoComprobante;
function mostrarTipoComprobante(condicionTributaria) {

    fetch(`/Venta/ObtenerTipoComprobante?busqueda=${condicionTributaria}`)
        .then(response => {
            return response.ok ? response.json() : Promise.reject(response);
        })
        .then(responseJson => {

            if (responseJson) {
                const d = responseJson;
                $("#txtTipoComprobante").val(d.descripcion);
                CodigoTipoComprobante = d.codigo;
                
            }
        })
        .catch(error => {

            console.error('Error al obtener tipo de comprobante:', error);
        });
}

/*-------------------------------------------------------------BUSCAR INVENTARIO--------------------------------------------------------*/

$(document).ready(function () {

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
                            costo: item.costoArticulo,
                            iva: item.ivaArticulo,
                            margenGanancia: item.margenGananciaArticulo
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
        inputPlaceholder: "Ingrese Cantidad",
        inputValue: "1"
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
                netoGravado: parseFloat(data.costo) + (parseFloat(data.costo) * (parseFloat(data.margenGanancia) / 100)),
                iva: parseFloat(data.iva)/100,
                margenGanancia: parseFloat(data.margenGanancia)/100
            }
            
            InventariosParaVenta.push(inventario)

            mostrarInventario_Precios();
            $("#cboBuscarInventario").val("").trigger("change")
            swal.close()
        }
    )

})

let netoGravadoArticulo = 0;
let netoGravadoTotal = 0;
let ivaTotal = 0;
let ivaArticulo = 0;
let precioIndividual = 0;
let precioVenta = 0;
let precioTotal = 0;

function mostrarInventario_Precios() {


    $("#tbInventario tbody").html("")

    InventariosParaVenta.forEach((item) => {

        netoGravadoArticulo = item.netoGravado;
        ivaArticulo = netoGravadoArticulo * item.iva;
        precioIndividual= (netoGravadoArticulo + ivaArticulo);
        precioVenta = (netoGravadoArticulo + ivaArticulo) * item.cantidad;

        netoGravadoTotal += netoGravadoArticulo;
        ivaTotal += ivaArticulo;
        precioTotal += precioVenta; 

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
                $("<td>").text(precioIndividual),
                $("<td>").text(precioVenta)
            )
        )
        $("#txtNetoGravado").val(netoGravadoTotal)
        $("#txtIva").val(ivaTotal)
        $("#txtPrecioVenta").val(precioTotal)
    })
}

$(document).on("click", "button.btn-eliminar", function () {

    const _idinventario = $(this).data("idInventario")

    InventariosParaVenta = InventariosParaVenta.filter(p => p.idInventario != _idinventario);

    mostrarInventario_Precios();
})

/**--------------------------------------- Confirmar Venta ---------------------------------------------------------------**/

$("#btnConfirmarVenta").click(function () {
    if (InventariosParaVenta.length < 1) {
        toastr.warning("", "Debe ingresar inventarios")
        return;
    }

    if (ClienteParaVenta.length < 1) {
        toastr.warning("", "Debe ingresar un cliente")
        return;
    }

    const codigo = "4E6C1831-0C85-439F-AB55-02D4227CE970";

    const options = {
        method: 'POST',
        headers: {'Content-Type': 'application/json'},
        body: JSON.stringify(codigo) 
    };

    fetch('/Venta/SolicitarAutorizacion', options)
        .then(response => {
            if (!response.ok) {

                throw new Error('Error al realizar la solicitud');
            }
            return response.json();
        })
        .then(data => {
            console.log('Token:', data.token);
            if (data.token !== null) {

                swal("Confirmado!", `Token : ${responseJson.data.token}`, "success");
                mostrarModalPago();
            }
            else {
                swal("Lo sentimos!", "No se pudo confirmar la venta", "error")
            }
            
        })
        .catch(error => {
            console.error('Error:', error);
        });

 
})

/*------------------------------------- PAGO ------------------------------------------*/

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

function mostrarModalPago(modelo = MODELO_BASE_PAGO) {
    $("#txtId").val(modelo.idPago)
    $("#txtMonto").val(modelo.monto)
    $("#cboTipoPago").val(modelo.idTipoPago == 0 ? $("#cboTipoPago option:first").val() : modelo.idTipoPago)
    $("#txtTotalVenta").text(precioVenta);
    $("#txtMonto").on("input", function () {
        calcularCambio();
    });
    $("#modalPago").modal("show")
}


/*-------------------------------------------------------------TERMINAR VENTA--------------------------------------------------------*/
$("#btnTerminarVenta").click(function () {

    const montoIngresado = parseFloat($("#txtMonto").val());
    const totalVenta = parseFloat($("#txtPrecioVenta").text());

    if (isNaN(montoIngresado) || montoIngresado < totalVenta) {
        toastr.warning("", "El monto ingresado debe ser igual o mayor al total de la venta");
        $("#txtMonto").focus();
        return;
    }

    const vmDetalleVenta = InventariosParaVenta;
    const ultimoCliente = ClienteParaVenta[ClienteParaVenta.length - 1];

    // Obtener la fecha actual
    const fechaActual = new Date();

    // Formatear la fecha según el formato deseado (por ejemplo, YYYY-MM-DD)
    const dia = String(fechaActual.getDate()).padStart(2, '0');
    const mes = String(fechaActual.getMonth() + 1).padStart(2, '0'); // Enero es 0!
    const anio = fechaActual.getFullYear();

    // Crear la cadena de fecha en formato YYYY-MM-DD
    const fechaFormateada = `${dia}-${mes}-${anio}`;

    // Construir el objeto de venta con el nombre del cliente
    const venta = {
        fecha: fechaFormateada,
        importeIva: $("#txtIva").val(),
        importeNetoGravado: $("#txtNetoGravado").val(),
        importeTotal: $("#txtPrecioVenta").val(),
        numeroVenta: 1,
        numeroDocumento: ultimoCliente.numeroDocumentoCliente,
        tipoComprobante: CodigoTipoComprobante,
        tipoDocumento: ultimoCliente.codigoCondicionTributariaCliente,
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
            $("#modalPago").modal("hide")
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



function calcularCambio() {
    // Obtener el monto pagado
    const montoPagado = parseFloat($("#txtMonto").val());
    // Obtener el total de la venta
    const totalVenta = parseFloat($("#txtTotalVenta").text());

    // Calcular el cambio
    const cambio = montoPagado - totalVenta;

    if (cambio >= 0) {
        // Mostrar el cambio en el campo correspondiente del modal de pago
        $("#txtCambio").text(cambio.toFixed(2)); // Redondear a 2 decimales
    } else {
        // Si el cambio es negativo o cero, ocultar el campo de cambio
        $("#txtCambio").text(""); // Limpiar el contenido del campo
    }
}
