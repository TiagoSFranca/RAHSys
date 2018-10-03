using RAHSys.Entidades;
using RAHSys.Entidades.Entidades;
using System.Collections.Generic;

namespace RAHSys.Dominio.Servicos.Interfaces.Servicos
{
    public interface IContratoServico : IServicoBase<ContratoModel>
    {
        ConsultaModel<ContratoModel> Consultar(IEnumerable<int> idList, IEnumerable<int> idEstadoList, string nomeEmpresa, decimal? receita, string cidade, 
            string ordenacao, bool crescente, int pagina, int quantidade);

        void AdicionarAnaliseInvestimento(AnaliseInvestimentoModel analiseInvestimentoModel);

        void AdicionarFichaCliente(ClienteModel clienteModel);

        void AdicionarDocumento(int idContrato, ArquivoModel arquivo);
    }
}
