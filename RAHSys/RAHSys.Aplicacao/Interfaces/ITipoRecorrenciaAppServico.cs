using RAHSys.Aplicacao.AppModels;
using System.Collections.Generic;

namespace RAHSys.Aplicacao.Interfaces
{
    public interface ITipoRecorrenciaAppServico : IAppServicoBase<TipoRecorrenciaAppModel>
    {
        List<TipoRecorrenciaAppModel> ListarTodos();
    }
}
