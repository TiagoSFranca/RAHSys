using RAHSys.Aplicacao.AppModels;
using System.Collections.Generic;

namespace RAHSys.Aplicacao.Interfaces
{
    public interface ICidadeAppServico : IAppServicoBase<CidadeAppModel>
    {
        List<CidadeAppModel> ObterCidadesPorEstado(int idEstado);
    }
}