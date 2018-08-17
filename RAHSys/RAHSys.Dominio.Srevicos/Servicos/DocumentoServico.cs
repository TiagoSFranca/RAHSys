using RAHSys.Dominio.Servicos.Interfaces.Repositorios;
using RAHSys.Dominio.Servicos.Interfaces.Servicos;
using RAHSys.Entidades.Entidades;

namespace RAHSys.Dominio.Servicos.Servicos
{
    public class DocumentoServico : ServicoBase<DocumentoModel>, IDocumentoServico
    {
        private readonly IDocumentoRepositorio _DocumentoRepositorio;

        public DocumentoServico(IDocumentoRepositorio DocumentoRepositorio) : base(DocumentoRepositorio)
        {
            _DocumentoRepositorio = DocumentoRepositorio;
        }
    }
}
