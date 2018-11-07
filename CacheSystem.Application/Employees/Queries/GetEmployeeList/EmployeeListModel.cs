using CacheSystem.Application.Employees.Queries.GetEmployee;
using System.Collections.Generic;

namespace CacheSystem.Application.Employees.Queries.GetEmployeeList
{
    public class EmployeeListModel
    {
        public IList<EmployeeDto> Employees { get; set; }
    }
}
