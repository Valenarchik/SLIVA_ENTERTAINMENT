using System;
using WantApp.ThemesModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using WantApp.Views;

namespace WantApp
{
    public partial class App : Application
    {
        public App()
        {
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
