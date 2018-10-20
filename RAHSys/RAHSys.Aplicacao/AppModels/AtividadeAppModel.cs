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

        [Display(Name = "Equipe")]
        public int IdEquipe { get; set; }

        [Display(Name = "Contrato")]
        public int IdContrato { get; set; }

        [Display(Name = "Atribuido Para")]
        public string IdUsuario { get; set; }

        [Display(Name = "Descrição")]
        public string Descricao { get; set; }

        [Display(Name = "Observações")]
        public string Observacao { get; set; }

        [Display(Name = "Realizada")]
        public bool Realizada { get; set; }

        [Display(Name = "Data de Realização Prevista")]
        public DateTime? DataPrevista { get; set; }

        [Display(Name = "Data de Realização")]
        public DateTime? DataRealizacao { get; set; }

        public virtual TipoAtividadeAppModel TipoAtividade { get; set; }
        public virtual EquipeAppModel Equipe { get; set; }
        public virtual ContratoAppModel Contrato { get; set; }
        public virtual UsuarioAppModel Usuario { get; set; }
    }
}
