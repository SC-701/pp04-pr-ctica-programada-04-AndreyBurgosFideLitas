using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vehiculo.Abstracciones.Modelos;

namespace Vehiculo.Abstracciones.Interfaces.DA
{
    public interface IVehiculoDA
    {
        Task<IEnumerable<VehiculoResponse>> ObtenerVehiculos();
        Task<VehiculoDetalle> ObtenerVehiculo(Guid Id);
        Task<Guid> AgregarVehiculo(VehiculoRequest Vehiculo);
        Task<Guid> Editar(Guid Id, VehiculoRequest Vehiculo);
        Task<Guid> Eliminar(Guid Id);
    }
}
