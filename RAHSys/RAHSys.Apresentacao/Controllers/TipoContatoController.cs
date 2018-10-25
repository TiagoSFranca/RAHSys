using PagedList;
using RAHSys.Aplicacao.AppModels;
using RAHSys.Aplicacao.Interfaces;
using RAHSys.Extras;
using RAHSys.Extras.Enums;
using RAHSys.Infra.CrossCutting.Exceptions;
using System.Collections.Generic;
using System.Web.Mvc;

namespace RAHSys.Apresentacao.Controllers
{
    //TODO: Verificar se há regras de negócio quanto a duplicidade
    public class TipoContatoController : ControllerBase
    {
        private readonly ITipoContatoAppServico _tipoContatoAppServico;

        public TipoContatoController(ITipoContatoAppServico tipoContatoAppServico)
        {
            _tipoContatoAppServico = tipoContatoAppServico;
            ViewBag.Title = "Tipos de Contato";
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
                var consulta = _tipoContatoAppServico.Consultar(null, descricao, ordenacao, crescente ?? true, pagina ?? 1, itensPagina ?? (int)ItensPorPaginaEnum.MEDIO);
                var resultado = new StaticPagedList<TipoContatoAppModel>(consulta.Resultado, consulta.PaginaAtual, consulta.ItensPorPagina, consulta.TotalItens);
                return View(resultado);
            }
            catch (CustomBaseException ex)
            {
                MensagemErro(ex.Mensagem);
                return View(new StaticPagedList<TipoContatoAppModel>(new List<TipoContatoAppModel>(), 1, 1, 0));
            }
        }

        [HttpGet]
        public ActionResult Adicionar()
        {
            ViewBag.SubTitle = "Adicionar novo Tipo de Contato";

            return View(new TipoContatoAppModel());
        }

        [HttpPost]
        public ActionResult Adicionar(TipoContatoAppModel tipoContatoAppModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _tipoContatoAppServico.Adicionar(tipoContatoAppModel);
                    MensagemSucesso(MensagensPadrao.CadastroSucesso);
                    return RedirectToAction("Index", "TipoContato", new { descricao = tipoContatoAppModel.Descricao });
                }
                catch (CustomBaseException ex)
                {
                    MensagemErro(ex.Mensagem);
                    return View(tipoContatoAppModel);
                }
            }
            return View(tipoContatoAppModel);
        }

        [HttpGet]
        public ActionResult Editar(int id)
        {
            ViewBag.SubTitle = "Editar Tipo de Contato";
            var tipoContatoModel = new TipoContatoAppModel();
            try
            {
                tipoContatoModel = _tipoContatoAppServico.ObterPorId(id);
                if (tipoContatoModel == null)
                {
                    MensagemErro("Tipo de Contato não encontrado");
                    return RedirectToAction("Index");
                }
            }
            catch (CustomBaseException ex)
            {
                MensagemErro(ex.Mensagem);
                return RedirectToAction("Index");
            }
            return View(tipoContatoModel);
        }

        [HttpPost]
        public ActionResult Editar(TipoContatoAppModel tipoContatoAppModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _tipoContatoAppServico.Atualizar(tipoContatoAppModel);
                    MensagemSucesso(MensagensPadrao.AtualizacaoSucesso);
                    return RedirectToAction("Index", "TipoContato", new { descricao = tipoContatoAppModel.Descricao });
                }
                catch (CustomBaseException ex)
                {
                    MensagemErro(ex.Mensagem);
                    return View(tipoContatoAppModel);
                }
            }
            return View(tipoContatoAppModel);
        }

        [HttpGet]
        public ActionResult Excluir(int id)
        {
            ViewBag.SubTitle = "Excluir Tipo de Contato";
            var tipoContatoModel = new TipoContatoAppModel();
            try
            {
                tipoContatoModel = _tipoContatoAppServico.ObterPorId(id);
                if (tipoContatoModel == null)
                {
                    MensagemErro("Tipo de Contato não encontrado");
                    return RedirectToAction("Index");
                }
            }
            catch (CustomBaseException ex)
            {
                MensagemErro(ex.Mensagem);
                return RedirectToAction("Index");
            }
            return View(tipoContatoModel);
        }

        [HttpPost]
        public ActionResult Excluir(TipoContatoAppModel tipoContatoAppModel)
        {
            try
            {
                _tipoContatoAppServico.Remover(tipoContatoAppModel.IdTipoContato);
                MensagemSucesso(MensagensPadrao.ExclusaoSucesso);
            }
            catch (CustomBaseException ex)
            {
                MensagemErro(ex.Mensagem);
            }
            return RedirectToAction("Index", "TipoContato");
        }
    }
}