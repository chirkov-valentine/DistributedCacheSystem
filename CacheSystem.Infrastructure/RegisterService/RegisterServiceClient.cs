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

        public RegisterServiceClient(ICacheSystemSettings settings)
        {
            _settings = settings;         
        }

        public async Task<HostListModel> GetAll()
        {
            var model = new HostListModel();
            var httpClient = Initialize();
            var response = await httpClient.GetAsync("register");
            return await response.Content.ReadAsAsync<HostListModel>();
        }

        public async Task<bool> Register()
        {
            var model = new HostModel { Host = _settings.UrlHost };
            var httpClient = Initialize();
            
            var response = await httpClient.PostAsJsonAsync("register", model);
            return await response.Content.ReadAsAsync<bool>();
        }

        public async Task<bool> UnRegister()
        {
            return await UnRegister(_settings.UrlHost);
        }

        public async Task<bool> UnRegister(string serviceUrl)
        {
            var model = new HostModel { Host = serviceUrl };

            var httpClient = Initialize();

            var request = new HttpRequestMessage(HttpMethod.Delete, "register");
            request.Content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
            var response = await httpClient.SendAsync(request);
            return await response.Content.ReadAsAsync<bool>();

        }

        private HttpClient Initialize()
        {
            var result = new HttpClient();
            result.BaseAddress = new Uri(_settings.RegisterServiceUrl);
            return result;
        }
    }
}
