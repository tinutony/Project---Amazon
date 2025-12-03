
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;

namespace AmazonAutomation.Pages
{
    public class ProductPage
    {
        private readonly IWebDriver driver;
        private readonly WebDriverWait wait;

        private readonly By buyNowButton = By.Id("buy-now-button");

        public ProductPage(IWebDriver driver)
        {
            this.driver = driver;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
        }

        public void ClickBuyNow()
        {
            var element = wait.Until(ExpectedConditions.ElementToBeClickable(buyNowButton));
            element.Click();
        }
    }
}
