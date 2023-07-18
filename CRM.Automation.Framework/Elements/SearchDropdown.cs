using OpenQA.Selenium;

namespace CRM.Automation.Framework.Elements;

public class SearchDropdown : WebElement
{
    private const string BaseOptionLocator = "//*[contains (@id, 'search-list')]//*[contains(@class, 'menu-option')]";

    private Label DropdownOptionByName(string name)
    {
        return new Label(By.XPath($"{BaseOptionLocator}/*[contains(text(), '{name}')]"),
            $"SearchDropdown option {name}");
    }

    private TextField SearchInput => new(By.CssSelector(".popup-search input"), "Search Input");

    public SearchDropdown(By locator, string name) : base(locator, name)
    {
    }

    public void SelectMultiple(List<string> optionsList)
    {
        Element.Click();
        foreach (var option in optionsList)
        {
            SearchInput.ClearAndType(option);
            DropdownOptionByName(option).Click();
        }
    }
}