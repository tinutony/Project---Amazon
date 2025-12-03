
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;

namespace AmazonAutomation.Pages
{
    public class HomePage
    {
        private readonly IWebDriver driver;
        private readonly WebDriverWait wait;

        private readonly By searchBox = By.Id("twotabsearchtextbox");
        private readonly By searchButton = By.Id("nav-search-submit-button");

        public HomePage(IWebDriver driver)
        {
            this.driver = driver;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
        }

        public void GoToAmazon()
        {
            driver.Navigate().GoToUrl("https://www.amazon.in/");
        }

        public void SearchProduct(string product)
        {
            var box = wait.Until(ExpectedConditions.ElementIsVisible(searchBox));

            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].value='" + product + "';", box);

            wait.Until(ExpectedConditions.ElementToBeClickable(searchButton)).Click();
        }
    }
}
