using AutoMapper;
using Newtonsoft.Json;
using RAHSys.Aplicacao.AppModels;
using RAHSys.Aplicacao.Interfaces;
using RAHSys.Apresentacao.Models;
using RAHSys.Extras;
using RAHSys.Extras.Helper;
using RAHSys.Infra.CrossCutting.Exceptions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;

namespace RAHSys.Apresentacao.Controllers
{
    public class AtividadeController : ControllerBase
    {
        private readonly IAtividadeAppServico _atividadeAppServico;
        private readonly ITipoAtividadeAppServico _tipoAtividadeAppServico;
        private readonly IContratoAppServico _contratoAppServico;
        private readonly IEquipeAppServico _equipeAppServico;
        private readonly IUsuarioAppServico _usuarioAppServico;
        private readonly IRegistroRecorrenciaAppServico _registroRecorrenciaAppServico;
        private readonly IEvidenciaAppServico _evidenciaAppServico;

        public AtividadeController(IAtividadeAppServico atividadeAppServico, ITipoAtividadeAppServico tipoAtividadeAppServico,
            IContratoAppServico contratoAppServico, IEquipeAppServico equipeAppServico, IUsuarioAppServico usuarioAppServico, IRegistroRecorrenciaAppServico registroRecorrenciaAppServico,
            IEvidenciaAppServico evidenciaAppServico)
        {
            _atividadeAppServico = atividadeAppServico;
            _tipoAtividadeAppServico = tipoAtividadeAppServico;
            _contratoAppServico = contratoAppServico;
            _equipeAppServico = equipeAppServico;
            _usuarioAppServico = usuarioAppServico;
            _registroRecorrenciaAppServico = registroRecorrenciaAppServico;
            _evidenciaAppServico = evidenciaAppServico;
            ViewBag.Title = "Atividades";
        }

        [HttpGet]
        public ActionResult Index(string dataInicial, string dataFinal, string modoVisualizacao)
        {
            ViewBag.SubTitle = "Calendário";

            modoVisualizacao = modoVisualizacao ?? "basicDay";

            dataInicial = GetData(dataInicial, modoVisualizacao, true);
            dataFinal = GetData(dataFinal, modoVisualizacao, false);

            ViewBag.DataInicial = dataInicial;
            ViewBag.DataFinal = dataFinal;
            ViewBag.ModoVisualizacao = modoVisualizacao;

            var atividadeContratoModel = new AtividadeIndexModel();
            try
            {
                atividadeContratoModel.TodasAtividadesSerializadas = ObterAtividades(dataInicial, dataFinal);
                atividadeContratoModel.TodasEquipesSerializadas = ObterTodasEquipesSerializadas();
            }
            catch (CustomBaseException ex)
            {
                MensagemErro(ex.Mensagem);
                return RedirectToAction("Index");
            }

            return View(atividadeContratoModel);
        }

