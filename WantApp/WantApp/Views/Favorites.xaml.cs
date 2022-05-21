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
    public partial class Favorites : ContentPage
    {
        public Favorites()
        {
            InitializeComponent();

            BindingContext = new FavoritesViewModel();  
        }

        private void Button_OnClicked(object sender, EventArgs e)
        {
            var tabbed = this.Parent as TabbedApp;
            if (tabbed != null) tabbed.CurrentPage = tabbed.Children[2];
        }
    }
}