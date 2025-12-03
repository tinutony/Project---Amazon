
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using System;
using System.IO;

namespace AmazonAutomation.Reports
{
    public static class ReportManager
    {
        private static ExtentReports extent;
        private static ExtentSparkReporter sparkReporter;

        public static ExtentReports GetInstance()
        {
            if (extent == null)
            {
                string reportFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Reports", DateTime.Now.ToString("yyyy-MM-dd"));
                Directory.CreateDirectory(reportFolder);

                string reportPath = Path.Combine(reportFolder, "ExtentReport.html");
                sparkReporter = new ExtentSparkReporter(reportPath);
                sparkReporter.Config.DocumentTitle = "Amazon Automation Report";
                sparkReporter.Config.ReportName = "Buy Laptop Flow";

                extent = new ExtentReports();
                extent.AttachReporter(sparkReporter);
            }
            return extent;
        }
    }
}
