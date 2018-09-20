using PagedList;
using RAHSys.Aplicacao.AppModels;

namespace RAHSys.Apresentacao.Models
{
    public class PagamentoIndexModel
    {
        public ContratoAppModel Contrato { get; set; }
        public StaticPagedList<PagamentoAppModel> Pagamentos { get; set; }


        public PagamentoIndexModel(ContratoAppModel contrato, StaticPagedList<PagamentoAppModel> pagamentos)
        {
            Contrato = contrato;
            Pagamentos = pagamentos;
        }
    }

    public class PagamentoAdicionarModel
    {
        public ContratoAppModel Contrato { get; set; }
        public PagamentoAppModel Pagamento { get; set; }
    }
}