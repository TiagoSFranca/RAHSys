using RAHSys.Aplicacao.Interfaces;
using System.Web.Mvc;

namespace RAHSys.Apresentacao.Controllers
{
    public class CameraController : Controller
    {
        private readonly ICameraAppServico _cameraAppServico;

        public CameraController(ICameraAppServico cameraAppServico)
        {
            _cameraAppServico = cameraAppServico;
            ViewBag.Title = "Câmeras";
        }

        public ActionResult Index(string localizacao, string descricao, string ordenacao, bool? crescente, int? pagina, int? itensPagina)
        {
            ViewBag.SubTitle = "Consultar";
            var consulta = _cameraAppServico.Consultar(null, localizacao, descricao, ordenacao, crescente ?? true, pagina ?? 1, itensPagina ?? 40);
            return View(consulta);
        }
    }
}