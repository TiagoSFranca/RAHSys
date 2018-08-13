using System;

namespace RAHSys.Aplicacao.AppModels
{
    public class DocumentoAppModel
    {
        public int IdDocumento { get; set; }
        public string CaminhoArquivo { get; set; }
        public DateTime DataUpload { get; set; }
        public string NomeArquivo { get; set; }
        public int IdContrato { get; set; }
    }
}
