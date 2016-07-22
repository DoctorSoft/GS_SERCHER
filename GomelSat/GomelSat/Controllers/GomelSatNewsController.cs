using System.Web.Mvc;
using Services;
using Services.GomelSat;

namespace GomelSat.Controllers
{
    public class GomelSatNewsController : Controller
    {
        private readonly IGomelSatService gomelSatService;

        public GomelSatNewsController(IGomelSatService gomelSatService)
        {
            this.gomelSatService = gomelSatService;
        }

        public ActionResult Index()
        {
            var news = gomelSatService.GetNews();
            return View(news);
        }

        [HttpPost]
        public ActionResult AnalizeText(string newsHeader, string newsText)
        {
            var id = gomelSatService.SaveAnalizingText(newsHeader, newsText);
            return RedirectToAction("AnalizedData", new { id = id });
        }

        public ActionResult AnalizedData(long id)
        {
            var analizedResults = gomelSatService.GetAnalizedData(id);
            return View(analizedResults);
        }

        public ActionResult ReviewingData(long id)
        {
            var reviewingData = gomelSatService.GetReviewingData(id);
            return View(reviewingData);
        }
    }
}