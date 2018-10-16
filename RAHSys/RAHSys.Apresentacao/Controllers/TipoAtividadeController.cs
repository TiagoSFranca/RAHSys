using PagedList;
using RAHSys.Aplicacao.AppModels;
using RAHSys.Aplicacao.Interfaces;
using RAHSys.Apresentacao.Attributes;
using RAHSys.Extras;
using RAHSys.Infra.CrossCutting.Exceptions;
using System.Collections.Generic;
using System.Web.Mvc;

namespace RAHSys.Apresentacao.Controllers
{
    [RAHAudit]
    public class TipoAtividadeController : ControllerBase
    {
        private readonly ITipoAtividadeAppServico _tipoAtividadeAppServico;

        public TipoAtividadeController(ITipoAtividadeAppServico tipoAtividadeAppServico)
        {
            _tipoAtividadeAppServico = tipoAtividadeAppServico;
            ViewBag.Title = "Tipos de Atividade";
        }

        public ActionResult Index(string descricao, string ordenacao, bool? crescente, int? pagina, int? itensPagina)
        {
            ViewBag.SubTitle = "Consultar";
            ViewBag.Cescricao = descricao;
            ViewBag.Ordenacao = ordenacao;
            ViewBag.Crescente = crescente ?? true;
            ViewBag.ItensPagina = itensPagina;
            try
            {
                var consulta = _tipoAtividadeAppServico.Consultar(null, descricao, ordenacao, crescente ?? true, pagina ?? 1, itensPagina ?? 40);
                var resultado = new StaticPagedList<TipoAtividadeAppModel>(consulta.Resultado, consulta.PaginaAtual, consulta.ItensPorPagina, consulta.TotalItens);
                return View(resultado);
            }
            catch (CustomBaseException ex)
            {
                MensagemErro(ex.Mensagem);
                return View(new StaticPagedList<TipoAtividadeAppModel>(new List<TipoAtividadeAppModel>(), 1, 1, 0));
            }
        }

        [HttpGet]
        [RAHAuthorize]
        public ActionResult Adicionar()
        {
            ViewBag.SubTitle = "Adicionar novo Tipo de Atividade";

            return View(new TipoAtividadeAppModel());
        }

        [HttpPost]
        public ActionResult Adicionar(TipoAtividadeAppModel tipoAtividadeAppModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _tipoAtividadeAppServico.Adicionar(tipoAtividadeAppModel);
                    MensagemSucesso(MensagensPadrao.CadastroSucesso);
                    return RedirectToAction("Index", "TipoAtividade", new { descricao = tipoAtividadeAppModel.Descricao });
                }
                catch (CustomBaseException ex)
                {
                    MensagemErro(ex.Mensagem);
                    return View(tipoAtividadeAppModel);
                }
            }
            return View(tipoAtividadeAppModel);
        }

        [HttpGet]
        [RAHAuthorize]
        public ActionResult Editar(int id)
        {
            ViewBag.SubTitle = "Editar Tipo de Atividade";
            var tipoAtividadeModel = new TipoAtividadeAppModel();
            try
            {
                tipoAtividadeModel = _tipoAtividadeAppServico.ObterPorId(id);
                if (tipoAtividadeModel == null)
                {
                    MensagemErro("Tipo de Atividade não encontrada");
                    return RedirectToAction("Index");
                }
            }
            catch (CustomBaseException ex)
            {
                MensagemErro(ex.Mensagem);
                return RedirectToAction("Index");
            }
            return View(tipoAtividadeModel);
        }

        [HttpPost]
        public ActionResult Editar(TipoAtividadeAppModel tipoAtividadeAppModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _tipoAtividadeAppServico.Atualizar(tipoAtividadeAppModel);
                    MensagemSucesso(MensagensPadrao.AtualizacaoSucesso);
                    return RedirectToAction("Index", "TipoAtividade", new { descricao = tipoAtividadeAppModel.Descricao });
                }
                catch (CustomBaseException ex)
                {
                    MensagemErro(ex.Mensagem);
                    return View(tipoAtividadeAppModel);
                }
            }
            return View(tipoAtividadeAppModel);
        }

        [HttpGet]
        [RAHAuthorize]
        public ActionResult Excluir(int id)
        {
            ViewBag.SubTitle = "Excluir Tipo de Atividade";
            var tipoAtividadeModel = new TipoAtividadeAppModel();
            try
            {
                tipoAtividadeModel = _tipoAtividadeAppServico.ObterPorId(id);
                if (tipoAtividadeModel == null)
                {
                    MensagemErro("Tipo de Atividade não encontrada");
                    return RedirectToAction("Index");
                }
            }
            catch (CustomBaseException ex)
            {
                MensagemErro(ex.Mensagem);
                return RedirectToAction("Index");
            }
            return View(tipoAtividadeModel);
        }

        [HttpPost]
        public ActionResult Excluir(TipoAtividadeAppModel tipoAtividadeAppModel)
        {
            try
            {
                _tipoAtividadeAppServico.Remover(tipoAtividadeAppModel.IdTipoAtividade);
                MensagemSucesso(MensagensPadrao.ExclusaoSucesso);
            }
            catch (CustomBaseException ex)
            {
                MensagemErro(ex.Mensagem);
            }
            return RedirectToAction("Index", "TipoAtividade");
        }
    }
}