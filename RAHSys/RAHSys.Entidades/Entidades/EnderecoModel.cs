using System.Collections.Generic;

namespace RAHSys.Entidades.Entidades
{
    public class EnderecoModel
    {
        public int IdEndereco { get; set; }
        public int IdCidade { get; set; }
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Bairro { get; set; }
        public string CEP { get; set; }

        public virtual CidadeModel Cidade { get; set; }

        public virtual ICollection<ContratoEnderecoModel> ContratoEnderecos { get; set; }
    }
}
