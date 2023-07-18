using OpenQA.Selenium;

namespace CRM.Automation.Framework.Elements;

public class Checkbox : WebElement
{
    public Checkbox(By locator, string name) : base(locator, name)
    {
    }

    public void Check()
    {
        if (!IsSelected)
        {
            Element.Click();
        }
    }
}