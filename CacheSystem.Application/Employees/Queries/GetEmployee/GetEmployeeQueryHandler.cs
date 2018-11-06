using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using CacheSystem.Persistance;
using CacheSystem.Domain.Entities;

namespace CacheSystem.Application.Employees.Queries.GetEmployee
{
    public class GetEmployeeQueryHandler : IRequestHandler<GetEmployeeQuery, EmployeeDto>
    {
        private readonly IRepository<Employee, int> _repository;

        public GetEmployeeQueryHandler(IRepository<Employee, int> repository)
        {
            _repository = repository;
        }

        public async Task<EmployeeDto> Handle(GetEmployeeQuery request, CancellationToken cancellationToken)
        {
            var e = _repository.Get(request.EmployeeId);
            return new EmployeeDto
            {
                EmployeeId = e.EmployeeId,
                FirstName = e.FirstName,
                LastName = e.LastName
            };
        }
    }
}
