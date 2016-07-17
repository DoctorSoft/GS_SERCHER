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

        private readonly IWordService wordService;

        public GomelSatSettingsController(IGomelSatService gomelSatService, IWordService wordService)
        {
            this.gomelSatService = gomelSatService;
            this.wordService = wordService;
        }

        public ActionResult Index()
        {
            var wordsToDelete = wordService.GetWordListToDelete();
            return View(wordsToDelete);
        }

        [HttpPost]
        public ActionResult RefreshNews()
        {
            gomelSatService.RefreshNews();
            return RedirectToAction("Index", "GomelSatNews");
        }

        [HttpPost]
        public ActionResult AddWord(string word)
        {
            wordService.AddWord(word);
            return RedirectToAction("Index");
        }

        public ActionResult RemoveWords(IEnumerable<string> words)
        {
            wordService.DeleteWords(words);
            return RedirectToAction("Index");
        }

        public ActionResult DownloadWords()
        {
            var file = wordService.GetWordsFile();
            return File(file, "text/plain", FileNameConstants.WordsFileName);
        }
    }
}