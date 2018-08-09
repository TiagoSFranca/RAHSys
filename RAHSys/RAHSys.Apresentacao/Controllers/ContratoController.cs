using PagedList;
using RAHSys.Aplicacao.AppModels;
using RAHSys.Aplicacao.Interfaces;
using RAHSys.Infra.CrossCutting.Exceptions;
using System.Collections.Generic;
using System.Web.Mvc;

namespace RAHSys.Apresentacao.Controllers
{
    public class ContratoController : ControllerBase
    {
        private readonly IContratoAppServico _cameraAppServico;

        public ContratoController(IContratoAppServico cameraAppServico)
        {
            _cameraAppServico = cameraAppServico;
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
                var consulta = _cameraAppServico.Consultar(null, nomeEmpresa, cidade, ordenacao, crescente ?? true, pagina ?? 1, itensPagina ?? 40);
                var resultado = new StaticPagedList<ContratoAppModel>(consulta.Resultado, consulta.PaginaAtual, consulta.ItensPorPagina, consulta.TotalItens);
                return View(resultado);
            }
            catch (CustomBaseException ex)
            {
                MensagemErro(ex.Mensagem);
                return View(new StaticPagedList<ContratoAppModel>(new List<ContratoAppModel>(), 1, 1, 0));
            }
        }
    }
}