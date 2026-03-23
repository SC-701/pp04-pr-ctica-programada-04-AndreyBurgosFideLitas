using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System.Text.Json;
using Vehiculo.Abstracciones.Interfaces.Reglas;
using Vehiculo.Abstracciones.Interfaces.Servicios;
using Vehiculo.Abstracciones.Modelos.Servicios.Revision;

namespace Servicios
{
    
    public class RevisionServicio : IRevisionServicio
    {
        private readonly IConfiguracion _configuracion;
        private readonly System.Net.Http.IHttpClientFactory _httpClient;

        public RevisionServicio(IConfiguracion configuracion, System.Net.Http.IHttpClientFactory httpClient)
        {
            _configuracion = configuracion;
            _httpClient = httpClient;
        }

        public async Task<Revision> Obtener(string placa)
        {
            var endPoint = _configuracion.ObtenerMetodo("ApiEndPointsRevision", "ObtenerRevision");

            var servicioRevision = _httpClient.CreateClient("ServicioRevision");
            var respuesta = await servicioRevision.GetAsync(string.Format(endPoint, placa));

            if (!respuesta.IsSuccessStatusCode)
                return null;
            var resultado = await respuesta.Content.ReadAsStringAsync();
            var opciones = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            var resultadoDeserializado = JsonSerializer.Deserialize<List<Revision>>(resultado, opciones);
            return resultadoDeserializado.FirstOrDefault();
        }
    }
}
