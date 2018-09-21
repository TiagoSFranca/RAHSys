using PagedList;
using RAHSys.Aplicacao.AppModels;
using RAHSys.Aplicacao.Interfaces;
using RAHSys.Apresentacao.Models;
using RAHSys.Extras;
using RAHSys.Infra.CrossCutting.Exceptions;
using System.Collections.Generic;
using System.Web.Mvc;

namespace RAHSys.Apresentacao.Controllers
{
    public class PagamentoController : ControllerBase
    {
        private readonly IContratoAppServico _contratoAppServico;
        private readonly IPagamentoAppServico _pagamentoAppServico;

        public PagamentoController(IContratoAppServico contratoAppServico, IPagamentoAppServico pagamentoAppServico)
        {
            _contratoAppServico = contratoAppServico;
            _pagamentoAppServico = pagamentoAppServico;
            ViewBag.Title = "Pagamentos";
        }

        public ActionResult Index(int id, int? codigo, string data, string ordenacao, bool? crescente, int? pagina, int? itensPagina)
        {
            var contratoModel = new ContratoAppModel();

            bool error = false;
            ObterContrato(id, ref contratoModel, ref error);

            if (error)
                return RedirectToAction("Index", "Contrato");

            var viewIndex = new PagamentoIndexModel(contratoModel, new StaticPagedList<PagamentoAppModel>(new List<PagamentoAppModel>(), 1, 1, 0));

            ViewBag.Codigo = codigo;
            ViewBag.DataPagamento = data;
            ViewBag.Ordenacao = ordenacao;
            ViewBag.Crescente = crescente ?? true;
            ViewBag.ItensPagina = itensPagina;
            try
            {
                var consulta = _pagamentoAppServico.Consultar(id, codigo != null ? new int[] { (int)codigo } : null, data, ordenacao, crescente ?? true, pagina ?? 1, itensPagina ?? 40);
                viewIndex.Pagamentos = new StaticPagedList<PagamentoAppModel>(consulta.Resultado, consulta.PaginaAtual, consulta.ItensPorPagina, consulta.TotalItens);

                return View(viewIndex);
            }
            catch (CustomBaseException ex)
            {
                MensagemErro(ex.Mensagem);
                return View(viewIndex);
            }
        }

        [HttpGet]
        public ActionResult Adicionar(int id)
        {
            ViewBag.SubTitle = "Adicionar novo Pagamento";

            bool error = false;
            var pagamentoModel = MontarPagamentoAdicionar(id, ref error);

            if (error)
                return RedirectToAction("Index", "Contrato");

            return View(pagamentoModel);
        }

        [HttpPost]
        public ActionResult Adicionar(PagamentoAdicionarModel contratoAdicionarModel)
        {
            bool error = false;
            var contratoRetorno = MontarPagamentoAdicionar(contratoAdicionarModel.Pagamento.IdContrato, ref error);

            if (error)
                return RedirectToAction("Index", "Contrato");

            contratoRetorno.Pagamento = contratoAdicionarModel.Pagamento;
            if (ModelState.IsValid)
            {
                try
                {
                    _pagamentoAppServico.Adicionar(contratoAdicionarModel.Pagamento);
                    MensagemSucesso(MensagensPadrao.CadastroSucesso);
                    return RedirectToAction("Index", "Pagamento", new { id = contratoAdicionarModel.Pagamento.IdContrato });
                }
                catch (CustomBaseException ex)
                {
                    MensagemErro(ex.Mensagem);
                    return View(contratoRetorno);
                }
            }
            return View(contratoRetorno);
        }

        [HttpGet]
        public ActionResult Excluir(int idContrato, int idPagamento)
        {
            ViewBag.SubTitle = "Excluir Pagamento";
            var pagamentoModel = new PagamentoAppModel();
            try
            {
                pagamentoModel = _pagamentoAppServico.ObterPorId(idPagamento);
                if (pagamentoModel == null)
                {
                    MensagemErro("Pagamento não encontrado");
                    return RedirectToAction("Index", new { idContrato });
                }
            }
            catch (CustomBaseException ex)
            {
                MensagemErro(ex.Mensagem);
                return RedirectToAction("Index", new { idContrato });
            }
            return View(pagamentoModel);
        }

        [HttpPost]
        public ActionResult Excluir(PagamentoAppModel pagamentoAppModel)
        {
            try
            {
                _pagamentoAppServico.Remover(pagamentoAppModel.IdPagamento);
                MensagemSucesso(MensagensPadrao.ExclusaoSucesso);
            }
            catch (CustomBaseException ex)
            {
                MensagemErro(ex.Mensagem);
            }
            return RedirectToAction("Index", new { id = pagamentoAppModel.IdContrato });
        }

        #region Métodos Aux

        private PagamentoAdicionarModel MontarPagamentoAdicionar(int id, ref bool error)
        {
            var retorno = new PagamentoAdicionarModel();
            var contratoModel = new ContratoAppModel();


            ObterContrato(id, ref contratoModel, ref error);

            retorno.Contrato = contratoModel;

            retorno.Pagamento = new PagamentoAppModel()
            {
                IdContrato = id
            };

            return retorno;
        }

        private void ObterContrato(int id, ref ContratoAppModel contratoModel, ref bool error)
        {
            try
            {
                contratoModel = _contratoAppServico.ObterPorId(id);
                if (contratoModel == null)
                {
                    MensagemErro("Contrato não encontrado");
                    error = true;
                }
            }
            catch (CustomBaseException ex)
            {
                MensagemErro(ex.Mensagem);
                error = true;
            }
        }

        #endregion
    }
}