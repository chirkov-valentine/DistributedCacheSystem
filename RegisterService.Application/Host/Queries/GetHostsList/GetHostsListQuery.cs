using MediatR;

namespace RegisterService.Application.Host.Queries.GetHostsList
{
    public class GetHostsListQuery : IRequest<HostListModel>
    {
    }
}
