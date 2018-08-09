using System.Collections.Generic;

namespace RAHSys.Entidades.Entidades
{
    public class CidadeModel
    {
        public int IdCidade { get; set; }
        public int IdEstado { get; set; }
        public string CodCidade { get; set; }
        public string Nome { get; set; }

        public virtual EstadoModel Estado { get; set; }

        public virtual ICollection<EnderecoModel> Enderecos { get; set; }
    }
}
