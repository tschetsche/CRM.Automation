using CRM.Automation.Tests.PageObjects;
using Microsoft.Extensions.Configuration;
using TechTalk.SpecFlow;

namespace CRM.Automation.Tests.Steps;

[Binding]
public class LoginSteps
{
    private readonly IConfiguration _configuration;

    public LoginSteps(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    [When(@"I login to the CRM")]
    public void LoginToTheCrm()
    {
        new LoginPage().Login(_configuration["loginUsername"]!, 
            _configuration["loginPassword"]!);
    }
}