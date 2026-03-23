using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vehiculo.Abstracciones.Modelos.Servicios.Revision;

namespace Vehiculo.Abstracciones.Interfaces.Servicios
{
    public interface IRevisionServicio
    {
        Task<Revision> Obtener(string placa);
    }
}
