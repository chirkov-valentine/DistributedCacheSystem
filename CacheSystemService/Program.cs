using Autofac;
using Microsoft.Owin.Hosting;
using Topshelf;
//using Topshelf.Autofac;

namespace CacheSystemService
{
    class Program
    {
        static void Main(string[] args)
        {
            var baseUri = "http://localhost:8080";
            WebApp.Start<Startup>(baseUri);

            HostFactory.Run(host =>
            {
                // Pass it to Topshelf
                host.SetServiceName("CacheService"); //cannot contain spaces or / or \
                host.SetDisplayName("Служба распределенной системы кеширования");
                host.SetDescription("Служба распределенной системы кеширования.");
                host.StartAutomatically();
                
                host.Service<CacheService>(s =>
                {
                    s.ConstructUsing(name => new CacheService());
                    s.WhenStarted((service, control) => service.Start(control));
                    s.WhenStopped((service, control) => service.Stop(control));
                });
            });
        }
    }
}
