using System.ComponentModel.DataAnnotations;

namespace RAHSys.Aplicacao.AppModels
{
    public class EnderecoAppModel
    {
        public int IdEndereco { get; set; }

        [Display(Name = "Cidade")]
        public int IdCidade { get; set; }

        [Display(Name = "Logradouro")]
        public string Logradouro { get; set; }

        [Display(Name = "Número")]
        public string Numero { get; set; }

        [Display(Name = "Bairro")]
        public string Bairro { get; set; }

        [Display(Name = "CEP")]
        public string CEP { get; set; }

        public CidadeAppModel Cidade { get; set; }

    }
}
