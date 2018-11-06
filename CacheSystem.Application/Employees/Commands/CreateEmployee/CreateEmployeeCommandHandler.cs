using CacheSystem.Domain.Entities;
using CacheSystem.Persistance;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CacheSystem.Application.Employees.Commands.CreateEmployee
{
    public class CreateEmployeeCommandHandler : RequestHandler<CreateEmployeeCommand, bool>
    {
        private readonly IRepository<Employee, int> _repository;

        public CreateEmployeeCommandHandler(IRepository<Employee, int> repository)
        {
            _repository = repository;
        }
        protected override bool Handle(CreateEmployeeCommand request)
        {
            var employee = new Employee
            {
                FirstName = request.FirstName,
                LastName = request.LastName
            };
            _repository.Create(employee, request.Id);
            return true;
        }
    }
}
