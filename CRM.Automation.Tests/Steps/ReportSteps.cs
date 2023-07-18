using CRM.Automation.Tests.PageObjects;
using TechTalk.SpecFlow;
using Xunit;

namespace CRM.Automation.Tests.Steps;

[Binding]
public class ReportSteps
{
    [When(@"find '(.*)' report")]
    public void FindReport(string reportName)
    {
        var reportsPage = new ReportsPage();
        reportsPage.SearchReport(reportName);
    }

    [Then(@"some results are returned after running the report")]
    public void IsResultReturnedAfterRunningTheReport()
    {
        var projectPage = new ProjectPage();
        projectPage.RunReport();
        var rowsTotal = projectPage.GetRowsTotal();
        Assert.True(rowsTotal > 0, "No results are displayed");
    }
}