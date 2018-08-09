using PagedList;
using RAHSys.Aplicacao.AppModels;
using RAHSys.Aplicacao.Interfaces;
using RAHSys.Apresentacao.Models;
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

        public ActionResult Index(string nomeEmpresa, string cidade, string ordenacao, bool? crescente, int? pagina, int? itensPagina)
        {
            ViewBag.SubTitle = "Consultar";
            ViewBag.NomeEmpresa = nomeEmpresa;
            ViewBag.Cidade = cidade;
            ViewBag.Ordenacao = ordenacao;
            ViewBag.Crescente = crescente ?? true;
            ViewBag.ItensPagina = itensPagina;
            try
            {
                var consulta = _contratoAppServico.Consultar(null, nomeEmpresa, cidade, ordenacao, crescente ?? true, pagina ?? 1, itensPagina ?? 40);
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
            var contratoModel = new ContratoAdicionarModel();
            contratoModel.Estados = _estadoAppService.ListarTodos();
            var estado = contratoModel.Estados.FirstOrDefault();
            if (estado != null)
                contratoModel.Cidades = _cidadeAppService.ObterCidadesPorEstado(estado.IdEstado);
            return View(contratoModel);
        }

        [HttpPost]
        public ActionResult Adicionar(ContratoAppModel contrato)
        {
            return null;
        }
    }
}