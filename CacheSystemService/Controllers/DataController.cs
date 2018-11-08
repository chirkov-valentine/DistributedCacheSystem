using CacheSystem.Application.Employees.Commands.CreateEmployee;
using CacheSystem.Domain.Entities;
using CacheSystem.Infrastructure.CacheService;
using MediatR;
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
        private readonly IMediator _mediator;

        public DataController(
            ICacheServiceClient cacheServiceClient,
            IMediator mediator)
        {
            _cacheServiceClient = cacheServiceClient;
            _mediator = mediator;
        }

        public async Task<EmployeeDto> GetAsync(string id)
        {
            return await _cacheServiceClient.GetFirst(id);
        }

        public async Task<bool> PostAsync(string id, [FromBody] EmployeeDto employeeDto)
        {
            var updatePost = await _cacheServiceClient.Post(id, employeeDto);
            if(!updatePost)
            {
                // Изменить нигде не смогли, поэтому создаем у себя
                return await _mediator.Send(
                    new CreateEmployeeCommand
                    {
                        Id = id,
                        FirstName = employeeDto.FirstName,
                        LastName = employeeDto.LastName
                    });
            }
            return updatePost;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            return await _cacheServiceClient.Delete(id);
        }
    }
}
