
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Linq;

namespace AmazonAutomation.Pages
{
    public class SearchResultsPage
    {
        private readonly IWebDriver driver;
        private readonly WebDriverWait wait;

        private readonly By productLinks = By.CssSelector("div[data-component-type='s-search-result'] h2 a");

        public SearchResultsPage(IWebDriver driver)
        {
            this.driver = driver;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
        }




        public void ClickFirstProduct()
        {
            var productLinks = By.XPath("//a[contains(@href,'/dp/')]");
            wait.Until(d => d.FindElements(productLinks).Count > 0);

            var elements = driver.FindElements(productLinks);
            Console.WriteLine($"Found {elements.Count} product links with /dp/ in href.");

            if (elements.Count == 0)
                throw new NoSuchElementException("No valid product links found.");

            var firstValid = elements.First();
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView({block:'center'});", firstValid);
            System.Threading.Thread.Sleep(500);
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", firstValid);
        }



    }
}
