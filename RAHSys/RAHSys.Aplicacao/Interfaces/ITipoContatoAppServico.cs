using RAHSys.Aplicacao.AppModels;
using System.Collections.Generic;

namespace RAHSys.Aplicacao.Interfaces
{
    public interface ITipoContatoAppServico : IAppServicoBase<TipoContatoAppModel>
    {
        ConsultaAppModel<TipoContatoAppModel> Consultar(IEnumerable<int> idList, string descricao, string ordenacao, bool crescente, int pagina, int quantidade);
    }
}
