using CRM.Automation.Framework.Elements;
using OpenQA.Selenium;

namespace CRM.Automation.Tests.PageObjects;

public class ContactsPage : BaseCrmPage
{
    private Button CreateButton => new(By.CssSelector(".panel-subheader button[id*='create']"), "Create Button");
    
    public ContactsPage() : base(By.CssSelector(".mod-Contacts.ctx-listview"), "Contacts")
    {
    }
    
    public void ClickCreate()
    {
        WaitForPageDisplayed();
        CreateButton.Hover();
        CreateButton.Click();
    }
}