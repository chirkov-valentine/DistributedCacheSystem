using MediatR;

namespace CacheSystem.Application.Employees.Queries.GetEmployeeList
{
    public class GetEmployeeListQuery : IRequest<EmployeeListModel>
    {
    }
}
