using PagedList;
using RAHSys.Aplicacao.AppModels;
using System.Collections.Generic;

namespace RAHSys.Apresentacao.Models
{
    public class AtividadeContratoModel
    {
        public ContratoAppModel Contrato { get; set; }
        public EquipeAppModel Equipe { get; set; }
        public StaticPagedList<AtividadeAppModel> Atividades { get; set; }
        public string TodasAtividadesSerializadas { get; set; }

        public AtividadeContratoModel()
        {
            Atividades = new StaticPagedList<AtividadeAppModel>(new List<AtividadeAppModel>(), 1, 1, 0);
        }
    }

    public class AtividadeEquipeModel
    {
        public EquipeAppModel Equipe { get; set; }
        public StaticPagedList<AtividadeAppModel> Atividades { get; set; }
        public string TodasAtividadesSerializadas { get; set; }

        public AtividadeEquipeModel()
        {
            Atividades = new StaticPagedList<AtividadeAppModel>(new List<AtividadeAppModel>(), 1, 1, 0);
        }
    }

    public class AtividadeContratoAdicionarEditarModel
    {
        public ContratoAppModel Contrato { get; set; }
        public EquipeAppModel Equipe { get; set; }
        public AtividadeAppModel Atividade { get; set; }
        public List<TipoAtividadeAppModel> TipoAtividades { get; set; }

        public AtividadeContratoAdicionarEditarModel()
        {
            TipoAtividades = new List<TipoAtividadeAppModel>();
        }
    }

    public class AtividadeEquipeAdicionarEditarModel
    {
        public EquipeAppModel Equipe { get; set; }
        public AtividadeAppModel Atividade { get; set; }
        public List<ContratoAppModel> Contratos { get; set; }
        public List<TipoAtividadeAppModel> TipoAtividades { get; set; }

        public AtividadeEquipeAdicionarEditarModel()
        {
            TipoAtividades = new List<TipoAtividadeAppModel>();
            Contratos = new List<ContratoAppModel>();
        }
    }
}