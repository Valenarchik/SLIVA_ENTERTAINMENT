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

        public Command GetRouteCommand { get; }
        private MapBoxRouteService osmrRouteService = new MapBoxRouteService();

        private YandexSearchOrganizationsService yandexSearchOrganizationsService =
            new YandexSearchOrganizationsService();
        

        public RouteViewModel()
        {
            SetCurrentLocation();
            GetRouteCommand = new Command(async () => await ExecuteRequestAsync());
        }

        private async void SetCurrentLocation()
        {
            var loc = await Geolocation.GetLocationAsync(new GeolocationRequest(GeolocationAccuracy.Medium));
            Start = new Position(loc.Latitude, loc.Longitude);
        }


        public async Task ExecuteRequestAsync()
        {
            if (request is null)
            {
                await DisplayAlert("Ошибка:", "Укажите место назначения.", "ОК");
                return;
            }

            var answer = await yandexSearchOrganizationsService.GetResponseAsync(request,start,new Size(0.5,0.5));
            if (answer is null)
            {
                await DisplayAlert("Ошибка:", "Не удалось обработать запрос.", "ОК");
                return;
            }

            var end = answer.Features.First().Geometry.Coordinates;
            var endPosition = new Position(end[1], end[0]);
            await LoadRouteAsync(start, endPosition);
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

            var direction = await osmrRouteService.GetDirectionResponseAsync(startPosition, endPosition);
            if (direction == null)
            {
                await DisplayAlert("Ошибка:", "Не удалось найти маршрут.", "ОК");
                return;
            }

            var route = direction.Routes.First();
            RouteDuration = Math.Round(route.Duration/60, 0);
            RouteDistance = Math.Round(route.Distance/1000, 1);
            var locations = Decode(route.Geometry);
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

            var dist = Direction.GetDistance(startPosition, endPosition);
            var mapSpan = MapSpan.FromCenterAndRadius(
                startPosition,
                Distance.FromKilometers(dist));
            Map.MoveToRegion(mapSpan);

            var polyline = new Polyline()
            {
                StrokeWidth = 4,
                StrokeColor = Color.FromHex("#1BA1E2")
            };
            polyline.Geopath.Add(startPosition);
            foreach (var location in locations)
                polyline.Geopath.Add(location);
            polyline.Geopath.Add(endPosition);
            Map.MapElements.Add(polyline);
        }

        public static IEnumerable<Position> Decode(string encodedPoints)
        {
            if (string.IsNullOrEmpty(encodedPoints))
                throw new ArgumentNullException("encodedPoints");

            char[] polylineChars = encodedPoints.ToCharArray();
            int index = 0;

            int currentLat = 0;
            int currentLng = 0;
            int next5bits;
            int sum;
            int shifter;

            while (index < polylineChars.Length)
            {
                // calculate next latitude
                sum = 0;
                shifter = 0;
                do
                {
                    next5bits = (int)polylineChars[index++] - 63;
                    sum |= (next5bits & 31) << shifter;
                    shifter += 5;
                } while (next5bits >= 32 && index < polylineChars.Length);

                if (index >= polylineChars.Length)
                    break;

                currentLat += (sum & 1) == 1 ? ~(sum >> 1) : (sum >> 1);

                //calculate next longitude
                sum = 0;
                shifter = 0;
                do
                {
                    next5bits = (int)polylineChars[index++] - 63;
                    sum |= (next5bits & 31) << shifter;
                    shifter += 5;
                } while (next5bits >= 32 && index < polylineChars.Length);

                if (index >= polylineChars.Length && next5bits >= 32)
                    break;

                currentLng += (sum & 1) == 1 ? ~(sum >> 1) : (sum >> 1);

                yield return new Position(Convert.ToDouble(currentLat) / 1E5, Convert.ToDouble(currentLng) / 1E5);
            }
        }
    }
}