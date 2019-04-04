using Newtonsoft.Json;
using PagedList;
using RAHSys.Aplicacao.AppModels;
using RAHSys.Aplicacao.Interfaces;
using RAHSys.Apresentacao.Attributes;
using RAHSys.Apresentacao.Models;
using RAHSys.Extras;
using RAHSys.Extras.Enums;
using RAHSys.Infra.CrossCutting.Exceptions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;

namespace RAHSys.Apresentacao.Controllers
{
    [RAHAudit]
    public class EquipeController : ControllerBase
    {
        private readonly IEquipeAppServico _equipeAppServico;
        private readonly IUsuarioAppServico _usuarioAppServico;
        private readonly IAtividadeAppServico _atividadeAppServico;
        private readonly ITipoAtividadeAppServico _tipoAtividadeAppServico;
        private readonly IContratoAppServico _contratoAppServico;

        public EquipeController(IEquipeAppServico equipeAppServico, IUsuarioAppServico usuarioAppServico, ITipoAtividadeAppServico tipoAtividadeAppServico,
            IAtividadeAppServico atividadeAppServico, IContratoAppServico contratoAppServico)
        {
            _equipeAppServico = equipeAppServico;
            _usuarioAppServico = usuarioAppServico;
            _atividadeAppServico = atividadeAppServico;
            _tipoAtividadeAppServico = tipoAtividadeAppServico;
            _contratoAppServico = contratoAppServico;
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

        [RAHAuthorize]
        [HttpGet]
        public ActionResult Atividades(int id, string dataInicial, string dataFinal, string modoVisualizacao)
        {
            //TODO: Verificar se o usuário é o lider da equipe

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

                atividadeContratoModel.Equipe = equipeModel;
                atividadeContratoModel.TodasAtividadesSerializadas = ObterAtividadesEquipe(id, dataInicial, dataFinal);
            }
            catch (CustomBaseException ex)
            {
                MensagemErro(ex.Mensagem);
                return RedirectToAction("Index");
            }

            return View(atividadeContratoModel);
        }

        #endregion

        #region Métodos Aux

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
                item.Equipe = null;
            });
            return JsonConvert.SerializeObject(lista);
        }

        private AtividadeEquipeAdicionarEditarModel MontarAtividadeEquipeAdicionarEditar(int idEquipe)
        {
            var equipeModel = _equipeAppServico.ObterPorId(idEquipe);
            if (equipeModel != null)
            {
                var atividadeContratoAdicionarModel = new AtividadeEquipeAdicionarEditarModel();
                atividadeContratoAdicionarModel.Equipe = equipeModel;
                atividadeContratoAdicionarModel.TipoAtividades = _tipoAtividadeAppServico.ListarTodos();
                atividadeContratoAdicionarModel.Contratos = ObterContratosEquipe(idEquipe);
                return atividadeContratoAdicionarModel;
            }
            return null;
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