using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Threading;
using System;
using OpenQA.Selenium.Support.UI;

namespace NUnitTest
{
    public class Exercise2
    {
        private IWebDriver driver;
        String leavingFrom = "Brussels";
        String goingTo = "New York";   
        int child = 3;

        [SetUp]
        public void SetupTest()
        {
            driver = new ChromeDriver();
        }

        [Test]
        public void FlightBookingTest()
        {
            driver.Navigate().GoToUrl("https://www.expedia.com/");
            //searchs the flight availability
            searchFlight();
            //clicks traveler options
            driver.FindElement(By.XPath(".//a[@aria-label='1 traveler']")).Click();
            // adds travelers
            addTraveler();
            //Clicks search button
            driver.FindElement(By.XPath("(.//button[@data-testid='submit-button'])")).Click();
            Thread.Sleep(2000);
            // Checks the result page
            String actualResult = "Arrival time in New York";
            String expectedResult = driver.FindElement(By.XPath("//legend[contains(.,'Arrival time in New York')]")).Text;
            Console.WriteLine(expectedResult);
            Assert.AreEqual(expectedResult, actualResult);
            //searchs accomodations
            searchStays();
        }

        ///method to select source and destination
        public void searchFlight()
        {
            //clicks the flight tab
            driver.FindElement(By.XPath("//ul[@id='uitk-tabs-button-container']/li[2]/a")).Click();
            // clicks the Leaving from textbox
            driver.FindElement(By.XPath("(.//button[@aria-label='Leaving from'])")).Click();
            //enters source
            driver.FindElement(By.Id("location-field-leg1-origin")).SendKeys(leavingFrom);
            Thread.Sleep(1000);
            //selects the source
            driver.FindElement(By.XPath(".//strong[contains(text(), 'Brussels')]")).Click();
            Thread.Sleep(200);
            //clicks destination
            driver.FindElement(By.XPath("(.//button[@aria-label='Going to'])")).Click();
            //enters destinaton
            driver.FindElement(By.Id("location-field-leg1-destination")).SendKeys(goingTo);
            Thread.Sleep(1000);
            //selects the destination
            driver.FindElement(By.XPath(".//strong[contains(text(), 'New York')]")).Click();
            Thread.Sleep(1000);
        }

        ///method to add travelers
        public void addTraveler()
        {
            //clicks the traveler drop down
            driver.FindElement(By.XPath("//*[@id='adaptive-menu']/div/div/section/div[1]/div[2]/div/button[2]")).Click();
            //adds a child
            driver.FindElement(By.Id("child-age-input-0-0")).Click();
            //selects the child age
            new SelectElement(driver.FindElement(By.Id("child-age-input-0-0"))).SelectByIndex(child);
            //clicks the done button in dropdown
            driver.FindElement(By.XPath("(.//button[@data-testid='guests-done-button'])")).Click();
        }

        /// method to search accomodation
        public void searchStays()
        {
            //clicks the stays tab
            driver.FindElement(By.XPath("//nav[@id='header-toolbar-nav-extended']/a/div")).Click();
            //clicks the destionation textbox
            driver.FindElement(By.XPath("//div[@id='location-field-destination-menu']/div/button")).Click();
            //enters the destination
            driver.FindElement(By.Id("location-field-destination")).SendKeys(goingTo);
            driver.FindElement(By.Id("location-field-destination")).SendKeys(Keys.Enter);
            //clicks the traveler option
            driver.FindElement(By.XPath("(.//button[@data-testid='travelers-field-trigger'])")).Click();
            //deselects adut count from 2 to 1
            driver.FindElement(By.CssSelector("svg.uitk-icon.uitk-step-input-icon.uitk-icon-medium")).Click();
            // method call to select child
            addTraveler();
            //clicks the  search button
            driver.FindElement(By.XPath("//button[@type='submit']")).Click();
        }

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
        }
    }
}