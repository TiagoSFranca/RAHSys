using System.Collections.Generic;

namespace RAHSys.Entidades.Entidades
{
    public class ClienteModel
    {
        public int IdAnaliseInvestimento { get; set; }
        public int? IdEquipe { get; set; }
        public decimal MediaKW { get; set; }
        public decimal ConsumoTotal { get; set; }

        public virtual AnaliseInvestimentoModel AnaliseInvestimento { get; set; }
        public virtual EquipeModel Equipe { get; set; }

        public virtual ICollection<FiadorModel> Fiadores { get; set; }
    }
}
