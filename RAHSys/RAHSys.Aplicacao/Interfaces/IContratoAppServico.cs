using RAHSys.Aplicacao.AppModels;
using System.Collections.Generic;

namespace RAHSys.Aplicacao.Interfaces
{
    public interface IContratoAppServico : IAppServicoBase<ContratoAppModel>
    {
        ConsultaAppModel<ContratoAppModel> Consultar(IEnumerable<int> idList, IEnumerable<int> idEstadoList, string nomeEmpresa, decimal? receita, string cidade, 
            string ordenacao, bool crescente, int pagina, int quantidade);

        void AdicionarAnaliseInvestimento(AnaliseInvestimentoAppModel analiseInvestimento);

        void AdicionarFichaCliente(ClienteAppModel cliente);

        void AdicionarDocumento(int idContrato, ArquivoAppModel arquivo);
    }
}
