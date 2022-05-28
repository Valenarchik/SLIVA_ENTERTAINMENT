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

        private async void FindButton_OnClicked(object sender, EventArgs e)
        {
            var button = (ImageButton)sender;
            var favoritesElement = (FavoritesElement)button.BindingContext;
            favoritesElement.Response.Clear();
            favoritesElement.Request = await DisplayPromptAsync("Question 1", "What's your name?");
            if(favoritesElement.Request is null || favoritesElement.Request.Length==0)
                return;
            ((Button) ((Grid) button.Parent).Children[0]).Text = favoritesElement.Request;
            var response = await YandexSearchOrganizationsService
                .GetResponseAsync(favoritesElement.Request, routeViewModel.Start, new Size(0.5, 0.5));
            
            favoritesElement.Response = response.Features
                .Select(x => new Position(x.Geometry.Coordinates[1], x.Geometry.Coordinates[0]))
                .OrderBy(x=>Model.GetDistance(x,routeViewModel.Start))
                .ToList();
        }
        
        private async void Button_OnClicked(object sender, EventArgs e)
        {
            var button = (Button)sender;
            var favoritesElement = (FavoritesElement)button.BindingContext;
            if( favoritesElement.Request is null || favoritesElement.Response.Count == 0)
                return;
          
            await routeViewModel
                .LoadRouteAsync(routeViewModel.Start,favoritesElement.Response[favoritesElement.CurrentResponse]);
            favoritesElement.CurrentResponse++;
            if (Parent is TabbedApp parentPage) parentPage.CurrentPage = parentPage.Children[2];
        }
    }
}