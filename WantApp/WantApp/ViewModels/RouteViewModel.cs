using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MvvmHelpers;
using WantApp.Models;
using WantApp.Services;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Map = Xamarin.Forms.Maps.Map;

namespace WantApp.ViewModels
{
    public class RouteViewModel : DisplayAlertViewModel
    {
        public static Map Map = new Map();
        private Position start;
        public Position Start
        {
            get => start;
            set
            {
                start = value;
                OnPropertyChanged();
            }
        }
        private string request;
        public string Request
        {
            get => request;
            set
            {
                request = value;
                OnPropertyChanged();
            }
        }
        private double routeDistance;
        public double RouteDistance
        {
            get => routeDistance;
            set
            {
                routeDistance = value;
                OnPropertyChanged();
            }
        }
        private double routeDuration;
        public double RouteDuration
        {
            get => routeDuration;
            set
            {
                routeDuration = value;
                OnPropertyChanged();
            }
        }
        public RouteViewModel()
        {
            SetCurrentLocation();
        } 
        private async void SetCurrentLocation()
        {
            var loc = await Geolocation.GetLocationAsync(new GeolocationRequest(GeolocationAccuracy.Medium));
            Start = new Position(loc.Latitude, loc.Longitude);
        }

        public async Task LoadRouteAsync(Position startPosition, Position endPosition)
        {
            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                await DisplayAlert("Ошибка:", "Нет подключения к Интернету.", "ОК");
                return;
            }
            Map.MapElements.Clear();
            Map.Pins.Clear();

            var direction = await MapBoxRouteService.GetDirectionResponseAsync(startPosition, endPosition);
            if (direction == null)
            {
                await DisplayAlert("Ошибка:", "Не удалось найти маршрут.", "ОК");
                return;
            }

            var route = direction.Routes.First();
            RouteDuration = Math.Round(route.Duration/60, 0);
            RouteDistance = Math.Round(route.Distance, 1);
            var locations = Model.Decode(route.Geometry);
            var startPin = new Pin
            {
                Label = "You",
                Type = PinType.Place,
                Position = startPosition
            };
            Map.Pins.Add(startPin);

            var endPin = new Pin
            {
                Label = "End point",
                Type = PinType.Place,
                Position = endPosition
            };
            Map.Pins.Add(endPin);

            var dist = Model.GetDistance(startPosition, endPosition);
            var mapSpan = MapSpan.FromCenterAndRadius(
                startPosition,
                Distance.FromKilometers(dist));
            Map.MoveToRegion(mapSpan);

            var polyline = new Polyline()
            {
                StrokeWidth = 8,
                StrokeColor = Color.FromHex("#1BA1E2")
            };
            polyline.Geopath.Add(startPosition);
            foreach (var location in locations)
                polyline.Geopath.Add(location);
            polyline.Geopath.Add(endPosition);
            Map.MapElements.Add(polyline);
        }
    }
}