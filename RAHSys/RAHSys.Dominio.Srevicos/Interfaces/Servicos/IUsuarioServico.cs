using RAHSys.Entidades.Entidades;
using System.Collections.Generic;

namespace RAHSys.Dominio.Servicos.Interfaces.Servicos
{
    public interface IUsuarioServico : IServicoBase<UsuarioModel>
    {
        IEnumerable<UsuarioModel> ListarTodos(IEnumerable<string> idNaoListar);
        UsuarioModel ObterPorId(string idUsuario);
    }
}
