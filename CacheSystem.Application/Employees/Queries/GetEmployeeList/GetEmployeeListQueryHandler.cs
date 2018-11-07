﻿using CacheSystem.Domain.Entities;
using CacheSystem.Persistance;
using MediatR;
using System.Linq;

namespace CacheSystem.Application.Employees.Queries.GetEmployeeList
{
    public class GetEmployeeListQueryHandler : RequestHandler<GetEmployeeListQuery, EmployeeListModel>
    {
        private readonly IRepository<Employee, int> _repository;

        public GetEmployeeListQueryHandler(IRepository<Employee, int> repository)
        {
            _repository = repository;
        }

        protected override EmployeeListModel Handle(GetEmployeeListQuery request)
        {
            var employeeListModel = new EmployeeListModel
            {
                Employees = _repository
                    .GetAll()
                    .Select(p => new EmployeeDto
                    {
                        Id = p.Key,
                        FirstName = p.Value.FirstName,
                        LastName = p.Value.LastName
                    }
                ).ToList()
            };
            return employeeListModel;
        }
    }
}
