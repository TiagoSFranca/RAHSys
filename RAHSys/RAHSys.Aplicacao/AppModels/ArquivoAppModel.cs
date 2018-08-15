using System.IO;

namespace RAHSys.Aplicacao.AppModels
{
    public class ArquivoAppModel
    {
        public virtual int ContentLength { get; set; }
        public virtual string ContentType { get; set; }
        public virtual string FileName { get; set; }
        public virtual Stream InputStream { get; set; }
    }
}
