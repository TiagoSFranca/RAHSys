using PagedList;
using RAHSys.Aplicacao.AppModels;
using RAHSys.Aplicacao.Interfaces;
using RAHSys.Apresentacao.Attributes;
using RAHSys.Extras;
using RAHSys.Infra.CrossCutting.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace RAHSys.Apresentacao.Controllers
{
    [RAHAuthorize]
    public class AuditoriaController : ControllerBase
    {
        private readonly IAuditoriaAppServico _auditoriaAppServico;

        public AuditoriaController(IAuditoriaAppServico auditoriaAppServico)
        {
            _auditoriaAppServico = auditoriaAppServico;
            ViewBag.Title = "Auditoria de Dados";
        }

        public ActionResult Index(string usuario, string funcao, string acao, string enderecoIP, string ordenacao, bool? crescente, int? pagina, int? itensPagina, DateTime? dataHora)
        {
            ViewBag.SubTitle = "Consultar";
            ViewBag.Usuario = usuario;
            ViewBag.Funcao = funcao;
            ViewBag.Acao = acao;
            ViewBag.EnderecoIP = enderecoIP;
            ViewBag.DataHora = dataHora;
            ViewBag.Ordenacao = ordenacao;
            ViewBag.Crescente = crescente ?? true;
            ViewBag.ItensPagina = itensPagina;
            try
            {
                var consulta = _auditoriaAppServico.Consultar(null, usuario, funcao, acao, enderecoIP, dataHora ?? DateTime.Now, ordenacao, crescente ?? true, pagina ?? 1, itensPagina ?? 40);
                var resultado = new StaticPagedList<AuditoriaAppModel>(consulta.Resultado, consulta.PaginaAtual, consulta.ItensPorPagina, consulta.TotalItens);
                return View(resultado);
            }
            catch (CustomBaseException ex)
            {
                MensagemErro(ex.Mensagem);
                return View(new StaticPagedList<AuditoriaAppModel>(new List<AuditoriaAppModel>(), 1, 1, 0));
            }
        }

        public void Adicionar(AuditoriaAppModel auditoriaAppModel)
        {
            //if (ModelState.IsValid)
            //{
            //    try
            //    {
            //        _auditoriaAppServico.Adicionar(auditoriaAppModel);
            //    }
            //    catch (CustomBaseException ex)
            //    {

            //    }
            //}
        }

        [HttpGet]
        public ActionResult Detalhar(int id)
        {
            ViewBag.SubTitle = "Detalhes Auditoria";
            var auditModel = new AuditoriaAppModel();
            try
            {
                auditModel = _auditoriaAppServico.ObterPorId(id);

                if (auditModel == null)
                {
                    MensagemErro("Registor de Auditoria não encontrado");
                    return RedirectToAction("Index");
                }
            }
            catch (CustomBaseException ex)
            {
                MensagemErro(ex.Mensagem);
                return RedirectToAction("Index");
            }

            return View(auditModel);
        }

    }
}