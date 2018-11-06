using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace CacheSystemService.Controllers
{
    public class BaseController : ApiController
    {
        protected IMediator _mediator;
    }
}
