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
        private readonly IEstadoAppServico _estadoAppServico;
        private readonly ICidadeAppServico _cidadeAppServico;
        private readonly ITipoTelhadoAppServico _tipoTelhadoAppServico;
        private readonly IEstadoCivilAppServico _estadoCivilAppServico;

        public ContratoController(IContratoAppServico contratoAppServico, IEstadoAppServico estadoAppServico,
            ICidadeAppServico cidadeAppServico, ITipoTelhadoAppServico tipoTelhadoAppServico, IEstadoCivilAppServico estadoCivilAppServico)
        {
            _contratoAppServico = contratoAppServico;
            _estadoAppServico = estadoAppServico;
            _cidadeAppServico = cidadeAppServico;
            _tipoTelhadoAppServico = tipoTelhadoAppServico;
            _estadoCivilAppServico = estadoCivilAppServico;
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

            contratoModel.Estados = _estadoAppServico.ListarTodos();
            if (idEstado != null)
                contratoModel.Cidades = _cidadeAppServico.ObterCidadesPorEstado((int)idEstado);
            return contratoModel;
        }

        private AnaliseInvestimentoAdicionar MontarAnaliseInvestimento()
        {
            var retorno = new AnaliseInvestimentoAdicionar();
            retorno.TipoTelhados = _tipoTelhadoAppServico.ListarTodos();
            return retorno;
        }

        private FichaClienteAdicionar MontarFichaCliente(int id, int? idEstado = null, int? idEstadoConjuge = null)
        {
            var retorno = new FichaClienteAdicionar();
            retorno.Contrato = _contratoAppServico.ObterPorId(id);

            retorno.Estados = _estadoAppServico.ListarTodos();
            retorno.EstadosCivis = _estadoCivilAppServico.ListarTodos();

            if (idEstado != null)
                retorno.CidadesFiador = _cidadeAppServico.ObterCidadesPorEstado((int)idEstado);

            if (idEstadoConjuge != null)
                retorno.CidadesFiadorConjuge = _cidadeAppServico.ObterCidadesPorEstado((int)idEstado);

            return retorno;
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

        [HttpGet]
        public ActionResult AdicionarAnaliseInvestimento(int id)
        {
            ViewBag.SubTitle = "Editar Contrato";
            ViewBag.SubSubTitle = "Ficha do Cliente (Análise de Investimento/Receita)";

            try
            {
                var contratoModel = _contratoAppServico.ObterPorId(id);
                if (contratoModel == null)
                {
                    MensagemErro("Contrato não encontrado");
                    return RedirectToAction("Index");
                }
                if (contratoModel.AnaliseInvestimento != null)
                {
                    MensagemErro("Não é possível adicionar análise de investimento");
                    return RedirectToAction("Index");
                }
            }
            catch (CustomBaseException ex)
            {
                MensagemErro(ex.Mensagem);
                return RedirectToAction("Index");
            }

            var retorno = MontarAnaliseInvestimento();
            retorno.AnaliseInvestimento = new AnaliseInvestimentoAppModel() { IdContrato = id };

            return View(retorno);
        }

        [HttpPost]
        public ActionResult AdicionarAnaliseInvestimento(AnaliseInvestimentoAdicionar analiseInvestimentoAdicionarModel)
        {
            var analiseInvestimentoRetorno = MontarAnaliseInvestimento();
            analiseInvestimentoRetorno.AnaliseInvestimento = analiseInvestimentoAdicionarModel.AnaliseInvestimento;
            if (ModelState.IsValid)
            {
                try
                {
                    _contratoAppServico.AdicionarAnaliseInvestimento(analiseInvestimentoAdicionarModel.AnaliseInvestimento);
                    MensagemSucesso(MensagensPadrao.CadastroSucesso);
                    return RedirectToAction("Index", "Contrato");
                }
                catch (CustomBaseException ex)
                {
                    MensagemErro(ex.Mensagem);
                    return View(analiseInvestimentoRetorno);
                }
            }
            return View(analiseInvestimentoRetorno);
        }

        [HttpGet]
        public ActionResult AdicionarFichaCliente(int id)
        {
            ViewBag.SubTitle = "Editar Contrato";
            ViewBag.SubSubTitle = "Ficha do Cliente";

            try
            {
                var contratoModel = _contratoAppServico.ObterPorId(id);
                if (contratoModel == null)
                {
                    MensagemErro("Contrato não encontrado");
                    return RedirectToAction("Index");
                }

                if (contratoModel.AnaliseInvestimento == null)
                {

                    MensagemErro("Necessário adicionar análise de investimento");
                    return RedirectToAction("Index");
                }

                if (contratoModel.AnaliseInvestimento.Cliente != null)
                {
                    MensagemErro("Não é possível adicionar ficha do cliente");
                    return RedirectToAction("Index");
                }
            }
            catch (CustomBaseException ex)
            {
                MensagemErro(ex.Mensagem);
                return RedirectToAction("Index");
            }

            var retorno = MontarFichaCliente(id);
            retorno.Cliente = new ClienteAppModel() { IdAnaliseInvestimento = id };

            return View(retorno);
        }

        [HttpPost]
        public ActionResult AdicionarFichaCliente(FichaClienteAdicionar fichaCliente)
        {
            int? idEstado = fichaCliente?.Cliente?.Fiadores?.FirstOrDefault()?.FiadorEndereco?.Endereco?.Cidade?.IdEstado;
            int? idEstadoConjuge = null;
            if (fichaCliente?.Cliente?.Fiadores?.Count() > 1)
            {
                idEstadoConjuge = fichaCliente?.Cliente?.Fiadores[1]?.FiadorEndereco?.Endereco?.Cidade?.IdEstado;
                fichaCliente.Cliente.Fiadores[1].IdEstadoCivil = fichaCliente.Cliente.Fiadores[0].IdEstadoCivil;
                fichaCliente.Cliente.Fiadores[1].Conjuge = true;
            }
            var analiseInvestimentoRetorno = MontarFichaCliente(fichaCliente.Cliente.IdAnaliseInvestimento, idEstado, idEstadoConjuge);
            analiseInvestimentoRetorno.Cliente = fichaCliente.Cliente;
            if (ModelState.IsValid)
            {
                try
                {
                    _contratoAppServico.AdicionarFichaCliente(fichaCliente.Cliente);
                    MensagemSucesso(MensagensPadrao.CadastroSucesso);
                    return RedirectToAction("Index", "Contrato");
                }
                catch (CustomBaseException ex)
                {
                    MensagemErro(ex.Mensagem);
                    return View(analiseInvestimentoRetorno);
                }
            }
            return View(analiseInvestimentoRetorno);
        }

        [HttpGet]
        public ActionResult Editar(int id)
        {
            ViewBag.SubTitle = "Editar Contrato";
            var contratoModel = new ContratoAppModel();
            try
            {
                contratoModel = _contratoAppServico.ObterPorId(id);
                if (contratoModel == null)
                {
                    MensagemErro("Contrato não encontrado");
                    return RedirectToAction("Index");
                }
                if (contratoModel.AnaliseInvestimento == null)
                    return RedirectToAction("AdicionarAnaliseInvestimento", new { id = id });
                else if (contratoModel.AnaliseInvestimento.Cliente == null)
                    return RedirectToAction("AdicionarFichaCliente", new { id = id });
                else
                    MensagemErro("Não é possível editar o contrato");
                return RedirectToAction("Index");
            }
            catch (CustomBaseException ex)
            {
                MensagemErro(ex.Mensagem);
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public JsonResult ObterCidadesPorEstado(int id)
        {
            List<CidadeAppModel> lista = new List<CidadeAppModel>();
            lista.AddRange(_cidadeAppServico.ObterCidadesPorEstado(id));
            return Json(lista, JsonRequestBehavior.AllowGet);
        }
    }
}