using PagedList;
using RAHSys.Aplicacao.AppModels;
using RAHSys.Aplicacao.Interfaces;
using RAHSys.Infra.CrossCutting.Exceptions;
using System.Collections.Generic;
using System.Web.Mvc;

namespace RAHSys.Apresentacao.Controllers
{
    public class AtividadeController : ControllerBase
    {
        private readonly IAtividadeAppServico _atividadeAppServico;

        public AtividadeController(IAtividadeAppServico atividadeAppServico)
        {
            _atividadeAppServico = atividadeAppServico;
            ViewBag.Title = "Atividades";
        }

        public ActionResult Index(int? codigo, int? tipoAtividade, int? contrato, int? equipe, string usuario, string dataRealizacaoInicio,
            string dataRealizacaoFim, string dataPrevistaInicio, string dataPrevistaFim, bool? realizada,
            string ordenacao, bool? crescente, int? pagina, int? itensPagina)
        {
            ViewBag.SubTitle = "Consultar";
            ViewBag.Codigo = codigo;
            ViewBag.TipoAtividade = tipoAtividade;
            ViewBag.Contrato = contrato;
            ViewBag.Equipe = equipe;
            ViewBag.DataRealizacaoInicio = dataRealizacaoInicio;
            ViewBag.DataRealizacaoFim = dataRealizacaoFim;
            ViewBag.DataPrevistaInicio = dataPrevistaInicio;
            ViewBag.DataPrevistaFim = dataPrevistaFim;
            ViewBag.Realizada = realizada;
            ViewBag.Ordenacao = ordenacao;
            ViewBag.Crescente = crescente ?? true;
            ViewBag.ItensPagina = itensPagina;
            try
            {
                var consulta = _atividadeAppServico.Consultar(codigo != null ? new int[] { (int)codigo } : null,
                    tipoAtividade != null ? new int[] { (int)tipoAtividade } : null,
                    equipe != null ? new int[] { (int)equipe } : null,
                    contrato != null ? new int[] { (int)contrato } : null,
                    !string.IsNullOrEmpty(usuario) ? new string[] { usuario } : null,
                    realizada, dataRealizacaoInicio, dataRealizacaoFim, dataPrevistaInicio, dataPrevistaFim,
                    ordenacao, crescente ?? true, pagina ?? 1, itensPagina ?? 40);
                var resultado = new StaticPagedList<AtividadeAppModel>(consulta.Resultado, consulta.PaginaAtual, consulta.ItensPorPagina, consulta.TotalItens);
                return View(resultado);
            }
            catch (CustomBaseException ex)
            {
                MensagemErro(ex.Mensagem);
                return View(new StaticPagedList<AtividadeAppModel>(new List<AtividadeAppModel>(), 1, 1, 0));
            }
        }
    }
}