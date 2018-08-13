using RAHSys.Aplicacao.AppModels;
using System.Collections.Generic;

namespace RAHSys.Aplicacao.Interfaces
{
    public interface IEstadoCivilAppServico : IAppServicoBase<EstadoCivilAppModel>
    {
        List<EstadoCivilAppModel> ListarTodos();
    }
}
