using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WantApp.Models;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace WantApp.Services
{
    public static class YandexSearchOrganizationsService
    {
        private static readonly string ApiKey = "0d497b8a-0ccb-4c09-a48d-50b5cf234229";
        private static readonly HttpClient httpClient = new HttpClient();

        public static async Task<YandexResponse> GetResponseAsync(string request, Position centre, Size areaSize)
        {
            if (request == null) return null;
            var url = $"https://search-maps.yandex.ru/v1/" +
                      $"?text={request}" +
                      "&type=biz" +
                      $"&apikey={ApiKey}" +
                      "&lang=ru_RU" +
                      $"&ll={centre.Longitude},{centre.Latitude}" +
                      $"&spn={areaSize.Width},{areaSize.Height}";
            var response = await httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var jason = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<YandexResponse>(jason);
                return result;
            }

            return null;
        }
    }
}