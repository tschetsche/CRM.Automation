using OpenQA.Selenium;

namespace CRM.Automation.Framework.App;

public static class Application
{
    private static readonly ThreadLocal<IWebDriver> _driver = new();

    public static IWebDriver Driver
    {
        get => _driver.Value!;
        set => _driver.Value = value;
    }
}