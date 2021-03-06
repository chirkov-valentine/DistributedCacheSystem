﻿using MediatR;

namespace CacheSystem.Application.Employees.Commands.UpdateEmployee
{
    public class UpdateEmployeeCommand : IRequest<bool>
    {
        public string Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
    }
}
