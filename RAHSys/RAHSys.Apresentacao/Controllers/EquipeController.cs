using AutoMapper;
using Microsoft.AspNet.Identity;
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
using System.Linq;
using System.Web.Mvc;

namespace RAHSys.Apresentacao.Controllers
{
    [RAHAudit]
    [RAHAuthorize(Roles = "Comercial, Engenharia")]
    public class EquipeController : ControllerBase
    {
        private readonly IEquipeAppServico _equipeAppServico;
        private readonly IUsuarioAppServico _usuarioAppServico;
        private readonly IAtividadeAppServico _atividadeAppServico;
        private readonly ITipoAtividadeAppServico _tipoAtividadeAppServico;
        private readonly IContratoAppServico _contratoAppServico;
        private readonly IDiaSemanaAppServico _diaSemanaAppServico;
        private readonly ITipoRecorrenciaAppServico _tipoRecorrenciaAppServico;
        private readonly IRegistroRecorrenciaAppServico _registroRecorrenciaAppServico;
        private readonly IEvidenciaAppServico _evidenciaAppServico;

        public EquipeController(IEquipeAppServico equipeAppServico, IUsuarioAppServico usuarioAppServico, IAtividadeAppServico atividadeAppServico,
            ITipoAtividadeAppServico tipoAtividadeAppServico, IContratoAppServico contratoAppServico, IDiaSemanaAppServico diaSemanaAppServico,
            ITipoRecorrenciaAppServico tipoRecorrenciaAppServico, IRegistroRecorrenciaAppServico registroRecorrenciaAppServico, IEvidenciaAppServico evidenciaAppServico)
        {
            _equipeAppServico = equipeAppServico;
            _usuarioAppServico = usuarioAppServico;
            _atividadeAppServico = atividadeAppServico;
            _tipoAtividadeAppServico = tipoAtividadeAppServico;
            _contratoAppServico = contratoAppServico;
            _diaSemanaAppServico = diaSemanaAppServico;
            _tipoRecorrenciaAppServico = tipoRecorrenciaAppServico;
            _registroRecorrenciaAppServico = registroRecorrenciaAppServico;
            _evidenciaAppServico = evidenciaAppServico;
            ViewBag.Title = "Equipes";
        }

        public ActionResult Index(int? codigo, string email, string ordenacao, bool? crescente, int? pagina, int? itensPagina)
        {
            ViewBag.SubTitle = "Consultar";
            ViewBag.Codigo = codigo;
            ViewBag.Email = email;
            ViewBag.Ordenacao = ordenacao;
            ViewBag.Crescente = crescente ?? true;
            ViewBag.ItensPagina = itensPagina;
            try
            {
                var consulta = _equipeAppServico.Consultar(codigo != null ? new int[] { (int)codigo } : null, email, ordenacao, crescente ?? true, pagina ?? 1, itensPagina ?? (int)ItensPorPaginaEnum.MEDIO);
                var resultado = new StaticPagedList<EquipeAppModel>(consulta.Resultado, consulta.PaginaAtual, consulta.ItensPorPagina, consulta.TotalItens);
                return View(resultado);
            }
            catch (CustomBaseException ex)
            {
                MensagemErro(ex.Mensagem);
                return View(new StaticPagedList<ContratoAppModel>(new List<ContratoAppModel>(), 1, 1, 0));
            }
        }

        [HttpGet]
        [RAHAuthorize(Roles = "Admin")]
        public ActionResult Adicionar()
        {
            ViewBag.SubTitle = "Adicionar Nova Equipe";
            var equipeAdicionar = MontarEquipeAdicionarEditarModel();

            return View(equipeAdicionar);
        }

        [HttpPost]
        [RAHAuthorize(Roles = "Admin")]
        public ActionResult Adicionar(EquipeAdicionarEditarModel equipeAdicionarModel)
        {
            var equipeRetorno = MontarEquipeAdicionarEditarModel(equipeAdicionarModel.Equipe?.IdLider);
            equipeRetorno.Equipe = equipeAdicionarModel.Equipe;
            equipeRetorno.IdIntegrantes = equipeAdicionarModel.IdIntegrantes;
            if (ModelState.IsValid)
            {
                try
                {
                    var equipe = equipeAdicionarModel.Equipe;
                    equipe.EquipeUsuarios = equipeAdicionarModel.IdIntegrantes?.Select(e => new EquipeUsuarioAppModel() { IdUsuario = e }).ToList();

                    _equipeAppServico.Adicionar(equipe);
                    MensagemSucesso(MensagensPadrao.CadastroSucesso);
                    return RedirectToAction("Index", "Equipe");
                }
                catch (CustomBaseException ex)
                {
                    MensagemErro(ex.Mensagem);
                    return View(equipeRetorno);
                }
            }
            return View(equipeRetorno);
        }

