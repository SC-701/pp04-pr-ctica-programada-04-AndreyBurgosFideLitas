using System.Text.RegularExpressions;
using Vehiculo.Abstracciones.Modelos;

namespace Abstracciones.Interfaces.Flujo
{
    public interface IMarcaFlujo
    {
        Task<IEnumerable<Marca>> Obtener();
    }
}