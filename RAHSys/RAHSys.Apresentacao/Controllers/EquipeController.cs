using PagedList;
using RAHSys.Aplicacao.AppModels;
using RAHSys.Aplicacao.Interfaces;
using RAHSys.Apresentacao.Models;
using RAHSys.Extras;
using RAHSys.Infra.CrossCutting.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RAHSys.Apresentacao.Controllers
{
    public class EquipeController : ControllerBase
    {
        private readonly IEquipeAppServico _equipeAppServico;
        private readonly IUsuarioAppServico _usuarioAppServico;

        public EquipeController(IEquipeAppServico equipeAppServico, IUsuarioAppServico usuarioAppServico)
        {
            _equipeAppServico = equipeAppServico;
            _usuarioAppServico = usuarioAppServico;
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
                var consulta = _equipeAppServico.Consultar(codigo != null ? new int[] { (int)codigo } : null, email, ordenacao, crescente ?? true, pagina ?? 1, itensPagina ?? 40);
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
        public ActionResult Adicionar()
        {
            ViewBag.SubTitle = "Adicionar Nova Equipe";
            var equipeAdicionar = MontarEquipeAdicionarModel();

            return View(equipeAdicionar);
        }

        [HttpPost]
        public ActionResult Adicionar(EquipeAdicionarModel equipeAdicionarModel)
        {
            var equipeRetorno = MontarEquipeAdicionarModel(equipeAdicionarModel.Equipe?.IdLider);
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

        private EquipeAdicionarModel MontarEquipeAdicionarModel(string idLider = null)
        {
            var equipeAdicionar = new EquipeAdicionarModel();
            var usuarios = _usuarioAppServico.ListarTodos(null);
            equipeAdicionar.Equipe = new EquipeAppModel();
            equipeAdicionar.Lideres = usuarios;
            if (!string.IsNullOrEmpty(idLider))
            {
                var integrantes = usuarios.Where(e => !e.IdUsuario.ToLower().Equals(idLider.ToLower())).ToList();
                equipeAdicionar.Integrantes = integrantes.GroupBy(e => e.UsuarioPerfis.FirstOrDefault().Perfil.Nome).Select(e => e.ToList()).ToList();
            }

            return equipeAdicionar;
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
    }
}