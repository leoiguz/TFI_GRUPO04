using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace SistemaVentaSpecflow.TEST.StepDefinitions
{
    [Binding]
    public class SolicitarAutorizacionStepsDefinition
    {
        private string _codigo;
        private HttpResponseMessage _response;

        [Given(@"que tengo un código de autorización válido ""(.*)""")]
        public void GivenQueTengoUnCodigoDeAutorizacionValido(string codigo)
        {
            _codigo = codigo;
        }

        [When(@"envío una solicitud para solicitar autorización con el código ""(.*)""")]
        public async Task WhenEnvioUnaSolicitudParaSolicitarAutorizacionConElCodigo(string codigo)
        {
            // Establecer la URL base
            var baseUrl = "http://localhost:5043/";
            var client = new HttpClient();
            client.BaseAddress = new Uri(baseUrl);

            // Simular la solicitud HTTP al método SolicitarAutorizacion
            var request = new HttpRequestMessage(HttpMethod.Post, "/Venta/SolicitarAutorizacion");
            request.Content = new StringContent(codigo);

            // Enviar la solicitud al servidor
            _response = await client.SendAsync(request);
        }

        [Then(@"debería recibir un código de autorización válido")]
        public async Task ThenDeberiaRecibirUnCodigoDeAutorizacionValido()
        {
            // Verificar si la solicitud fue exitosa (código de estado 200 OK)
            Assert.AreEqual(HttpStatusCode.OK, _response.StatusCode);

            // Leer y verificar el cuerpo de la respuesta
            var responseBody = await _response.Content.ReadAsStringAsync();
            Assert.IsTrue(!string.IsNullOrEmpty(responseBody)); // Verifica si el cuerpo de la respuesta no está vacío
            // Puedes agregar más verificaciones según sea necesario
        }

        [Given(@"que tengo un código de autorización inválido ""(.*)""")]
        public void GivenQueTengoUnCodigoDeAutorizacionInvalido(string codigoInvalido)
        {
            _codigo = codigoInvalido;
        }

        [Then(@"debería recibir un mensaje de error")]
        public async Task ThenDeberiaRecibirUnMensajeDeError()
        {
            // Verificar si la solicitud no fue exitosa (código de estado diferente de 200 OK)
            Assert.AreNotEqual(HttpStatusCode.OK, _response.StatusCode);

            // Leer y verificar el cuerpo de la respuesta (mensaje de error)
            var responseBody = await _response.Content.ReadAsStringAsync();
            Assert.IsTrue(!string.IsNullOrEmpty(responseBody)); // Verifica si el cuerpo de la respuesta no está vacío
            // Puedes agregar más verificaciones según sea necesario
        }
    }
}
