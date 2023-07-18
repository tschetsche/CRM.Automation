using CRM.Automation.Framework.App;
using CRM.Automation.Framework.Logging;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;

namespace CRM.Automation.Framework.Elements;

public class TextField : WebElement
{
    public TextField(By locator, string name) : base(locator, name)
    {
    }

    public void Type(string value)
    {
        LogHelper.Logger.Info($"Type '{value}' to '{Name}' text field");
        Element.SendKeys(value);
    }

    public void ClearAndType(string value)
    {
        LogHelper.Logger.Info($"Clear '{Name}' text field");
        Element.Clear();
        Type(value);
    }

    public void TypeAndSubmit(string value)
    {
        Type(value);
        LogHelper.Logger.Info($"Pressing Enter in'{Name}' text field");
        var builder = new Actions(Application.Driver);
        builder.SendKeys(Keys.Enter).Perform();
    }
}