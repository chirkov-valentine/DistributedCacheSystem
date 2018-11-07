using MediatR;

namespace RegisterService.Application.Host.Queries.GetHost
{
    public class GetHostQuery : IRequest<string>
    {
        public string Host { get; set; }
    }
}
