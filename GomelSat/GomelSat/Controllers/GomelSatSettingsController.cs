using System.Collections.Generic;
using System.Web.Mvc;
using FilesManagers.Constants;
using Services.GomelSat;
using Services.Words;

namespace GomelSat.Controllers
{
    public class GomelSatSettingsController : Controller
    {
        private readonly IGomelSatService gomelSatService;

        public GomelSatSettingsController(IGomelSatService gomelSatService)
        {
            this.gomelSatService = gomelSatService;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult RefreshNews()
        {
            gomelSatService.RefreshNews();
            return RedirectToAction("Index", "GomelSatNews");
        }
    }
}