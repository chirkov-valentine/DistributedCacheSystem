using System.Threading;
using System.Threading.Tasks;
using CacheSystem.Domain.Entities;
using CacheSystem.Persistance;
using MediatR;

namespace CacheSystem.Application.Employees.Commands.DeleteEmployee
{
    public class DeleteEmployeeCommandHandler : IRequestHandler<DeleteEmployeeCommand, Unit>
    {
        private readonly IRepository<Employee, int> _repository;

        public DeleteEmployeeCommandHandler(IRepository<Employee, int> repository)
        {
            _repository = repository;
        }
        public async Task<Unit> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
        {
            _repository.Delete(request.EmployeeId);
            return await Unit.Task;
        }
    }
}