        [HttpGet]
        [RAHAuthorize(Roles = "Admin")]
        public ActionResult Editar(int id)
        {
            ViewBag.SubTitle = "Editar Equipe";
            EquipeAdicionarEditarModel equipeEditar = new EquipeAdicionarEditarModel();
            try
            {
                var equipeModel = _equipeAppServico.ObterPorId(id);
                if (equipeModel == null)
                {
                    MensagemErro("Equipe não encontrada");
                    return RedirectToAction("Index");
                }
                equipeEditar = MontarEquipeAdicionarEditarModel(equipeModel.IdLider);
                equipeEditar.Equipe = equipeModel;
                equipeEditar.IdIntegrantes = equipeModel.EquipeUsuarios.Select(e => e.IdUsuario).ToList();
            }
            catch (CustomBaseException ex)
            {
                MensagemErro(ex.Mensagem);
                return RedirectToAction("Index");
            }
            return View(equipeEditar);
        }

        [HttpPost]
        [RAHAuthorize(Roles = "Admin")]
        public ActionResult Editar(EquipeAdicionarEditarModel equipeAdicionarModel)
        {
            var equipeRetorno = MontarEquipeAdicionarEditarModel(equipeAdicionarModel.Equipe?.IdLider);
            equipeRetorno.Equipe = equipeAdicionarModel.Equipe;
            equipeRetorno.IdIntegrantes = equipeAdicionarModel.IdIntegrantes;
            if (ModelState.IsValid)
            {
                try
                {
                    var equipe = equipeAdicionarModel.Equipe;
                    equipe.EquipeUsuarios = equipeAdicionarModel.IdIntegrantes?.Select(e => new EquipeUsuarioAppModel() { IdUsuario = e, IdEquipe = equipeAdicionarModel.Equipe.IdEquipe }).ToList();

                    _equipeAppServico.Atualizar(equipe);
                    MensagemSucesso(MensagensPadrao.CadastroSucesso);
                    return RedirectToAction("Index", "Equipe");
                }
                catch (CustomBaseException ex)
                {
                    MensagemErro(ex.Mensagem);
                    return View(equipeRetorno);
                }
            }
            return View(equipeRetorno);
        }

        [HttpGet]
        [RAHAuthorize(Roles = "Admin")]
        public ActionResult Excluir(int id)
        {
            ViewBag.SubTitle = "Excluir Equipe";
            var equipeModel = new EquipeAppModel();
            try
            {
                equipeModel = _equipeAppServico.ObterPorId(id);
                if (equipeModel == null)
                {
                    MensagemErro("Equipe não encontrada");
                    return RedirectToAction("Index");
                }
            }
            catch (CustomBaseException ex)
            {
                MensagemErro(ex.Mensagem);
                return RedirectToAction("Index");
            }
            return View(equipeModel);
        }

        [HttpPost]
        [RAHAuthorize(Roles = "Admin")]
        public ActionResult Excluir(EquipeAppModel equipeAppModel)
        {
            try
            {
                _equipeAppServico.Remover(equipeAppModel.IdEquipe);
                MensagemSucesso(MensagensPadrao.ExclusaoSucesso);
            }
            catch (CustomBaseException ex)
            {
                MensagemErro(ex.Mensagem);
            }
            return RedirectToAction("Index", "Equipe");
        }

        #region Atividades

