using CRM.Automation.Framework.Logging;
using OpenQA.Selenium;

namespace CRM.Automation.Framework.Elements;

public class Dropdown : WebElement
{
    private Label DropdownOptionByName(string name) => new (By.XPath(
        $"//*[contains (@class, 'popup-default')]//*[contains(@class, 'menu-option')]/*[contains(text(), '{name}')]"), 
         $"Dropdown Option {name}");

    public Dropdown(By locator, string name) : base(locator, name)
    {
    }
    
    public void SelectByName(string name)
    {
        LogHelper.Logger.Info($"Opening '{Name}' Dropdown");
        Hover();
        Click();
        LogHelper.Logger.Info($"Selecting '{name}' Dropdown Option");
        DropdownOptionByName(name).Click();
    }
}