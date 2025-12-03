
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.IO;
using System.Linq;
using AmazonAutomation.Drivers;
using AmazonAutomation.Pages;
using AventStack.ExtentReports;
using AmazonAutomation.Reports;

namespace AmazonAutomation.Tests
{
    public class AmazonTests
    {
        private IWebDriver driver;
        private WebDriverWait wait;
        private HomePage homePage;
        private SearchResultsPage searchResultsPage;
        private ProductPage productPage;
        private ExtentReports extent;
        private ExtentTest test;
        private string screenshotFolder;

        [OneTimeSetUp]
        public void InitReport()
        {
            extent = ReportManager.GetInstance();

            screenshotFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Reports", DateTime.Now.ToString("yyyy-MM-dd"), "Screenshots");
            Directory.CreateDirectory(screenshotFolder);
        }

        [SetUp]
        public void Setup()
        {
            driver = DriverFactory.CreateDriver();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
            homePage = new HomePage(driver);
            searchResultsPage = new SearchResultsPage(driver);
            productPage = new ProductPage(driver);
        }

        [Test]
        public void BuyLaptopFlow()
        {
            test = extent.CreateTest("Buy Laptop Flow");

            try
            {
                homePage.GoToAmazon();
                test.Info("Navigated to Amazon");

                homePage.SearchProduct("laptop");
                test.Info("Searched for laptop");

                searchResultsPage.ClickFirstProduct();
                test.Info("Clicked first product");

                var handles = driver.WindowHandles;
                if (handles.Count > 1)
                {
                    driver.SwitchTo().Window(handles.Last());
                    test.Info("Switched to product tab");
                }

                productPage.ClickBuyNow();
                test.Info("Clicked Buy Now");

                Assert.That(driver.Url, Does.Contain("buy-now").IgnoreCase);

                string successScreenshot = CaptureScreenshot("BuyLaptopFlow_Success");
                test.Pass("Buy Laptop Flow completed successfully")
                    .AddScreenCaptureFromPath(successScreenshot);
            }
            catch (Exception ex)
            {
                string screenshotPath = CaptureScreenshot("BuyLaptopFlow_Failure");
                test.Fail($"Test failed: {ex.Message}")
                    .AddScreenCaptureFromPath(screenshotPath);
                throw;
            }
        }

        private string CaptureScreenshot(string testName)
        {
            string screenshotPath = Path.Combine(screenshotFolder, $"{testName}_{DateTime.Now:HHmmss}.png");
            Screenshot screenshot = ((ITakesScreenshot)driver).GetScreenshot();
            screenshot.SaveAsFile(screenshotPath); 
            return screenshotPath;
        }

        [TearDown]
        public void Cleanup()
        {
            driver?.Quit();
            driver?.Dispose();
        }


        [OneTimeTearDown]
        public void CloseReport()
        {
            extent.Flush();

            string reportPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Reports", DateTime.Now.ToString("yyyy-MM-dd"), "ExtentReport.html");
            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
            {
                FileName = reportPath,
                UseShellExecute = true
            });
        }

    }
}
