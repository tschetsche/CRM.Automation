using CRM.Automation.Framework.Elements;
using CRM.Automation.Framework.Waits;
using OpenQA.Selenium;

namespace CRM.Automation.Tests.PageObjects;

public class ProjectPage : BaseCrmPage
{
    private Table ReportsTable => new(By.CssSelector("table.listView tbody"), "Reports Table");

    private Button RunReportButton => new(
        By.CssSelector("button[id*='FilterForm_applyButton']"), "Run Report Button");

    public ProjectPage() : base(By.CssSelector(".mod-Project.ctx-listview"), "Projects")
    {
    }

    public void RunReport()
    {
        WaitForPageDisplayed();
        RunReportButton.Click();
        ConditionalWait.WaitForCondition(() => ReportsTable.IsVisible);
    }

    public int GetRowsTotal() => ReportsTable.GetTotalRowsDisplayed();
}