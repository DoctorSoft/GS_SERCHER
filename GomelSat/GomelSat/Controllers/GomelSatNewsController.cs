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

        [HttpPost]
        public ActionResult InsertHeaderToAnalizingText(long id, string newsHeader)
        {
            gomelSatService.UpdateAnalizingDataHeader(id, newsHeader);
            return RedirectToAction("ReviewingData", new { id = id });
        }

        [HttpPost]
        public ActionResult InsertImageLinkToAnalizingText(long id, string imageLink)
        {
            gomelSatService.UpdateAnalizingDataImageLink(id, imageLink);
            return RedirectToAction("ReviewingData", new { id = id });
        }

        [HttpPost]
        public ActionResult InsertSourceLinkToAnalizingText(long id, string sourceLink)
        {
            gomelSatService.UpdateAnalizingDataSourceLink(id, sourceLink);
            return RedirectToAction("ReviewingData", new { id = id });
        }

        [HttpPost]
        public ActionResult OpenGomelSatNewsRedactor(string header, string shortText, string text)
        {
            gomelSatService.OpenGomelSatRedactor(header, shortText, text);
            return RedirectToAction("Index", "GomelSatNews");
        }

        [HttpPost]
        public ActionResult OpenImageDownloader(long id)
        {
            var link = gomelSatService.GetImageLink();
            gomelSatService.UpdateAnalizingDataImageLink(id, link);
            return RedirectToAction("ReviewingData", new { id = id });
        }
    }
}