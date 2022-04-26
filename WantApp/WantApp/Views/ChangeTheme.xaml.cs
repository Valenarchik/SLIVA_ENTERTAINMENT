using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WantApp.Annotations;
using WantApp.Models;
using WantApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WantApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChangeTheme : ContentPage
    {
        public ChangeTheme()
        {
            InitializeComponent();
            BindingContext = Interface.Model;
        }

        private void ChangeThemeToBlack_OnClicked(object sender, EventArgs e)
        {
            Interface.Model.SetTheme(Themes.BlackTheme);
        }

        private void ChangeThemeToWhite_OnClicked(object sender, EventArgs e)
        {
            Interface.Model.SetTheme(Themes.WhiteTheme);
        }
    }
}