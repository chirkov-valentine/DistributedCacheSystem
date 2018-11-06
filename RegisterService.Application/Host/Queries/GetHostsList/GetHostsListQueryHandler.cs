using MediatR;
using System;
using RegisterService.Persistance;
using System.Linq;

namespace RegisterService.Application.Host.Queries.GetHostsList
{
    public class GetHostsListQueryHandler : RequestHandler<GetHostsListQuery, HostListModel>
    {
        private readonly HostsList _hosts;

        public GetHostsListQueryHandler(HostsList hosts)
        {
            _hosts = hosts;
        }

        protected override HostListModel Handle(GetHostsListQuery request)
        {
            var vModel = new HostListModel
            {
                Hosts = _hosts.Select(s => s).ToList()
            };
            return vModel;
        }
    }
}
