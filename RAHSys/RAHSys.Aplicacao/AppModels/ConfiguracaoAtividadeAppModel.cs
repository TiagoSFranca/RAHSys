using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RAHSys.Aplicacao.AppModels
{
    public class ConfiguracaoAtividadeAppModel
    {
        public int IdConfiguracaoAtividade { get; set; }

        [Display(Name = "Frequência")]
        [Range(1, Int32.MaxValue)]
        public int Frequencia { get; set; }

        [Display(Name = "Quantidade de Repetições")]
        [Range(0, Int32.MaxValue)]
        public int? QtdRepeticoes { get; set; }

        [Display(Name = "Termina em")]
        public DateTime? TerminaEm { get; set; }

        [Display(Name = "Dia do mês")]
        [Range(1, 31)]
        public int? DiaMes { get; set; }

        [Display(Name = "Dia da semana")]
        public virtual List<AtividadeDiaSemanaAppModel> AtividadeDiaSemanas { get; set; }
    }
}
