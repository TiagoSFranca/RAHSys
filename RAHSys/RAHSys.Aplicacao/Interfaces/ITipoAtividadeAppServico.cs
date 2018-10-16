using RAHSys.Aplicacao.AppModels;
using System.Collections.Generic;

namespace RAHSys.Aplicacao.Interfaces
{
    public interface ITipoAtividadeAppServico : IAppServicoBase<TipoAtividadeAppModel>
    {
        ConsultaAppModel<TipoAtividadeAppModel> Consultar(IEnumerable<int> idList, string descricao, string ordenacao, bool crescente, int pagina, int quantidade);
    }
}
