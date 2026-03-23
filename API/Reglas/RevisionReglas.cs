using Vehiculo.Abstracciones.Interfaces.Reglas;
using Vehiculo.Abstracciones.Interfaces.Servicios;
using Vehiculo.Abstracciones.Modelos.Servicios.Revision;

namespace Reglas
{
    public class RevisionReglas : IRevisionReglas
    {
        private readonly IRevisionServicio _revisionServicio;
        private readonly IConfiguracion _configuracion;

        public RevisionReglas(IRevisionServicio revisionServicio, IConfiguracion configuracion)
        {
            _revisionServicio = revisionServicio;
            _configuracion = configuracion;
        }

        public async Task<bool> RevisionEsValida(string placa)
        {
            var resultadoRevision = await _revisionServicio.Obtener(placa);

            if (resultadoRevision == null)
                return false;

            return ValidarEstado(resultadoRevision) && ValidarPeriodo(resultadoRevision.Periodo);
        }

        private bool ValidarEstado(Revision resultadoRevision)
        {
            string estadoRevision = _configuracion.ObtenerValor("EstadoRevisionSatisfactorio");
            return resultadoRevision.Resultado == estadoRevision;
        }

        public static string ObtenerPeriodoActual() {
            return DateTime.Now.ToString("MM-yyyy");
        }

        private static bool ValidarPeriodo(string periodo)
        {
            var periodoActual = ObtenerPeriodoActual();
            return periodo == periodoActual;
        }
    }
}
