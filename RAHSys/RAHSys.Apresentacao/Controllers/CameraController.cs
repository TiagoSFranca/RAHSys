using RAHSys.Aplicacao.AppModels;
using RAHSys.Aplicacao.Interfaces;
using System;
using System.Web.Mvc;

namespace RAHSys.Apresentacao.Controllers
{
    public class CameraController : ControllerBase
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

        [HttpGet]
        public ActionResult Adicionar()
        {
            ViewBag.SubTitle = "Adicionar nova Câmera";

            return View(new CameraAppModel());
        }

        [HttpPost]
        public ActionResult Adicionar(CameraAppModel cameraAppModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _cameraAppServico.Adicionar(cameraAppModel);
                    MensagemSucesso("Cadastro realizado com sucesso!");
                    return RedirectToAction("Index", "Camera", new { localizacao = cameraAppModel.Localizacao, descricao = cameraAppModel.Descricao });
                }
                catch (Exception ex)
                {
                    MensagemErro(ex.Message);
                    return View(cameraAppModel);
                }
            }
            return View(cameraAppModel);
        }

        [HttpGet]
        public ActionResult Editar(int id)
        {
            ViewBag.SubTitle = "Editar Câmera";
            var cameraModel = new CameraAppModel();
            if (id > 0)
            {
                cameraModel = _cameraAppServico.ObterPorId(id);
                if (cameraModel == null)
                    MensagemErro("Câmera não encontrada");
            }
            return View(cameraModel);
        }
    }
}