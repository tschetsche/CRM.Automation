using CRM.Automation.Framework.Waits;
using CRM.Automation.Tests.PageObjects;
using TechTalk.SpecFlow;
using Xunit;

namespace CRM.Automation.Tests.Steps;

[Binding]
public class ActivityLogSteps
{
    private readonly ScenarioContext _scenarioContext;
    
    public ActivityLogSteps(ScenarioContext scenarioContext)
    {
        _scenarioContext = scenarioContext;
    }
    
    [Then(@"selected items are deleted from Activity log table")]
    public void SelectedItemsAreDeletedFromActivityLogTable()
    {
        var expectedDeletedLogs = _scenarioContext.Get<List<string>>("ActivityLogsToDelete");
        var activityLogPage = new ActivityLogPage();
        var actualLogs = activityLogPage.GetActivityLogsDisplayed();
        Assert.Empty(actualLogs.Intersect(expectedDeletedLogs));
    }

    [When(@"delete selected items from Activity log table")]
    public void DeleteSelectedItemsFromActivityLogTable()
    {
        var activityLogPage = new ActivityLogPage();
        var logTotalBeforeDelete = activityLogPage.GetActivityLogTotal();
        activityLogPage.Delete();
        ConditionalWait.WaitForCondition(() => activityLogPage.GetActivityLogTotal() < logTotalBeforeDelete);
    }

    [When(@"select first (.*) items in Activity log table")]
    public void SelectFirstItemsInActivityLogTable(int numbersOfItemsToSelect)
    {
        var activityLogPage = new ActivityLogPage();
        activityLogPage.WaitForPageDisplayed();
        var itemsToDelete = activityLogPage.GetLogDataIds(numbersOfItemsToSelect);
        _scenarioContext["ActivityLogsToDelete"] = itemsToDelete;
        activityLogPage.CheckFirstItems(numbersOfItemsToSelect);
    }
}