using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WantApp.Annotations;
using WantApp.ThemesModel;
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
        }

        private void ChangeThemeToBlack_OnClicked(object sender, EventArgs e)
        {
            var mergedDictionaries = Application.Current.Resources.MergedDictionaries;
            if (mergedDictionaries == null) return;
            mergedDictionaries.Clear();
            mergedDictionaries.Add(new DarkTheme());

        }

        private void ChangeThemeToWhite_OnClicked(object sender, EventArgs e)
        {
            var mergedDictionaries = Application.Current.Resources.MergedDictionaries;
            if (mergedDictionaries == null) return;
            mergedDictionaries.Clear();
            mergedDictionaries.Add(new LightTheme());
        }
    }
}