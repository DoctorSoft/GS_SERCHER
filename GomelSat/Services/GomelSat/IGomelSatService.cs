using System.Collections.Generic;
using DataParsers.Models;
using Services.GomelSat.Models;
using TextAnalizators.Models;

namespace Services.GomelSat
{
    public interface IGomelSatService
    {
        IEnumerable<GomelSatNewsModel> GetNews();

        long SaveAnalizingText(string newsHeader, string newsText);

        AnalizedDataViewModel GetAnalizedData(long id);

        void RefreshNews();
    }
}
