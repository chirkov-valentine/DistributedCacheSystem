using CacheSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CacheSystem.Infrastructure.CacheService
{
    public interface ICacheServiceClient
    {
        /// <summary>
        /// Поиск по ключу в других кластерах кеша.
        /// </summary>
        /// <param name="key">Ключ.</param>
        /// <returns>Данные.</returns>
        Task<EmployeeDto> GetFirst(string key);
        /// <summary>
        /// Обновление там, где найдено.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<bool> Post(string key, EmployeeDto employeeDto);
        /// <summary>
        /// Удаление там, где найдено.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<bool> Delete(string key);
    }
}
