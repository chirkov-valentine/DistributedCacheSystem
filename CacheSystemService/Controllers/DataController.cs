using CacheSystem.Domain.Entities;
using CacheSystem.Infrastructure.CacheService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace CacheSystemService.Controllers
{
    public class DataController : ApiController
    {
        private readonly ICacheServiceClient _cacheServiceClient;

        public DataController(ICacheServiceClient cacheServiceClient)
        {
            _cacheServiceClient = cacheServiceClient;
        }

        public async Task<EmployeeDto> GetAsync(int id)
        {
            return await _cacheServiceClient.GetFirst(id);
        }

        public async Task<bool> PostAsync(int id, [FromBody] EmployeeDto employeeDto)
        {
            return await _cacheServiceClient.Post(id, employeeDto);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _cacheServiceClient.Delete(id);
        }
    }
}
