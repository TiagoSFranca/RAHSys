using RAHSys.Entidades;
using RAHSys.Entidades.Entidades;
using System.Collections.Generic;

namespace RAHSys.Dominio.Servicos.Interfaces.Servicos
{
    public interface IPagamentoServico : IServicoBase<PagamentoModel>
    {
        ConsultaModel<PagamentoModel> Consultar(int idContrato, IEnumerable<int> idList, string dataPagamento, 
            string ordenacao, bool crescente, int pagina, int quantidade);
    }
}
