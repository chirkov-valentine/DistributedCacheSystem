using MediatR;
using RegisterService.Application.Host.Commands.CreateHost;
using RegisterService.Application.Host.Commands.DeleteHost;
using RegisterService.Application.Host.Queries.GetHost;
using RegisterService.Application.Host.Queries.GetHostsList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace RegisterService.Controllers
{
    public class RegisterController : ApiController
    {
        private readonly IMediator _mediator;

        public RegisterController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<HostListModel> GetAsync()
        {
            return await _mediator.Send(new GetHostsListQuery());      
        }

        public async Task<string> GetAsync(string host)
        {
            return await _mediator.Send(new GetHostQuery { Host = host });
        }

        public async Task<bool> PostAsync([FromBody]CreateHostCommand command)
        {
            return await _mediator.Send(command);
        }

        public async Task<bool> DeleteAsync([FromBody]DeleteHostCommand command)
        {
            return await _mediator.Send(command);
        }

    }
}
