using System;

namespace RAHSys.Entidades.Entidades
{
    public class EvidenciaModel
    {
        public int IdEvidencia { get; set; }
        public string CaminhoArquivo { get; set; }
        public DateTime DataUpload { get; set; }
        public string NomeArquivo { get; set; }
        public int IdRegistroRecorrencia { get; set; }

        public virtual RegistroRecorrenciaModel RegistroRecorrencia { get; set; }
    }
}
