using System.Collections.Generic;

namespace RAHSys.Entidades.Entidades
{
    public class EstadoCivilModel
    {
        public int IdEstadoCivil { get; set; }
        public string Descricao { get; set; }

        public ICollection<FiadorModel> Fiadores { get; set; }

    }
}
