using PagedList;
using RAHSys.Aplicacao.AppModels;
using RAHSys.Aplicacao.Interfaces;
using RAHSys.Apresentacao.Models;
using RAHSys.Extras;
using RAHSys.Infra.CrossCutting.Exceptions;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace RAHSys.Apresentacao.Controllers
{
    public class ContratoController : ControllerBase
    {
        private readonly IContratoAppServico _contratoAppServico;
        private readonly IEstadoAppServico _estadoAppService;
        private readonly ICidadeAppServico _cidadeAppService;

        public ContratoController(IContratoAppServico contratoAppServico, IEstadoAppServico estadoAppService, ICidadeAppServico cidadeAppService)
        {
            _contratoAppServico = contratoAppServico;
            _estadoAppService = estadoAppService;
            _cidadeAppService = cidadeAppService;
            ViewBag.Title = "Clientes/Contratos";
        }

        public ActionResult Index(int? codigo, string nomeEmpresa, string cidade, string ordenacao, bool? crescente, int? pagina, int? itensPagina)
        {
            ViewBag.SubTitle = "Consultar";
            ViewBag.Codigo = codigo;
            ViewBag.NomeEmpresa = nomeEmpresa;
            ViewBag.Cidade = cidade;
            ViewBag.Ordenacao = ordenacao;
            ViewBag.Crescente = crescente ?? true;
            ViewBag.ItensPagina = itensPagina;
            try
            {
                var consulta = _contratoAppServico.Consultar(codigo != null ? new int[] { (int)codigo } : null, nomeEmpresa, cidade, ordenacao, crescente ?? true, pagina ?? 1, itensPagina ?? 40);
                var resultado = new StaticPagedList<ContratoAppModel>(consulta.Resultado, consulta.PaginaAtual, consulta.ItensPorPagina, consulta.TotalItens);
                return View(resultado);
            }
            catch (CustomBaseException ex)
            {
                MensagemErro(ex.Mensagem);
                return View(new StaticPagedList<ContratoAppModel>(new List<ContratoAppModel>(), 1, 1, 0));
            }
        }

        [HttpGet]
        public ActionResult Adicionar()
        {
            ViewBag.SubTitle = "Adicionar novo Contrato";
            var contratoModel = MontarContratoAdicionar();
            return View(contratoModel);
        }

        private ContratoAdicionarModel MontarContratoAdicionar(int? idEstado = null)
        {
            var contratoModel = new ContratoAdicionarModel();

            contratoModel.Estados = _estadoAppService.ListarTodos();
            if (idEstado != null)
                contratoModel.Cidades = _cidadeAppService.ObterCidadesPorEstado((int)idEstado);
            else
            {
                var estado = contratoModel.Estados.FirstOrDefault();
                if (estado != null)
                    contratoModel.Cidades = _cidadeAppService.ObterCidadesPorEstado(estado.IdEstado);
            }
            return contratoModel;
        }

        [HttpPost]
        public ActionResult Adicionar(ContratoAdicionarModel contratoAdicionarModel)
        {
            var contratoRetorno = MontarContratoAdicionar(contratoAdicionarModel.Contrato.ContratoEndereco.Endereco.Cidade.IdEstado);
            contratoRetorno.Contrato = contratoAdicionarModel.Contrato;
            if (ModelState.IsValid)
            {
                try
                {
                    _contratoAppServico.Adicionar(contratoAdicionarModel.Contrato);
                    MensagemSucesso(MensagensPadrao.CadastroSucesso);
                    return RedirectToAction("Index", "Contrato", new { nomeEmpresa = contratoAdicionarModel.Contrato.NomeEmpresa, cidade = contratoAdicionarModel.Contrato.ContratoEndereco.Endereco.Cidade.Nome });
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
        public ActionResult Excluir(int id)
        {
            ViewBag.SubTitle = "Excluir Contrato";
            var contratoModel = new ContratoAppModel();
            try
            {
                contratoModel = _contratoAppServico.ObterPorId(id);
                if (contratoModel == null)
                {
                    MensagemErro("Contrato não encontrado");
                    return RedirectToAction("Index");
                }
            }
            catch (CustomBaseException ex)
            {
                MensagemErro(ex.Mensagem);
                return RedirectToAction("Index");
            }
            return View(contratoModel);
        }

        [HttpPost]
        public ActionResult Excluir(ContratoAppModel contratoAppModel)
        {
            try
            {
                _contratoAppServico.Remover(contratoAppModel.IdContrato);
                MensagemSucesso(MensagensPadrao.ExclusaoSucesso);
            }
            catch (CustomBaseException ex)
            {
                MensagemErro(ex.Mensagem);
            }
            return RedirectToAction("Index", "Contrato");
        }
    }
}