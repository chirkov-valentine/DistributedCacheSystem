using CacheSystem.Common.Models;
using CacheSystem.Infrastructure.Models;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CacheSystem.Infrastructure.RegisterService
{
    public class RegisterServiceClient : IRegisterServiceClient
    {
        private readonly ICacheSystemSettings _settings;
        private HttpClient _httpClient;

        public RegisterServiceClient(ICacheSystemSettings settings)
        {
            _settings = settings;
            _httpClient = new HttpClient();
        }

        public async Task<HostListModel> GetAll()
        {
            var model = new HostListModel();
            _httpClient.BaseAddress = new Uri(_settings.RegisterServiceUrl);
            var response = await _httpClient.GetAsync(_settings.RegisterServiceUrl);
            return await response.Content.ReadAsAsync<HostListModel>();
        }

        public async Task<bool> Register()
        {
            var model = new HostModel { Host = _settings.UrlHost };

            _httpClient.BaseAddress = new Uri(_settings.RegisterServiceUrl);
            var response = await _httpClient.PostAsJsonAsync("register", model);
            return await response.Content.ReadAsAsync<bool>();
        }

        public async Task<bool> UnRegister()
        {
            return await UnRegister(_settings.UrlHost);
        }

        public async Task<bool> UnRegister(string serviceUrl)
        {
            var model = new HostModel { Host = serviceUrl };

            var request = new HttpRequestMessage(HttpMethod.Delete, "register");
            request.Content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
            var response = await _httpClient.SendAsync(request);
            return await response.Content.ReadAsAsync<bool>();

        }
    }
}
