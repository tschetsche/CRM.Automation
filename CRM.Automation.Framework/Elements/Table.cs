using CRM.Automation.Framework.Logging;
using OpenQA.Selenium;

namespace CRM.Automation.Framework.Elements;

public class Table : WebElement
{
    public Table(By locator, string name) : base(locator, name)
    {
    }

    public int GetTotalRowsDisplayed()
    {   
        LogHelper.Logger.Info($"Getting rows total for '{Name}' table");
        return Element.FindElements(By.CssSelector("tr")).Count;
    }
}