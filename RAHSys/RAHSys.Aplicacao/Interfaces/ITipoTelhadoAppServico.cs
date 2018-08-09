using RAHSys.Aplicacao.AppModels;
using System.Collections.Generic;

namespace RAHSys.Aplicacao.Interfaces
{
    public interface ITipoTelhadoAppServico : IAppServicoBase<TipoTelhadoAppModel>
    {
        ConsultaAppModel<TipoTelhadoAppModel> Consultar(IEnumerable<int> idList, string descricao, string ordenacao, bool crescente, int pagina, int quantidade);

        List<TipoTelhadoAppModel> ListarTodos();
    }
}
