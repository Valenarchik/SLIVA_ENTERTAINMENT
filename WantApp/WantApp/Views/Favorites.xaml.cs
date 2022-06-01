using System;
using System.Linq;
using System.Security;
using WantApp.Models;
using WantApp.Services;
using WantApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Maps;


namespace WantApp.Views
{
    public partial class Favorites : ContentPage
    {
        private readonly FavoritesViewModel viewModel = new FavoritesViewModel();
        private readonly RouteViewModel routeViewModel = Intenface.routeViewModel;
        public Favorites()
        {
            InitializeComponent();
            BindingContext = viewModel;
        }
        
        private void NextRouteButton_OnClicked(object sender, EventArgs e)
        {
            if (Parent is TabbedApp parentPage) parentPage.CurrentPage = parentPage.Children[2];
        }
    }
}