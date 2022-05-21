using System;
using System.Linq;
using WantApp.Services;
using WantApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Maps;


namespace WantApp.Views
{
    public partial class Favorites : ContentPage
    {
        private FavoritesViewModel viewModel = new FavoritesViewModel();
        public Favorites()
        {
            InitializeComponent();
            BindingContext = viewModel;
        }

        private async void Button_OnClicked(object sender, EventArgs e)
        {
            var routeViewModel = Intenface.routeViewModel;
            var button = (Button)sender;
            var favoritesElement = (FavoritesElement)button.BindingContext;
            if(button.Text == null) return;
            if (button.Text.Equals(favoritesElement.Request))
            { 
                await routeViewModel
                    .LoadRouteAsync(routeViewModel.Start,favoritesElement.Response[favoritesElement.CurrentResponse]);
                favoritesElement.CurrentResponse++;
                return;
            }
            favoritesElement.Request = button.Text;
            var response = await routeViewModel
                .yandexSearchOrganizationsService
                .GetResponseAsync(favoritesElement.Request, routeViewModel.Start, new Size(0.5, 0.5));
            favoritesElement.Response = response.Features
                .Select(x => new Position(x.Geometry.Coordinates[1], x.Geometry.Coordinates[0]))
                .ToList();
            await routeViewModel
                .LoadRouteAsync(routeViewModel.Start,favoritesElement.Response[favoritesElement.CurrentResponse]);
            favoritesElement.CurrentResponse++;
            var 
        }
    }
}