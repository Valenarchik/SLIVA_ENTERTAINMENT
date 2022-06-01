using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using MvvmHelpers;
using WantApp.Models;
using WantApp.Services;
using WantApp.ViewModels;
using WantApp.Views;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace WantApp
{
    public class FavoriteElementViewModel: DisplayAlertViewModel
    {
        private string request = defaultRequest;
        private static string defaultRequest = "Введите запрос";
        public string Request
        {
            get => request;
            set
            {
                request = value;
                OnPropertyChanged(nameof(Request));
            }
        }
        
        public ICommand FindOrganizationCommand { get; private set; }
        public ICommand FindNextRouteCommand { get; private set; }

        public FavoriteElementViewModel()
        {
            FindNextRouteCommand = new Command(FindNextRoute);
            FindOrganizationCommand = new Command(FindOrganization);
        }

        public List<Position> Response { get; set; } = new List<Position>();

        private int currentResponse;
        public int CurrentResponse
        {
            get => currentResponse;
            set
            {
                if (value < 0 || value >= Response.Count)
                    currentResponse = 0;
                else
                    currentResponse = value;
            }
        }
        
        private async void FindOrganization()
        {
            Response.Clear();
            Request = await DisplayPromptAsync("Пожалуйста,", "Укажите место назначения");
            if(Request is null || Request.Length==0)
            {
                await DisplayAlert("Ошибка:", "Место назначения не указанно", "Ок");
                Request = defaultRequest;
                return;
            }
            var response = await YandexSearchOrganizationsService
                .GetResponseAsync(Request, Intenface.routeViewModel.Start, new Size(0.5,0.5));
            if(response is null)
            {
                await DisplayAlert("Ошибка:", "Не удалось обработать запрос.", "ОК");
                Request = defaultRequest;
                return;
            }
            Response = response.Features
                .Select(x => new Position(x.Geometry.Coordinates[1], x.Geometry.Coordinates[0]))
                .OrderBy(x=>Model.GetDistance(x,Intenface.routeViewModel.Start))
                .ToList();
        }

        private async void FindNextRoute()
        {
            if(Request is null || Response.Count == 0)
            {
                await DisplayAlert("Ошибка:", "Путь не найден.", "ОК");
                return;
            }
            await Intenface.routeViewModel
                .LoadRouteAsync(Intenface.routeViewModel.Start, Response[CurrentResponse]);
            CurrentResponse++;
        }
    }
}