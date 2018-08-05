using RAHSys.Aplicacao.AppModels;
using System.Collections.Generic;

namespace RAHSys.Aplicacao.Interfaces
{
    public interface ICameraAppServico : IAppServicoBase<CameraAppModel>
    {
        ConsultaAppModel<CameraAppModel> Consultar(IEnumerable<int> idList, string localizacao, string descricao, string ordenacao, bool crescente, int pagina, int quantidade);
    }
}
