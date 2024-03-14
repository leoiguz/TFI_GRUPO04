/*-------------------------------------------------------------BUSCAR CLIENTE-----------------------------------------------------------*/

//Obtengo los clientes
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

//Muestro los clientes a buscar
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

//Agrego el cliente a la venta
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
        mostrarTipoComprobante(cliente.nombreCondicionTributariaCliente);
        $("#cboBuscarCliente").val("").trigger("change")
        swal.close()
    }

})

//Muestra los datos del cliente en la tabla
function mostrarCliente() {

    $("#tbCliente tbody").html("")

    ClienteParaVenta.forEach((item) => {
        if (precioTotal >= 98000) {

            item.nombreCondicionTributariaCliente = "Dni";
            item.codigoCondicionTributariaCliente = "96";

        }

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

//Obtengo el tipo de comprobante segun condicion tributaria
let CodigoTipoComprobante;
let IdTipoComprobante;
function mostrarTipoComprobante(condicionTributaria) {

    fetch(`/Venta/ObtenerTipoComprobante?busqueda=${condicionTributaria}`)
        .then(response => {
            return response.ok ? response.json() : Promise.reject(response);
        })
        .then(responseJson => {

            if (responseJson) {
                const d = responseJson;
                $("#txtTipoComprobante").val(d.descripcion);
                IdTipoComprobante = d.idTipoComprobante;
                CodigoTipoComprobante = d.codigo;
                
            }
        })
        .catch(error => {

            console.error('Error al obtener tipo de comprobante:', error);
        });
}

/*-------------------------------------------------------------BUSCAR INVENTARIO--------------------------------------------------------*/
//Obtengo los inventarios

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

//Muestro los inventarios a buscar
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


//Agrego el inventario a la venta
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
                netoGravado: (parseFloat(data.costo) + (parseFloat(data.costo) * (parseFloat(data.margenGanancia) / 100))).toString(),
                iva: (parseFloat(data.iva)/100).toString(),
                margenGanancia: (parseFloat(data.margenGanancia)/100).toString()
            }
            
            InventariosParaVenta.push(inventario)
            mostrarInventario_Precios();
            mostrarCliente();
            $("#cboBuscarInventario").val("").trigger("change")
            swal.close()
        }
    )

})

//Calculo los precios a mostrar
let netoGravadoArticulo;
let netoGravadoTotal;
let ivaTotal;
let ivaArticulo;
let precioIndividual;
let precioVenta;
let precioTotal ;

