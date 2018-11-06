using MediatR;

namespace RegisterService.Application.Host.Commands.DeleteHost
{
    public class DeleteHostCommand : IRequest<bool>
    {
        public string Host { get; set; }
    }
}
