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
    class UpdateEmployeeCommandHandler : RequestHandler<UpdateEmployeeCommand, bool>
    {
        private readonly IRepository<Employee, int> _repository;

        public UpdateEmployeeCommandHandler(IRepository<Employee, int> repository)
        {
            _repository = repository;
        }

        protected override bool Handle(UpdateEmployeeCommand request)
        {
            var e = _repository.Get(request.Id);
            e.FirstName = request.FirstName;
            e.LastName = request.LastName;
            return true;
        }
    }
}
