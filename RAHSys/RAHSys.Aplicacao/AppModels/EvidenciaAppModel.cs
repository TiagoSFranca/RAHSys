using System;

namespace RAHSys.Aplicacao.AppModels
{
    public class EvidenciaAppModel
    {
        public int IdEvidencia { get; set; }
        public string CaminhoArquivo { get; set; }
        public DateTime DataUpload { get; set; }
        public string NomeArquivo { get; set; }
        public int IdRegistroRecorrencia { get; set; }
    }
}
