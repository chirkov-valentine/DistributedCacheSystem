using CacheSystem.Common.Models;
using CacheSystem.Domain.Entities;
using CacheSystem.Infrastructure.RegisterService;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace CacheSystem.Infrastructure.CacheService
{
    public class CacheServiceClient : ICacheServiceClient
    {
        private readonly ICacheSystemSettings _cacheSystemSettings;
        private readonly IRegisterServiceClient _registerServiceClient;

        public CacheServiceClient(
            ICacheSystemSettings cacheSystemSettings,
            IRegisterServiceClient registerServiceClient)
        {
            _cacheSystemSettings = cacheSystemSettings;
            _registerServiceClient = registerServiceClient;
        }

        public async Task<bool> Delete(int key)
        {
            var clients = await GetClients();

            foreach (var client in clients.Hosts)
            {
                try
                {
                    var e = await GetData(key, client);
                    if (e != null)
                        return await DeleteData(key, client);
                }
                catch (HttpRequestException ex)
                {
                    // Удаляем из списка зарегистрированных тот адрес, до которого не достучались
                    await _registerServiceClient.UnRegister(client);
                    throw ex;
                }
            }

            return false;
        }

        public async Task<EmployeeDto> GetFirst(int key)
        {
            var clients = await GetClients();

            foreach(var client in clients.Hosts)
            {
                try
                {
                    var e = await GetData(key, client);
                    if (e != null)
                        return e;
                }
                catch (HttpRequestException ex)
                {
                    // Удаляем из списка зарегистрированных тот адрес, до которого не достучались
                    await _registerServiceClient.UnRegister(client);
                    throw ex;
                }
            }
            // Ничего не нашли
            return null;
        }

        public async Task<bool> Post(int key, EmployeeDto employeeDto)
        {
            var clients = await GetClients();

            foreach (var client in clients.Hosts)
            {
                try
                {
                    var e = await GetData(key, client);
                    if (e != null)
                        return await PostData(key, client, employeeDto);
                }
                catch (HttpRequestException ex)
                {
                    // Удаляем из списка зарегистрированных тот адрес, до которого не достучались
                    await _registerServiceClient.UnRegister(client);
                    throw ex;
                }
            }
            return false;
        }

        private async Task<HostListModel> GetClients()
        {
            return await _registerServiceClient.GetAll();
        }

        private async Task<EmployeeDto> GetData(int key, string urlClient)
        {
            var path = $"cluster/{key}";
            var httpClient = Initialize(urlClient);
            EmployeeDto employeeDto = null;
            var response = await httpClient.GetAsync(path);
            // Вызываем исключение, если что не так
            response.EnsureSuccessStatusCode();

            employeeDto = await response.Content.ReadAsAsync<EmployeeDto>();

            return employeeDto;
        }

        private async Task<bool> PostData(int key, string urlClient, EmployeeDto employeeDto)
        {
            var path = $"cluster/{key}";
            var httpClient = Initialize(urlClient);

            var response = await httpClient.PostAsJsonAsync(path, employeeDto);
            // Вызываем исключение, если что не так
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsAsync<bool>();
        }

        private async Task<bool> DeleteData(int key, string urlClient)
        {
            var path = $"cluster/{key}";
            var httpClient = Initialize(urlClient);

            var response = await httpClient.DeleteAsync(path);
            // Вызываем исключение, если что не так
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsAsync<bool>();
        }

        private HttpClient Initialize(string urlClient)
        {
            var result = new HttpClient();
            result.BaseAddress = new Uri(urlClient);
            result.DefaultRequestHeaders.Accept.Clear();
            result.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            return result;
        }

    }
}
