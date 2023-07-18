using CRM.Automation.Framework.Elements;
using CRM.Automation.Framework.Pages;
using CRM.Automation.Framework.Waits;
using CRM.Automation.Framework.Utils;
using CRM.Automation.Tests.PageObjects.Enums;
using OpenQA.Selenium;

namespace CRM.Automation.Tests.PageObjects;

public abstract class BaseCrmPage : BasePage
{
    public TopMenu TopMenu { get; private set; } = new();

    protected string OperationStatusSelector(OperationStatus status) =>
        $"//*[@id='ajaxStatusDiv' and contains(text(), '{status.GetDescription()}')]";

    private Label LoadingLabel => new(
        By.XPath(OperationStatusSelector(OperationStatus.Loading)), "Loading Label");

    protected BaseCrmPage(By locator, string title) : base(locator, title)
    {
    }

    public void WaitForPageDisplayed()
    {
        ConditionalWait.WaitForCondition(() => WaitForPageToLoad() && !LoadingLabel.IsVisible && IsDisplayed());
    }
}