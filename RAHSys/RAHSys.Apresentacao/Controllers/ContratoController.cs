using AutoMapper;
using Newtonsoft.Json;
using PagedList;
using RAHSys.Aplicacao.AppModels;
using RAHSys.Aplicacao.Interfaces;
using RAHSys.Apresentacao.Attributes;
using RAHSys.Apresentacao.Models;
using RAHSys.Extras;
using RAHSys.Extras.Enums;
using RAHSys.Extras.Helper;
using RAHSys.Infra.CrossCutting.Exceptions;
using System;
using System.Collections.Generic;
using System.Globalization;
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
        private readonly IUsuarioAppServico _usuarioAppServico;
        private readonly IDiaSemanaAppServico _diaSemanaAppServico;
        private readonly ITipoRecorrenciaAppServico _tipoRecorrenciaAppServico;
        private readonly IRegistroRecorrenciaAppServico _registroRecorrenciaAppServico;

        public ContratoController(IContratoAppServico contratoAppServico, IEstadoAppServico estadoAppServico,
            ICidadeAppServico cidadeAppServico, ITipoTelhadoAppServico tipoTelhadoAppServico, IEstadoCivilAppServico estadoCivilAppServico,
            IDocumentoAppServico documentoAppServico, IEquipeAppServico equipeAppServico, ITipoAtividadeAppServico tipoAtividadeAppServico,
            IAtividadeAppServico atividadeAppServico, IUsuarioAppServico usuarioAppServico, IDiaSemanaAppServico diaSemanaAppServico,
            ITipoRecorrenciaAppServico tipoRecorrenciaAppServico, IRegistroRecorrenciaAppServico registroRecorrenciaAppServico)
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
            _usuarioAppServico = usuarioAppServico;
            _diaSemanaAppServico = diaSemanaAppServico;
            _tipoRecorrenciaAppServico = tipoRecorrenciaAppServico;
            _registroRecorrenciaAppServico = registroRecorrenciaAppServico;
            ViewBag.Title = "Clientes/Contratos";
        }

        [RAHAuthorize]
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
                var consulta = _contratoAppServico.Consultar(codigo != null ? new int[] { (int)codigo } : null, estado != null ? new int[] { (int)estado } : null, nomeEmpresa, receita, cidade, ordenacao, crescente ?? true, pagina ?? 1, itensPagina ?? (int)ItensPorPaginaEnum.MEDIO);
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
            retorno.AnaliseInvestimento = new AnaliseInvestimentoAppModel() { IdContrato = id, NomeCliente = contratoModel.NomeEmpresa };

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
            retorno.Cliente = new ClienteAppModel()
            {
                IdAnaliseInvestimento = id,
                ResponsavelFinanceiro = new ResponsavelFinanceiroAppModel()
                {
                    IdResponsavelFinanceiro = id
                }
            };

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
                    MensagemSucesso();
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

        [HttpGet]
        [RAHAuthorize(Roles = "Comercial")]
        public ActionResult EditarResponsavelFinanceiro(int id)
        {
            ViewBag.SubTitle = "Editar Contrato";
            ViewBag.SubSubTitle = "Responsável Financeiro";

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

                if (contratoModel.AnaliseInvestimento.Cliente == null)
                {
                    MensagemErro("Necessário adicionar ficha do cliente");
                    return RedirectToAction("Index");
                }
                ResponsavelFinanceiroEditar responsavelFinanceiroEditar = new ResponsavelFinanceiroEditar();
                responsavelFinanceiroEditar.Contrato = contratoModel;
                var responsavelFinanceiro = contratoModel.AnaliseInvestimento.Cliente.ResponsavelFinanceiro;
                responsavelFinanceiroEditar.ResponsavelFinanceiro = responsavelFinanceiro ?? new ResponsavelFinanceiroAppModel() { IdResponsavelFinanceiro = id };
                return View(responsavelFinanceiroEditar);
            }
            catch (CustomBaseException ex)
            {
                MensagemErro(ex.Mensagem);
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        [RAHAuthorize(Roles = "Comercial")]
        public ActionResult EditarResponsavelFinanceiro(ResponsavelFinanceiroEditar responsavelFinanceiroPost)
        {
            ResponsavelFinanceiroEditar responsavelFinanceiroEditar = new ResponsavelFinanceiroEditar();
            responsavelFinanceiroEditar.Contrato = _contratoAppServico.ObterPorId(responsavelFinanceiroPost.ResponsavelFinanceiro.IdResponsavelFinanceiro);
            responsavelFinanceiroEditar.ResponsavelFinanceiro = responsavelFinanceiroPost.ResponsavelFinanceiro;
            if (ModelState.IsValid)
            {
                try
                {
                    _contratoAppServico.AtualizarResponsavelFinanceiro(responsavelFinanceiroPost.ResponsavelFinanceiro);
                    MensagemSucesso();
                    return RedirectToAction("Index", "Contrato");
                }
                catch (CustomBaseException ex)
                {
                    MensagemErro(ex.Mensagem);
                    return View(responsavelFinanceiroEditar);
                }
            }
            return View(responsavelFinanceiroEditar);
        }

        #region Atividades

        public ActionResult Atividades(int id, string dataInicial, string dataFinal, string modoVisualizacao)
        {
            ViewBag.SubTitle = "Atividades";

            modoVisualizacao = modoVisualizacao ?? "basicDay";

            dataInicial = GetData(dataInicial, modoVisualizacao, true);
            dataFinal = GetData(dataFinal, modoVisualizacao, false);

            ViewBag.DataInicial = dataInicial;
            ViewBag.DataFinal = dataFinal;
            ViewBag.ModoVisualizacao = modoVisualizacao;

            AtividadeContratoModel atividadeContratoModel = new AtividadeContratoModel();
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

                atividadeContratoModel.Contrato = contratoModel;
                int idEquipe = (int)contratoModel.AnaliseInvestimento.Cliente.IdEquipe;
                atividadeContratoModel.TodasAtividadesSerializadas = ObterAtividadesContrato(id, dataInicial, dataFinal);
                atividadeContratoModel.TodasEquipesSerializadas = ObterTodasEquipesSerializadas();

            }
            catch (CustomBaseException ex)
            {
                MensagemErro(ex.Mensagem);
                return RedirectToAction("Index", "Contrato");
            }

            return View(atividadeContratoModel);
        }

        public ActionResult AdicionarAtividade(int id)
        {
            ViewBag.SubTitle = "Adicionar nova Atividade";
            try
            {
                var atividadeContratoAdicionarModel = MontarAtividadeContratoAdicionarEditar();
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
                    IdEquipe = idEquipe,
                    ConfiguracaoAtividade = new ConfiguracaoAtividadeAppModel()
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
            var atividadeRetornoModel = MontarAtividadeContratoAdicionarEditar();
            atividadeRetornoModel.Atividade = atividadePostModel.Atividade;
            atividadeRetornoModel.DiaSemanasSelecionadas = atividadePostModel.DiaSemanasSelecionadas ?? new List<int>();
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
                    var atividade = atividadePostModel.Atividade;
                    if (atividadePostModel.DiaSemanasSelecionadas?.Count > 0)
                    {
                        atividade.ConfiguracaoAtividade.AtividadeDiaSemanas = atividadePostModel.DiaSemanasSelecionadas.Select(e => new AtividadeDiaSemanaAppModel() { IdDiaSemana = e }).ToList();
                    }
                    _atividadeAppServico.Adicionar(atividade);
                    MensagemSucesso(MensagensPadrao.CadastroSucesso);
                    return RedirectToAction("Atividades", "Contrato", new { id = atividadePostModel.Atividade.IdContrato });
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
            try
            {
                var atividadeContratoAdicionarModel = MontarAtividadeContratoAdicionarEditar();
                var atividade = _atividadeAppServico.ObterPorId(idAtividade);

                if (atividade == null)
                {
                    MensagemErro("Atividade não encontrada");
                    return RedirectToAction("Atividades", "Contrato", new { id });
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
                    return RedirectToAction("Atividades", "Contrato", new { id });
                }

                atividadeContratoAdicionarModel.Contrato = contratoModel;
                atividadeContratoAdicionarModel.Equipe = _equipeAppServico.ObterPorId(atividade.IdEquipe);
                atividadeContratoAdicionarModel.Atividade = atividade;
                atividadeContratoAdicionarModel.DiaSemanasSelecionadas = atividade.ConfiguracaoAtividade?.AtividadeDiaSemanas?.Select(e => e.IdDiaSemana).ToList();

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
            ViewBag.SubTitle = "Editar Atividade";
            var atividadeRetornoModel = MontarAtividadeContratoAdicionarEditar();
            atividadeRetornoModel.Atividade = atividadePostModel.Atividade;
            atividadeRetornoModel.DiaSemanasSelecionadas = atividadePostModel.DiaSemanasSelecionadas ?? new List<int>();
            var atividade = atividadePostModel.Atividade;
            int idContrato = atividadePostModel.Atividade.IdContrato;
            if (atividade == null)
            {
                MensagemErro("Atividade não encontrada");
                return RedirectToAction("Atividades", "Contrato", new { id = idContrato });
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
                return RedirectToAction("Atividades", "Contrato", new { id = idContrato });
            }

            atividadeRetornoModel.Contrato = contratoModel;

            int idEquipe = (int)contratoModel.AnaliseInvestimento.Cliente.IdEquipe;
            atividadeRetornoModel.Equipe = _equipeAppServico.ObterPorId(idEquipe);

            if (ModelState.IsValid)
            {
                try
                {
                    if (atividadePostModel.DiaSemanasSelecionadas?.Count > 0)
                    {
                        atividade.ConfiguracaoAtividade.AtividadeDiaSemanas = atividadePostModel.DiaSemanasSelecionadas.Select(e => new AtividadeDiaSemanaAppModel() { IdDiaSemana = e }).ToList();
                    }
                    _atividadeAppServico.Atualizar(atividade);
                    MensagemSucesso();
                    return RedirectToAction("Atividades", "Contrato", new { id = idContrato });
                }
                catch (CustomBaseException ex)
                {
                    MensagemErro(ex.Mensagem);
                    return View(atividadeRetornoModel);
                }
            }

            return View(atividadeRetornoModel);
        }

        public ActionResult FinalizarAtividade(int id, int idAtividade, string data, string urlRetorno)
        {
            ViewBag.SubTitle = "Finalizar Atividade";
            try
            {
                if (string.IsNullOrEmpty(urlRetorno))
                {
                    MensagemErro("Ocorreu um erro!");
                    return RedirectToAction("Atividades", "Contrato", new { id });
                }

                ViewBag.UrlRetorno = urlRetorno;

                var atividadeContratoFinalizarModel = new FinalizarAtividadeModel();
                var atividade = _atividadeAppServico.ObterPorId(idAtividade);

                if (atividade == null)
                {
                    MensagemErro("Atividade não encontrada");
                    return Redirect(urlRetorno);
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
                    return Redirect(urlRetorno);
                }

                var dataConvertida = DataHelper.ConverterStringParaData(data);

                var registroRecorrencia = _registroRecorrenciaAppServico.Consultar(idAtividade, null, dataConvertida, null, null, true, 1, Int32.MaxValue);

                if (registroRecorrencia.Resultado.Count() > 0)
                {
                    MensagemErro("Atividade já foi finalizada");
                    return Redirect(urlRetorno);
                }

                //atividadeContratoAdicionarModel.Contrato = contratoModel;
                //atividadeContratoAdicionarModel.Equipe = _equipeAppServico.ObterPorId(atividade.IdEquipe);
                atividadeContratoFinalizarModel.AtividadeInfo = new AtividadeInfoModel(atividade, dataConvertida);
                //atividadeContratoAdicionarModel.DiaSemanasSelecionadas = atividade.ConfiguracaoAtividade?.AtividadeDiaSemanas?.Select(e => e.IdDiaSemana).ToList();

                return View(atividadeContratoFinalizarModel);
            }
            catch (CustomBaseException ex)
            {
                MensagemErro(ex.Mensagem);
                return Redirect(urlRetorno);
            }
        }

        [HttpPost]
        public ActionResult FinalizarAtividade(FinalizarAtividadeModel finalizarAtividadeModel, string urlRetorno)
        {
            ViewBag.SubTitle = "Finalizar Atividade";

            var idAtividade = finalizarAtividadeModel.AtividadeInfo.Atividade.IdAtividade;
            var idContrato = finalizarAtividadeModel.AtividadeInfo.Atividade.IdContrato;
            if (string.IsNullOrEmpty(urlRetorno))
            {
                MensagemErro("Ocorreu um erro!");
                return RedirectToAction("Atividades", "Contrato", new { idContrato });
            }

            ViewBag.UrlRetorno = urlRetorno;

            var atividadeContratoFinalizarModel = new FinalizarAtividadeModel();
            var atividade = _atividadeAppServico.ObterPorId(idAtividade);

            if (atividade == null)
            {
                MensagemErro("Atividade não encontrada");
                return Redirect(urlRetorno);
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
                return Redirect(urlRetorno);
            }

            var dataConvertida = finalizarAtividadeModel.AtividadeInfo.DataPrevista;

            var registroRecorrencia = _registroRecorrenciaAppServico.Consultar(idAtividade, null, dataConvertida, null, null, true, 1, Int32.MaxValue);

            if (registroRecorrencia.Resultado.Count() > 0)
            {
                MensagemErro("Atividade já foi finalizada");
                return Redirect(urlRetorno);
            }

            atividadeContratoFinalizarModel.AtividadeInfo = new AtividadeInfoModel(atividade, dataConvertida);
            var arquivos = Request.Files;
            if (ModelState.IsValid)
            {
                try
                {
                    _registroRecorrenciaAppServico.FinalizarRegistroRecorrencia(idAtividade, dataConvertida, Mapper.Map<List<ArquivoAppModel>>(arquivos));
                    MensagemSucesso();
                    return Redirect(urlRetorno);
                }
                catch (CustomBaseException ex)
                {
                    MensagemErro(ex.Mensagem);
                    return View(atividadeContratoFinalizarModel);
                }
            }

            return View(atividadeContratoFinalizarModel);
        }

        #endregion

        #region Métodos Aux

        public string ObterAtividadesContrato(int id, string dataInicial, string dataFinal)
        {
            DateTimeFormatInfo formatter = new CultureInfo("pt-BR", false).DateTimeFormat;
            var dataInicialConvertida = Convert.ToDateTime(dataInicial, formatter);
            var dataFinalConvertida = Convert.ToDateTime(dataFinal, formatter);
            var consulta = _atividadeAppServico.Consultar(null, null, null, new[] { id },
                null, dataInicialConvertida, dataFinalConvertida, null, true, 1, Int32.MaxValue);
            List<AtividadeRecorrenciaAppModel> lista = consulta.Resultado.ToList();
            lista.ForEach(item =>
            {
                item.Contrato = null;
                item.Equipe.Lider.UsuarioPerfis = null;
                item.Equipe.EquipeUsuarios.ForEach(eu =>
                {
                    eu.Usuario.UsuarioPerfis = null;
                });
            });
            return JsonConvert.SerializeObject(lista);
        }

        public string ObterTodasEquipesSerializadas()
        {
            var consulta = _equipeAppServico.Consultar(null, null, null, true, 1, Int32.MaxValue);
            var lista = consulta.Resultado.ToList();
            lista.ForEach(item =>
            {
                item.Lider.UsuarioPerfis = null;
                item.EquipeUsuarios.ForEach(eu =>
                {
                    eu.Usuario.UsuarioPerfis = null;
                });
            });
            return JsonConvert.SerializeObject(lista);
        }

        private AtividadeContratoAdicionarEditarModel MontarAtividadeContratoAdicionarEditar()
        {
            var atividadeContratoAdicionarModel = new AtividadeContratoAdicionarEditarModel();
            atividadeContratoAdicionarModel.TipoAtividades = _tipoAtividadeAppServico.ListarTodos();
            atividadeContratoAdicionarModel.DiaSemanas = _diaSemanaAppServico.ListarTodos();
            atividadeContratoAdicionarModel.TipoRecorrencias = _tipoRecorrenciaAppServico.ListarTodos();
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

        private string GetData(string data, string modoVisualizacao, bool dataInicial)
        {
            string formato = "{0}/{1}/{2}";
            if (!string.IsNullOrEmpty(data))
                return data;
            data = string.Format(formato, DateTime.Now.Day, DateTime.Now.Month, DateTime.Now.Year);
            return data;
        }

        #endregion
    }
}