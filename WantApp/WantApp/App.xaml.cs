using System;
using System.Globalization;
using WantApp.Services;
using WantApp.ThemesModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using WantApp.Views;
using Xamarin.Essentials;
using Xamarin.Forms.Maps;

namespace WantApp
{
    public partial class App : Application
    {
        public App()
        {
            CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;
            MainPage = new TabbedApp();
            InitializeComponent();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
