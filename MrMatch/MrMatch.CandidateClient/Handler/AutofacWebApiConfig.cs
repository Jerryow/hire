using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using MrMatch.Domain.EntityBase.Repository;
using MrMatch.MysqlFramework;
using MrMatch.MysqlFramework.BaseContext;
using MrMatch.MysqlFramework.Repositories.Base;

namespace MrMatch.CandidateClient.Handler
{
    public class AutofacWebApiConfig
    {
        public static void RegisterDependencies()
        {
            SetAutofacWebapi();
        }

        private static void SetAutofacWebapi()
        {
            ContainerBuilder builder = new ContainerBuilder();
            HttpConfiguration config = GlobalConfiguration.Configuration;

            // Register API controllers using assembly scanning.
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            //builder.Re(Assembly.GetExecutingAssembly());
            builder.RegisterAssemblyTypes(AppDomain.CurrentDomain.GetAssemblies()).Where(t => t.Name.EndsWith("Service")).AsImplementedInterfaces().InstancePerRequest();

            #region Basic
            builder.RegisterType<MrMatchDbContext>().As<IDbContext>().InstancePerDependency();
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerDependency();
            builder.RegisterGeneric(typeof(ImpRepositoriesBase<>)).As(typeof(IRepository<>)).InstancePerDependency();
            builder.RegisterGeneric(typeof(ImpCommunityRepositoriesBase<>)).As(typeof(ICommunityRepository<>)).InstancePerDependency();
            #endregion

            //builder.RegisterType<SqlServerUserRepository>().As<IUserRepository>().InstancePerRequest();
            var container = builder.Build();
            // Set the WebApi dependency resolver.
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}