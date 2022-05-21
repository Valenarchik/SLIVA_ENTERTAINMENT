using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WantApp.Models;
using Xamarin.Essentials;
using Xamarin.Forms.Maps;

namespace WantApp.Services
{
    public class MapBoxRouteService
    {
        private readonly string baseRouteUrl = "https://api.mapbox.com/directions/v5/mapbox/walking/";

        private readonly string apiKey =
            "pk.eyJ1IjoidmFsZW5hcmNoaWsiLCJhIjoiY2wzZnhoMmwzMDNhZDNqcXU5d2Qwbm5zeCJ9.vpbpthWNYb9BYcxv_DVarw";
            
        private HttpClient httpClient;

        public MapBoxRouteService()
        {
            httpClient = new HttpClient();
        }

        public async Task<DirectionResponse> GetDirectionResponseAsync(Position startLocation, Position endLocation)
        {
            var url = baseRouteUrl +
                      $"{startLocation.Longitude},{startLocation.Latitude};" +
                      $"{endLocation.Longitude},{endLocation.Latitude}" +
                      $"?access_token={apiKey}" +
                      $"&overview=full" +
                      $"&geometries=polyline";
            var response = await httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var jason = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<DirectionResponse>(jason);
                return result;
            }

            return null;
        }
    }
}