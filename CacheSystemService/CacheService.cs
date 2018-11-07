using CacheSystem.Infrastructure.RegisterService;
using Topshelf;

namespace CacheSystemService
{
    public class CacheService : ServiceControl
    {
        private readonly IRegisterServiceClient _serviceClient;

        public CacheService(IRegisterServiceClient serviceClient)
        {
            _serviceClient = serviceClient;
        }

        public bool Start(HostControl hostControl)
        {
            _serviceClient.Register();
            return true;
        }

        public bool Stop(HostControl hostControl)
        {
            _serviceClient.UnRegister();
            return true;
        }
    }
}
