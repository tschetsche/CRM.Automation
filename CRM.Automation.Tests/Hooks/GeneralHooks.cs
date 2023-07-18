using System.Text;
using Allure.Net.Commons;
using CRM.Automation.Framework.App;
using CRM.Automation.Framework.Browser;
using CRM.Automation.Framework.Logging;
using CRM.Automation.Tests.Extensions;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OpenQA.Selenium;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace CRM.Automation.Tests.Hooks;

[Binding]
public class GeneralHooks
{
    private readonly IConfiguration _configuration;
    private readonly ScenarioContext _scenarioContext;
    
    public GeneralHooks(ScenarioContext scenarioContext)
    {
        _scenarioContext = scenarioContext;
        _configuration = GetConfiguration();
    }

    [BeforeTestRun]
    public static void BeforeTestRun()
    {
        Service.Instance.ValueRetrievers.Register(new ListValueRetriever());
    }

    [BeforeScenario(Order = 1)]
    public void BeforeScenario()
    {
        _scenarioContext.ScenarioContainer.RegisterInstanceAs(_configuration);
        
        LogHelper.Logger.Info($"Starting scenario: {_scenarioContext.ScenarioInfo.Title}");
        Application.Driver = new BrowserFactory(_configuration).CreateDriver();
        Application.Driver.Url = _configuration["url"];
    }

    [BeforeScenario("loginViaApi", Order = 2)]
    public void LoginViaApi()
    {
        var loginData = new
        {
            username = _configuration["loginUsername"],
            password = _configuration["loginPassword"]
        };
        var content = new StringContent(JsonConvert.SerializeObject(loginData), Encoding.UTF8, "application/json");

        using var client = new HttpClient();
        var response = client.PostAsync(_configuration["loginUrl"], content)
            .GetAwaiter().GetResult();
        var responseObject = ParseResponse(response);
        AddSessionCookieToBrowser(responseObject);
        
        Application.Driver.Url = _configuration["url"];
    }

    [AfterScenario(Order = 100)]
    public void AfterScenario()
    {
        Application.Driver.Quit();
        Application.Driver.Dispose();
        LogHelper.Logger.Info($"Finished scenario: {_scenarioContext.ScenarioInfo.Title}");
    }
    
    [AfterStep]
    public void AfterStep()
    {
        if (_scenarioContext.TestError != null)
        {
            var screenshotFilePath = TakeScreenshot();
            if (!string.IsNullOrWhiteSpace(screenshotFilePath))
            {
                AllureLifecycle.Instance.AddAttachment($"{_scenarioContext.StepContext.StepInfo.Text} - Screenshot",
                    "image/png", screenshotFilePath);
            }
        }
    }

    private static IConfiguration GetConfiguration()
    {
        var configurationBuilder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

        return configurationBuilder.Build();
    }
    
    private static JObject ParseResponse(HttpResponseMessage response)
    {
        try
        {
            var responseContent = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            return JObject.Parse(responseContent);
        }
        catch (JsonReaderException ex)
        {
            LogHelper.Logger.Error($"Could not parse login response: {ex}");
            throw;
        }
    }

    private static void AddSessionCookieToBrowser(JObject responseObject)
    {
        var sessionName = responseObject["session_name"]!.Value<string>();
        var sessionId = responseObject["json_session_id"]!.Value<string>();

        var cookie = new Cookie(sessionName, sessionId);
        Application.Driver.Manage().Cookies.AddCookie(cookie);
    }
    
    private static string TakeScreenshot()
    {
        if (Application.Driver is ITakesScreenshot screenshotDriver)
        {
            var screenshot = screenshotDriver.GetScreenshot();
            var screenshotsDir =
                Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), "Screenshots"));
            var screenshotFileName = Path.Combine(screenshotsDir.FullName, $"{DateTime.Now:yyyyMMdd_HHmmss}.png");
            screenshot.SaveAsFile(screenshotFileName, ScreenshotImageFormat.Png);

            return screenshotFileName;
        }

        return string.Empty;
    }
}