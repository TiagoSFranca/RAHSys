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

        public ActionResult Atividades(int id, int? codigo, string tipoAtividade, string contrato, string usuario, string dataRealizacaoInicio,
            string dataRealizacaoFim, string dataPrevistaInicio, string dataPrevistaFim, string realizada,
            string ordenacao, bool? crescente, int? pagina, int? itensPagina)
        {
            ViewBag.SubTitle = "Equipe";
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
            var atividadeEquipeModel = new AtividadeEquipeModel();
            try
            {
                atividadeEquipeModel.TodasAtividadesSerializadas = ObterAtividadesContrato(id);
                var equipeModel = _equipeAppServico.ObterPorId(id);
                if (equipeModel == null)
                {
                    MensagemErro("Equipe não encontrada");
                    return RedirectToAction("Index", "Equipe");
                }

                atividadeEquipeModel.Equipe = equipeModel;

                var listaTiposAtividade = ObterIdsTipoAtividade(tipoAtividade);
                var resultadoRealidada = ObterRealizada(realizada);
                var listaUsuarios = ObterIdsUsuario(usuario);
                var listaContratos = ObterIdsContrato(contrato);
                var consulta = _atividadeAppServico.Consultar(codigo != null ? new int[] { (int)codigo } : null,
                    listaTiposAtividade,
                    new[] { id },
                    listaContratos,
                    listaUsuarios,
                    resultadoRealidada, dataRealizacaoInicio, dataRealizacaoFim, dataPrevistaInicio, dataPrevistaFim,
                    ordenacao, crescente ?? true, pagina ?? 1, itensPagina ?? (int)ItensPorPaginaEnum.MEDIO);

                var resultado = new StaticPagedList<AtividadeAppModel>(consulta.Resultado, consulta.PaginaAtual, consulta.ItensPorPagina, consulta.TotalItens);

                atividadeEquipeModel.Atividades = resultado;
            }
            catch (CustomBaseException ex)
            {
                MensagemErro(ex.Mensagem);
                return RedirectToAction("Index", "Equipe");
            }

            return View(atividadeEquipeModel);
        }

        public ActionResult AdicionarAtividade(int id)
        {
            ViewBag.SubTitle = "Adicionar nova Atividade";
            try
            {
                var atividadeEquipeAdicionarModel = MontarAtividadeEquipeAdicionarEditar(id);
                if (atividadeEquipeAdicionarModel == null)
                {
                    MensagemErro("Equipe não encontrada");
                    return RedirectToAction("Index", "Equipe");
                }

                atividadeEquipeAdicionarModel.Atividade = new AtividadeAppModel()
                {
                    IdEquipe = id
                };

                return View(atividadeEquipeAdicionarModel);
            }
            catch (CustomBaseException ex)
            {
                MensagemErro(ex.Mensagem);
                return RedirectToAction("Index", "Equipe");
            }
        }

        [HttpPost]
        public ActionResult AdicionarAtividade(AtividadeContratoAdicionarEditarModel atividadePostModel)
        {
            ViewBag.SubTitle = "Adicionar nova Atividade";
            var atividadeRetornoModel = MontarAtividadeEquipeAdicionarEditar(atividadePostModel.Atividade.IdEquipe);
            atividadeRetornoModel.Atividade = atividadePostModel.Atividade;

            if (atividadeRetornoModel == null)
            {
                MensagemErro("Equipe não encontrada");
                return RedirectToAction("Index", "Equipe");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _atividadeAppServico.Adicionar(atividadePostModel.Atividade);
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

        public ActionResult EditarAtividade(int id, int idAtividade)
        {
            ViewBag.SubTitle = "Editar Atividade";
            try
            {
                var atividadeEquipeEditarModel = MontarAtividadeEquipeAdicionarEditar(id);
                var atividade = _atividadeAppServico.ObterPorId(idAtividade);

                if (atividade == null)
                {
                    MensagemErro("Atividade não encontrada");
                    return RedirectToAction("Atividades", "Equipe", new { id });
                }

                if (atividadeEquipeEditarModel == null)
                {
                    MensagemErro("Contrato não encontrado");
                    return RedirectToAction("Index", "Equipe");
                }

                if (atividade.IdEquipe != atividadeEquipeEditarModel.Equipe.IdEquipe)
                {
                    MensagemErro("Atividade não pertence a Equipe");
                    return RedirectToAction("Atividades", "Equipe", new { id });
                }

                atividadeEquipeEditarModel.Atividade = atividade;

                return View(atividadeEquipeEditarModel);
            }
            catch (CustomBaseException ex)
            {
                MensagemErro(ex.Mensagem);
                return RedirectToAction("Index", "Equipe");
            }
        }

        [HttpPost]
        public ActionResult EditarAtividade(AtividadeContratoAdicionarEditarModel atividadePostModel)
        {
            ViewBag.SubTitle = "Adicionar nova Atividade";
            var atividadeRetornoModel = MontarAtividadeEquipeAdicionarEditar(atividadePostModel.Atividade.IdEquipe);
            atividadeRetornoModel.Atividade = atividadePostModel.Atividade;
            var atividade = atividadePostModel.Atividade;
            int idEquipe = atividadePostModel.Atividade.IdEquipe;
            if (atividadeRetornoModel == null)
            {
                MensagemErro("Atividade não encontrada");
                return RedirectToAction("Atividades", "Equipe", new { id = idEquipe });
            }

            if (atividade.IdEquipe != atividadeRetornoModel.Equipe.IdEquipe)
            {
                MensagemErro("Atividade não pertence a Equipe");
                return RedirectToAction("Atividades", "Equipe", new { id = idEquipe });
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _atividadeAppServico.Atualizar(atividadePostModel.Atividade);
                    MensagemSucesso(MensagensPadrao.CadastroSucesso);
                    return RedirectToAction("Atividades", "Equipe", new { id = idEquipe });
                }
                catch (CustomBaseException ex)
                {
                    MensagemErro(ex.Mensagem);
                    return View(atividadeRetornoModel);
                }
            }

            return View(atividadeRetornoModel);
        }

        public string ObterAtividadesContrato(int id)
        {
            var consulta = _atividadeAppServico.Consultar(null, null, null,
                new[] { id },
                null, null, null, null,
                null, null,
                null, true, 1, Int32.MaxValue);
            List<AtividadeAppModel> lista = consulta.Resultado.ToList();
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

        private static bool? ObterRealizada(string realizada)
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

        private List<string> ObterIdsUsuario(string usuario)
        {
            if (string.IsNullOrEmpty(usuario))
                return new List<string>();
            var usuarios = _usuarioAppServico.Consultar(null, usuario, usuario, null, true, 1, Int32.MaxValue);
            return usuarios.Resultado.Select(e => e.IdUsuario).ToList();
        }

        private List<int> ObterIdsContrato(string nomeEmpresa)
        {
            if (string.IsNullOrEmpty(nomeEmpresa))
                return new List<int>();
            var contratos = _contratoAppServico.Consultar(null, null, nomeEmpresa, null, null, null, true, 1, Int32.MaxValue);
            return contratos.Resultado.Select(e => e.IdContrato).ToList();
        }

        #endregion

        #region Métodos Aux

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