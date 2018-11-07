using System.Threading.Tasks;

namespace CacheSystem.Infrastructure.RegisterService
{
    public interface IRegisterServiceClient
    {
        Task<bool> Register();
        Task<bool> UnRegister();
    }
}
