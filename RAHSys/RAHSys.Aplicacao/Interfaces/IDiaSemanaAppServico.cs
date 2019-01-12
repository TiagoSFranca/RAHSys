using RAHSys.Aplicacao.AppModels;
using System.Collections.Generic;

namespace RAHSys.Aplicacao.Interfaces
{
    public interface IDiaSemanaAppServico : IAppServicoBase<DiaSemanaAppModel>
    {
        List<DiaSemanaAppModel> ListarTodos();
    }
}
