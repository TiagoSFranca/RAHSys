using PagedList;
using RAHSys.Aplicacao.AppModels;
using RAHSys.Aplicacao.Interfaces;
using RAHSys.Apresentacao.Models;
using RAHSys.Infra.CrossCutting.Exceptions;
using System;
using System.Collections.Generic;
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

        public AtividadeController(IAtividadeAppServico atividadeAppServico, ITipoAtividadeAppServico tipoAtividadeAppServico,
            IContratoAppServico contratoAppServico, IEquipeAppServico equipeAppServico, IUsuarioAppServico usuarioAppServico)
        {
            _atividadeAppServico = atividadeAppServico;
            _tipoAtividadeAppServico = tipoAtividadeAppServico;
            _contratoAppServico = contratoAppServico;
            _equipeAppServico = equipeAppServico;
            _usuarioAppServico = usuarioAppServico;
            ViewBag.Title = "Atividades";
        }

        public ActionResult Index(int? codigo, string tipoAtividade, string contrato, string equipe, string usuario, string dataRealizacaoInicio,
            string dataRealizacaoFim, string dataPrevistaInicio, string dataPrevistaFim, string realizada,
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
                var listaTiposAtividade = ObterIdsTipoAtividade(tipoAtividade);
                var listaContratos = ObterIdsContrato(contrato);
                var listaEquipes = ObterIdsEquipe(equipe);
                var resultadoRealidada = ObterRealizada(realizada);
                var listaUsuarios = ObterIdsUsuario(usuario);
                var consulta = _atividadeAppServico.Consultar(codigo != null ? new int[] { (int)codigo } : null,
                    listaTiposAtividade,
                    listaEquipes,
                    listaContratos,
                    listaUsuarios,
                    resultadoRealidada, dataRealizacaoInicio, dataRealizacaoFim, dataPrevistaInicio, dataPrevistaFim,
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

        public ActionResult Contrato(int id, int? codigo, string tipoAtividade, string usuario, string dataRealizacaoInicio,
            string dataRealizacaoFim, string dataPrevistaInicio, string dataPrevistaFim, string realizada,
            string ordenacao, bool? crescente, int? pagina, int? itensPagina)
        {
            ViewBag.SubTitle = "Contrato";
            ViewBag.SubSubTitle = "Atividades";
            ViewBag.TipoAtividade = tipoAtividade;
            ViewBag.DataRealizacaoInicio = dataRealizacaoInicio;
            ViewBag.DataRealizacaoFim = dataRealizacaoFim;
            ViewBag.DataPrevistaInicio = dataPrevistaInicio;
            ViewBag.DataPrevistaFim = dataPrevistaFim;
            ViewBag.Realizada = realizada;
            ViewBag.Ordenacao = ordenacao;
            ViewBag.Crescente = crescente ?? true;
            ViewBag.ItensPagina = itensPagina;
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
                atividadeContratoModel.Equipe = _equipeAppServico.ObterPorId(idEquipe);

                var listaTiposAtividade = ObterIdsTipoAtividade(tipoAtividade);
                var resultadoRealidada = ObterRealizada(realizada);
                var listaUsuarios = ObterIdsUsuario(usuario);
                var consulta = _atividadeAppServico.Consultar(codigo != null ? new int[] { (int)codigo } : null,
                    listaTiposAtividade,
                    new[] { idEquipe },
                    new[] { contratoModel.IdContrato },
                    listaUsuarios,
                    resultadoRealidada, dataRealizacaoInicio, dataRealizacaoFim, dataPrevistaInicio, dataPrevistaFim,
                    ordenacao, crescente ?? true, pagina ?? 1, itensPagina ?? 40);

                var resultado = new StaticPagedList<AtividadeAppModel>(consulta.Resultado, consulta.PaginaAtual, consulta.ItensPorPagina, consulta.TotalItens);

                atividadeContratoModel.Atividades = resultado;
            }
            catch (CustomBaseException ex)
            {
                MensagemErro(ex.Mensagem);
                return RedirectToAction("Index", "Contrato");
            }

            return View(atividadeContratoModel);
        }

        #region Métodos Auxiliares

        private bool? ObterRealizada(string realizada)
        {
            if (string.IsNullOrEmpty(realizada))
                return null;
            return realizada == "1";
        }

        private List<int> ObterIdsTipoAtividade(string descricao)
        {
            if (string.IsNullOrEmpty(descricao))
                return new List<int>();
            var tipos = _tipoAtividadeAppServico.Consultar(null, descricao, null, true, 1, Int32.MaxValue);
            return tipos.Resultado.Select(e => e.IdTipoAtividade).ToList();
        }

        private List<int> ObterIdsContrato(string nomeEmpresa)
        {
            if (string.IsNullOrEmpty(nomeEmpresa))
                return new List<int>();
            var contratos = _contratoAppServico.Consultar(null, null, nomeEmpresa, null, null, null, true, 1, Int32.MaxValue);
            return contratos.Resultado.Select(e => e.IdContrato).ToList();
        }

        private List<int> ObterIdsEquipe(string usuario)
        {
            if (string.IsNullOrEmpty(usuario))
                return new List<int>();
            var equipes = _equipeAppServico.Consultar(null, usuario, null, true, 1, Int32.MaxValue);
            return equipes.Resultado.Select(e => e.IdEquipe).ToList();
        }

        private List<string> ObterIdsUsuario(string usuario)
        {
            if (string.IsNullOrEmpty(usuario))
                return new List<string>();
            var usuarios = _usuarioAppServico.Consultar(null, usuario, usuario, null, true, 1, Int32.MaxValue);
            return usuarios.Resultado.Select(e => e.IdUsuario).ToList();
        }

        #endregion
    }
}