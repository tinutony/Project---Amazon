
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;

namespace AmazonAutomation.Drivers
{
    public static class DriverFactory
    {
        public static IWebDriver CreateDriver()
        {
            var options = new ChromeOptions();
            options.AddArgument("--start-maximized");
            var driver = new ChromeDriver(options); 
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(3);
            return driver;
        }
    }
}
