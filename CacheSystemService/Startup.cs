using Autofac;
using Autofac.Integration.WebApi;
using CacheSystem.Application.Employees.Queries.GetEmployee;
using CacheSystem.Persistance;
using CacheSystem.PersistanceInMemory;
using MediatR;
using Owin;
using System.Collections.Generic;
using System.Reflection;
using System.Web.Http;

namespace CacheSystemService
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var builder = new ContainerBuilder();
            // Register dependencies, then...
            builder.RegisterGeneric(typeof(GenericRepository<,>))
                .As(typeof(IRepository<,>))
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
                typeof(INotificationHandler<>),
               // typeof(IRequest<>)
            };

            foreach (var mediatrOpenType in mediatrOpenTypes)
            {
                builder
                    .RegisterAssemblyTypes(typeof(EmployeeDto).Assembly)
                    .AsClosedTypesOf(mediatrOpenType)
                    .AsImplementedInterfaces();
            }

            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            
            var container = builder.Build();
            var webApiConfiguration = ConfigureWebApi();
            webApiConfiguration.DependencyResolver = new AutofacWebApiDependencyResolver(container);

            app.UseAutofacMiddleware(container);
            app.UseAutofacWebApi(webApiConfiguration);
            app.UseWebApi(webApiConfiguration);
        }

        private HttpConfiguration ConfigureWebApi()
        {
            var config = new HttpConfiguration();
            config.Routes.MapHttpRoute(
                "DefaultApi",
                "{controller}/{id}",
                new { id = RouteParameter.Optional });
            return config;
        }
    }
}
