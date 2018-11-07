using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CacheSystem.Infrastructure
{
    public interface ICacheSystemSettings
    {
        string UrlHost { get; set; }
        string RegisterServiceUrl { get; set; }
    }
}
