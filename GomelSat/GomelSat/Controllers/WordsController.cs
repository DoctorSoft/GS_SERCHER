using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FilesManagers.Constants;
using Services.Words;

namespace GomelSat.Controllers
{
    public class WordsController : Controller
    {
        private readonly IWordService wordService;

        public WordsController(IWordService wordService)
        {
            this.wordService = wordService;
        }

        // GET: Words
        public ActionResult Index()
        {
            var wordsToDelete = wordService.GetWordListToDelete();
            return View(wordsToDelete);
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