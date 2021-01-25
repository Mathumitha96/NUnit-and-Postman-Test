using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Threading;
using System;

namespace NUnitTest
{
    public class Exercise1
    {
        private IWebDriver driver;

        [SetUp]
        public void SetupTest()
        {
            driver = new ChromeDriver();
        }

        [Test]
        public void GoogleSearchTest()
        {
            driver.Navigate().GoToUrl("https://www.google.com/");
            //Accepts the agree button
            driver.SwitchTo().Frame(driver.FindElement(By.XPath("//iframe[contains(@src, 'consent.google.com')]")));
            Thread.Sleep(1000);
            driver.FindElement(By.XPath("//*[@id='introAgreeButton']/span/span")).Click();
            //assigns google searchbox to local variable
            IWebElement searchBox = driver.FindElement(By.Name("q"));
            //clicks the searchbox
            searchBox.Click();
            //enters the search keyword
            searchBox.SendKeys("Bahamas");
            //clicks search icon
            driver.FindElement(By.Id("tsf")).Submit();

            //Checks whether the result page is arrived
            String expectedResult = driver.FindElement(By.XPath("//div[@id='wp-tabs-container']/div/div[2]/div[2]/div/div/div/h2/span")).Text;
            if (expectedResult == "The Bahamas")
            {
                String actualResult = "The Bahamas";
                Console.WriteLine(expectedResult);
                Assert.AreEqual(expectedResult, actualResult);
            }
            else
            {
                String actualResult = "Bahamas";
                Console.WriteLine(expectedResult);
                Assert.AreEqual(expectedResult, actualResult);
            }
            //takes screenshot of the result page
            takeScreenShot();
            //clicks the searchbox
            driver.FindElement(By.Name("q")).Click();
            //clears the old keywords
            driver.FindElement(By.Name("q")).Clear();
            //enters the new search keyword
            driver.FindElement(By.Name("q")).SendKeys("Amsterdam");
            //clicks the serach
            driver.FindElement(By.CssSelector("div.FAuhyb > span.z1asCe.MZy1Rb > svg")).Click();
            Assert.Pass();
        }

        ///method to take screenshot of the result page
        public void takeScreenShot()
        {
            //takes screenshot
            Screenshot s = ((ITakesScreenshot)driver).GetScreenshot();
            //saves the file in the gien location
            s.SaveAsFile("D://ResultImage.png", ScreenshotImageFormat.Png);
        }

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
        }
    }
}