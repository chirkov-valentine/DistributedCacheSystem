using Autofac;
using Autofac.Integration.WebApi;
using MediatR;
using Microsoft.Owin.Hosting;
using Owin;
using RegisterService.Application.Host.Queries.GetHost;
using RegisterService.Persistance;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Topshelf;

namespace RegisterService
{
    class Program
    {
        static IContainer InitializeServices()
        {
            var builder = new ContainerBuilder();

            builder
                .RegisterType<HostsList>()
                .AsSelf()
                .SingleInstance();

            // MediatR
            builder
              .RegisterType<Mediator>()
              .As<IMediator>()
              .InstancePerLifetimeScope();

            builder.Register<ServiceFactory>(context =>
            {
                var c = context.Resolve<IComponentContext>();
                return t => c.Resolve(t);
            });

            var mediatrOpenTypes = new[]
            {
                typeof(IRequestHandler<,>),
                typeof(INotificationHandler<>)
            };

            foreach (var mediatrOpenType in mediatrOpenTypes)
            {
                builder
                    .RegisterAssemblyTypes(typeof(GetHostQuery).Assembly)
                    .AsClosedTypesOf(mediatrOpenType)
                    .AsImplementedInterfaces();
            }

            //WebApi
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            return builder.Build();
        }

        static void Main(string[] args)
        {
            var baseUri = ConfigurationManager.AppSettings["hostUrl"].ToString();
            var container = InitializeServices();

            using (WebApp.Start(baseUri, appBuilder =>
                {
                    var config = new HttpConfiguration();
                    config.Routes.MapHttpRoute(
                        "DefaultApi",
                        "{controller}/{id}",
                        new { id = RouteParameter.Optional });

                    config.DependencyResolver = new AutofacWebApiDependencyResolver(container);

                    appBuilder.UseAutofacMiddleware(container);
                    appBuilder.UseAutofacWebApi(config);
                    appBuilder.UseWebApi(config);

                }))
            {
                HostFactory.Run(host =>
                {
                    
                    host.SetServiceName("RegisterService");
                    host.SetDisplayName("Служба регистрации системы кеширования");
                    host.SetDescription("Служба регистрации системы кеширования.");
                    host.StartAutomatically();

                    host.Service<RegisterCacheService>(s =>
                    {
                        s.ConstructUsing(name => new RegisterCacheService());
                        s.WhenStarted((service, control) => service.Start(control));
                        s.WhenStopped((service, control) => service.Stop(control));
                    });

                });
            }
        }
    }
}
