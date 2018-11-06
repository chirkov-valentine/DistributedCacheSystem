using MediatR;
using Topshelf;

namespace CacheSystemService
{
    public class CacheService : ServiceControl
    {
        protected IMediator _m;
        public bool Start(HostControl hostControl)
        {
            return true;
        }

        public bool Stop(HostControl hostControl)
        {
            return true;
        }
    }
}
