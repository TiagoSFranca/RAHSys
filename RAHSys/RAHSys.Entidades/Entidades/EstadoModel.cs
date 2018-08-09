using System.Collections.Generic;

namespace RAHSys.Entidades.Entidades
{
    public class EstadoModel
    {
        public int IdEstado { get; set; }
        public string Descricao { get; set; }
        public string Sigla { get; set; }

        public virtual ICollection<CidadeModel> Cidades { get; set; }
    }
}
