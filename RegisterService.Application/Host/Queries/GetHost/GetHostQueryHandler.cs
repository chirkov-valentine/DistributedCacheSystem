using MediatR;
using RegisterService.Persistance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegisterService.Application.Host.Queries.GetHost
{
    public class GetHostQueryHandler : RequestHandler<GetHostQuery, string>
    {
        private readonly HostsList _hosts;

        public GetHostQueryHandler(HostsList hosts)
        {
            _hosts = hosts;
        }

        protected override string Handle(GetHostQuery request)
        {
            return _hosts.FirstOrDefault(h => h == request.Host);
        }
    }
}
