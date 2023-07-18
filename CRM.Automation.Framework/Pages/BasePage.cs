using CRM.Automation.Framework.App;
using CRM.Automation.Framework.Elements;
using CRM.Automation.Framework.Logging;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace CRM.Automation.Framework.Pages;

public abstract class BasePage
{
    private const int DefaultWaitTime = 30;
    private string Name { get; }
    private By Locator { get; }

    protected BasePage(By locator, string name)
    {
        Name = name;
        Locator = locator;
    }

    protected bool WaitForPageToLoad(int? timeToWaitInSeconds = null)
    {
        try
        {
            LogHelper.Logger.Info($"Waiting for '{Name}' page to load");
            var wait = new WebDriverWait(Application.Driver, TimeSpan.FromSeconds(timeToWaitInSeconds ?? DefaultWaitTime));
            return wait.Until(driver =>
            {
                var state = ((IJavaScriptExecutor)driver).ExecuteScript("return document.readyState").ToString();
                return state == "complete";
            });
        }
        catch (WebDriverTimeoutException)
        {
            throw new TimeoutException($"Page '{Name}' did not load within {timeToWaitInSeconds ?? DefaultWaitTime} seconds");
        }
    }

    protected void ConfirmAlert(int? timeToWaitInSeconds = null)
    {
        LogHelper.Logger.Info($"Confirming alert on '{Name}' page");
        var wait = new WebDriverWait(Application.Driver, TimeSpan.FromSeconds(timeToWaitInSeconds ?? DefaultWaitTime));
        wait.Until(ExpectedConditions.AlertIsPresent());
        var alert = Application.Driver.SwitchTo().Alert();
        alert.Accept();
    }

    protected bool IsDisplayed()
    {
        LogHelper.Logger.Info($"Checking if '{Name}' page is displayed");
        return new Label(Locator, Name).IsVisible;
    }
}