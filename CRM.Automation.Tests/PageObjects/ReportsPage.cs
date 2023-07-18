using CRM.Automation.Framework.Elements;
using CRM.Automation.Framework.Waits;
using OpenQA.Selenium;

namespace CRM.Automation.Tests.PageObjects;

public class ReportsPage: BaseCrmPage
{
    private TextField SearchTextField => new(
        By.CssSelector("[id*='FilterFormfilter_text-input'] input"), "Search Field");

    private Link SearchResultByName(string name) =>
        new(
            By.XPath($"//*[@class='listViewNameLink' and contains(text(), '{name}')]"), $"Search Result {name} Link");

    public ReportsPage() : base(By.CssSelector(".mod-Reports.ctx-listview"), "Reports")
    {
    }

    public void SearchReport(string reportName)
    {
        WaitForPageDisplayed();
        SearchTextField.TypeAndSubmit(reportName);
        ConditionalWait.WaitForCondition(() => SearchResultByName(reportName).IsVisible);
        SearchResultByName(reportName).Click();
    }
}