function mostrarInventario_Precios() {

    netoGravadoArticulo = 0;
    netoGravadoTotal = 0;
    ivaTotal = 0;
    ivaArticulo = 0;
    precioIndividual = 0;
    precioVenta = 0;
    precioTotal = 0;


    $("#tbInventario tbody").html("")
    InventariosParaVenta.forEach((item) => {

        netoGravadoArticulo = parseFloat(item.netoGravado);
        ivaArticulo = netoGravadoArticulo * parseFloat(item.iva);
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

//Eliminar inventario de la venta
$(document).on("click", "button.btn-eliminar", function () {

    const _idinventario = $(this).data("idInventario")

    InventariosParaVenta = InventariosParaVenta.filter(p => p.idInventario != _idinventario);
    $("#txtNetoGravado").val(0)
    $("#txtIva").val(0)
    $("#txtPrecioVenta").val(0)
    mostrarInventario_Precios();
})

/**--------------------------------------- CONFIRMAR VENTA ---------------------------------------------------------------**/
let fechaActual = new Date();
let fechaVencimiento = new Date();
let miToken;
let UltimosComprobantes;

let modeloAFIP = {
    idAFIP: '',
    token: '',
    cae: '',
    vencimientoToken: null
};

$("#btnConfirmarVenta").click(function () {
    

    // Verifica que se ingreso un cliente e inventarios para la venta
    if (InventariosParaVenta.length < 1) {
        toastr.warning("", "Debe ingresar inventarios")
        return;
    }

    if (ClienteParaVenta.length < 1) {
        toastr.warning("", "Debe ingresar un cliente")
        return;
    }


    $("#modalData").find("div.modal-content").LoadingOverlay("show");
    fetch(`/Venta/ObtenerAFIP`)
        .then(response => {
            return response.ok ? response.json() : Promise.reject(response);
        })
        .then(responseJson => {
            // Procesa los datos de AFIP
            const d = responseJson;
            modeloAFIP = {
                idAFIP: d.idAFIP,
                token: d.token,
                cae: d.cae,
                vencimientoToken: new Date(d.vencimientoToken)
            };            

            // Verifica la fecha de vencimiento del token
            if (modeloAFIP.vencimientoToken < fechaActual || modeloAFIP.vencimientoToken == null) {
                const codigo = '4E6C1831-0C85-439F-AB55-02D4227CE970';

                const requisitosToken = {
                    method: 'POST',
                    headers: { 'Content-Type': 'application/json' },
                    body: JSON.stringify(codigo)
                };

                // Realiza la solicitud de autorización si es necesario
                fetch('/Venta/SolicitarAutorizacion', requisitosToken)
                    .then(response => {
                        if (!response.ok) {
                            throw new Error('Error al realizar la solicitud');
                        }
                        return response.json();
                    })
                    .then(data => {
                        // Procesa la respuesta de la solicitud de autorización
                        if (data.token !== null) {                  
                            modeloAFIP.token = data.token;
                            modeloAFIP.vencimientoToken = data.vencimiento;
                            fechaVencimiento = data.vencimiento;

                            // Edita los datos del Modelo AFIP: Token y Vencimiento Token
                            fetch("/Venta/EditarAFIP", {
                                method: "PUT",
                                headers: { "Content-Type": "application/json; charset=utf-8" },
                                body: JSON.stringify(modeloAFIP)
                            })
                                .then(response => {
                                    return response.ok ? response.json() : Promise.reject(response);
                                })                         

                        } else {
                            swal("Lo sentimos!", "No se pudo confirmar la venta", "error")
                        }
                    })
                    .catch(error => {
                        console.error('Error:', error);
                    });
            }     
            // Obtengo el Token para utilizarlo en otras solicitudes
            miToken = modeloAFIP.token;

            const requisitosUltimosComprobantes = {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify(miToken)
            };

            //Realiza la solicitud de los ultimos comprobantes
            $("#modalData").find("div.modal-content").LoadingOverlay("show");
            fetch('/Venta/SolicitarUltimosComprobantes', requisitosUltimosComprobantes)
                .then(response => {
                    if (!response.ok) {

                        throw new Error('Error al realizar la solicitud');
                    }
                    return response.json();
                })
                .then(data => {
                    UltimosComprobantes = data.comprobantes;
                    $("#modalData").find("div.modal-content").LoadingOverlay("hide");
                })
                .catch(error => {
                    console.error('Error:', error);
                });
        })
        .catch(error => {
            console.error('Error al obtener datos AFIP:', error);
        });

    //Ventana de Pago
    /*mostrarModalPago();*/

    miFuncion();

})

async function miFuncion() {
    try {
        // Esperar 3 segundos (3000 milisegundos)
        await esperar(3000);

        // Llamar a la función para mostrar el modal
        await mostrarModalPago();

        console.log('El modal se ha mostrado correctamente.');
    } catch (error) {
        console.error('Ocurrió un error al mostrar el modal:', error);
    }
}

// Función para esperar un cierto tiempo
function esperar(ms) {
    return new Promise(resolve => setTimeout(resolve, ms));
}

/*--------------------------------------------------- REALIZAR PAGO --------------------------------------------------------*/

const MODELO_BASE_PAGO = {
    idPago: 0,
    monto: 0,
    idTipoPago: 0
}

$(document).ready(function () {

    //Obtengo la lista de pagos. Ej: Efectivo, Tarjeta, etc.
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

/*----------------------------------------------------------- TERMINAR VENTA ------------------------------------------------------*/
let binPago;
let idPago;
let estadoCae;

$("#btnTerminarVenta").click(function () {


    //Validacion de todos los campos del pago
    const montoIngresado = parseFloat($("#txtMonto").val());
    const totalVenta = parseFloat($("#txtTotalVenta").text());
    const numeroTarjeta = document.getElementById("txtNumeroTarjeta").value;
    const inputMesTarjeta = document.getElementById("txtMesTarjeta");
    const mesTarjeta = inputMesTarjeta.value.trim();
    const inputAnioTarjeta = document.getElementById("txtAnioTarjeta");
    const anioTarjeta = inputAnioTarjeta.value.trim();
    const inputCodigoTarjeta = document.getElementById("txtCodigoTarjeta");
    const codigoTarjeta = inputCodigoTarjeta.value.trim();


    if (isNaN(montoIngresado) || montoIngresado < totalVenta) {
        toastr.warning("", "El monto ingresado debe ser igual o mayor al total de la venta");
        $("#txtMonto").focus();
        return;
    }

    if (numeroTarjeta.length !== 16 && tarjeta.style.display === 'block') {
        toastr.warning("", "El número de tarjeta debe tener 16 caracteres");
        $("#txtNumeroTarjeta").focus();
        return;
    }

    if (mesTarjeta.length !== 2 && tarjeta.style.display === 'block') {
        toastr.warning("", "El mes de tarjeta debe tener 2 caracteres");
        $("#txtMesTarjeta").focus();
        return;
    }

    if (anioTarjeta.length !== 2 && tarjeta.style.display === 'block') {
        toastr.warning("", "El año de tarjeta debe tener 2 caracteres");
        $("#txtAnioTarjeta").focus();
        return;
    }

    if (codigoTarjeta.length !== 3 && tarjeta.style.display === 'block') {
        toastr.warning("", "El código de seguridad de la tarjeta debe tener 3 dígitos");
        $("#txtCodigoTarjeta").focus();
        return;
    }

    //Obtengo el numero de comprobante segun el tipo de comprobante
    const ultimoCliente = ClienteParaVenta[ClienteParaVenta.length - 1];
    let numeroComprobante = 0;

    UltimosComprobantes.forEach((ultComp) => {
        if (ultComp.id === parseInt(CodigoTipoComprobante)) {
            numeroComprobante = ultComp.numero;
        }
    });

    //Adapto la fecha al formato xs:dateTime
    function obtenerFechaHoraActual() {
        var fecha = new Date();
        var año = fecha.getFullYear();
        var mes = pad(fecha.getMonth() + 1);
        var dia = pad(fecha.getDate());
        var horas = pad(fecha.getHours());
        var minutos = pad(fecha.getMinutes());
        var segundos = pad(fecha.getSeconds());

        var fechaHoraFormateada = año + '-' + mes + '-' + dia + 'T' + horas + ':' + minutos + ':' + segundos;

        return fechaHoraFormateada;
    }
    function pad(numero) {
        return numero < 10 ? '0' + numero : numero;
    }

    //Solicitar Cae
    const ventaSolicitarCae = {
        token: miToken,
        fecha: fechaActual = obtenerFechaHoraActual(),              
        importeIva: parseFloat($("#txtIva").val()),
        importeNeto: parseFloat($("#txtNetoGravado").val()),
        importeTotal: parseFloat($("#txtPrecioVenta").val()),
        numero: numeroComprobante,
        numeroDocumento: parseInt(ultimoCliente.numeroDocumentoCliente),
        tipoComprobante: parseInt(CodigoTipoComprobante),
        tipoDocumento: parseInt(ultimoCliente.codigoCondicionTributariaCliente)       
    };


    const requisitosCae = {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(ventaSolicitarCae )
    };
    $("#btnTerminarVenta").LoadingOverlay("show");
    $("#modalData").find("div.modal-content").LoadingOverlay("show");
    fetch("/Venta/SolicitarCae", requisitosCae)
        .then(response => {
            if (!response.ok) {
                throw new Error('Error al realizar la solicitud');
            }
            return response.json();
        })
        .then(data => {

            if (data.estado === 1) {
                estadoCae = "Aprobada"
            }
        })
        .catch(error => {
            console.error('Error:', error);
        });

   //Si el metodo de pago seleccionado es 'Tarjeta', se realiza este bloque de codigo
    if (tarjeta.style.display === 'block') {

        const cardData = {
            card_number: $("#txtNumeroTarjeta").val(),
            card_expiration_month: $("#txtMesTarjeta").val(),
            card_expiration_year: $("#txtAnioTarjeta").val(),
            security_code: $("#txtCodigoTarjeta").val(),
            card_holder_name: $("#txtNombreTarjeta").val(),
            card_holder_identification: {
                type: ultimoCliente.nombreCondicionTributariaCliente,
                number: ultimoCliente.numeroDocumentoCliente
            }
        };

        const requisitosTokenPago = {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
                'apikey': 'b192e4cb99564b84bf5db5550112adea'
            },
            body: JSON.stringify(cardData)
        };

        //Solicitar token de pago
        fetch('https://developers.decidir.com/api/v2/tokens', requisitosTokenPago)
            .then(response => {
                if (!response.ok) {
                    throw new Error('Error al solicitar el token de pago');
                }
                return response.json();
            })
            .then(data => {
                $("#modalData").find("div.modal-content").LoadingOverlay("hide");
                idPago = data.id;
                binPago = data.bin.slice(0, -2);

                numeroComprobante = numeroComprobante + 1;

                const cardData2 = {

                    site_transaction_id: (numeroComprobante).toString(),
                    payment_method_id: 1,
                    token: idPago,
                    bin: binPago,
                    amount: montoIngresado,
                    currency: "ARS",
                    installments: 1,
                    description: "",
                    payment_type: "single",
                    establishment_name: "single",
                    sub_payments: [{
                        site_id: "",
                        amount: montoIngresado,
                        installments: null
                    }]
                }

                const requisitosPago = {
                    method: 'POST',
                    headers: {
                        'Content-Type': 'application/json',
                        'apikey': '566f2c897b5e4bfaa0ec2452f5d67f13'
                    },
                    body: JSON.stringify(cardData2)
                };

                //Solicitar autorizacion de pago
                fetch('https://developers.decidir.com/api/v2/payments', requisitosPago)
                    .then(response => {
                        if (!response.ok) {
                            $("#btnTerminarVenta").LoadingOverlay("hide");
                            $("#modalPago").modal("hide")
                            throw new Error('Error al solicitar el pago');
                        }
                        return response.json();
                    })
                    .then(data => {
                        console.log('Pago:', data);
                        registrarVenta();
                    })
                    .catch(error => {
                        console.error('Error:', error);
                        swal("Lo sentimos!", "No se pudo registrar la venta", "error")
                    });
            })
            .catch(error => {
                console.error('Error:', error);
            });

    } else {
        registrarVenta();
    }
   

})


//Se evalua que campos mostrar dependiendo el pago seleccionado

let tarjeta = document.getElementById('Tarjeta');

function mostrarModalPago(modelo = MODELO_BASE_PAGO) {

    const cboTipoPago = document.getElementById('cboTipoPago');
    const efectivo = document.getElementById('Efectivo');

    $("#txtId").val(modelo.idPago)
    $("#txtMonto").val(modelo.monto)
    cboTipoPago.addEventListener('change', function () {
        // Obtener el valor seleccionado del tipo de pago
        const tipoPago = cboTipoPago.value;

        // Mostrar u ocultar los campos según el tipo de pago seleccionado
        if (tipoPago === '1') {
            efectivo.style.display = 'block';
            tarjeta.style.display = 'none';
        } else if (tipoPago === '2') {
            efectivo.style.display = 'none';
            tarjeta.style.display = 'block';
            mostrarCamposTarjeta();
        } else {
            // En caso de otro tipo de pago, ocultar ambos conjuntos de campos
            efectivo.style.display = 'none';
            tarjeta.style.display = 'none';
        }
    });

    $("#txtTotalVenta").text(precioVenta);
    $("#txtMonto").on("input", function () {
        calcularCambio();
    });

    $("#modalPago").modal("show")
}

const MODELO_BASE_TARJETA = {
    idTarjeta: 0,
    numeroTarjeta: "",
    mesTarjeta: "",
    anioTarjeta: "",
    nombreTarjeta: "",
    codigoSeguridadTarjeta: ""
}

function mostrarCamposTarjeta(modelo = MODELO_BASE_TARJETA) {

    $("#txtNumeroTarjeta").val(modelo.numeroTarjeta)
    $("#txtMesTarjeta").val(modelo.MesTarjeta)
    $("#txtAnioTarjeta").val(modelo.AnioTarjeta)
    $("#txtNombreTarjeta").val(modelo.nombreTarjeta)
    $("#txtCodigoTarjeta").val(modelo.codigoSeguridadTarjeta)

    $("#modalPago").modal("show")
}

//Se calcula el cambio
function calcularCambio() {

    const montoPagado = parseFloat($("#txtMonto").val());
    const totalVenta = parseFloat($("#txtTotalVenta").text());

    const cambio = montoPagado - totalVenta;

    if (cambio >= 0) {
        $("#txtCambio").text(cambio.toFixed(2));
    } else {
        $("#txtCambio").text("");
    }
}

//Registrar la venta
function registrarVenta() {

    const vmDetalleVenta = InventariosParaVenta;
    const ultimoCliente = ClienteParaVenta[ClienteParaVenta.length - 1];

    const venta = {
        idCliente: ultimoCliente.idCliente,
        idTipoComprobante: IdTipoComprobante,
        tipoComprobante: CodigoTipoComprobante,
        documentoCliente: ultimoCliente.numeroDocumentoCliente,
        tipoDocumentoCliente: ultimoCliente.codigoCondicionTributariaCliente,
        nombreCliente: ultimoCliente.apellidosCliente,
        importeIva: $("#txtIva").val(),
        netoGravado: $("#txtNetoGravado").val(),
        monto: $("#txtPrecioVenta").val(),
        estado: estadoCae,
        DetalleVenta: vmDetalleVenta
    }

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
                $("#txtTipoComprobante").val($("#txtTipoComprobante option:first").val());
                $("#txtCambio").text("");

                swal("Registrado!", `Numero Venta : ${responseJson.objeto.numeroComprobante}`, "success");
            } else {
                swal("Lo sentimos!", "No se pudo registrar la venta", "error")
            }
        })

}