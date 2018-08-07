using System.Web.Mvc;

namespace RAHSys.Apresentacao.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Inicio";
            return View();
        }
    }
}