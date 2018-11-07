using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CacheSystem.Infrastructure.RegisterService
{
    public interface IRegisterServiceClient
    {
        bool Register(string urlHost);
        bool UnRegister(string urlHost);
    }
}
