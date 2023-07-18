using CRM.Automation.Framework.App;
using CRM.Automation.Framework.Logging;
using CRM.Automation.Framework.Waits;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;

namespace CRM.Automation.Framework.Elements;

public abstract class WebElement
{
    protected IWebElement Element => FindElement();
    protected string Name { get; }
    private By Locator { get; }


    protected WebElement(By locator, string name)
    {
        Name = name;
        Locator = locator;
    }

    private IWebElement FindElement()
    {
        LogHelper.Logger.Info($"Trying to find {Name} element on the page");
        var elements = Application.Driver.FindElements(Locator);
        if (elements.Any())
        {
            return elements[0];
        }
        
        throw new NoSuchElementException($"No elements with locator '{Locator}' were found");
    }

    public void ScrollToElement()
    {
        new Actions(Application.Driver)
            .ScrollToElement(Element)
            .Perform();
    }

    public bool IsVisible
    {
        get
        {
            try
            {
                return Element.Displayed;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }
    }

    public bool IsSelected
    {
        get
        {
            try
            {
                return Element.Selected;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }
    }

    public bool IsEnabled
    {
        get
        {
            try
            {
                return Element.Enabled;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }
    }

    public void Click()
    {
        LogHelper.Logger.Info($"Clicking on '{Name}' element");
        Element.Click();
    }

    public string GetAttribute(string attr)
    {
        LogHelper.Logger.Info($"Getting '{attr}' attribute for '{Name}' element");
        return Element.GetAttribute(attr);
    }

    public void Hover()
    {
        LogHelper.Logger.Info($"Hovering over '{Name}' element");
        ConditionalWait.WaitForCondition(() => IsVisible);
        var actions = new Actions(Application.Driver);
        actions.MoveToElement(FindElement()).Perform();
    }

    public void JsClick()
    {
        LogHelper.Logger.Info($"Performing click via JS on '{Name}' element");
        var executor = (IJavaScriptExecutor)Application.Driver;
        executor.ExecuteScript("arguments[0].click();", Element);
    }

    public string JsFindElementNextSiblingText()
    {
        LogHelper.Logger.Info($"Getting '{Name}' element's next sibling text content");
        var executor = (IJavaScriptExecutor)Application.Driver;
        return (string)executor.ExecuteScript("return arguments[0].nextSibling.textContent.trim()", Element);
    }

    public string GetTextContent()
    {
        LogHelper.Logger.Info($"Getting '{Name}' element text content");
        ConditionalWait.WaitForCondition(() => IsVisible);
        return Element.Text;
    }

    public int GetElementCount()
    {
        LogHelper.Logger.Info($"Getting '{Name}' element count");
        var elements = Application.Driver.FindElements(Locator);
        return elements.Count;
    }
}