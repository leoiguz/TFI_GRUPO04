



/* -----------------------------------------------------------------------BUSCAR CLIENTE------------------------------------------------------------------ */
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

            let cliente = {
                idCliente: data.id,

                numeroDocumentoCliente: data.documentoCliente,
                nombresCliente: data.nombreCliente,
                apellidosCliente: data.apellidoCliente,
                nombreCondicionTributariaCliente: data.condicionTributaria,
            }

            ClienteParaVenta.push(cliente)

            mostrarCliente();
            $("#cboBuscarCliente").val("").trigger("change")
            swal.close()
        }

})

function mostrarCliente() {

    $("#tbCliente tbody").html("")

    ClienteParaVenta.forEach((item) => {

        $("#tbCliente tbody").append(
            $("<tr>").append(
                $("<td>").append(
                    $("<button>").addClass("btn btn-danger btn-eliminar btn-sm").append(
                        $("<i>").addClass("fas fa-trash-alt")
                    ).data("idCliente", item.idCliente)
                ),
                $("<td>").text(item.numeroDocumentoCliente),
                $("<td>").text(item.nombresCliente),
                $("<td>").text(item.apellidosCliente),
                $("<td>").text(item.nombreCondicionTributariaCliente),
            )
        )
    })
}

$(document).on("click", "button.btn-eliminar", function () {

    const _idcliente = $(this).data("idCliente")

    ClienteParaVenta = ClienteParaVenta.filter(c => c.idCliente != _idcliente);

    mostrarCliente();
})
