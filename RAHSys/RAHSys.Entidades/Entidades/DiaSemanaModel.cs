using System.Collections.Generic;

namespace RAHSys.Entidades.Entidades
{
    public class DiaSemanaModel
    {
        public int IdDiaSemana { get; set; }
        public string Descricao { get; set; }

        public virtual ICollection<AtividadeDiaSemanaModel> AtividadeDiaSemanas { get; set; }
    }
}
