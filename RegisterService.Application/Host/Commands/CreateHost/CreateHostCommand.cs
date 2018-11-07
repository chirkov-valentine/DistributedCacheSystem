using MediatR;

namespace RegisterService.Application.Host.Commands.CreateHost
{
    public class CreateHostCommand : IRequest<bool>
    {
        public string Host { get; set; }
    }
}
