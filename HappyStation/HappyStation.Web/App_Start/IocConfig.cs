using System.Web.Mvc;

using Autofac;
using Autofac.Integration.Mvc;

using AutoMapper;

using HappyStation.Core.DatabaseContext;
using HappyStation.Core.Services.Implementations;
using HappyStation.Web.Services;
using HappyStation.Web.Settings;

using OAuth2;

namespace HappyStation.Web.App_Start
{
    public class IocConfig
    {
        public static IContainer Configure()
        {
            var builder = new ContainerBuilder();

            OnConfigure(builder);
            builder.RegisterControllers(typeof(MvcApplication).Assembly).InstancePerRequest();
            builder.RegisterAssemblyModules(typeof(MvcApplication).Assembly);
            builder.RegisterModule<AutofacWebTypesModule>();

            builder.RegisterType<AuthorizationRoot>().AsSelf().InstancePerRequest();

            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            return container;
        }

        private static void OnConfigure(ContainerBuilder builder)
        {
            builder.RegisterType<DatabaseContext>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<ServicesRepository>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<NewsRepository>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<UserRepository>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<EventsRepository>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<CommentsRepository>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<FileUploadService>().AsSelf().InstancePerLifetimeScope();

            builder.RegisterType<ApplicationSettings>().AsSelf().InstancePerLifetimeScope();
            builder.Register(c => Mapper.Engine);
        }
    }
}