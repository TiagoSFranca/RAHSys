using RAHSys.Extras.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace RAHSys.Aplicacao.AppModels
{
    public class AtividadeAppModel
    {
        [Display(Name = "Código da Atividade")]
        public int IdAtividade { get; set; }

        [Display(Name = "Tipo da Atividade")]
        public int IdTipoAtividade { get; set; }

        [Display(Name = "Tipo da Recorrência")]
        public int? IdTipoRecorrencia { get; set; }

        [Display(Name = "Equipe")]
        public int IdEquipe { get; set; }

        [Display(Name = "Contrato")]
        public int IdContrato { get; set; }

        [Display(Name = "Atribuido Para")]
        public string IdUsuario { get; set; }

        [Display(Name = "Descrição")]
        public string Descricao { get; set; }
        
        [Display(Name = "Finalizada")]
        public bool Finalizada { get; set; }

        [Display(Name = "Começa em")]
        public DateTime? DataInicial { get; set; }

        [Display(Name = "Criado Em")]
        public DateTime? CriadoEm { get; set; }

        public virtual ConfiguracaoAtividadeAppModel ConfiguracaoAtividade { get; set; }
        public virtual TipoAtividadeAppModel TipoAtividade { get; set; }
        public virtual TipoRecorrenciaAppModel TipoRecorrencia { get; set; }
        public virtual EquipeAppModel Equipe { get; set; }
        public virtual ContratoAppModel Contrato { get; set; }
        public virtual UsuarioAppModel Usuario { get; set; }
    }
}
