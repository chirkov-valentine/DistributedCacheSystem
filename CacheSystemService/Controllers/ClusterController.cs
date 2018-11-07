using CacheSystem.Application.Employees.Commands.CreateEmployee;
using CacheSystem.Application.Employees.Commands.DeleteEmployee;
using CacheSystem.Application.Employees.Commands.UpdateEmployee;
using CacheSystem.Application.Employees.Queries.GetEmployee;
using CacheSystem.Application.Employees.Queries.GetEmployeeList;
using CacheSystem.Domain.Entities;
using MediatR;
using System.Threading.Tasks;
using System.Web.Http;

namespace CacheSystemService.Controllers
{
    public class ClusterController : ApiController
    {
        private IMediator _mediator;

        public ClusterController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<EmployeeListModel> GetAsync()
        {
            return await _mediator.Send(new GetEmployeeListQuery());
        }

        public async Task<EmployeeDto> GetAsync(int id)
        {
           return await _mediator.Send(new GetEmployeeQuery { Id = id });
        }

        public async Task<bool> PostAsync(int id, [FromBody] EmployeeDto employeeDto)
        {
            EmployeeDto e = await _mediator.Send(new GetEmployeeQuery { Id = id }); ;
           
            if (e == null)
                return await _mediator.Send(new CreateEmployeeCommand
                    {
                        Id = id,
                        FirstName = employeeDto.FirstName,
                        LastName = employeeDto.LastName
                    });
            else
                return await _mediator.Send(new UpdateEmployeeCommand
                {
                    Id = id,
                    FirstName = employeeDto.FirstName,
                    LastName = employeeDto.LastName
                });
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _mediator.Send(new DeleteEmployeeCommand { Id = id });
        }

    }
}
