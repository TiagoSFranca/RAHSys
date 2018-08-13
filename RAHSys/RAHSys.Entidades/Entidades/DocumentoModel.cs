using System;

namespace RAHSys.Entidades.Entidades
{
    public class DocumentoModel
    {
        public int IdDocumento { get; set; }
        public string CaminhoArquivo { get; set; }
        public DateTime DataUpload { get; set; }
        public string NomeArquivo { get; set; }
        public int IdContrato { get; set; }

        public virtual ContratoModel Contrato { get; set; }
    }
}
