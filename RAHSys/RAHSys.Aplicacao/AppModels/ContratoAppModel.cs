using System.ComponentModel.DataAnnotations;

namespace RAHSys.Aplicacao.AppModels
{
    public class ContratoAppModel
    {
        public int IdContrato { get; set; }
        [Required]
        [Display(Name = "Nome da Empresa")]
        [MaxLength(256)]
        public string NomeEmpresa { get; set; }
        [Required]
        [Display(Name = "Contato Inicial")]
        [MaxLength(256)]
        public string ContatoInicial { get; set; }

        public ContratoEnderecoAppModel ContratoEndereco { get; set; }
    }
}