        [HttpGet]
        public ActionResult Atividades(int id, string dataInicial, string dataFinal, string modoVisualizacao)
        {
            ViewBag.SubTitle = "Atividades";

            modoVisualizacao = modoVisualizacao ?? "basicDay";

            dataInicial = GetData(dataInicial, modoVisualizacao, true);
            dataFinal = GetData(dataFinal, modoVisualizacao, false);

            ViewBag.DataInicial = dataInicial;
            ViewBag.DataFinal = dataFinal;
            ViewBag.ModoVisualizacao = modoVisualizacao;

            AtividadeEquipeModel atividadeContratoModel = new AtividadeEquipeModel();
            try
            {
                var equipeModel = _equipeAppServico.ObterPorId(id);

                if (equipeModel == null)
                {
                    MensagemErro("Equipe não encontrada");
                    return RedirectToAction("Index");
                }

                ValidarUsuarioLogado(equipeModel);

                atividadeContratoModel.Equipe = equipeModel;
                atividadeContratoModel.TodasAtividadesSerializadas = ObterAtividadesEquipe(id, dataInicial, dataFinal);
            }
            catch (UnauthorizedException)
            {
                return RedirectToAction("Unauthorized", "Account");
            }
            catch (CustomBaseException ex)
            {
                MensagemErro(ex.Mensagem);
                return RedirectToAction("Index");
            }

            return View(atividadeContratoModel);
        }

        [HttpGet]
        public ActionResult AdicionarAtividade(int id)
        {
            ViewBag.SubTitle = "Adicionar nova Atividade";
            try
            {
                var equipeModel = _equipeAppServico.ObterPorId(id);

                if (equipeModel == null)
                {
                    MensagemErro("Equipe não encontrada");
                    return RedirectToAction("Index");
                }

                ValidarUsuarioLogado(equipeModel);

                var atividadeEquipeAdicionarModel = MontarAtividadeEquipeAdicionarEditar();

                atividadeEquipeAdicionarModel.Equipe = equipeModel;
                atividadeEquipeAdicionarModel.Contratos = ObterContratosEquipe(id);
                atividadeEquipeAdicionarModel.Atividade = new AtividadeAppModel()
                {
                    IdEquipe = id,
                    ConfiguracaoAtividade = new ConfiguracaoAtividadeAppModel()
                };

                return View(atividadeEquipeAdicionarModel);
            }
            catch (UnauthorizedException)
            {
                return RedirectToAction("Unauthorized", "Account");
            }
            catch (CustomBaseException ex)
            {
                MensagemErro(ex.Mensagem);
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult AdicionarAtividade(AtividadeEquipeAdicionarEditarModel atividadePostModel)
        {
            ViewBag.SubTitle = "Adicionar nova Atividade";
            var equipeModel = _equipeAppServico.ObterPorId(atividadePostModel.Atividade.IdEquipe);

            if (equipeModel == null)
            {
                MensagemErro("Equipe não encontrada");
                return RedirectToAction("Index");
            }

            try
            {
                ValidarUsuarioLogado(equipeModel);
            }
            catch (Exception)
            {
                return RedirectToAction("Unauthorized", "Account");
            }

            var atividadeRetornoModel = MontarAtividadeEquipeAdicionarEditar();
            atividadeRetornoModel.Atividade = atividadePostModel.Atividade;
            atividadeRetornoModel.DiaSemanasSelecionadas = atividadePostModel.DiaSemanasSelecionadas ?? new List<int>();

            atividadeRetornoModel.Equipe = equipeModel;
            atividadeRetornoModel.Contratos = ObterContratosEquipe(equipeModel.IdEquipe);

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
                    return RedirectToAction("Atividades", "Equipe", new { id = atividadePostModel.Atividade.IdEquipe });
                }
                catch (CustomBaseException ex)
                {
                    MensagemErro(ex.Mensagem);
                    return View(atividadeRetornoModel);
                }
            }

            return View(atividadeRetornoModel);
        }

