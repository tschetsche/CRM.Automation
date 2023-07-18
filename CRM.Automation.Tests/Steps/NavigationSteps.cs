using CRM.Automation.Tests.PageObjects;
using CRM.Automation.Tests.PageObjects.Enums;
using TechTalk.SpecFlow;

namespace CRM.Automation.Tests.Steps;

[Binding]
public class NavigationSteps
{
    [When(@"navigate to '(.*)' -> '(.*)'")]
    public void NavigateTo(TopMenuItems mainMenuItem, TopSubMenuItems subMenuItem)
    {
        var homePage = new HomePage();
        homePage.WaitForPageDisplayed();
        homePage.TopMenu.NavigateTo(mainMenuItem, subMenuItem);
    }
}