using CacheSystem.Application.Employees.Queries.GetEmployee;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace CacheSystemService.Controllers
{
    public class DataController : ApiController
    {
        private IMediator _mediator;

        public DataController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public Task<EmployeeDto> GetAsync(int id)
        {
           return _mediator.Send(new GetEmployeeQuery());
        }
    }
}
