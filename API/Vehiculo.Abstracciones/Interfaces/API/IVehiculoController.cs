using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vehiculo.Abstracciones.Modelos;

namespace Vehiculo.Abstracciones.Interfaces.API
{
    public interface IVehiculoController
    {
        Task<IActionResult> ObtenerVehiculos();
        Task<IActionResult> ObtenerVehiculo(Guid Id);
        Task<IActionResult> AgregarVehiculo(VehiculoRequest Vehiculo);
        Task<IActionResult> Editar(Guid Id, VehiculoRequest Vehiculo);
        Task<IActionResult> Eliminar(Guid Id);
    }
}