        [HttpGet]
        public ActionResult FinalizarAtividade(int id, string data, string urlRetorno)
        {
            ViewBag.SubTitle = "Finalizar Atividade";
            try
            {
                if (string.IsNullOrEmpty(urlRetorno))
                {
                    MensagemErro("Ocorreu um erro!");
                    return RedirectToAction("Index");
                }

                ViewBag.UrlRetorno = urlRetorno;

                var atividadeFinalizarModel = new FinalizarAtividadeModel();
                var atividade = _atividadeAppServico.ObterPorId(id);

                if (atividade == null)
                {
                    MensagemErro("Atividade não encontrada");
                    return Redirect(urlRetorno);
                }

                var dataConvertida = DataHelper.ConverterStringParaData(data);

                var registroRecorrencia = _registroRecorrenciaAppServico.Consultar(id, null, dataConvertida, null, null, true, 1, Int32.MaxValue);

                if (registroRecorrencia.Resultado.Count() > 0)
                {
                    MensagemErro("Atividade já foi finalizada");
                    return Redirect(urlRetorno);
                }

                atividadeFinalizarModel.AtividadeInfo = new AtividadeInfoModel(atividade, dataConvertida);

                return View(atividadeFinalizarModel);
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
            if (string.IsNullOrEmpty(urlRetorno))
            {
                MensagemErro("Ocorreu um erro!");
                return RedirectToAction("Index");
            }

            ViewBag.UrlRetorno = urlRetorno;

            var atividadeContratoFinalizarModel = new FinalizarAtividadeModel();
            var atividade = _atividadeAppServico.ObterPorId(idAtividade);

            if (atividade == null)
            {
                MensagemErro("Atividade não encontrada");
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
        public ActionResult EvidenciaAtividade(int id, string urlRetorno)
        {
            ViewBag.SubTitle = "Evidências da Atividade";
            try
            {
                if (string.IsNullOrEmpty(urlRetorno))
                {
                    MensagemErro("Ocorreu um erro!");
                    return RedirectToAction("Index");
                }

                ViewBag.UrlRetorno = urlRetorno;

                var atividadeContratoFinalizarModel = new EvidenciaAtividadeModel();
                var registroRecorrencia = _registroRecorrenciaAppServico.ObterPorId(id);

                if (registroRecorrencia == null)
                {
                    MensagemErro("Atividade não encontrada");
                    return Redirect(urlRetorno);
                }

                atividadeContratoFinalizarModel.AtividadeInfo = new AtividadeInfoModel(registroRecorrencia.Atividade, registroRecorrencia.DataPrevista)
                {
                    RegistroRecorrencia = registroRecorrencia
                };

                return View(atividadeContratoFinalizarModel);
            }
            catch (CustomBaseException ex)
            {
                MensagemErro(ex.Mensagem);
                return Redirect(urlRetorno);
            }
        }

        [HttpPost]
        public ActionResult AdicionarEvidencias(int idAtividadeGeral, int idRegistroRecorrencia, string urlRetorno)
        {
            var arquivos = Request.Files;
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

                _evidenciaAppServico.AdicionarEvidencias(idAtividadeGeral, idRegistroRecorrencia, listaArquivos);
            }
            catch (CustomBaseException ex)
            {
                MensagemErro(ex.Mensagem);
            }

            return RedirectToAction("EvidenciaAtividade", new { id = idRegistroRecorrencia, urlRetorno });
        }

        [HttpGet]
        public ActionResult ExcluirEvidencia(int id, int idRegistroRecorrencia, string urlRetorno)
        {
            ViewBag.SubTitle = "Excluir Evidência";
            var evidenciaModel = new EvidenciaAppModel();
            try
            {
                evidenciaModel = _evidenciaAppServico.ObterPorId(id);
                if (evidenciaModel == null)
                {
                    MensagemErro("Evidência não encontrada");
                    return RedirectToAction("EvidenciaAtividade", new { id = idRegistroRecorrencia, urlRetorno });
                }

            }
            catch (CustomBaseException ex)
            {
                MensagemErro(ex.Mensagem);
                return RedirectToAction("EvidenciaAtividade", new { id = idRegistroRecorrencia, urlRetorno });
            }

            ViewBag.UrlRetorno = urlRetorno;
            return View(evidenciaModel);
        }

        [HttpPost]
        public ActionResult ExcluirEvidencia(EvidenciaAppModel evidencia, string urlRetorno)
        {
            try
            {

                _evidenciaAppServico.Remover(evidencia.IdEvidencia);
                MensagemSucesso(MensagensPadrao.ExclusaoSucesso);
            }
            catch (CustomBaseException ex)
            {
                MensagemErro(ex.Mensagem);
            }

            return RedirectToAction("EvidenciaAtividade", new { id = evidencia.IdRegistroRecorrencia, urlRetorno });
        }

        public string ObterAtividades(string dataInicial, string dataFinal)
        {
            DateTimeFormatInfo formatter = new CultureInfo("pt-BR", false).DateTimeFormat;
            var dataInicialConvertida = Convert.ToDateTime(dataInicial, formatter);
            var dataFinalConvertida = Convert.ToDateTime(dataFinal, formatter);
            var consulta = _atividadeAppServico.Consultar(null, null, null, null,
                null, dataInicialConvertida, dataFinalConvertida);
            List<AtividadeRecorrenciaAppModel> lista = consulta.ToList();
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

        [HttpPost]
        public ActionResult FinalizarRecorrencia(int idAtividade, DateTime dataRealizacaoPrevista, DateTime dataRealizacao, string observacao, string urlRetorno)
        {
            try
            {
                var atividade = _atividadeAppServico.ObterPorId(idAtividade);
                if (atividade == null)
                    MensagemErro("Atividade não encontrada");
                else
                {
                    //_atividadeAppServico.FinalizarRecorrencia(idAtividade, dataRealizacaoPrevista, dataRealizacao, observacao);
                    MensagemSucesso();
                }
            }
            catch (CustomBaseException ex)
            {
                MensagemErro(ex.Mensagem);
            }

            return Redirect(urlRetorno);
        }

        [HttpPost]
        public ActionResult CopiarAtividade(int idAtividade, string urlRetorno)
        {
            try
            {
                var atividade = _atividadeAppServico.ObterPorId(idAtividade);
                if (atividade == null)
                    MensagemErro("Atividade não encontrada");
                else
                {
                    _atividadeAppServico.CopiarAtividade(idAtividade);
                    MensagemSucesso();
                }
            }
            catch (CustomBaseException ex)
            {
                MensagemErro(ex.Mensagem);
            }

            return Redirect(urlRetorno);
        }

        [HttpPost]
        public ActionResult TransferirAtividade(AtividadeAppModel atividadeApp, string urlRetorno)
        {
            try
            {
                var atividade = _atividadeAppServico.ObterPorId(atividadeApp.IdAtividade);
                if (atividade == null)
                    MensagemErro("Atividade não encontrada");
                else
                {
                    _atividadeAppServico.TransferirAtividade(atividadeApp.IdAtividade, atividadeApp.IdUsuario);
                    MensagemSucesso();
                }
            }
            catch (CustomBaseException ex)
            {
                MensagemErro(ex.Mensagem);
            }

            return Redirect(urlRetorno);
        }
        
        [HttpPost]
        public ActionResult ExcluirAtividade(int idAtividade, string urlRetorno)
        {
            try
            {
                var atividade = _atividadeAppServico.ObterPorId(idAtividade);
                if (atividade == null)
                    MensagemErro("Atividade não encontrada");
                else
                {
                    _atividadeAppServico.Remover(idAtividade);
                    MensagemSucesso();
                }
            }
            catch (CustomBaseException ex)
            {
                MensagemErro(ex.Mensagem);
            }

            return Redirect(urlRetorno);
        }

        [HttpPost]
        public ActionResult EncerrarAtividade(int idAtividade, DateTime dataEncerramento, string urlRetorno)
        {
            try
            {
                var atividade = _atividadeAppServico.ObterPorId(idAtividade);
                if (atividade == null)
                    MensagemErro("Atividade não encontrada");
                else
                {
                    _atividadeAppServico.EncerrarAtividade(idAtividade, dataEncerramento);
                    MensagemSucesso();
                }
            }
            catch (CustomBaseException ex)
            {
                MensagemErro(ex.Mensagem);
            }

            return Redirect(urlRetorno);
        }

        [HttpPost]
        public ActionResult AlterarEquipe(int idAtividade, int idEquipe, string urlRetorno)
        {
            try
            {
                var atividade = _atividadeAppServico.ObterPorId(idAtividade);
                if (atividade == null)
                    MensagemErro("Atividade não encontrada");
                else
                {
                    _atividadeAppServico.AlterarEquipe(idAtividade, idEquipe);
                    MensagemSucesso();
                }
            }
            catch (CustomBaseException ex)
            {
                MensagemErro(ex.Mensagem);
            }

            return Redirect(urlRetorno);
        }

    }
}