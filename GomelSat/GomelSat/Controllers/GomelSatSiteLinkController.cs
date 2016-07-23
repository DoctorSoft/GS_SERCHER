using System.Web.Mvc;
using Services.GomelSat;

namespace GomelSat.Controllers
{
    public class GomelSatSiteLinkController : Controller
    {
        private IGomelSatService gomelSatService;

        public GomelSatSiteLinkController(IGomelSatService gomelSatService)
        {
            this.gomelSatService = gomelSatService;
        }

        // GET: GomelSatSiteLink
        public ActionResult Index()
        {
            return View();
        }

        public PartialViewResult Links()
        {
            var links = gomelSatService.GetGomelSatSiteLinks();
            return PartialView(links);
        }
    }
}