        [HttpGet]
        public ActionResult EditarAtividade(int id, int idAtividade)
        {
            ViewBag.SubTitle = "Editar Atividade";
            try
            {
                var atividade = _atividadeAppServico.ObterPorId(idAtividade);

                if (atividade == null)
                {
                    MensagemErro("Atividade não encontrada");
                    return RedirectToAction("Atividades", new { id });
                }

                var equipeModel = _equipeAppServico.ObterPorId(id);
                if (equipeModel == null)
                {
                    MensagemErro("Contrato não encontrado");
                    return RedirectToAction("Index");
                }

                ValidarUsuarioLogado(equipeModel);

                var atividadeContratoAdicionarModel = MontarAtividadeEquipeAdicionarEditar();

                atividadeContratoAdicionarModel.Contratos = ObterContratosEquipe(equipeModel.IdEquipe);
                atividadeContratoAdicionarModel.Equipe = _equipeAppServico.ObterPorId(atividade.IdEquipe);
                atividadeContratoAdicionarModel.Atividade = atividade;
                atividadeContratoAdicionarModel.DiaSemanasSelecionadas = atividade.ConfiguracaoAtividade?.AtividadeDiaSemanas?.Select(e => e.IdDiaSemana).ToList();

                return View(atividadeContratoAdicionarModel);
            }
            catch (UnauthorizedException)
            {
                return RedirectToAction("Unauthorized", "Account");
            }
            catch (CustomBaseException ex)
            {
                MensagemErro(ex.Mensagem);
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult EditarAtividade(AtividadeContratoAdicionarEditarModel atividadePostModel)
        {
            ViewBag.SubTitle = "Editar Atividade";
            int idEquipe = atividadePostModel.Atividade.IdEquipe;

            var equipeModel = _equipeAppServico.ObterPorId(idEquipe);
            if (equipeModel == null)
            {
                MensagemErro("Equipe não encontrada");
                return RedirectToAction("Index");
            }

            try
            {
                ValidarUsuarioLogado(equipeModel);
            }
            catch (Exception)
            {
                return RedirectToAction("Unauthorized", "Account");
            }

            var atividadeRetornoModel = MontarAtividadeEquipeAdicionarEditar();
            atividadeRetornoModel.Atividade = atividadePostModel.Atividade;
            atividadeRetornoModel.DiaSemanasSelecionadas = atividadePostModel.DiaSemanasSelecionadas ?? new List<int>();
            atividadeRetornoModel.Contratos = ObterContratosEquipe(equipeModel.IdEquipe);

            atividadeRetornoModel.Equipe = equipeModel;

            if (ModelState.IsValid)
            {
                try
                {
                    var atividade = atividadePostModel.Atividade;
                    if (atividadePostModel.DiaSemanasSelecionadas?.Count > 0)
                    {
                        atividade.ConfiguracaoAtividade.AtividadeDiaSemanas = atividadePostModel.DiaSemanasSelecionadas.Select(e => new AtividadeDiaSemanaAppModel() { IdDiaSemana = e }).ToList();
                    }
                    _atividadeAppServico.Atualizar(atividade);
                    MensagemSucesso();
                    return RedirectToAction("Atividades", new { id = idEquipe });
                }
                catch (CustomBaseException ex)
                {
                    MensagemErro(ex.Mensagem);
                    return View(atividadeRetornoModel);
                }
            }

            return View(atividadeRetornoModel);
        }

        [HttpGet]
        public ActionResult FinalizarAtividade(int id, int idAtividade, string data, string urlRetorno)
        {
            ViewBag.SubTitle = "Finalizar Atividade";
            try
            {
                if (string.IsNullOrEmpty(urlRetorno))
                {
                    MensagemErro("Ocorreu um erro!");
                    return RedirectToAction("Atividades", new { id });
                }

                ViewBag.UrlRetorno = urlRetorno;

                var atividadeContratoFinalizarModel = new FinalizarAtividadeModel();
                var atividade = _atividadeAppServico.ObterPorId(idAtividade);

                if (atividade == null)
                {
                    MensagemErro("Atividade não encontrada");
                    return Redirect(urlRetorno);
                }

                var equipeModel = _equipeAppServico.ObterPorId(id);
                if (equipeModel == null)
                {
                    MensagemErro("Equipe não encontrada");
                    return RedirectToAction("Index");
                }

                ValidarUsuarioLogado(equipeModel);

                var dataConvertida = DataHelper.ConverterStringParaData(data);

                var registroRecorrencia = _registroRecorrenciaAppServico.Consultar(idAtividade, null, dataConvertida, null, null, true, 1, Int32.MaxValue);

                if (registroRecorrencia.Resultado.Count() > 0)
                {
                    MensagemErro("Atividade já foi finalizada");
                    return Redirect(urlRetorno);
                }

                atividadeContratoFinalizarModel.AtividadeInfo = new AtividadeInfoModel(atividade, dataConvertida);

                return View(atividadeContratoFinalizarModel);
            }
            catch (UnauthorizedException)
            {
                return RedirectToAction("Unauthorized", "Account");
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
            var idEquipe = finalizarAtividadeModel.AtividadeInfo.Atividade.IdEquipe;
            if (string.IsNullOrEmpty(urlRetorno))
            {
                MensagemErro("Ocorreu um erro!");
                return RedirectToAction("Atividades", new { idEquipe });
            }

            ViewBag.UrlRetorno = urlRetorno;

            var atividadeContratoFinalizarModel = new FinalizarAtividadeModel();
            var atividade = _atividadeAppServico.ObterPorId(idAtividade);

            if (atividade == null)
            {
                MensagemErro("Atividade não encontrada");
                return Redirect(urlRetorno);
            }

            var equipeModel = _equipeAppServico.ObterPorId(idEquipe);
            if (equipeModel == null)
            {
                MensagemErro("Equipe não encontrada");
                return RedirectToAction("Index");
            }

            try
            {
                ValidarUsuarioLogado(equipeModel);
            }
            catch (Exception)
            {
                return RedirectToAction("Unauthorized", "Account");
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
                    var listaArquivos = new List<ArquivoAppModel>();
                    if (arquivos.Count > 0)
                    {
                        for (int i = 0; i < arquivos.Count; i++)
                        {
                            listaArquivos.Add(Mapper.Map<ArquivoAppModel>(arquivos[i]));
                        }
                    }

                    _registroRecorrenciaAppServico.FinalizarRegistroRecorrencia(idAtividade, dataConvertida, finalizarAtividadeModel.DataRealizacao.Value, finalizarAtividadeModel.Observacao, listaArquivos);
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

        [HttpGet]
        public ActionResult EvidenciaAtividade(int id, int idAtividade, string urlRetorno)
        {
            ViewBag.SubTitle = "Evidências da Atividade";
            try
            {
                if (string.IsNullOrEmpty(urlRetorno))
                {
                    MensagemErro("Ocorreu um erro!");
                    return RedirectToAction("Atividades", new { id });
                }

                ViewBag.UrlRetorno = urlRetorno;

                var atividadeContratoFinalizarModel = new EvidenciaAtividadeModel();
                var registroRecorrencia = _registroRecorrenciaAppServico.ObterPorId(idAtividade);

                if (registroRecorrencia == null)
                {
                    MensagemErro("Atividade não encontrada");
                    return Redirect(urlRetorno);
                }

                var equipeModel = _equipeAppServico.ObterPorId(id);
                if (equipeModel == null)
                {
                    MensagemErro("Equipe não encontrado");
                    return RedirectToAction("Index");
                }

                if (registroRecorrencia.Atividade.IdEquipe != equipeModel.IdEquipe)
                {
                    MensagemErro("Atividade não pertence à equipe");
                    return Redirect(urlRetorno);
                }

                ValidarUsuarioLogado(equipeModel);

                atividadeContratoFinalizarModel.AtividadeInfo = new AtividadeInfoModel(registroRecorrencia.Atividade, registroRecorrencia.DataPrevista)
                {
                    RegistroRecorrencia = registroRecorrencia
                };

                return View(atividadeContratoFinalizarModel);
            }
            catch (UnauthorizedException)
            {
                return RedirectToAction("Unauthorized", "Account");
            }
            catch (CustomBaseException ex)
            {
                MensagemErro(ex.Mensagem);
                return Redirect(urlRetorno);
            }
        }

        [HttpGet]
        public ActionResult ExcluirEvidencia(int id, int idRegistroRecorrencia, int idEquipe, string urlRetorno)
        {
            ViewBag.SubTitle = "Excluir Evidência";
            var evidenciaModel = new EvidenciaAppModel();
            try
            {
                evidenciaModel = _evidenciaAppServico.ObterPorId(id);
                if (evidenciaModel == null)
                {
                    MensagemErro("Evidência não encontrada");
                    return RedirectToAction("EvidenciaAtividade", new { id = idEquipe, idAtividade = idRegistroRecorrencia, urlRetorno });
                }

                var equipeModel = _equipeAppServico.ObterPorId(idEquipe);
                if (equipeModel == null)
                {
                    MensagemErro("Equipe não encontrada");
                    return RedirectToAction("Index");
                }

                ValidarUsuarioLogado(equipeModel);
            }
            catch (UnauthorizedException)
            {
                return RedirectToAction("Unauthorized", "Account");
            }
            catch (CustomBaseException ex)
            {
                MensagemErro(ex.Mensagem);
                return RedirectToAction("EvidenciaAtividade", new { id = idEquipe, idAtividade = idRegistroRecorrencia, urlRetorno });
            }
            ViewBag.IdEquipe = idEquipe;
            ViewBag.UrlRetorno = urlRetorno;
            return View(evidenciaModel);
        }

        [HttpPost]
        public ActionResult ExcluirEvidencia(EvidenciaAppModel evidencia, int idEquipe, string urlRetorno)
        {
            try
            {
                var equipeModel = _equipeAppServico.ObterPorId(idEquipe);
                if (equipeModel == null)
                {
                    MensagemErro("Equipe não encontrada");
                    return RedirectToAction("Index");
                }

                ValidarUsuarioLogado(equipeModel);

                _evidenciaAppServico.Remover(evidencia.IdEvidencia);
                MensagemSucesso(MensagensPadrao.ExclusaoSucesso);
            }
            catch (UnauthorizedException)
            {
                return RedirectToAction("Unauthorized", "Account");
            }
            catch (CustomBaseException ex)
            {
                MensagemErro(ex.Mensagem);
            }

            return RedirectToAction("EvidenciaAtividade", new { id = idEquipe, idAtividade = evidencia.IdRegistroRecorrencia, urlRetorno });
        }

        [HttpPost]
        public ActionResult AdicionarEvidencias(int idEquipe, int idAtividadeGeral, int idRegistroRecorrencia, string urlRetorno)
        {
            var arquivos = Request.Files;
            try
            {
                var equipeModel = _equipeAppServico.ObterPorId(idEquipe);
                if (equipeModel == null)
                {
                    MensagemErro("Equipe não encontrada");
                    return RedirectToAction("Index");
                }

                ValidarUsuarioLogado(equipeModel);
                var listaArquivos = new List<ArquivoAppModel>();
                if (arquivos.Count > 0)
                {
                    for (int i = 0; i < arquivos.Count; i++)
                    {
                        listaArquivos.Add(Mapper.Map<ArquivoAppModel>(arquivos[i]));
                    }
                }

                _evidenciaAppServico.AdicionarEvidencias(idAtividadeGeral, idRegistroRecorrencia, listaArquivos);
            }
            catch (UnauthorizedException)
            {
                return RedirectToAction("Unauthorized", "Account");
            }
            catch (CustomBaseException ex)
            {
                MensagemErro(ex.Mensagem);
            }

            return RedirectToAction("EvidenciaAtividade", new { id = idEquipe, idAtividade = idRegistroRecorrencia, urlRetorno });
        }

        public ActionResult MinhasEquipes()
        {
            ViewBag.SubTitle = "Minhas Equipes";
            try
            {
                var equipes = ObterEquipesUsuario();
                return View(equipes);
            }
            catch (UnauthorizedException)
            {
                return RedirectToAction("Unauthorized", "Account");
            }
        }

        #endregion

        #region Métodos Aux

        private List<EquipeAppModel> ObterEquipesUsuario()
        {
            var usuarioLogado = ObterUsuarioLogado();
            ConsultaAppModel<EquipeAppModel> equipes = new ConsultaAppModel<EquipeAppModel>();
            if (usuarioLogado != null)
                equipes = _equipeAppServico.Consultar(null, usuarioLogado.Email, null, true, 1, Int32.MaxValue);
            else
                throw new UnauthorizedException();

            var resultado = equipes.Resultado.ToList();
            resultado = resultado.Where(e => e.IdLider.ToLower().Equals(usuarioLogado?.IdUsuario?.ToLower())).ToList();

            return resultado;
        }

        private UsuarioAppModel ObterUsuarioLogado()
        {
            var usuario = User.Identity.GetUserId();
            var usuarioLogado = _usuarioAppServico.Consultar(new string[] { usuario }, null, null, null, true, 1, 1);

            UsuarioAppModel retorno = null;

            if (usuarioLogado.Resultado.Count() > 0)
                retorno = usuarioLogado.Resultado.FirstOrDefault();

            return retorno;
        }

        private void ValidarUsuarioLogado(EquipeAppModel equipeModel)
        {
            var usuarioLogado = ObterUsuarioLogado();

            if (usuarioLogado?.UsuarioPerfis?.Where(e => e.Perfil.Nome.ToLower().Equals(PerfilEnum.Admin.Nome.ToLower())).Count() <= 0 && usuarioLogado.IdUsuario != equipeModel?.IdLider)
                throw new UnauthorizedException();
        }

        private AtividadeEquipeAdicionarEditarModel MontarAtividadeEquipeAdicionarEditar()
        {
            var atividadeContratoAdicionarModel = new AtividadeEquipeAdicionarEditarModel
            {
                TipoAtividades = _tipoAtividadeAppServico.ListarTodos(),
                DiaSemanas = _diaSemanaAppServico.ListarTodos(),
                TipoRecorrencias = _tipoRecorrenciaAppServico.ListarTodos()
            };
            return atividadeContratoAdicionarModel;
        }

        public string ObterAtividadesEquipe(int id, string dataInicial, string dataFinal)
        {
            DateTimeFormatInfo formatter = new CultureInfo("pt-BR", false).DateTimeFormat;
            var dataInicialConvertida = Convert.ToDateTime(dataInicial, formatter);
            var dataFinalConvertida = Convert.ToDateTime(dataFinal, formatter);
            var consulta = _atividadeAppServico.Consultar(null, null, new[] { id }, null,
                null, dataInicialConvertida, dataFinalConvertida, null, true, 1, Int32.MaxValue);
            List<AtividadeRecorrenciaAppModel> lista = consulta.Resultado.ToList();
            lista.ForEach(item =>
            {
                item.Equipe.Lider.UsuarioPerfis = null;
                item.Equipe.EquipeUsuarios.ForEach(eu =>
                {
                    eu.Usuario.UsuarioPerfis = null;
                });
            });
            return JsonConvert.SerializeObject(lista);
        }

        private List<ContratoAppModel> ObterContratosEquipe(int idEquipe)
        {
            List<ContratoAppModel> contratos = _contratoAppServico.ListarPorEquipe(idEquipe);
            return contratos;
        }

        private EquipeAdicionarEditarModel MontarEquipeAdicionarEditarModel(string idLider = null)
        {
            var equipeAdicionarEditar = new EquipeAdicionarEditarModel();
            var usuarios = _usuarioAppServico.ListarTodos(null);
            equipeAdicionarEditar.Equipe = new EquipeAppModel();
            equipeAdicionarEditar.Lideres = usuarios;
            if (!string.IsNullOrEmpty(idLider))
            {
                var integrantes = usuarios.Where(e => !e.IdUsuario.ToLower().Equals(idLider.ToLower())).ToList();
                equipeAdicionarEditar.Integrantes = integrantes.GroupBy(e => e.UsuarioPerfis.FirstOrDefault().Perfil.Nome).Select(e => e.ToList()).ToList();
            }

            return equipeAdicionarEditar;
        }

        [HttpGet]
        public JsonResult ObterIntegrantes(string id)
        {
            var lista = _usuarioAppServico.ListarTodos(new string[] { id });
            var listaRetorno = MontarListaDropDown(lista);
            return Json(listaRetorno, JsonRequestBehavior.AllowGet);
        }

        private List<UsuarioMultiSelectList> MontarListaDropDown(List<UsuarioAppModel> lista)
        {
            var retorno = new List<UsuarioMultiSelectList>();

            var listaTemp = lista.GroupBy(e => e.UsuarioPerfis.FirstOrDefault().Perfil.IdPerfil).Select(o => o.ToList()).ToList();

            foreach (var item in listaTemp)
            {
                var usuarioMultiSelectList = new UsuarioMultiSelectList
                {
                    Perfil = item.FirstOrDefault()?.UsuarioPerfis?.FirstOrDefault()?.Perfil?.Nome
                };
                var usuarios = item.Select(e => new UsuarioMultiSelect(e.IdUsuario, e.EmailEUserName)).ToList();

                usuarioMultiSelectList.Usuarios = usuarios;

                retorno.Add(usuarioMultiSelectList);
            }

            return retorno;
        }

        #endregion
    }
}