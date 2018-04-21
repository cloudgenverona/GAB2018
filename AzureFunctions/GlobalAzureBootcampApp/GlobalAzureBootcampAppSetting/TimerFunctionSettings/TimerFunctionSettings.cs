using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;

namespace GlobalAzureBootcampAppSetting
{
    public static class TimerFunctionSettings
    {
        public static void Run(TimerInfo myTimer, TraceWriter log)
        {
            log.Info($"TimerFunctionSettings trigger function executed at: {DateTime.Now}");
        }
    }
}
