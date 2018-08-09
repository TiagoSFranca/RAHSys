using RAHSys.Aplicacao.AppModels;
using System.Collections.Generic;

namespace RAHSys.Aplicacao.Interfaces
{
    public interface IEstadoAppServico : IAppServicoBase<EstadoAppModel>
    {
        List<EstadoAppModel> ListarTodos();
    }
}
