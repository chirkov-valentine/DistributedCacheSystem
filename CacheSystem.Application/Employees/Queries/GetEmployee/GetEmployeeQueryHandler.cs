using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using CacheSystem.Persistance;
using CacheSystem.Domain.Entities;

namespace CacheSystem.Application.Employees.Queries.GetEmployee
{
    public class GetEmployeeQueryHandler : RequestHandler<GetEmployeeQuery, EmployeeDto>
    {
        private readonly IRepository<Employee, int> _repository;

        public GetEmployeeQueryHandler(IRepository<Employee, int> repository)
        {
            _repository = repository;
        }

        protected override EmployeeDto Handle(GetEmployeeQuery request)
        {
            Employee e = null;
            try
            {
                e = _repository.Get(request.Id);
                return new EmployeeDto
                {
                    Id = request.Id,
                    FirstName = e.FirstName,
                    LastName = e.LastName
                };
            }
            catch
            {
            }
            return null;
        }
    }
}
