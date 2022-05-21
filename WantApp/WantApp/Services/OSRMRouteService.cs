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
    public class OSRMRouteService
    {
        private readonly string baseRouteUrl = "https://router.project-osrm.org/route/v1/foot/";
        private HttpClient httpClient;

        public OSRMRouteService()
        {
            httpClient = new HttpClient();
        }

        public async Task<DirectionResponse> GetDirectionResponseAsync(Position startLocation, Position endLocation)
        {
            var url = baseRouteUrl + $"{startLocation.Longitude},{startLocation.Latitude};" +
                      $"{endLocation.Longitude},{endLocation.Latitude}" +
                      $"?overview=full&geometries=polyline&steps=false";
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