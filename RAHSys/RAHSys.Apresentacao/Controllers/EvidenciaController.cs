using RAHSys.Aplicacao.Interfaces;
using RAHSys.Extras;
using RAHSys.Infra.CrossCutting.Exceptions;
using System.IO;
using System.Web.Mvc;

namespace RAHSys.Apresentacao.Controllers
{
    public class EvidenciaController : ControllerBase
    {
        private readonly IEvidenciaAppServico _evidenciaAppServico;

        public EvidenciaController(IEvidenciaAppServico evidenciaAppServico)
        {
            _evidenciaAppServico = evidenciaAppServico;
        }

        [HttpGet]
        public ActionResult VisualizarEvidencia(int id)
        {
            ViewBag.SubTitle = "Visualizar Evidência";
            try
            {
                var documento = _evidenciaAppServico.ObterPorId(id);
                if (documento != null)
                {
                    var route = HttpContext.Server.MapPath(documento.CaminhoArquivo);
                    if (System.IO.File.Exists(route))
                    {
                        var extensao = Path.GetExtension(route);
                        var arquivo = System.IO.File.ReadAllBytes(route);
                        return File(arquivo, ContentTypeHelper.GetMimeType(extensao));
                    }
                }
                MensagemErro("Documento não encontrado");
                return RedirectToAction("Index");
            }
            catch (CustomBaseException ex)
            {
                MensagemErro(ex.Mensagem);
                return RedirectToAction("Index");
            }
        }
    }
}