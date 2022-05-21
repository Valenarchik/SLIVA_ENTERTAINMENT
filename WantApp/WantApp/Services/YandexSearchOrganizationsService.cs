using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WantApp.Models;
using Xamarin.Essentials;

namespace WantApp.Services
{
    public class YandexSearchOrganizationsService
    {
        private readonly string ApiKey = "ee063c4d-5157-40c4-88b7-32adf25f578a";
        private readonly HttpClient httpClient = new HttpClient();
        
        public async Task<YandexResponse> GetResponseAsync(string request)
        {
            if (request == null) return null;
            var url = $"https://search-maps.yandex.ru/v1/?text={request} 4а&type=biz&lang=ru_RU&results=1&apikey={ApiKey}";
            var response = await httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var jason = await response.Content.ReadAsStringAsync();
                var result =  JsonConvert.DeserializeObject<YandexResponse>(jason);
                return result;
            }

            return null;
        }
    }
}