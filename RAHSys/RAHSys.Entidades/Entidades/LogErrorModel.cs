using System;

namespace RAHSys.Entidades.Entidades
{
    public class LogErrorModel
    {
        public int IdLogError { get; set; }
        public string Excecao { get; set; }
        public string Tipo { get; set; }
        public string Metodo { get; set; }
        public string Mensagem { get; set; }
        public DateTime DataOcorrencia { get; set; }
        public Guid CodErro { get; set; }
    }
}
