using System.Web.Mvc;

namespace RAHSys.Apresentacao.Controllers
{
    public class ControllerBase : Controller
    {
        public void MensagemSucesso(string mensagem)
        {
            TempData["success"] = mensagem;
        }

        public void MensagemErro(string mensagem)
        {
            TempData["error"] = mensagem;
        }

        public void MensagemInformacao(string mensagem)
        {
            TempData["info"] = mensagem;
        }

        public void MensagemAtencao(string mensagem)
        {
            TempData["warning"] = mensagem;
        }
    }
}