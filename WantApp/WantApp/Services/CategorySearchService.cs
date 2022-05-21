using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WantApp.Models;
using Xamarin.Essentials;

namespace WantApp.Services
{
    public class CategorySearchService
    {
        private readonly string baseSearchUrl = "https://search-maps.yandex.ru/v1/";
        private HttpClient httpClient;

        public CategorySearchService()
        {
            httpClient = new HttpClient();
        }

        public async Task<CategorySearchResponse> GetSearchResponseAsync(Location startLocation, string text)
        {
            if (startLocation == null) return null;

            if (startLocation != null)
            {
                var url = string.Format(baseSearchUrl) +
                          $"?apikey=0d497b8a-0ccb-4c09-a48d-50b5cf234229&text={text}&type=biz&lang=ru_RU&ll={startLocation.Longitude},{startLocation.Latitude}&spn=3.552069,2.400552";
                var response = await httpClient.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    var jason = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<CategorySearchResponse>(jason);
                    return result;
                }
            }

            return null;
        }
    }
}