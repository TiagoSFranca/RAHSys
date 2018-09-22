using System.Collections.Generic;
using RAHSys.Dominio.Servicos.Interfaces.Repositorios;
using RAHSys.Dominio.Servicos.Interfaces.Servicos;
using RAHSys.Entidades.Entidades;
using System.Linq;

namespace RAHSys.Dominio.Servicos.Servicos
{
    public class UsuarioServico : ServicoBase<UsuarioModel>, IUsuarioServico
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;

        public UsuarioServico(IUsuarioRepositorio usuarioRepositorio) : base(usuarioRepositorio)
        {
            _usuarioRepositorio = usuarioRepositorio;
        }

        public IEnumerable<UsuarioModel> ListarTodos(IEnumerable<string> idNaoListar)
        {
            var query = _usuarioRepositorio.Consultar();

            if (idNaoListar?.Where(e => !string.IsNullOrEmpty(e)).Count() > 0)
                query = query.Where(e => !idNaoListar.Contains(e.IdUsuario));

            return query.OrderBy(e => e.Email).ToList();
        }

        public UsuarioModel ObterPorId(string idUsuario)
        {
            return _usuarioRepositorio.Consultar().FirstOrDefault(e => e.IdUsuario.ToLower().Equals(idUsuario.ToLower()));
        }
    }
}
