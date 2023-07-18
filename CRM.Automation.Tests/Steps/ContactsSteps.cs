using CRM.Automation.Tests.Models;
using CRM.Automation.Tests.PageObjects;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using Xunit;

namespace CRM.Automation.Tests.Steps;

[Binding]
public class ContactsSteps
{
    private readonly ScenarioContext _scenarioContext;
    
    public ContactsSteps(ScenarioContext scenarioContext)
    {
        _scenarioContext = scenarioContext;
    }
    
    [When(@"create new contact with the following data:")]
    public void CreateNewContact(Table table)
    {
        var contact = table.CreateInstance<Contact>();
        var contactsPage = new ContactsPage();
        contactsPage.ClickCreate();
        var createContactPage = new CreateContactPage();
        createContactPage.CreateContact(contact);
        _scenarioContext["CreatedContact"] = contact;
    }

    [Then(@"created contact data matches entered on the previous step")]
    public void CreatedContactDataMatchesEnteredOnThePreviousStep()
    {
        var expectedContact = _scenarioContext.Get<Contact>("CreatedContact");
        var actualContact = new ContactProfilePage().GetContactDetails();
        Assert.Equal(expectedContact, actualContact);
        _scenarioContext["RemoveContact"] = true;
    }
}