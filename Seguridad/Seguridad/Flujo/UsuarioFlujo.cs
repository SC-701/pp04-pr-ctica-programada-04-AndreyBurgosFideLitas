using Abstracciones.Flujo;
using Abstracciones.DA;
using Abstracciones.Modelos;

namespace Flujo
{
    public class UsuarioFlujo : IUsuarioFlujo
    {
        private IUsuarioDA _usuarioDA;

        public UsuarioFlujo(IUsuarioDA usuarioDA)
        {
            _usuarioDA = usuarioDA;
        }

        public async Task<Guid> CrearUsuario(UsuarioBase usuario)
        {
            return await _usuarioDA.CrearUsuario(usuario);
        }

        public async Task<Usuario> ObtenerUsuario(UsuarioBase usuario)
        {
            return await _usuarioDA.ObtenerUsuario(usuario);
        }
    }
}
