using CRM.Automation.Framework.Elements;
using CRM.Automation.Framework.Pages;
using CRM.Automation.Framework.Utils;
using CRM.Automation.Framework.Waits;
using CRM.Automation.Tests.PageObjects.Enums;
using OpenQA.Selenium;

namespace CRM.Automation.Tests.PageObjects;

public class TopMenu: BasePage
{
    private Label ParentMenuLabel(TopMenuItems menuItem) => new(By.CssSelector($"a.{menuItem.GetDescription()}"),
        $"{nameof(menuItem)} Menu Item");
    private Label SubMenuLabel(TopSubMenuItems menuItem) => new(
        By.CssSelector($"a[href*='module={menuItem.GetDescription()}']"), $"{nameof(menuItem)} Sub Menu Item");

    public TopMenu() : base(By.CssSelector("nav.nav-wrap"), "Top Menu")
    {
    }

    public void NavigateTo(TopMenuItems menuItem, TopSubMenuItems subMenuItem)
    {
        var mainMenuItem = ParentMenuLabel(menuItem);
        ConditionalWait.WaitForCondition(() => mainMenuItem.IsVisible);
        ParentMenuLabel(menuItem).Hover();
        ConditionalWait.WaitForCondition(() => mainMenuItem.IsVisible);
        SubMenuLabel(subMenuItem).Hover();
        SubMenuLabel(subMenuItem).Click();
    }
}