using System.Threading;
using System.Threading.Tasks;
using CacheSystem.Domain.Entities;
using CacheSystem.Persistance;
using MediatR;

namespace CacheSystem.Application.Employees.Commands.DeleteEmployee
{
    public class DeleteEmployeeCommandHandler : RequestHandler<DeleteEmployeeCommand, bool>
    {
        private readonly IRepository<Employee, string> _repository;

        public DeleteEmployeeCommandHandler(IRepository<Employee, string> repository)
        {
            _repository = repository;
        }
        protected override bool Handle(DeleteEmployeeCommand request)
        {
            try
            {
                _repository.Delete(request.Id);
            }
            catch
            {
                return false;
            }
            return true;
        }
    }
}
