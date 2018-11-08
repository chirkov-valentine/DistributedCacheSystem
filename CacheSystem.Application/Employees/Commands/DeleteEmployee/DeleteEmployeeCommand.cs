using MediatR;

namespace CacheSystem.Application.Employees.Commands.DeleteEmployee
{
    public class DeleteEmployeeCommand : IRequest<bool>
    {
        public string Id { get; set; }
    }
}
