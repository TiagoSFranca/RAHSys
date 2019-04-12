using RAHSys.Extras;
using System;
using System.Web.Mvc;

namespace RAHSys.Apresentacao.Controllers
{
    public class ControllerBase : Controller
    {
        public void MensagemSucesso(string mensagem = null)
        {

            TempData["success"] = mensagem ?? MensagensPadrao.SucessoPadrao;
        }

        public void MensagemErro(string mensagem = null)
        {
            TempData["error"] = mensagem ?? MensagensPadrao.ErroPadrao;
        }

        public void MensagemInformacao(string mensagem = null)
        {
            TempData["info"] = mensagem ?? MensagensPadrao.InformacaoPadrao;
        }

        public void MensagemAtencao(string mensagem = null)
        {
            TempData["warning"] = mensagem ?? MensagensPadrao.AtencaoPadrao;
        }

        protected string GetData(string data, string modoVisualizacao)
        {
            string formato = "{0}/{1}/{2}";
            if (!string.IsNullOrEmpty(data))
                return data;
            data = string.Format(formato, DateTime.Now.Day, DateTime.Now.Month, DateTime.Now.Year);
            return data;
        }
    }
}