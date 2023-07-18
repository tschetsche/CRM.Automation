using OpenQA.Selenium;

namespace CRM.Automation.Tests.PageObjects;

public class HomePage : BaseCrmPage
{
    public HomePage() : base(By.CssSelector(".mod-Home.ctx-dashboard"), "Home")
    {
    }
}