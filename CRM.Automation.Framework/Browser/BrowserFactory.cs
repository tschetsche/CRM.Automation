using Microsoft.Extensions.Configuration;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;

namespace CRM.Automation.Framework.Browser;

public class BrowserFactory
{
    private readonly IConfiguration _configuration;

    public BrowserFactory(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public IWebDriver CreateDriver()
    {
        var browserType = _configuration["browserType"];
        switch (Enum.Parse<BrowserTypes>(browserType!))
        {
            case BrowserTypes.Chrome:
                var chromeOptions = new ChromeOptions();
                SetBrowserPreferences(chromeOptions, "driverSettings:chrome:options");
                chromeOptions.AddArguments(GetStartArguments("driverSettings:chrome:startArguments"));
                return new ChromeDriver(chromeOptions);

            case BrowserTypes.Edge:
                var edgeOptions = new EdgeOptions();
                SetBrowserPreferences(edgeOptions, "driverSettings:edge:options");
                edgeOptions.AddArguments(GetStartArguments("driverSettings:edge:startArguments"));
                return new EdgeDriver(edgeOptions);

            default:
                throw new NotSupportedException(
                    $"Browser type {browserType} is not supported. Supported browser types are: {string.Join(", ", Enum.GetNames<BrowserTypes>())}");
        }
    }
    
    private void SetBrowserPreferences(DriverOptions options, string configSection)
    {
        var prefs = _configuration.GetSection(configSection).GetChildren()
            .ToDictionary(config => config.Key, config => (object)config.Value!);
        foreach (var pref in prefs)
        {
            options.AddAdditionalOption(pref.Key, pref.Value);
        }
    }

    private IEnumerable<string> GetStartArguments(string configSection)
    {
        return _configuration.GetSection(configSection).GetChildren().Select(c => c.Value).ToList()!;
    }
}