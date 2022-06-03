using Android.App;
using Android.Content;
using Android.OS;

namespace WantApp.Droid
{
    [Activity(MainLauncher = true, Theme = "@style/MyTheme.Splash", NoHistory = true)]
    public class SplashActivity : Activity
    {
        protected override void OnResume()
        {
            base.OnResume();
            StartActivity(new Intent(Application.Context, typeof(MainActivity)));
        }
    }
}