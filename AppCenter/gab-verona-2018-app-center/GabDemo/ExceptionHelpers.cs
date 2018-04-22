using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;

namespace GabDemo
{
    static class ExceptionHelpers
    {
        // try to change this value to see what happens in App Center
        const bool shouldReportAnEvent = false;

        public static void Report(this Exception ex, [CallerMemberName] string caller = "")
        {
            if (!shouldReportAnEvent)
            {
                Crashes.TrackError(ex);
                return;
            }

            while (ex.InnerException != null)
            {
                ex = ex.InnerException;
            }

            Analytics.TrackEvent($"{ex.GetType().Name} ({caller})",
                    new Dictionary<string, string>()
                    {
                        { "message", ex.Message }
                    });
        }
    }
}