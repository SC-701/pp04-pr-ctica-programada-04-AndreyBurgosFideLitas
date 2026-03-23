using Abstracciones.Interfaces.DA;
using Abstracciones.Interfaces.Flujo;
using Vehiculo.Abstracciones.Modelos;

namespace Flujo
{
    public class MarcaFlujo : IMarcaFlujo
    {
        private IMarcaDA _marcaDA;

        public MarcaFlujo(IMarcaDA marcaDA)
        {
            _marcaDA = marcaDA;
        }

        public async Task<IEnumerable<Marca>> Obtener()
        {
            return await _marcaDA.Obtener();
        }
    }
}
