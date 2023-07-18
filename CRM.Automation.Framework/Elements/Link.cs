using OpenQA.Selenium;

namespace CRM.Automation.Framework.Elements;

public class Link : WebElement
{
    public Link(By locator, string name) : base(locator, name)
    {
    }
    
    public string Href => GetAttribute("href");
}