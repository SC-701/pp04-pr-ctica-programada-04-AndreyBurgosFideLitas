using Vehiculo.Abstracciones.Interfaces.DA;
using Vehiculo.Abstracciones.Interfaces.Flujo;
using Vehiculo.Abstracciones.Interfaces.Reglas;
using Vehiculo.Abstracciones.Modelos;

namespace Vehiculo.Flujo
{
    public class VehiculoFlujo : IVehiculoFlujo
    {
        private readonly IVehiculoDA _vehiculoDA;
        private readonly IRegistroReglas _registroReglas;
        private readonly IRevisionReglas _revisionReglas;

        public VehiculoFlujo(IVehiculoDA vehiculoDA, IRevisionReglas revisionReglas, IRegistroReglas registroReglas)
        {
            _vehiculoDA = vehiculoDA;
            _revisionReglas = revisionReglas;
            _registroReglas = registroReglas;
        }

        public Task<Guid> AgregarVehiculo(VehiculoRequest vehiculo)
        {
            return _vehiculoDA.AgregarVehiculo(vehiculo);
        }

        public Task<Guid> Editar(Guid Id, VehiculoRequest vehiculo)
        {
            return _vehiculoDA.Editar(Id, vehiculo);
        }

        public Task<Guid> Eliminar(Guid Id)
        {
            return _vehiculoDA.Eliminar(Id);
        }

        public Task<IEnumerable<VehiculoResponse>> ObtenerVehiculos()
        {
            return _vehiculoDA.ObtenerVehiculos();
        }

        public async Task<VehiculoDetalle> ObtenerVehiculo(Guid Id)
        {
            var vehiculo = await _vehiculoDA.ObtenerVehiculo(Id);
            vehiculo.RevisionValida = await _revisionReglas.RevisionEsValida(vehiculo.Placa);
            vehiculo.RegistroValido = await _registroReglas.VehiculoEstaRegistrado(vehiculo.Placa, vehiculo.CorreoPropietario);
            return vehiculo;
        }
    }
}
