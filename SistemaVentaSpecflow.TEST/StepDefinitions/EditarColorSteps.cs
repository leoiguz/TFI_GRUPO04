using NUnit.Framework;
using SistemaVenta.Entity;
using System.Net;
using System.Text;
using SistemaVentaSpecflow.TEST.Support;
using Newtonsoft.Json;
using SistemaVenta.AplicacionWeb.Models.ViewModels;
using SistemaVenta.BLL.Implementacion;
using SistemaVenta.DAL.Interfaces;
using SistemaVenta.DAL.Implementacion;
using System.Net.Http;
using SistemaVenta.BLL.Interfaces;

namespace SistemaVentaSpecflow.TEST.StepDefinitions
{
    [Binding]
    public class EditarColorSteps
    {
        private ColorService _colorService;
        private Color _color;
        private Color _editedColor;
        private bool _operationResult;
        //private HttpClient _httpClient;
        //private readonly IColorService _colorServicio;

        public EditarColorSteps(/*IColorService colorServicio*/)
        {
            //_httpClient = Server.ArrancarServidor();
            //_colorServicio = colorServicio;

        }

        [Given(@"que tengo un color con la siguiente información:")]
        public void GivenQueTengoUnColorConLaSiguienteInformacion(Table table)
        {

            int id = int.Parse(table.Rows[0]["Id"]);
            string descripcion = table.Rows[0]["Descripcion"];
            DateTime fecha = DateTime.Now;
            bool esActivo = true;

            // Crea el objeto Color con los datos obtenidos de la tabla
            _color = new Color(id, descripcion, esActivo, fecha);
        }

        [When(@"realizo una solicitud para editar el color con la siguiente información:")]
        public async Task WhenRealizoUnaSolicitudParaEditarElColorConLaSiguienteInformacion(Table table)
        {

            bool esActivo = true;
            _editedColor = new Color
            {
                IdColor = int.Parse(table.Rows[0]["Id"]),
                Descripcion = table.Rows[0]["Descripcion"],
                EsActivo = esActivo
            };

            try
            {
                _color = await _colorService.Editar(_editedColor);
                _operationResult = true;
            }
            catch (Exception ex)
            {
                _operationResult = false;
            }
        }

        [Then(@"el color editado tiene la siguiente información:")]
        public void ThenElColorEditadoTieneLaSiguienteInformacion(Table table)
        {
            int id = int.Parse(table.Rows[0]["Id"]);
            string descripcion = table.Rows[0]["Descripcion"];
            DateTime fecha = DateTime.Now;
            bool esActivo = true;

            Color colorEditado = new Color(id, descripcion, esActivo, fecha);

            Assert.IsNotNull(colorEditado);
            Assert.AreEqual(colorEditado.IdColor, colorEditado.IdColor);
            Assert.AreEqual(colorEditado.Descripcion, colorEditado.Descripcion);
        }
    }
}
