using RAHSys.Entidades;
using RAHSys.Entidades.Entidades;
using System.Collections.Generic;

namespace RAHSys.Dominio.Servicos.Interfaces.Servicos
{
    public interface IContratoServico : IServicoBase<ContratoModel>
    {
        ConsultaModel<ContratoModel> Consultar(IEnumerable<int> idList, string nomeEmpresa, string cidade, string ordenacao, bool crescente, int pagina, int quantidade);
        void AdicionarAnaliseInvestimento(AnaliseInvestimentoModel analiseInvestimentoModel);
        void AdicionarFichaCliente(ClienteModel clienteModel);
    }
}
