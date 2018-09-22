using RAHSys.Aplicacao.AppModels;
using System.Collections.Generic;

namespace RAHSys.Aplicacao.Interfaces
{
    public interface IUsuarioAppServico : IAppServicoBase<UsuarioAppModel>
    {
        List<UsuarioAppModel> ListarTodos(IEnumerable<string> idNaoListar);
    }
}