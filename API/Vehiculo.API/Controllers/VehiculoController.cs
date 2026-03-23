using Microsoft.AspNetCore.Mvc;
using Vehiculo.Abstracciones.Interfaces.API;
using Vehiculo.Abstracciones.Interfaces.Flujo;
using Vehiculo.Abstracciones.Modelos;

namespace Vehiculo.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VehiculoController : ControllerBase, IVehiculoController
    {
  
            private readonly IVehiculoFlujo _vehiculoFlujo;
            private readonly ILogger<VehiculoController> _logger;

            public VehiculoController(IVehiculoFlujo vehiculoFlujo, ILogger<VehiculoController> logger)
            {
                _vehiculoFlujo = vehiculoFlujo;
                _logger = logger;
            }

            [HttpPost]
            public async Task<IActionResult> AgregarVehiculo([FromBody] VehiculoRequest vehiculo)
            {
                var resultado = await _vehiculoFlujo.AgregarVehiculo(vehiculo);
                return CreatedAtAction(nameof(ObtenerVehiculo), new { Id = resultado }, null);
            }

            [HttpPut("{Id}")]
            public async Task<IActionResult> Editar([FromRoute] Guid Id, [FromBody] VehiculoRequest vehiculo)
            {
                var existe = await VehiculoExiste(Id);

                if (!existe)
                    return NotFound("El vehículo no existe");

                var resultado = await _vehiculoFlujo.Editar(Id, vehiculo);
                return Ok(resultado);
            }

            [HttpDelete("{Id}")]
            public async Task<IActionResult> Eliminar([FromRoute] Guid Id)
            {
                var existe = await VehiculoExiste(Id);

                if (!existe)
                    return NotFound("El vehículo no existe");

                var resultado = await _vehiculoFlujo.Eliminar(Id);
                return Ok(resultado);
            }

            [HttpGet]
            public async Task<IActionResult> ObtenerVehiculos()
            {
                var resultado = await _vehiculoFlujo.ObtenerVehiculos();

                if (!resultado.Any())
                    return NoContent();

                return Ok(resultado);
            }

            [HttpGet("{Id}")]
            public async Task<IActionResult> ObtenerVehiculo([FromRoute] Guid Id)
            {
                var resultado = await _vehiculoFlujo.ObtenerVehiculo(Id);

                if (resultado == null)
                    return NotFound("El vehículo no existe");

                return Ok(resultado);
            }

            private async Task<bool> VehiculoExiste(Guid Id)
            {
                var resultadoVehiculo = await _vehiculoFlujo.ObtenerVehiculo(Id);
                return resultadoVehiculo != null;
            }
        }
    }

