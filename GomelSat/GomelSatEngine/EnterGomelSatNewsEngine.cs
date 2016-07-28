using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace GomelSatEngine
{
    public class EnterGomelSatNewsEngine : IEnterGomelSatNewsEngine
    {
        public void Run(string header, string shortText, string text)
        {
            string url;
            string login;
            string password;

            using (var dataReader = new StreamReader("C:\\Temp\\GSWords\\licence.txt"))
            {
                url = dataReader.ReadLine();
                login = dataReader.ReadLine();
                password = dataReader.ReadLine();
            }

            using (var driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl(url);

                IList<IWebElement> userNameInputs = driver.FindElements(By.Name("username"));
                userNameInputs.First().SendKeys(login);

                Thread.Sleep(500);

                IList<IWebElement> passwordInputs = driver.FindElements(By.Name("password"));
                passwordInputs.First().SendKeys(password);

                Thread.Sleep(500);

                IList<IWebElement> buttons = driver.FindElements(By.TagName("input"));
                buttons.First(element => element.GetAttribute("type") == "image").Click();

                Thread.Sleep(2000);

                IList<IWebElement> newsButtons = driver.FindElements(By.TagName("area"));
                newsButtons.First(element => element.GetAttribute("href").Contains("addnews")).Click();

                Thread.Sleep(500);

                IList<IWebElement> headerInputs = driver.FindElements(By.TagName("input"));
                headerInputs.First(element => element.GetAttribute("name") == "title").SendKeys(header);

                Thread.Sleep(500);

                var combobox = driver.FindElements(By.TagName("input")).First(element => element.GetAttribute("value").Contains("категорию"));
                combobox.Click();
                combobox.SendKeys("Информация");
                driver.Keyboard.SendKeys(Keys.Enter);

                Thread.Sleep(500);

                IList<IWebElement> shortTextInputs = driver.FindElements(By.TagName("textarea"));
                shortTextInputs.First(element => element.GetAttribute("name") == "short_story").SendKeys(shortText);

                Thread.Sleep(500);

                IList<IWebElement> textInputs = driver.FindElements(By.TagName("textarea"));
                textInputs.First(element => element.GetAttribute("name") == "full_story").SendKeys(text);

                while (driver.FindElements(By.TagName("input")).Any(element => element.GetAttribute("value") == "Добавить"))
                {
                    Thread.Sleep(1000);
                }
            }
        }
    }
}
