using RAHSys.Aplicacao.AppModels;
using System.Collections.Generic;

namespace RAHSys.Aplicacao.Interfaces
{
    public interface IUsuarioAppServico : IAppServicoBase<UsuarioAppModel>
    {
        ConsultaAppModel<UsuarioAppModel> Consultar(IEnumerable<string> idList, string email, string username, string ordenacao, bool crescente, int pagina, int quantidade);
        List<UsuarioAppModel> ListarTodos(IEnumerable<string> idNaoListar);
    }
}