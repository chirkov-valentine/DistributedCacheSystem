using Autofac;
using Autofac.Integration.WebApi;
using CacheSystem.Application.Employees.Queries.GetEmployee;
using CacheSystem.Infrastructure;
using CacheSystem.Infrastructure.RegisterService;
using CacheSystem.Persistance;
using CacheSystem.PersistanceInMemory;
using MediatR;
using Microsoft.Owin.Hosting;
using Owin;
using System;
using System.Configuration;
using System.Reflection;
using System.Web.Http;
using Topshelf;
using Topshelf.Autofac;

namespace CacheSystemService
{
    class Program
    {
        /// <summary>
        /// Подготовка контейнера Autofac.
        /// </summary>
        /// <returns></returns>
        static IContainer InitializeServices()
        {
            var builder = new ContainerBuilder();

            // Repository
            builder.RegisterGeneric(typeof(GenericRepository<,>))
                .As(typeof(IRepository<,>))
                .SingleInstance();

            // Settings
            builder.RegisterType<CacheSystemSettings>()
                .As<ICacheSystemSettings>()
                .SingleInstance();

            builder.RegisterType<RegisterServiceClient>()
                .As<IRegisterServiceClient>();

            builder.RegisterType<CacheService>();

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
                    .RegisterAssemblyTypes(typeof(EmployeeDto).Assembly)
                    .AsClosedTypesOf(mediatrOpenType)
                    .AsImplementedInterfaces();
            }

            //WebApi
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            return builder.Build();
        }

        static ICacheSystemSettings InitializeSettings(IContainer container)
        {
            var settings = container.Resolve<ICacheSystemSettings>();
            settings.UrlHost = ConfigurationManager.AppSettings["hostUrl"].ToString();
            settings.RegisterServiceUrl = ConfigurationManager.AppSettings["registerUrl"].ToString();
            return settings;
        }

        static void Main(string[] args)
        {

            var baseUri = ConfigurationManager.AppSettings["hostUrl"].ToString();
            var container = InitializeServices();

            var settings = InitializeSettings(container);

            using (WebApp.Start(settings.UrlHost, appBuilder =>
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
                    host.UseAutofacContainer(container);
                    host.SetServiceName("CacheService");
                    host.SetDisplayName("Служба распределенной системы кеширования");
                    host.SetDescription("Служба распределенной системы кеширования.");
                    host.StartAutomatically();

                    host.Service<CacheService>(s =>
                    {
                        s.ConstructUsingAutofacContainer();
                        s.WhenStarted((service, control) => service.Start(control));
                        s.WhenStopped((service, control) => service.Stop(control));

                    });
                });
            }
        }
    }
}
