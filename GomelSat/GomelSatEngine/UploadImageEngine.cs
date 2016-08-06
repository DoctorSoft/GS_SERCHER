using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Windows.Forms;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace GomelSatEngine
{
    public class UploadImageEngine : IUploadImageEngine
    {
        private WebClient client = new WebClient();

        public bool IsElementVisible(IWebElement element)
        {
            return element.Displayed && element.Enabled;
        }

        public string Run()
        {
            string textUrl = null;

            using (var driver = new ChromeDriver())
            {
                var googleUrl = "https://www.google.ru/search?q=%D1%82%D1%80%D0%B8%D0%BA%D0%BE%D0%BB%D0%BE%D1%80&newwindow=1&biw=929&bih=931&source=lnms&tbm=isch&sa=X&sqi=2&ved=0ahUKEwjn6IKv1azOAhUEPxQKHbjGDN0Q_AUIBygC";
                driver.Navigate().GoToUrl(googleUrl);
                
                try
                {
                    while (driver.FindElements(By.ClassName("irc_mi")).First().GetAttribute("src") == null)
                    {
                        Thread.Sleep(500);
                    }
                }
                catch
                {
                    return null;
                }

                IList<IWebElement> images = driver.FindElements(By.ClassName("irc_mi"));
                var image = images.First().GetAttribute("src");

                var fileExtension = Path.GetExtension(image);
                var fileShortName = Guid.NewGuid().ToString();
                var localDirectory = @"C:\Temp\GSWords\Images";
                if (!Directory.Exists(localDirectory))
                {
                    Directory.CreateDirectory(localDirectory);
                }
                var fileName = Path.Combine(localDirectory, fileShortName + fileExtension);

                try
                {
                    client.DownloadFile(image, fileName);
                }
                catch (Exception)
                {
                    client.DownloadFile(image, fileName);
                }

                Thread.Sleep(1000);

                var fileUploaderUrl = "http://img-host.org.ua/";
                driver.Navigate().GoToUrl(fileUploaderUrl);

                driver.FindElements(By.Id("rclosed")).First().Click();
                Thread.Sleep(500);
                driver.FindElements(By.Id("resize")).First().SendKeys("70");
                Thread.Sleep(500);
                driver.FindElements(By.Id("localUP")).First().Click();

                Thread.Sleep(500);
                SendKeys.SendWait(fileName);
                Thread.Sleep(500);
                SendKeys.SendWait(@"{Enter}");

                driver.FindElements(By.Id("subir")).First().Click();

                textUrl = driver
                    .FindElements(By.TagName("input"))
                    .First(element => element.GetAttribute("tabindex") == "4")
                    .GetAttribute("value");

            }

            return textUrl;
        }
    }
}
