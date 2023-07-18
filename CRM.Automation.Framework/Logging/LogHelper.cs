namespace CRM.Automation.Framework.Logging;

public static class LogHelper
{
    public static NLog.Logger Logger { get; } = NLog.LogManager.GetCurrentClassLogger();
}