using AutoMapper;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using RAHSys.Aplicacao.AppModels;
using RAHSys.Aplicacao.Interfaces;
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
    public class UsuarioController : ControllerBase
    {
        private readonly IAtividadeAppServico _atividadeAppServico;
        private readonly IUsuarioAppServico _usuarioAppServico;
        private readonly IRegistroRecorrenciaAppServico _registroRecorrenciaAppServico;
        private readonly IEvidenciaAppServico _evidenciaAppServico;

        public UsuarioController(IAtividadeAppServico atividadeAppServico, IUsuarioAppServico usuarioAppServico, IRegistroRecorrenciaAppServico registroRecorrenciaAppServico, IEvidenciaAppServico evidenciaAppServico)
        {
            _atividadeAppServico = atividadeAppServico;
            _usuarioAppServico = usuarioAppServico;
            _registroRecorrenciaAppServico = registroRecorrenciaAppServico;
            _evidenciaAppServico = evidenciaAppServico;
            ViewBag.Title = "Usuarios";
        }

        [HttpGet]
        public ActionResult MinhasAtividades(string dataInicial, string dataFinal, string modoVisualizacao)
        {
            ViewBag.SubTitle = "Minhas Atividades";

            modoVisualizacao = modoVisualizacao ?? ModoVisualizacaoEnum.Dia.Nome;

            dataInicial = GetData(dataInicial, modoVisualizacao);
            dataFinal = GetData(dataFinal, modoVisualizacao);

            ViewBag.DataInicial = dataInicial;
            ViewBag.DataFinal = dataFinal;
            ViewBag.ModoVisualizacao = modoVisualizacao;

            var atividadeContratoModel = new AtividadeUsuarioModel();
            try
            {
                var usuarioLogado = ObterUsuarioLogado();
                if (usuarioLogado == null)
                    throw new UnauthorizedException();

                if (modoVisualizacao.Equals(ModoVisualizacaoEnum.Dia.Nome))
                    atividadeContratoModel.AtividadesAtrasadas = ObterAtividadesAtrasadas(dataInicial, usuarioLogado);
                atividadeContratoModel.TodasAtividadesSerializadas = ObterAtividades(dataInicial, dataFinal, usuarioLogado);
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
        public ActionResult FinalizarAtividade(int id, string data, string urlRetorno)
        {
            ViewBag.SubTitle = "Finalizar Atividade";
            try
            {
                if (string.IsNullOrEmpty(urlRetorno))
                {
                    MensagemErro("Ocorreu um erro!");
                    return RedirectToAction("MinhasAtividades");
                }

                ViewBag.UrlRetorno = urlRetorno;

                var atividadeFinalizarModel = new FinalizarAtividadeModel();
                var atividade = _atividadeAppServico.ObterPorId(id);

                if (atividade == null)
                {
                    MensagemErro("Atividade não encontrada");
                    return Redirect(urlRetorno);
                }

                ValidarUsuarioLogado(atividade);

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

            try
            {
                ValidarUsuarioLogado(atividade);
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

                ValidarUsuarioLogado(registroRecorrencia.Atividade);

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

        [HttpPost]
        public ActionResult AdicionarEvidencias(int idAtividadeGeral, int idRegistroRecorrencia, string urlRetorno)
        {
            var arquivos = Request.Files;
            try
            {
                var atividade = _atividadeAppServico.ObterPorId(idAtividadeGeral);
                if (atividade == null)
                {
                    MensagemErro("Atividade não encontrada");
                    return RedirectToAction("MinhasAtividades");
                }

                ValidarUsuarioLogado(atividade);

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

                var registroRecorrencia = _registroRecorrenciaAppServico.ObterPorId(evidenciaModel.IdRegistroRecorrencia);
                var atividade = registroRecorrencia?.Atividade;
                if (atividade == null)
                {
                    MensagemErro("Atividade não encontrada");
                    return RedirectToAction("MinhasAtividades");
                }

                ValidarUsuarioLogado(atividade);

            }
            catch (UnauthorizedException)
            {
                return RedirectToAction("Unauthorized", "Account");
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
                var evidenciaModel = _evidenciaAppServico.ObterPorId(evidencia.IdEvidencia);
                if (evidenciaModel == null)
                {
                    MensagemErro("Evidência não encontrada");
                    return RedirectToAction("EvidenciaAtividade", new { id = evidencia.IdRegistroRecorrencia, urlRetorno });
                }

                var registroRecorrencia = _registroRecorrenciaAppServico.ObterPorId(evidenciaModel.IdRegistroRecorrencia);
                var atividade = registroRecorrencia?.Atividade;
                if (atividade == null)
                {
                    MensagemErro("Atividade não encontrada");
                    return RedirectToAction("MinhasAtividades");
                }

                ValidarUsuarioLogado(atividade);

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

            return RedirectToAction("EvidenciaAtividade", new { id = evidencia.IdRegistroRecorrencia, urlRetorno });
        }

        public string ObterAtividades(string dataInicial, string dataFinal, UsuarioAppModel usuarioLogado)
        {

            DateTimeFormatInfo formatter = new CultureInfo("pt-BR", false).DateTimeFormat;
            var dataInicialConvertida = Convert.ToDateTime(dataInicial, formatter);
            var dataFinalConvertida = Convert.ToDateTime(dataFinal, formatter);
            var consulta = _atividadeAppServico.Consultar(null, null, null, null,
                new string[] { usuarioLogado.IdUsuario }, dataInicialConvertida, dataFinalConvertida);
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

        private UsuarioAppModel ObterUsuarioLogado()
        {
            var usuario = User.Identity.GetUserId();
            var usuarioLogado = _usuarioAppServico.Consultar(new string[] { usuario }, null, null, null, true, 1, 1);

            UsuarioAppModel retorno = null;

            if (usuarioLogado.Resultado.Count() > 0)
                retorno = usuarioLogado.Resultado.FirstOrDefault();

            return retorno;
        }

        private void ValidarUsuarioLogado(AtividadeAppModel atividadeModel)
        {
            var usuarioLogado = ObterUsuarioLogado();

            if (usuarioLogado?.UsuarioPerfis?.Where(e => e.Perfil.Nome.ToLower().Equals(PerfilEnum.Admin.Nome.ToLower())).Count() <= 0 && usuarioLogado.IdUsuario != atividadeModel?.IdUsuario)
                throw new UnauthorizedException();
        }

        public Dictionary<DateTime, int> ObterAtividadesAtrasadas(string dataInicial, UsuarioAppModel usuarioLogado)
        {
            DateTimeFormatInfo formatter = new CultureInfo("pt-BR", false).DateTimeFormat;
            var dataInicialConvertida = Convert.ToDateTime(dataInicial, formatter);
            var consulta = _atividadeAppServico.ObterRecorrenciasAtrasadas(null, null, null, null,
                new string[] { usuarioLogado.IdUsuario }, DateTime.Now);

            var lista = consulta.Where(e => e.DataRealizacaoPrevista != dataInicialConvertida).GroupBy(e => e.DataRealizacaoPrevista).Select(e => new { Data = e.Key, Quantidade = e.Count() }).ToDictionary(e => e.Data, e => e.Quantidade);

            return lista;
        }
    }
}