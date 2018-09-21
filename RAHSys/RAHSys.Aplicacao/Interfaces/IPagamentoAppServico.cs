using RAHSys.Aplicacao.AppModels;
using System.Collections.Generic;

namespace RAHSys.Aplicacao.Interfaces
{
    public interface IPagamentoAppServico : IAppServicoBase<PagamentoAppModel>
    {
        ConsultaAppModel<PagamentoAppModel> Consultar(int idContrato, IEnumerable<int> idList, string dataPagamento, 
            string ordenacao, bool crescente, int pagina, int quantidade);
    }
}
