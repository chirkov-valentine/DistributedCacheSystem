using MediatR;

namespace CacheSystem.Application.Employees.Queries.GetEmployee
{
    public class GetEmployeeQuery : IRequest<EmployeeDto>
    {
        public int EmployeeId { get; set; }
    }
}
