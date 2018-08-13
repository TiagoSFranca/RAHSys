using System.ComponentModel.DataAnnotations;

namespace RAHSys.Aplicacao.AppModels
{
    public class AnaliseInvestimentoAppModel
    {
        [Required]
        [Display(Name = "Código do Contrato")]
        public int IdContrato { get; set; }

        [Required]
        [Display(Name = "Tipo de Telhado")]
        public int IdTipoTelhado { get; set; }

        [Required]
        [Display(Name = "Nome do Cliente")]
        [MaxLength(256)]
        public string NomeCliente { get; set; }

        [Required]
        [Display(Name = "Potência")]
        public decimal Potencia { get; set; }

        [Required]
        [Display(Name = "Investimento")]
        public decimal Investimento { get; set; }

        [Required]
        [Display(Name = "Consumo Total")]
        public decimal ConsumoTotal { get; set; }

        [Required]
        [Display(Name = "Placas(Qt)")]
        public int NumeroPlacas { get; set; }

        [Required]
        [Display(Name = "Placas(Tipo)")]
        [MaxLength(256)]
        public string TipoPlacas { get; set; }

        [Required]
        [Display(Name = "Inversores(Qt)")]
        public int QtdInversores { get; set; }

        [Required]
        [Display(Name = "Placas(Tipo)")]
        [MaxLength(256)]
        public string TipoInversores { get; set; }

        public virtual TipoTelhadoAppModel TipoTelhado { get; set; }

        public virtual ClienteAppModel Cliente { get; set; }
    }
}
