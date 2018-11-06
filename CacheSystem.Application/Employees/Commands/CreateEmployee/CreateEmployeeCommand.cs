using MediatR;

namespace CacheSystem.Application.Employees.Commands.CreateEmployee
{
    public class CreateEmployeeCommand : IRequest<bool>
    {
        public int Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
    }
}
