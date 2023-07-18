using CRM.Automation.Framework.Elements;
using CRM.Automation.Framework.Pages;
using OpenQA.Selenium;

namespace CRM.Automation.Tests.PageObjects;

public class LoginPage : BasePage
{
    private TextField UsernameField => new(By.Id("login_user"), "User Name Field");
    private TextField PasswordField => new(By.Id("login_pass"), "Password Field");
    private Button LoginButton => new(By.Id("login_button"), "Login Button");

    public LoginPage() : base(By.Id("login_user"), "Login")
    {
    }

    public void Login(string username, string password)
    {
        UsernameField.ClearAndType(username);
        PasswordField.ClearAndType(password);
        LoginButton.Click();
    }
}