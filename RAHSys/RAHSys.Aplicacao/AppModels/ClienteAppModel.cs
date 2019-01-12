using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RAHSys.Aplicacao.AppModels
{
    public class ClienteAppModel
    {
        [Display(Name = "Código do Contrato")]
        public int IdAnaliseInvestimento { get; set; }

        [Display(Name = "Média KW")]
        public decimal MediaKW { get; set; }

        [Display(Name = "Consumo Total")]
        public decimal ConsumoTotal { get; set; }

        [Required]
        [Display(Name = "Equipe")]
        public int? IdEquipe { get; set; }

        public virtual List<FiadorAppModel> Fiadores { get; set; }
        public virtual ResponsavelFinanceiroAppModel ResponsavelFinanceiro { get; set; }
    }
}
