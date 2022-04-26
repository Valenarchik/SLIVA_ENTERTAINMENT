using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using WantApp.Views;

namespace WantApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainPage = new AppShell();
            BindingContext = Interface.Model;
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
