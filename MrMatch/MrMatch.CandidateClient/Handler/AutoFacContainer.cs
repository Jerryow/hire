using Autofac;
using MrMatch.Application.Config;
using MrMatch.Application.System;
using MrMatch.Domain.EntityBase.Repository;
using MrMatch.MysqlFramework;
using MrMatch.MysqlFramework.BaseContext;
using MrMatch.MysqlFramework.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MrMatch.CandidateClient.Handler
{
    public class AutoFacContainer
    {
        /// <summary>
        /// IOC 容器
        /// </summary>
        public static IContainer container = null;

        /// <summary>
        /// 获取实例化对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T Resolve<T>()
        {
            try
            {
                if (container == null)
                {
                    Initialise();
                }
            }
            catch (System.Exception ex)
            {
                throw new System.Exception("IOC实例化出错!" + ex.Message);
            }

            return container.Resolve<T>();
        }

        /// <summary>
        /// 初始化
        /// </summary>
        public static void Initialise()
        {
            var builder = new ContainerBuilder();
            //格式：builder.RegisterType<xxxx>().As<Ixxxx>().InstancePerLifetimeScope();
            #region Basic
            builder.RegisterType<MrMatchDbContext>().As<IDbContext>().InstancePerDependency();
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerDependency();
            builder.RegisterGeneric(typeof(ImpRepositoriesBase<>)).As(typeof(IRepository<>)).InstancePerDependency();
            #endregion


            #region Application
            builder.RegisterType<SystemService>().As<ISystemService>().InstancePerDependency();
            builder.RegisterType<ConfigService>().As<IConfigService>().InstancePerDependency();
            #endregion
            container = builder.Build();
        }
    }
}