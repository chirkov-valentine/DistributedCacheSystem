using MediatR;
using RegisterService.Persistance;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace RegisterService.Application.Host.Commands.DeleteHost
{
    public class DeleteHostCommandHandler : RequestHandler<DeleteHostCommand, bool>
    {
        private readonly HostsList _hosts;

        public DeleteHostCommandHandler(HostsList hosts)
        {
            _hosts = hosts;
        }

        protected override bool Handle(DeleteHostCommand request)
        {
            if (_hosts.Exists(h => h == request.Host))
            {
                _hosts.Remove(request.Host);
                return true;
            }
            return false;
        }
    }
}
