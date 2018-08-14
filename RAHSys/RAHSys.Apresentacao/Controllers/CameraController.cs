using RAHSys.Aplicacao.AppModels;
using RAHSys.Aplicacao.Interfaces;
using System;
using System.Web.Mvc;
using PagedList;
using RAHSys.Extras;
using RAHSys.Infra.CrossCutting.Exceptions;
using System.Collections.Generic;
using RAHSys.Apresentacao.Attributes;

namespace RAHSys.Apresentacao.Controllers
{
    [RAHAudit]
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
            ViewBag.Localizacao = localizacao;
            ViewBag.Cescricao = descricao;
            ViewBag.Ordenacao = ordenacao;
            ViewBag.Crescente = crescente ?? true;
            ViewBag.ItensPagina = itensPagina;
            try
            {
                var consulta = _cameraAppServico.Consultar(null, localizacao, descricao, ordenacao, crescente ?? true, pagina ?? 1, itensPagina ?? 40);
                var resultado = new StaticPagedList<CameraAppModel>(consulta.Resultado, consulta.PaginaAtual, consulta.ItensPorPagina, consulta.TotalItens);
                return View(resultado);
            }
            catch (CustomBaseException ex)
            {
                MensagemErro(ex.Mensagem);
                return View(new StaticPagedList<CameraAppModel>(new List<CameraAppModel>(), 1, 1, 0));
            }
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
                    MensagemSucesso(MensagensPadrao.CadastroSucesso);
                    return RedirectToAction("Index", "Camera", new { localizacao = cameraAppModel.Localizacao, descricao = cameraAppModel.Descricao });
                }
                catch (CustomBaseException ex)
                {
                    MensagemErro(ex.Mensagem);
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
            try
            {
                cameraModel = _cameraAppServico.ObterPorId(id);
                if (cameraModel == null)
                {
                    MensagemErro("Câmera não encontrada");
                    return RedirectToAction("Index");
                }
            }
            catch (CustomBaseException ex)
            {
                MensagemErro(ex.Mensagem);
                return RedirectToAction("Index");
            }
            return View(cameraModel);
        }

        [HttpPost]
        public ActionResult Editar(CameraAppModel cameraAppModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _cameraAppServico.Atualizar(cameraAppModel);
                    MensagemSucesso(MensagensPadrao.AtualizacaoSucesso);
                    return RedirectToAction("Index", "Camera", new { localizacao = cameraAppModel.Localizacao, descricao = cameraAppModel.Descricao });
                }
                catch (CustomBaseException ex)
                {
                    MensagemErro(ex.Mensagem);
                    return View(cameraAppModel);
                }
            }
            return View(cameraAppModel);
        }

        [HttpGet]
        public ActionResult Excluir(int id)
        {
            ViewBag.SubTitle = "Excluir Câmera";
            var cameraModel = new CameraAppModel();
            try
            {
                cameraModel = _cameraAppServico.ObterPorId(id);
                if (cameraModel == null)
                {
                    MensagemErro("Câmera não encontrada");
                    return RedirectToAction("Index");
                }
            }
            catch (CustomBaseException ex)
            {
                MensagemErro(ex.Mensagem);
                return RedirectToAction("Index");
            }
            return View(cameraModel);
        }

        [HttpPost]
        public ActionResult Excluir(CameraAppModel cameraAppModel)
        {
            try
            {
                _cameraAppServico.Remover(cameraAppModel.IdCamera);
                MensagemSucesso(MensagensPadrao.ExclusaoSucesso);
            }
            catch (CustomBaseException ex)
            {
                MensagemErro(ex.Mensagem);
            }
            return RedirectToAction("Index", "Camera");
        }
    }
}