using CacheSystem.Common.Models;
using CacheSystem.Domain.Entities;
using CacheSystem.Infrastructure.RegisterService;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace CacheSystem.Infrastructure.CacheService
{
    public class CacheServiceClient : ICacheServiceClient
    {
        private readonly ICacheSystemSettings _cacheSystemSettings;
        private readonly IRegisterServiceClient _registerServiceClient;
        private HttpClient _httpClient;

        public CacheServiceClient(
            ICacheSystemSettings cacheSystemSettings,
            IRegisterServiceClient registerServiceClient)
        {
            _cacheSystemSettings = cacheSystemSettings;
            _registerServiceClient = registerServiceClient;
            _httpClient = new HttpClient();
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
                    if (client != _cacheSystemSettings.UrlHost)
                    {
                        var e = await GetData(key, client);
                        if (e != null)
                            return await PostData(key, client, employeeDto);
                    }
                }
                catch (HttpRequestException ex)
                {
                    // Удаляем из списка зарегистрированных тот адрес, до которого не достучались
                    await _registerServiceClient.UnRegister(client);
                    throw ex;
                }
            }
            // В других кластерах нет, дабавляем/изменяем у себя
            return await PostData(key, _cacheSystemSettings.UrlHost, employeeDto); ;
        }

        private async Task<HostListModel> GetClients()
        {
            return await _registerServiceClient.GetAll();
        }

        private async Task<EmployeeDto> GetData(int key, string urlClient)
        {
            var path = $"cluster/{key}";

            EmployeeDto employeeDto = null;
            _httpClient.BaseAddress = new Uri(urlClient);
            var response = await _httpClient.GetAsync(path);
            // Вызываем исключение, если что не так
            response.EnsureSuccessStatusCode();

            employeeDto = await response.Content.ReadAsAsync<EmployeeDto>();

            return employeeDto;
        }

        private async Task<bool> PostData(int key, string urlClient, EmployeeDto employeeDto)
        {
            var path = $"cluster/{key}";
            
            _httpClient.BaseAddress = new Uri(urlClient);
            var response = await _httpClient.PostAsJsonAsync(path, employeeDto);
            // Вызываем исключение, если что не так
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsAsync<bool>();
        }

        private async Task<bool> DeleteData(int key, string urlClient)
        {
            var path = $"cluster/{key}";
            
            _httpClient.BaseAddress = new Uri(urlClient);
            var response = await _httpClient.DeleteAsync(path);
            // Вызываем исключение, если что не так
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsAsync<bool>();
        }

    }
}
