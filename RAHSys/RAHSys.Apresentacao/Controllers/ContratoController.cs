using AutoMapper;
using PagedList;
using RAHSys.Aplicacao.AppModels;
using RAHSys.Aplicacao.Interfaces;
using RAHSys.Apresentacao.Attributes;
using RAHSys.Apresentacao.Models;
using RAHSys.Extras;
using RAHSys.Infra.CrossCutting.Exceptions;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;

namespace RAHSys.Apresentacao.Controllers
{
    [RAHAudit]
    public class ContratoController : ControllerBase
    {
        private readonly IContratoAppServico _contratoAppServico;
        private readonly IEstadoAppServico _estadoAppServico;
        private readonly ICidadeAppServico _cidadeAppServico;
        private readonly ITipoTelhadoAppServico _tipoTelhadoAppServico;
        private readonly IEstadoCivilAppServico _estadoCivilAppServico;
        private readonly IDocumentoAppServico _documentoAppServico;
        private readonly IEquipeAppServico _equipeAppServico;
        private readonly ITipoAtividadeAppServico _tipoAtividadeAppServico;
        private readonly IAtividadeAppServico _atividadeAppServico;

        public ContratoController(IContratoAppServico contratoAppServico, IEstadoAppServico estadoAppServico,
            ICidadeAppServico cidadeAppServico, ITipoTelhadoAppServico tipoTelhadoAppServico, IEstadoCivilAppServico estadoCivilAppServico,
            IDocumentoAppServico documentoAppServico, IEquipeAppServico equipeAppServico, ITipoAtividadeAppServico tipoAtividadeAppServico,
            IAtividadeAppServico atividadeAppServico)
        {
            _contratoAppServico = contratoAppServico;
            _estadoAppServico = estadoAppServico;
            _cidadeAppServico = cidadeAppServico;
            _tipoTelhadoAppServico = tipoTelhadoAppServico;
            _estadoCivilAppServico = estadoCivilAppServico;
            _documentoAppServico = documentoAppServico;
            _equipeAppServico = equipeAppServico;
            _tipoAtividadeAppServico = tipoAtividadeAppServico;
            _atividadeAppServico = atividadeAppServico;
            ViewBag.Title = "Clientes/Contratos";
        }

        public ActionResult Index(int? codigo, int? estado, string nomeEmpresa, decimal? receita, string cidade, string ordenacao, bool? crescente, int? pagina, int? itensPagina)
        {
            var contratoIndex = new ContratoIndexModel();
            ViewBag.SubTitle = "Consultar";
            ViewBag.Codigo = codigo;
            ViewBag.NomeEmpresa = nomeEmpresa;
            ViewBag.Receita = receita;
            ViewBag.Cidade = cidade;
            ViewBag.Ordenacao = ordenacao;
            ViewBag.Crescente = crescente ?? true;
            ViewBag.ItensPagina = itensPagina;
            ViewBag.Estado = estado;
            var estados = _estadoAppServico.ListarTodos();
            contratoIndex.Estados = estados;
            try
            {
                var consulta = _contratoAppServico.Consultar(codigo != null ? new int[] { (int)codigo } : null, estado != null ? new int[] { (int)estado } : null, nomeEmpresa, receita, cidade, ordenacao, crescente ?? true, pagina ?? 1, itensPagina ?? 40);
                contratoIndex.Dados = new StaticPagedList<ContratoAppModel>(consulta.Resultado, consulta.PaginaAtual, consulta.ItensPorPagina, consulta.TotalItens);
                return View(contratoIndex);
            }
            catch (CustomBaseException ex)
            {
                MensagemErro(ex.Mensagem);
                return View(contratoIndex);
            }
        }

        [HttpGet]
        [RAHAuthorize(Roles = "Comercial")]
        public ActionResult Adicionar()
        {
            ViewBag.SubTitle = "Adicionar novo Contrato";
            var contratoModel = MontarContratoAdicionar();
            return View(contratoModel);
        }

