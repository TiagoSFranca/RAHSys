using PagedList;
using RAHSys.Aplicacao.AppModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RAHSys.Apresentacao.Models
{
    public class AtividadeContratoModel
    {
        public ContratoAppModel Contrato { get; set; }
        public EquipeAppModel Equipe { get; private set; }
        public string TodasAtividadesSerializadas { get; set; }
        public string TodasEquipesSerializadas { get; set; }

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

    public class AtividadeAdicionarEditarModel
    {
        public List<TipoAtividadeAppModel> TipoAtividades { get; set; }
        public List<DiaSemanaAppModel> DiaSemanas { get; set; }
        public List<TipoRecorrenciaAppModel> TipoRecorrencias { get; set; }
        public List<int> DiaSemanasSelecionadas { get; set; }
        public AtividadeAppModel Atividade { get; set; }

        public AtividadeAdicionarEditarModel()
        {
            TipoAtividades = new List<TipoAtividadeAppModel>();
            DiaSemanas = new List<DiaSemanaAppModel>();
            TipoRecorrencias = new List<TipoRecorrenciaAppModel>();
            DiaSemanasSelecionadas = new List<int>();
        }
    }

    public class AtividadeContratoAdicionarEditarModel : AtividadeAdicionarEditarModel
    {
        public ContratoAppModel Contrato { get; set; }
        public EquipeAppModel Equipe { get; set; }
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

    public class FinalizarAtividadeModel
    {
        public AtividadeInfoModel AtividadeInfo { get; set; }

        [Display(Name = "Data De Realização")]
        public DateTime? DataRealizacao { get; set; }

        [Display(Name = "Observação")]
        public string Observacao { get; set; }
    }

    public class AtividadeInfoModel
    {
        public AtividadeInfoModel(AtividadeAppModel atividade, DateTime dataPrevista)
        {
            Atividade = atividade;
            DataPrevista = dataPrevista;
        }

        public AtividadeAppModel Atividade { get; set; }
        public RegistroRecorrenciaAppModel RegistroRecorrencia { get; set; }

        [Display(Name = "Data Prevista")]
        public DateTime DataPrevista { get; set; }
    }
}