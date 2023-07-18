using CRM.Automation.Framework.Elements;
using CRM.Automation.Framework.Utils;
using CRM.Automation.Framework.Waits;
using CRM.Automation.Tests.Models;
using CRM.Automation.Tests.PageObjects.Enums;
using OpenQA.Selenium;

namespace CRM.Automation.Tests.PageObjects;

public class ContactProfilePage : BaseCrmPage
{
    private PersonalDataPage PersonalDataPage { get; } = new();
    
    private Label BusinessRoleLabel => new(
        By.XPath(
            "//*[@class='form-label' and contains(text(), 'Business Role')]/following-sibling::*[@class='form-value']"),
        "BusinessRole Label");

    private Label CategoryLabel => new(
        By.XPath("//*[@class='form-label' and contains(text(), 'Category')]"), "Category Label");

    private Dropdown ViewPersonalDataDropdown => new(By.Id("DetailForm_personal_data-label"),
        "View Personal Data Dropdown");

    private Button DeleteButton => new(By.Id("DetailForm_delete-label"), "Delete Button");

    public ContactProfilePage() : base(By.CssSelector(".mod-Contacts.ctx-detail"), "Contact Profile")
    {
    }

    public Contact GetContactDetails()
    {
        WaitForPageDisplayed();
        var (firstName, lastName) = GetPersonalData();
        var businessRole = BusinessRoleLabel.GetTextContent();
        var categories = CategoryLabel.JsFindElementNextSiblingText().Split(',').Select(x => x.Trim()).ToList();
        return new Contact
        {
            FirstName = firstName,
            LastName = lastName,
            Categories = categories,
            Role = businessRole
        };
    }

    public void DeleteContact()
    {
        DeleteButton.Click();
        ConfirmAlert();
    }
    
    private void ViewPersonalData()
    {
        ConditionalWait.WaitForCondition(()=> ViewPersonalDataDropdown.IsVisible);
        ViewPersonalDataDropdown.ScrollToElement();
        ViewPersonalDataDropdown.SelectByName(ContactPersonalDataActions.ViewPersonalData.GetDescription());
    }

    private (string firstName, string lastName) GetPersonalData()
    {
        ViewPersonalData();
        PersonalDataPage.WaitForPageDisplayed();
        var firstName = PersonalDataPage.GetFirstName();
        var lastName = PersonalDataPage.GetLastName();
        PersonalDataPage.ClosePersonalData();
        WaitForPageDisplayed();
        return (firstName, lastName);
    }
}