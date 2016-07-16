using System.Collections.Generic;
using TextAnalizators.Models;

namespace Services.GomelSat.Models
{
    public class AnalizedDataViewModel
    {
        public IEnumerable<AnalizedTextModel> AnalizedTextModels { get; set; }

        public string ContentText { get; set; }

        public string HeaderText { get; set; }
    }
}