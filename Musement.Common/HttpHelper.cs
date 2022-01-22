using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Musement
{
    public static class HttpHelper
    {
        public static string ApiWeatherUrl(string key, string lat, string lon, int day) => $"forecast.json?key={key}&q={lat},{lon}&days={day}";
        public static string CityUrl(int cityId) => $"cities/{cityId}";

        public static async Task Post<T>(string basicUrl, string url, T contentValue)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(basicUrl);
                var content = new StringContent(JsonConvert.SerializeObject(contentValue), Encoding.UTF8, "application/json");
                var result = await client.PostAsync(url, content);
                result.EnsureSuccessStatusCode();
            }
        }

        public static async Task Put<T>(string basicUrl, string url, T stringValue)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(basicUrl);
                var content = new StringContent(JsonConvert.SerializeObject(stringValue), Encoding.UTF8, "application/json");
                var result = await client.PutAsync(url, content);
                result.EnsureSuccessStatusCode();
            }
        }

        public static async Task<T> Get<T>(string basicUrl, string url)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(basicUrl);
                var result = await client.GetAsync(url);
                result.EnsureSuccessStatusCode();
                string resultContentString = await result.Content.ReadAsStringAsync();
                T resultContent = JsonConvert.DeserializeObject<T>(resultContentString);
                return resultContent;
            }
        }

        public static async Task Delete(string basicUrl, string url)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(basicUrl);
                var result = await client.DeleteAsync(url);
                result.EnsureSuccessStatusCode();
            }
        }
    }
}