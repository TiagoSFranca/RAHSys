using System.Collections.Generic;

namespace RAHSys.Entidades.Entidades
{
    public class EnderecoModel
    {
        public int IdEndereco { get; set; }
        public int IdEstado { get; set; }
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string CEP { get; set; }

        public virtual EstadoModel Estado { get; set; }

        public virtual ICollection<ContratoEnderecoModel> ContratoEnderecos { get; set; }
    }
}
