using CacheSystem.Application.Employees.Queries.GetEmployee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace CacheSystemService.Controllers
{
    public class DataController : BaseController
    {

        Task<EmployeeDto> Get(int id)
        {
            return _mediator.Send(new GetEmployeeQuery());
        }
    }
}
