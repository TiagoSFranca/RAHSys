using System.ComponentModel.DataAnnotations;

namespace RAHSys.Aplicacao.AppModels
{
    public class ResponsavelFinanceiroAppModel
    {
        public int IdResponsavelFinanceiro { get; set; }

        [Required]
        [Display(Name = "Nome")]
        [MaxLength(256)]
        public string Nome { get; set; }

        [Required]
        [Display(Name = "Email")]
        [EmailAddress]
        [MaxLength(256)]
        public string Email { get; set; }

        [Display(Name = "Telefone")]
        [MaxLength(20)]
        public string Telefone { get; set; }

    }
}
