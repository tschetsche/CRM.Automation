using CRM.Automation.Framework.Elements;
using OpenQA.Selenium;

namespace CRM.Automation.Tests.PageObjects;

public class PersonalDataPage : BaseCrmPage
{
    private Label FirstName => new(By.CssSelector(".cell-first_name .form-value"), "First Name Label");
    private Label LastName => new(By.CssSelector(".cell-last_name .form-value"), "Last Name Label");
    private Button CancelButton => new(By.Id("ViewPersonal_cancel-label"), "Cancel Button");

    public PersonalDataPage() : base(By.Id("ViewPersonal"), "Personal Data")
    {
    }

    public string GetFirstName() => FirstName.GetTextContent();

    public string GetLastName() => LastName.GetTextContent();

    public void ClosePersonalData()
    {
        CancelButton.Click();
    }
}