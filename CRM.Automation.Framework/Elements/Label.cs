using OpenQA.Selenium;

namespace CRM.Automation.Framework.Elements;

public class Label : WebElement
{
    public Label(By locator, string name) : base(locator, name)
    {
    }
}