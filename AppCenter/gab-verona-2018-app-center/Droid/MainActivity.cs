using Android.App;
using Android.Content.PM;
using Android.OS;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Push;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Microsoft.AppCenter.Distribute;

namespace GabDemo.Droid
{
    [Activity(Label = "GabDemo.Droid", Icon = "@drawable/icon", Theme = "@style/MyTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            InitializeAppCenter();

            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);

            LoadApplication(new App());
        }

        private void InitializeAppCenter()
        {
            // customize push
            Push.SetSenderId("{sender-id}"); 
            Push.EnableFirebaseAnalytics();
            Push.SetEnabledAsync(true);

            // set the log level
            AppCenter.LogLevel = LogLevel.Verbose;

            // start the service
            AppCenter.Start("{app-center-id}",
                            typeof(Analytics), 
                            typeof(Crashes), 
                            typeof(Distribute),
                            typeof(Push));
        }
    }
}