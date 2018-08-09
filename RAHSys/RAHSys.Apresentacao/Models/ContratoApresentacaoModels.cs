using RAHSys.Aplicacao.AppModels;
using System.Collections.Generic;

namespace RAHSys.Apresentacao.Models
{
    public class ContratoAdicionarModel
    {
        public ContratoAppModel Contrato { get; set; }
        public List<CidadeAppModel> Cidades { get; set; }
        public List<EstadoAppModel> Estados { get; set; }

        public ContratoAdicionarModel()
        {
            Cidades = new List<CidadeAppModel>();
            Estados = new List<EstadoAppModel>();
        }
    }

    public class AnaliseInvestimentoAdicionar
    {
        public AnaliseInvestimentoAppModel AnaliseInvestimento { get; set; }
        public List<TipoTelhadoAppModel> TipoTelhados { get; set; }

        public AnaliseInvestimentoAdicionar()
        {
            TipoTelhados = new List<TipoTelhadoAppModel>();
        }
    }
}