using System.Diagnostics;

namespace CRM.Automation.Framework.Waits;

public static class ConditionalWait
{ 
    private const int DefaultWaitTimeInSeconds = 30;
    private const int DefaultCheckIntervalInSeconds = 1;
    
    public static void WaitForCondition(Func<bool> condition, int? timeoutInSeconds = null,
        int? checkIntervalInSeconds = null)
    {
        var timeout = timeoutInSeconds ?? DefaultWaitTimeInSeconds;
        var checkInterval = (checkIntervalInSeconds ?? DefaultCheckIntervalInSeconds) * 1000;
        var watch = Stopwatch.StartNew();
        while (watch.Elapsed.TotalSeconds < timeout)
        {
            if (condition())
            {
                return;
            }
            Thread.Sleep(checkInterval);
        }
        throw new TimeoutException($"Condition was not met within the timeout of {timeoutInSeconds} seconds");
    }
}