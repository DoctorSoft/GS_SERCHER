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

        IEnumerable<SiteLinkViewModel> GetGomelSatSiteLinks();

        ReviewingDataViewModel GetReviewingData(long id);

        void UpdateAnalizingDataHeader(long id, string header);

        void UpdateAnalizingDataImageLink(long id, string link);

        void UpdateAnalizingDataSourceLink(long id, string link);

        void OpenGomelSatRedactor(long id);
    }
}
