using MediatR;
using RegisterService.Persistance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegisterService.Application.Host.Commands.CreateHost
{
    public class CreateHostCommandHandler : RequestHandler<CreateHostCommand, bool>
    {
        private readonly HostsList _hosts;

        public CreateHostCommandHandler(HostsList hosts)
        {
            _hosts = hosts;
        }

        protected override bool Handle(CreateHostCommand request)
        {
            if (!_hosts.Exists(h => h == request.Host))
            {
                _hosts.Add(request.Host);
                return true;
            }
            return false;
        }
    }
}
