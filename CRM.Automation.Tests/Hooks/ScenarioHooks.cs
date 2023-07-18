using CRM.Automation.Tests.PageObjects;
using TechTalk.SpecFlow;

namespace CRM.Automation.Tests.Hooks;

[Binding]
public class ScenarioHooks
{
    
    private readonly ScenarioContext _scenarioContext;
    
    public ScenarioHooks(ScenarioContext scenarioContext)
    {
        _scenarioContext = scenarioContext;
    }
    
    [AfterScenario("removeContact", Order = 1)]
    public void RemoveContact()
    {
        var shouldContactBeRemoved = _scenarioContext.ContainsKey("RemoveContact");
        if (shouldContactBeRemoved)
        {
            new ContactProfilePage().DeleteContact();
            new ContactsPage().WaitForPageDisplayed();
        }
    }
}