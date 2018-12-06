using System.Collections.Generic;
using RAHSys.Dominio.Servicos.Interfaces.Repositorios;
using RAHSys.Dominio.Servicos.Interfaces.Servicos;
using RAHSys.Entidades.Entidades;
using System.Linq;
using RAHSys.Entidades;

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

        public ConsultaModel<UsuarioModel> Consultar(IEnumerable<string> idList, string email, string username, string ordenacao, bool crescente, int pagina, int quantidade)
        {
            var consultaModel = new ConsultaModel<UsuarioModel>(pagina, quantidade);

            var query = _usuarioRepositorio.Consultar();
            if (idList?.Count() > 0)
                query = query.Where(c => idList.Contains(c.IdUsuario));

            if (!string.IsNullOrEmpty(email))
                query = query.Where(c => c.Email.ToLower().Contains(email.ToLower()));

            if (!string.IsNullOrEmpty(username))
                query = query.Where(c => c.UserName.ToLower().Contains(username.ToLower()));

            switch ((ordenacao ?? string.Empty).ToLower())
            {
                case "email":
                    query = crescente ? query.OrderBy(c => c.Email) : query.OrderByDescending(c => c.Email);
                    break;
                case "username":
                    query = crescente ? query.OrderBy(c => c.UserName) : query.OrderByDescending(c => c.UserName);
                    break;
                default:
                    query = crescente ? query.OrderBy(c => c.IdUsuario) : query.OrderByDescending(c => c.IdUsuario);
                    break;

            }

            var resultado = query.Skip((pagina == 1 ? 0 : pagina - 1) * quantidade).Take(quantidade).ToList();
            consultaModel.TotalItens = query.Count();
            consultaModel.Resultado = resultado;

            return consultaModel;
        }
    }
}
