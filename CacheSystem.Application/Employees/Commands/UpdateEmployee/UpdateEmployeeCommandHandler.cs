using CacheSystem.Domain.Entities;
using CacheSystem.Persistance;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CacheSystem.Application.Employees.Commands.UpdateEmployee
{
    class UpdateEmployeeCommandHandler : IRequestHandler<UpdateEmployeeCommand, Unit>
    {
        private readonly IRepository<Employee, int> _repository;

        public UpdateEmployeeCommandHandler(IRepository<Employee, int> repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var e = _repository.Get(request.Id);
            e.FirstName = request.FirstName;
            e.LastName = request.LastName;
            return Unit.Value;
        }
    }
}
