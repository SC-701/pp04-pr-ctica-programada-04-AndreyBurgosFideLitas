using Microsoft.Extensions.Configuration;
using Producto.Abstracciones.Interfaces.Reglas;
using Vehiculo.Abstracciones.Modelos;

namespace Reglas
{
    public class Configuracion : IConfiguracion
    {
        private readonly IConfiguration _configuration;

        public Configuracion(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string ObtenerMetodo(string seccion, string nombre)
        {
            var endpoint = _configuration.GetSection(seccion).Get<APIEndPoint>();

            if (endpoint == null)
                throw new Exception($"No se encontró la sección {seccion}");

            var metodo = endpoint.Metodos?.FirstOrDefault(m => m.Nombre == nombre);

            if (metodo == null)
                throw new Exception($"No se encontró el método {nombre}");

            return $"{endpoint.UrlBase?.TrimEnd('/')}/{metodo.Valor}";
        }

        public string ObtenerValor(string llave)
        {
            return _configuration.GetSection(llave).Value ?? string.Empty;
        }
    }
}