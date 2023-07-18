using OpenQA.Selenium;

namespace CRM.Automation.Framework.Elements;

public class Button : WebElement
{
    public Button(By locator, string name) : base(locator, name)
    {
    }
}