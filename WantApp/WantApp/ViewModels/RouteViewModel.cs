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
    public class RouteViewModel : MyViewModel
    {
        private Location start;
        
        public Location Start
        {
            get { return start; }
            set
            {
                start = value;
                OnPropertyChanged();
            }
        }

        private string end;

        public string End
        {
            get { return end; }
            set
            {
                end = value;
                OnPropertyChanged();
            }
        }

        private double routeDistance;

        public double RouteDistance
        {
            get { return routeDistance; }
            set
            {
                routeDistance = value;
                OnPropertyChanged();
            }
        }

        private double routeDuration;

        public double RouteDuration
        {
            get { return routeDuration; }
            set
            {
                routeDuration = value;
                OnPropertyChanged();
            }
        }

        private bool showRouteDetails;

        public bool ShowRouteDetails
        {
            get { return showRouteDetails; }
            set
            {
                showRouteDetails = value;
                OnPropertyChanged();
            }
        }

        public static Map map;
        public Command GetRouteCommand { get; }
        private OSRMRouteService service;
        private DirectionResponse dr;

        public RouteViewModel()
        {
            ShowRouteDetails = false;
            map = new Map();
            service = new OSRMRouteService();
            dr = new DirectionResponse();
            SetCurrentLocation();
            GetRouteCommand = new Command(async () => await loadRouteAsync(End));
        }

        private async void SetCurrentLocation()
        {
            var request = new GeolocationRequest(GeolocationAccuracy.Medium);
            Start = await Geolocation.GetLocationAsync(request);
        }

        public async Task loadRouteAsync(string end)
        {
            var current = Xamarin.Essentials.Connectivity.NetworkAccess;

            if (current != Xamarin.Essentials.NetworkAccess.Internet)
            {
                await DisplayAlert("Error:", "Lost connection to Internet.", "Ok");
                return;
            }

            if (start == null || end == null)
            {
                await DisplayAlert("Error:", "Start or end empty.", "Ok");
                return;
            }

            var routes = new List<Route>();
            var locations = new List<LatLong>();

            dr = await service.GetDirectionResponseAsync(start, end);
            if (dr != null)
            {
                ShowRouteDetails = false;
                await Task.Delay(1000);
                routes = dr.Routes.ToList();

                RouteDuration = Math.Round(routes[0].Duration / 60, 0);
                RouteDistance = Math.Round(routes[0].Distance / 1609, 1);

                locations = DecodePolylinePoints(routes[0].Geometry);
                var firstPinLocation = locations[0];
                var lastPinLocation = locations[locations.Count - 1];

                Pin startPin = new Pin
                {
                    Label = "You",
                    Type = PinType.Place,
                    Position = new Position(firstPinLocation.Lat, firstPinLocation.Lng)
                };
                map.Pins.Add(startPin);

                Pin endPin = new Pin
                {
                    Label = "End point",
                    Address = End,
                    Type = PinType.Place,
                    Position = new Position(lastPinLocation.Lat, lastPinLocation.Lng)
                };
                map.Pins.Add(endPin);

                var mapSpan = MapSpan.FromCenterAndRadius(new Position(firstPinLocation.Lat, firstPinLocation.Lng),
                    Distance.FromKilometers(5));
                map.MoveToRegion(mapSpan);
                
                var polyline = new Polyline()
                {
                    StrokeWidth = 4,
                    StrokeColor = Color.FromHex("#1BA1E2")
                };
                foreach (var location in locations)
                {
                    polyline.Geopath.Add(new Position(location.Lat, location.Lng));
                }
                map.MapElements.Add(polyline);
                
                ShowRouteDetails = true;
            }
        }

        private List<LatLong> DecodePolylinePoints(string encodedPoints)
        {
            if (encodedPoints == null || encodedPoints == "") return null;
            List<LatLong> poly = new List<LatLong>();
            char[] polylinechars = encodedPoints.ToCharArray();
            int index = 0;

            int currentLat = 0;
            int currentLng = 0;
            int next5bits;
            int sum;
            int shifter;

            try
            {
                while (index < polylinechars.Length)
                {
                    // calculate next latitude
                    sum = 0;
                    shifter = 0;
                    do
                    {
                        next5bits = (int) polylinechars[index++] - 63;
                        sum |= (next5bits & 31) << shifter;
                        shifter += 5;
                    } while (next5bits >= 32 && index < polylinechars.Length);

                    if (index >= polylinechars.Length)
                        break;

                    currentLat += (sum & 1) == 1 ? ~(sum >> 1) : (sum >> 1);

                    //calculate next longitude
                    sum = 0;
                    shifter = 0;
                    do
                    {
                        next5bits = (int) polylinechars[index++] - 63;
                        sum |= (next5bits & 31) << shifter;
                        shifter += 5;
                    } while (next5bits >= 32 && index < polylinechars.Length);

                    if (index >= polylinechars.Length && next5bits >= 32)
                        break;

                    currentLng += (sum & 1) == 1 ? ~(sum >> 1) : (sum >> 1);
                    var p = new LatLong();
                    p.Lat = Convert.ToDouble(currentLat) / 100000.0;
                    p.Lng = Convert.ToDouble(currentLng) / 100000.0;
                    poly.Add(p);
                }
            }
            catch (Exception ex)
            {
                // logo it
            }

            return poly;
        }
    }
}