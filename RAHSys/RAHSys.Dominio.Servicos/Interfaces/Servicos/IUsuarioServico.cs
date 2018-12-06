using RAHSys.Entidades;
using RAHSys.Entidades.Entidades;
using System.Collections.Generic;

namespace RAHSys.Dominio.Servicos.Interfaces.Servicos
{
    public interface IUsuarioServico : IServicoBase<UsuarioModel>
    {
        ConsultaModel<UsuarioModel> Consultar(IEnumerable<string> idList, string email, string username, string ordenacao, bool crescente, int pagina, int quantidade);
        IEnumerable<UsuarioModel> ListarTodos(IEnumerable<string> idNaoListar);
        UsuarioModel ObterPorId(string idUsuario);
    }
}
