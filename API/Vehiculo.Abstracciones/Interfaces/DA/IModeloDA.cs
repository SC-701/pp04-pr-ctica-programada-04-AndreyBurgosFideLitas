using Vehiculo.Abstracciones.Modelos;

namespace Abstracciones.Interfaces.DA
{
    public interface IModeloDA
    {
        Task<IEnumerable<Modelo>> Obtener(Guid IdMarca);
    }
}