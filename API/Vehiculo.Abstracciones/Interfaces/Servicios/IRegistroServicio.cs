using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vehiculo.Abstracciones.Modelos.Servicios.Registro;

namespace Vehiculo.Abstracciones.Interfaces.Servicios
{
    public interface IRegistroServicio
    {
        Task<Propietario> Obtener(string placa);
    }
}