        [HttpPost]
        [RAHAuthorize(Roles = "Comercial")]
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
        [RAHAuthorize(Roles = "Engenharia")]
        public ActionResult AdicionarAnaliseInvestimento(int id)
        {
            ViewBag.SubTitle = "Editar Contrato";
            ViewBag.SubSubTitle = "Ficha do Cliente (Análise de Investimento/Receita)";
            ContratoAppModel contratoModel = null;
            try
            {
                contratoModel = _contratoAppServico.ObterPorId(id);
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
        [RAHAuthorize(Roles = "Engenharia")]
        public ActionResult AdicionarAnaliseInvestimento(AnaliseInvestimentoAdicionarModel analiseInvestimentoAdicionarModel)
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
        [RAHAuthorize(Roles = "Comercial")]
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
        [RAHAuthorize(Roles = "Comercial")]
        public ActionResult AdicionarFichaCliente(FichaClienteAdicionarModel fichaCliente)
        {
            int? idEstado = fichaCliente?.Cliente?.Fiadores?.FirstOrDefault()?.FiadorEndereco?.Endereco?.Cidade?.IdEstado;
            int? idEstadoConjuge = null;
            if (fichaCliente?.Cliente?.Fiadores?.Count() > 1)
            {
                idEstadoConjuge = fichaCliente?.Cliente?.Fiadores[1]?.FiadorEndereco?.Endereco?.Cidade?.IdEstado;
                fichaCliente.Cliente.Fiadores[1].IdEstadoCivil = fichaCliente.Cliente.Fiadores[0].IdEstadoCivil;
                fichaCliente.Cliente.Fiadores[1].Conjuge = true;
            }
            else
                fichaCliente.Cliente.Fiadores.Add(new FiadorAppModel());

            var analiseInvestimentoRetorno = MontarFichaCliente(fichaCliente.Cliente.IdAnaliseInvestimento, idEstado, idEstadoConjuge);
            analiseInvestimentoRetorno.Cliente = fichaCliente.Cliente;

            var arquivo = Request.Files[0];

            if (arquivo?.ContentLength > 0)
            {
                try
                {
                    _contratoAppServico.AdicionarDocumento(fichaCliente.Cliente.IdAnaliseInvestimento, Mapper.Map<ArquivoAppModel>(arquivo));
                    MensagemSucesso("Arquivo adicionado com sucesso!");
                }
                catch (CustomBaseException ex)
                {
                    MensagemErro(ex.Mensagem);
                    return View(analiseInvestimentoRetorno);
                }
            }


            analiseInvestimentoRetorno = MontarFichaCliente(fichaCliente.Cliente.IdAnaliseInvestimento, idEstado, idEstadoConjuge);
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
        public ActionResult VisualizarContrato(int id)
        {
            ViewBag.SubTitle = "Editar Contrato";
            var contratoModel = new ContratoAppModel();
            try
            {
                var documento = _documentoAppServico.ObterPorId(id);
                if (documento != null)
                {
                    var route = HttpContext.Server.MapPath(documento.CaminhoArquivo);
                    if (System.IO.File.Exists(route))
                    {
                        var extensao = Path.GetExtension(route);
                        var arquivo = System.IO.File.ReadAllBytes(route);
                        return File(arquivo, ContentTypeHelper.GetMimeType(extensao));
                    }
                }
                MensagemErro("Documento não encontrado");
                return RedirectToAction("Index");
            }
            catch (CustomBaseException ex)
            {
                MensagemErro(ex.Mensagem);
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public ActionResult VisualizarPagamentos(int id)
        {
            ViewBag.SubTitle = "Visualizar Pagamentos";
            var contratoModel = new ContratoAppModel();
            try
            {
                contratoModel = _contratoAppServico.ObterPorId(id);
                if (contratoModel == null)
                {
                    MensagemErro("Contrato não encontrado");
                    return RedirectToAction("Index");
                }

                return View(contratoModel);
            }
            catch (CustomBaseException ex)
            {
                MensagemErro(ex.Mensagem);
                return RedirectToAction("Index");
            }
        }

        public ActionResult AdicionarAtividade(int id)
        {
            ViewBag.SubTitle = "Adicionar nova Atividade";
            var atividadeContratoAdicionarModel = MontarAtividadeContratoAdicionar();
            try
            {
                var contratoModel = _contratoAppServico.ObterPorId(id);
                if (contratoModel == null)
                {
                    MensagemErro("Contrato não encontrado");
                    return RedirectToAction("Index", "Contrato");
                }
                if (contratoModel.AnaliseInvestimento?.Cliente == null)
                {
                    MensagemErro("O contrato precisa estar assinado");
                    return RedirectToAction("Index", "Contrato");
                }
                if (contratoModel.AnaliseInvestimento?.Cliente?.IdEquipe == null)
                {
                    MensagemErro("O contrato não possui equipe associada");
                    return RedirectToAction("Index", "Contrato");
                }

                atividadeContratoAdicionarModel.Contrato = contratoModel;
                int idEquipe = (int)contratoModel.AnaliseInvestimento.Cliente.IdEquipe;
                atividadeContratoAdicionarModel.Equipe = _equipeAppServico.ObterPorId(idEquipe);
                atividadeContratoAdicionarModel.Atividade = new AtividadeAppModel()
                {
                    IdContrato = id,
                    IdEquipe = idEquipe
                };

                return View(atividadeContratoAdicionarModel);
            }
            catch (CustomBaseException ex)
            {
                MensagemErro(ex.Mensagem);
                return RedirectToAction("Index", "Contrato");
            }
        }

        [HttpPost]
        public ActionResult AdicionarAtividade(AtividadeContratoAdicionarEditarModel atividadePostModel)
        {
            ViewBag.SubTitle = "Adicionar nova Atividade";
            var atividadeRetornoModel = MontarAtividadeContratoAdicionar();
            atividadeRetornoModel.Atividade = atividadePostModel.Atividade;
            var contratoModel = _contratoAppServico.ObterPorId(atividadePostModel.Atividade.IdContrato);
            if (contratoModel == null)
            {
                MensagemErro("Contrato não encontrado");
                return RedirectToAction("Index", "Contrato");
            }
            if (contratoModel.AnaliseInvestimento?.Cliente == null)
            {
                MensagemErro("O contrato precisa estar assinado");
                return RedirectToAction("Index", "Contrato");
            }
            if (contratoModel.AnaliseInvestimento?.Cliente?.IdEquipe == null)
            {
                MensagemErro("O contrato não possui equipe associada");
                return RedirectToAction("Index", "Contrato");
            }

            atividadeRetornoModel.Contrato = contratoModel;
            int idEquipe = (int)contratoModel.AnaliseInvestimento.Cliente.IdEquipe;
            atividadeRetornoModel.Equipe = _equipeAppServico.ObterPorId(idEquipe);

            if (ModelState.IsValid)
            {
                try
                {
                    _atividadeAppServico.Adicionar(atividadePostModel.Atividade);
                    MensagemSucesso(MensagensPadrao.CadastroSucesso);
                    return RedirectToAction("Contrato", "Atividade", new { id = atividadePostModel.Atividade.IdContrato });
                }
                catch (CustomBaseException ex)
                {
                    MensagemErro(ex.Mensagem);
                    return View(atividadeRetornoModel);
                }
            }

            return View(atividadeRetornoModel);
        }

        public ActionResult EditarAtividade(int id, int idAtividade)
        {
            ViewBag.SubTitle = "Editar Atividade";
            var atividadeContratoAdicionarModel = MontarAtividadeContratoAdicionar();
            try
            {
                var atividade = _atividadeAppServico.ObterPorId(idAtividade);

                if (atividade == null)
                {
                    MensagemErro("Atividade não encontrada");
                    return RedirectToAction("Contrato", "Atividade", new { id });
                }

                var contratoModel = _contratoAppServico.ObterPorId(id);
                if (contratoModel == null)
                {
                    MensagemErro("Contrato não encontrado");
                    return RedirectToAction("Index", "Contrato");
                }
                if (contratoModel.AnaliseInvestimento?.Cliente == null)
                {
                    MensagemErro("O contrato precisa estar assinado");
                    return RedirectToAction("Index", "Contrato");
                }
                if (contratoModel.AnaliseInvestimento?.Cliente?.IdEquipe == null)
                {
                    MensagemErro("O contrato não possui equipe associada");
                    return RedirectToAction("Index", "Contrato");
                }

                if (atividade.IdContrato != contratoModel.IdContrato)
                {
                    MensagemErro("Atividade não pertence ao contrato");
                    return RedirectToAction("Contrato", "Atividade", new { id });
                }

                atividadeContratoAdicionarModel.Contrato = contratoModel;
                atividadeContratoAdicionarModel.Equipe = _equipeAppServico.ObterPorId(atividade.IdEquipe);
                atividadeContratoAdicionarModel.Atividade = atividade;

                return View(atividadeContratoAdicionarModel);
            }
            catch (CustomBaseException ex)
            {
                MensagemErro(ex.Mensagem);
                return RedirectToAction("Index", "Contrato");
            }
        }

        [HttpPost]
        public ActionResult EditarAtividade(AtividadeContratoAdicionarEditarModel atividadePostModel)
        {
            ViewBag.SubTitle = "Adicionar nova Atividade";
            var atividadeRetornoModel = MontarAtividadeContratoAdicionar();
            atividadeRetornoModel.Atividade = atividadePostModel.Atividade;
            var atividade = _atividadeAppServico.ObterPorId(atividadePostModel.Atividade.IdAtividade);
            int idContrato = atividadePostModel.Atividade.IdContrato;
            if (atividade == null)
            {
                MensagemErro("Atividade não encontrada");
                return RedirectToAction("Contrato", "Atividade", new { id = idContrato });
            }

            var contratoModel = _contratoAppServico.ObterPorId(idContrato);
            if (contratoModel == null)
            {
                MensagemErro("Contrato não encontrado");
                return RedirectToAction("Index", "Contrato");
            }
            if (contratoModel.AnaliseInvestimento?.Cliente == null)
            {
                MensagemErro("O contrato precisa estar assinado");
                return RedirectToAction("Index", "Contrato");
            }
            if (contratoModel.AnaliseInvestimento?.Cliente?.IdEquipe == null)
            {
                MensagemErro("O contrato não possui equipe associada");
                return RedirectToAction("Index", "Contrato");
            }

            if (atividade.IdContrato != contratoModel.IdContrato)
            {
                MensagemErro("Atividade não pertence ao contrato");
                return RedirectToAction("Contrato", "Atividade", new { id = idContrato });
            }

            atividadeRetornoModel.Contrato = contratoModel;

            int idEquipe = (int)contratoModel.AnaliseInvestimento.Cliente.IdEquipe;
            atividadeRetornoModel.Equipe = _equipeAppServico.ObterPorId(idEquipe);

            if (ModelState.IsValid)
            {
                try
                {
                    _atividadeAppServico.Atualizar(atividadePostModel.Atividade);
                    MensagemSucesso(MensagensPadrao.CadastroSucesso);
                    return RedirectToAction("Contrato", "Atividade", new { id = idContrato });
                }
                catch (CustomBaseException ex)
                {
                    MensagemErro(ex.Mensagem);
                    return View(atividadeRetornoModel);
                }
            }

            return View(atividadeRetornoModel);
        }

        #region Métodos Aux

        private AtividadeContratoAdicionarEditarModel MontarAtividadeContratoAdicionar()
        {
            var atividadeContratoAdicionarModel = new AtividadeContratoAdicionarEditarModel();
            atividadeContratoAdicionarModel.TipoAtividades = _tipoAtividadeAppServico.ListarTodos();
            return atividadeContratoAdicionarModel;
        }

        private ContratoAdicionarModel MontarContratoAdicionar(int? idEstado = null)
        {
            var contratoModel = new ContratoAdicionarModel();

            contratoModel.Estados = _estadoAppServico.ListarTodos();
            if (idEstado != null)
                contratoModel.Cidades = _cidadeAppServico.ObterCidadesPorEstado((int)idEstado);
            return contratoModel;
        }

        private AnaliseInvestimentoAdicionarModel MontarAnaliseInvestimento()
        {
            var retorno = new AnaliseInvestimentoAdicionarModel();
            retorno.TipoTelhados = _tipoTelhadoAppServico.ListarTodos();
            return retorno;
        }

        private FichaClienteAdicionarModel MontarFichaCliente(int id, int? idEstado = null, int? idEstadoConjuge = null)
        {
            var retorno = new FichaClienteAdicionarModel();
            retorno.Contrato = _contratoAppServico.ObterPorId(id);

            retorno.Estados = _estadoAppServico.ListarTodos();
            retorno.EstadosCivis = _estadoCivilAppServico.ListarTodos();
            retorno.Equipes = _equipeAppServico.ListarTodos();
            if (idEstado != null)
                retorno.CidadesFiador = _cidadeAppServico.ObterCidadesPorEstado((int)idEstado);

            if (idEstadoConjuge != null)
                retorno.CidadesFiadorConjuge = _cidadeAppServico.ObterCidadesPorEstado((int)idEstado);

            return retorno;
        }

        [HttpGet]
        public JsonResult ObterCidadesPorEstado(int id)
        {
            List<CidadeAppModel> lista = new List<CidadeAppModel>();
            lista.AddRange(_cidadeAppServico.ObterCidadesPorEstado(id));
            return Json(lista, JsonRequestBehavior.AllowGet);
        }

        #endregion
    }
}