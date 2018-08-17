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
    //TODO: Verificar se há regras de negócio quanto a duplicidade
    [RAHAuthorize(Roles = "Engenharia")]
    [RAHAudit]
    public class TipoTelhadoController : ControllerBase
    {
        private readonly ITipoTelhadoAppServico _tipoTelhadoAppServico;

        public TipoTelhadoController(ITipoTelhadoAppServico tipoTelhadoAppServico)
        {
            _tipoTelhadoAppServico = tipoTelhadoAppServico;
            ViewBag.Title = "Tipos de Telhado";
        }

        public ActionResult Index(string descricao, string ordenacao, bool? crescente, int? pagina, int? itensPagina)
        {
            ViewBag.SubTitle = "Consultar";
            ViewBag.Descricao = descricao;
            ViewBag.Ordenacao = ordenacao;
            ViewBag.Crescente = crescente ?? true;
            ViewBag.ItensPagina = itensPagina;
            try
            {
                var consulta = _tipoTelhadoAppServico.Consultar(null, descricao, ordenacao, crescente ?? true, pagina ?? 1, itensPagina ?? 40);
                var resultado = new StaticPagedList<TipoTelhadoAppModel>(consulta.Resultado, consulta.PaginaAtual, consulta.ItensPorPagina, consulta.TotalItens);
                return View(resultado);
            }
            catch (CustomBaseException ex)
            {
                MensagemErro(ex.Mensagem);
                return View(new StaticPagedList<TipoTelhadoAppModel>(new List<TipoTelhadoAppModel>(), 1, 1, 0));
            }
        }

        [HttpGet]
        public ActionResult Adicionar()
        {
            ViewBag.SubTitle = "Adicionar novo Tipo de Telhado";

            return View(new TipoTelhadoAppModel());
        }

        [RAHAudit]
        [HttpPost]
        public ActionResult Adicionar(TipoTelhadoAppModel tipoTelhadoAppModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _tipoTelhadoAppServico.Adicionar(tipoTelhadoAppModel);
                    MensagemSucesso(MensagensPadrao.CadastroSucesso);
                    return RedirectToAction("Index", "TipoTelhado", new { descricao = tipoTelhadoAppModel.Descricao });
                }
                catch (CustomBaseException ex)
                {
                    MensagemErro(ex.Mensagem);
                    return View(tipoTelhadoAppModel);
                }
            }
            return View(tipoTelhadoAppModel);
        }

        [HttpGet]
        public ActionResult Editar(int id)
        {
            ViewBag.SubTitle = "Editar Tipo de Telhado";
            var tipoTelhadoModel = new TipoTelhadoAppModel();
            try
            {
                tipoTelhadoModel = _tipoTelhadoAppServico.ObterPorId(id);
                if (tipoTelhadoModel == null)
                {
                    MensagemErro("Tipo de Telhado não encontrado");
                    return RedirectToAction("Index");
                }
            }
            catch (CustomBaseException ex)
            {
                MensagemErro(ex.Mensagem);
                return RedirectToAction("Index");
            }
            return View(tipoTelhadoModel);
        }

        [HttpPost]
        public ActionResult Editar(TipoTelhadoAppModel tipoTelhadoAppModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _tipoTelhadoAppServico.Atualizar(tipoTelhadoAppModel);
                    MensagemSucesso(MensagensPadrao.AtualizacaoSucesso);
                    return RedirectToAction("Index", "TipoTelhado", new { descricao = tipoTelhadoAppModel.Descricao });
                }
                catch (CustomBaseException ex)
                {
                    MensagemErro(ex.Mensagem);
                    return View(tipoTelhadoAppModel);
                }
            }
            return View(tipoTelhadoAppModel);
        }

        [HttpGet]
        public ActionResult Excluir(int id)
        {
            ViewBag.SubTitle = "Excluir Tipo de Telhado";
            var tipoTelhadoModel = new TipoTelhadoAppModel();
            try
            {
                tipoTelhadoModel = _tipoTelhadoAppServico.ObterPorId(id);
                if (tipoTelhadoModel == null)
                {
                    MensagemErro("Tipo de Telhado não encontrado");
                    return RedirectToAction("Index");
                }
            }
            catch (CustomBaseException ex)
            {
                MensagemErro(ex.Mensagem);
                return RedirectToAction("Index");
            }
            return View(tipoTelhadoModel);
        }

        [HttpPost]
        public ActionResult Excluir(TipoTelhadoAppModel tipoTelhadoAppModel)
        {
            try
            {
                _tipoTelhadoAppServico.Remover(tipoTelhadoAppModel.IdTipoTelhado);
                MensagemSucesso(MensagensPadrao.ExclusaoSucesso);
            }
            catch (CustomBaseException ex)
            {
                MensagemErro(ex.Mensagem);
            }
            return RedirectToAction("Index", "TipoTelhado");
        }
    }
}