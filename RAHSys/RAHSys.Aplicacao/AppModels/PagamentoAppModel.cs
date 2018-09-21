using System;
using System.ComponentModel.DataAnnotations;

namespace RAHSys.Aplicacao.AppModels
{
    public class PagamentoAppModel
    {
        public int IdPagamento { get; set; }
        public int IdContrato { get; set; }
        public DateTime DataCriacao { get; set; }

        [Required]
        [Display(Name = "Data de Pagamento")]
        public DateTime? DataPagamento { get; set; }

        [MaxLength(256)]
        [Display(Name = "Observação")]
        public string Observacao { get; set; }
    }
}
