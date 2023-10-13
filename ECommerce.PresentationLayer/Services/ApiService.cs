using ECommerce.DtoLayer.Dtos.Employee;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text;

namespace ECommerce.PresentationLayer.Services
{
    public class ApiService : IApiService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ApiService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<List<T>> GetTableData<T>(string apiUrl)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync(apiUrl);

            if(responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<T>>(jsonData);
                return values;
            }

            return null;
        }

        public async Task<T> GetData<T>(string apiUrl) 
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync(apiUrl);

            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<T>(jsonData);
                return values;
            }
            return default(T);
        }

        public async Task<bool> AddData(string apiUrl, object T) 
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(T);
            StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var result = await client.PostAsync(apiUrl, content);
            if (result.IsSuccessStatusCode) 
            {
                return true;
            }
            return false;
        }

        public async Task<bool> GetNoContent(string apiUrl)
        {
            var client = _httpClientFactory.CreateClient();
            var result = await client.GetAsync(apiUrl);
            if (result.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }

    }
}
