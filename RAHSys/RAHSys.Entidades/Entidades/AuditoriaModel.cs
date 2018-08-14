using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAHSys.Entidades.Entidades
{
    public class AuditoriaModel
    {
        public int IdAuditoria { get; set; }
        public string Usuario { get; set; }
        public string Funcao { get; set; }
        public string Acao { get; set; }
        public DateTime DataHora { get; set; }
        public string EnderecoIP { get; set; }
        public string Dados { get; set; }
    }
}
