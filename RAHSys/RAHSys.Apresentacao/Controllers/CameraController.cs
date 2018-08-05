using RAHSys.Aplicacao.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RAHSys.Apresentacao.Controllers
{
    public class CameraController : Controller
    {
        private readonly ICameraAppServico _cameraAppServico;

        public CameraController(ICameraAppServico cameraAppServico)
        {
            _cameraAppServico = cameraAppServico;
        }

        public ActionResult Index()
        {
            var p = _cameraAppServico.GetById(1);
            return View();
        }
    }
}