using System.Globalization;
using CRM.Automation.Framework.Elements;
using CRM.Automation.Framework.Utils;
using CRM.Automation.Tests.PageObjects.Enums;
using OpenQA.Selenium;

namespace CRM.Automation.Tests.PageObjects;

public class ActivityLogPage : BaseCrmPage
{
    private Dropdown ActionsDropdown => new(By.CssSelector("button[id*='ActionButtonHead']"), "Actions Dropdown");

    private Checkbox ActivityLogCheckboxByIndex(int index) => new(
            By.XPath($"//tbody/descendant::input[@type='checkbox'][{index.ToString()}]"), "Activity Log Checkbox");

    private Label ActivityLogRow => new(By.XPath("//*[@class='listView']//tbody//tr"), "Activity Log");

    private Label ActivityLogRowByIndex(int index) => new(
            By.XPath($"//*[@class='listView']//tbody//tr[{index}]"), 
            $"Activity Log Row [{index}]");

    private Label ActivityLogTotalLabel =>
        new(By.CssSelector(".panel-subheader .listview-status .text-number:last-child"), "Activity Log Total Label");

    public ActivityLogPage() : base(By.CssSelector(".mod-ActivityLog.ctx-listview"), "Activity Log")
    {
    }

    public void Delete()
    {
        ActionsDropdown.SelectByName(ActivityLogActions.Delete.GetDescription());
        ConfirmAlert();
    }

    public List<string> GetLogDataIds(int number)
    {
        var logDataIds = new List<string>();
        for (var i = 1; i <= number; i++)
        {
            logDataIds.Add(ActivityLogRowByIndex(i).GetAttribute("data-id"));
        }
        return logDataIds;
    }

    public void CheckFirstItems(int number)
    {
        for (var i = 1; i <= number; i++)
        {
            ActivityLogCheckboxByIndex(i).Check();
        }
    }

    public List<string> GetActivityLogsDisplayed()
    {
        var totalLogsCount = ActivityLogRow.GetElementCount();
        return GetLogDataIds(totalLogsCount);
    }

    public int GetActivityLogTotal()
    {
        return int.Parse(ActivityLogTotalLabel.GetTextContent(), NumberStyles.AllowThousands);
    }
}