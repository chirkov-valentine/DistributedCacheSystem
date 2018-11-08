using CacheSystem.Domain.Entities;
using MediatR;

namespace CacheSystem.Application.Employees.Queries.GetEmployee
{
    public class GetEmployeeQuery : IRequest<EmployeeDto>
    {
        public string Id { get; set; }
    }
}
