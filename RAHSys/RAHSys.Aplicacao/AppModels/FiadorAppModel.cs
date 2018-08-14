using System.ComponentModel.DataAnnotations;

namespace RAHSys.Aplicacao.AppModels
{
    public class FiadorAppModel
    {
        public int IdFiador { get; set; }
        public int IdCliente { get; set; }

        [Display(Name = "Estado Civil")]
        public int IdEstadoCivil { get; set; }

        [Required]
        [Display(Name = "Nome")]
        [MaxLength(256)]
        public string Nome { get; set; }

        public bool Conjuge { get; set; }

        [Required]
        [Display(Name = "Telefone")]
        [MaxLength(20)]
        public string Telefone { get; set; }

        [Required]
        [Display(Name = "Email")]
        [EmailAddress]
        [MaxLength(256)]
        public string Email { get; set; }
        public int? IdFiadorEndereco { get; set; }

        public virtual FiadorEnderecoAppModel FiadorEndereco { get; set; }
        public virtual EstadoCivilAppModel EstadoCivil { get; set; }
    }
}
