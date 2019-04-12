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

    public class AtividadeIndexModel
    {
        public ContratoAppModel Contrato { get; set; }
        public EquipeAppModel Equipe { get; set; }
        public string TodasAtividadesSerializadas { get; set; }
        public string TodasEquipesSerializadas { get; set; }
    }

    public class AtividadeEquipeModel
    {
        public ContratoAppModel Contrato { get; set; }
        public EquipeAppModel Equipe { get; set; }
        public string TodasAtividadesSerializadas { get; set; }
        public Dictionary<DateTime, int> AtividadesAtrasadas { get; set; }
    }

    public class AtividadeUsuarioModel
    {
        public ContratoAppModel Contrato { get; set; }
        public EquipeAppModel Equipe { get; set; }
        public string TodasAtividadesSerializadas { get; set; }
        public Dictionary<DateTime, int> AtividadesAtrasadas { get; set; }
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

    public class AtividadeEquipeAdicionarEditarModel : AtividadeAdicionarEditarModel
    {
        public EquipeAppModel Equipe { get; set; }
        public List<ContratoAppModel> Contratos { get; set; }
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

        public AtividadeInfoModel()
        {

        }

        public AtividadeAppModel Atividade { get; set; }
        public RegistroRecorrenciaAppModel RegistroRecorrencia { get; set; }

        [Display(Name = "Data Prevista")]
        public DateTime DataPrevista { get; set; }
    }

    public class EvidenciaAtividadeModel
    {
        public AtividadeInfoModel AtividadeInfo { get; set; }
    }
}