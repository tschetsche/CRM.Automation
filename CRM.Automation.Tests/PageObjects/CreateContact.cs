using CRM.Automation.Framework.Elements;
using CRM.Automation.Framework.Waits;
using CRM.Automation.Tests.Models;
using CRM.Automation.Tests.PageObjects.Enums;
using OpenQA.Selenium;

namespace CRM.Automation.Tests.PageObjects;

public class CreateContactPage : BaseCrmPage
{
    private TextField FirstNameField => new(By.Id("DetailFormfirst_name-input"), "First Name Field");
    private TextField LastNameField => new(By.Id("DetailFormlast_name-input"), "Last Name Field");
    private Button SaveButton => new(By.Id("DetailForm_save2-label"), "Save Button");
    private Dropdown BusinessRoleDropdown => new(By.Id("DetailFormbusiness_role-input"), "Business Role Dropdown");

    private SearchDropdown CategorySearchDropdown =>
        new(By.Id("DetailFormcategories-input"), "Category SearchDropdown");

    private Label SavingLabel => new(
        By.XPath(OperationStatusSelector(OperationStatus.Saving)), "Saving Label");

    public CreateContactPage() : base(By.XPath("//*[@id='main-title-text' and contains(., 'Contacts: (new record)')]"),
        "Create Contact")
    {
    }

    public void CreateContact(Contact contact)
    {
        WaitForPageDisplayed();
        FirstNameField.ClearAndType(contact.FirstName);
        LastNameField.ClearAndType(contact.LastName);
        BusinessRoleDropdown.SelectByName(contact.Role);
        CategorySearchDropdown.SelectMultiple(contact.Categories);
        SaveButton.Click();
        ConditionalWait.WaitForCondition(() => !SavingLabel.IsVisible);
    }
}