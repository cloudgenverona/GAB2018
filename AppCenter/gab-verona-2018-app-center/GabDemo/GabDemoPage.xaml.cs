using System;
using System.Collections.Generic;
using Microsoft.AppCenter.Analytics;
using Xamarin.Forms;

namespace GabDemo
{
    public partial class GabDemoPage : ContentPage
    {
        public GabDemoPage()
        {
            InitializeComponent();
        }

        static int counter = 0;

        void OnSendEvent(object sender, EventArgs e)
        {
            Analytics.TrackEvent("SendEvent",
                new Dictionary<string, string>() { { "counter", (counter++).ToString() } });
        }

        void OnCrash(object sender, EventArgs e)
        {
            int n = 0;
            var results = 1 / n;
        }

        void OnHandledException(object sender, EventArgs e)
        {
            try
            {
                string msg = null;
                msg.IndexOf(".");
            }
            catch (Exception ex)
            {
                // Analytics.TrackEvent("Exception", new Dictionary<string, string> { { "Stacktrace", ex.StackTrace } });
                ex.Report();
            }
        }

        async void NavigateToTestPage(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TestPage());
        }
    }
}