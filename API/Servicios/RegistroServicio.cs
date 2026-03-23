using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using Vehiculo.Abstracciones.Interfaces.Reglas;
using Vehiculo.Abstracciones.Interfaces.Servicios;
using Vehiculo.Abstracciones.Modelos.Servicios.Registro;

namespace Servicios
{
    
    public class RegistroServicio : IRegistroServicio
    {
        private readonly IConfiguracion _configuracion;
        private readonly System.Net.Http.IHttpClientFactory _httpClient;

        public RegistroServicio(IConfiguracion configuracion, System.Net.Http.IHttpClientFactory httpClient)
        {
            _configuracion = configuracion;
            _httpClient = httpClient;
        }

        public async Task<Propietario> Obtener(string placa)
        {
            var endPoint = _configuracion.ObtenerMetodo("ApiEndPointsRegistro", "ObtenerRegistro");

            var servicioRegistro = _httpClient.CreateClient("ServicioRegistro");
            var respuesta = await servicioRegistro.GetAsync(string.Format(endPoint,placa));
            if (!respuesta.IsSuccessStatusCode)
                return null;
            var resultado = await respuesta.Content.ReadAsStringAsync();
            var opciones = new JsonSerializerOptions {PropertyNameCaseInsensitive = true 
            };
            var resultadoDeserializado = JsonSerializer.Deserialize<List<Propietario>>(resultado, opciones);
            return resultadoDeserializado.FirstOrDefault();
        }
    }
}
