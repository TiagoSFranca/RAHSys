using System.Collections.Generic;

namespace RAHSys.Entidades.Entidades
{
    public class TipoTelhadoModel
    {
        public int IdTipoTelhado { get; set; }
        public string Descricao { get; set; }

        public virtual ICollection<AnaliseInvestimentoModel> AnaliseInvestimentos { get; set; }
    }
}
