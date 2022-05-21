using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WantApp.Models;
using Xamarin.Essentials;

namespace WantApp.Services
{
    public class OSRMRouteService
    {
        private readonly string baseRouteUrl = "https://router.project-osrm.org/route/v1/driving/";
        private HttpClient httpClient;

        public OSRMRouteService()
        {
            httpClient = new HttpClient();
        }

        public async Task<DirectionResponse> GetDirectionResponseAsync(Location startLocation, double[] endLocation)
        {
            /*var endLocations = await Geocoding.GetLocationsAsync(end);
            var endLocation = endLocations.FirstOrDefault();*/
            
            if (startLocation == null || endLocation == null) return null;

            if (startLocation != null && endLocation != null)
            {
                var url =  string.Format(baseRouteUrl) + $"{startLocation.Longitude},{startLocation.Latitude};" +
                             $"{endLocation[0]},{endLocation[1]}?overview=full&geometries=polyline&steps=false";
                var response = await httpClient.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    var jason = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<DirectionResponse>(jason);
                    return result;
                }
            }
            return null;
        }
    }
}