using System;
using System.Collections.Generic;

namespace RAHSys.Entidades.Entidades
{
    public class ConfiguracaoAtividadeModel
    {
        public int IdConfiguracaoAtividade { get; set; }
        public int Frequencia { get; set; }
        public int? QtdRepeticoes { get; set; }
        public DateTime? TerminaEm { get; set; }
        public int? DiaMes { get; set; }
        public bool ApenasDiaUtil { get; set; }

        public virtual AtividadeModel Atividade { get; set; }

        public virtual ICollection<AtividadeDiaSemanaModel> AtividadeDiaSemanas { get; set; }
    }
}
