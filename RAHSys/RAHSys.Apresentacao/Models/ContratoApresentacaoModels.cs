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

    public class FichaClienteAdicionar
    {
        public ContratoAppModel Contrato { get; set; }
        public ClienteAppModel Cliente { get; set; }

        public List<CidadeAppModel> CidadesFiador { get; set; }
        public List<CidadeAppModel> CidadesFiadorConjuge { get; set; }
        public List<EstadoAppModel> Estados { get; set; }
        public List<EstadoCivilAppModel> EstadosCivis { get; set; }
        public List<EquipeAppModel> Equipes { get; set; }

        public FichaClienteAdicionar()
        {
            CidadesFiador = new List<CidadeAppModel>();
            CidadesFiadorConjuge = new List<CidadeAppModel>();
            Estados = new List<EstadoAppModel>();
            EstadosCivis = new List<EstadoCivilAppModel>();
            Equipes = new List<EquipeAppModel>();
        }
    }
}