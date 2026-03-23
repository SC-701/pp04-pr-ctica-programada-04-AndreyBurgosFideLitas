using Abstracciones.Interfaces.DA;
using Microsoft.Data.SqlClient;
using Dapper;
using Vehiculo.Abstracciones.Interfaces.DA;
using Vehiculo.Abstracciones.Modelos;

namespace DA
{
    public class MarcaDA : IMarcaDA
    {
        private IRepositorioDapper _repositorioDapper;
        private SqlConnection _sqlConnection;


        public MarcaDA(IRepositorioDapper repositorioDapper)
        {
            _repositorioDapper = repositorioDapper;
            _sqlConnection = _repositorioDapper.ObtenerRepositorio();
        }

        #region Operaciones
        public async Task<IEnumerable<Marca>> Obtener()
        {
            string query = @"ObtenerMarcas";
            var resultadoConsulta = await _sqlConnection.QueryAsync<Marca>(query);
            return resultadoConsulta;
        }

        #endregion
    }
}