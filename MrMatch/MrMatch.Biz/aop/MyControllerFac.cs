using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Dependencies;
using System.Web.Mvc;
using System.Web.Routing;
using Autofac;
using MrMatch.MysqlFramework;
using MrMatch.MysqlFramework.BaseContext;

namespace MrMatch.Biz.aop
{
    //mvc
    public class MyControllerFac : DefaultControllerFactory
    {
        //此方法去自我构造控制器实例
        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            //逻辑结果ioc容器去创建type返回
            /// <summary>
            /// IOC 容器
            /// </summary>
            var builder = new ContainerBuilder();
            //格式：builder.RegisterType<xxxx>().As<Ixxxx>().InstancePerLifetimeScope();
            //#region Basic
            builder.RegisterType<MrMatchDbContext>().As<IDbContext>().InstancePerDependency();
            //builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerDependency();
            //builder.RegisterGeneric(typeof(ImpRepositoriesBase<>)).As(typeof(IRepository<>)).InstancePerDependency();
            //#endregion


            //#region Application
            //builder.RegisterType<SystemService>().As<ISystemService>().InstancePerDependency();
            //builder.RegisterType<ConfigService>().As<IConfigService>().InstancePerDependency();

            builder.RegisterType(controllerType);



            IContainer container = builder.Build();

            return (IController)container.Resolve(controllerType);
            //return base.GetControllerInstance(requestContext, controllerType);//原始方式
        }
    }

    //api
    public class ApiControllerFac : System.Web.Http.Dependencies.IDependencyResolver
    {

        //实际流程--->容器需要在这里做单例
        //需要把这个实例在webapiconfig里面替换一下
        private IContainer _container;
        public ApiControllerFac(IContainer container)
        {
            _container = container;
        }
        public IDependencyScope BeginScope()//作用域
        {
            //这里需要结合容器创建scope的子容器
            //return new ApiControllerFac(this._container.CreateChildContaner());//这个需要ioc容器支持 或者自定义实现
            throw new NotImplementedException();
        }

        public void Dispose()//释放
        {
            //这里直接释放容器
            this._container.Dispose();
        }

        public object GetService(Type serviceType)
        {

            try
            {
                //逻辑结果ioc容器去创建type返回
                /// <summary>
                /// IOC 容器
                /// </summary>
                var builder = new ContainerBuilder();
                builder.RegisterType<MrMatchDbContext>().As<IDbContext>().InstancePerDependency();
                builder.RegisterType(serviceType);
                IContainer container = builder.Build();

                return container.Resolve(serviceType);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public IEnumerable<object> GetServices(Type serviceType)//这里是注册获取多个构造实例---->也需要用try  catch包起来
        {
            throw new NotImplementedException();
        }

        //两个方法用try catch的原因
        //weiapi除了构造控制器以外,还需要构造其他内置的一些服务.因为不在容器里面,所以得不到.  我们也不需要处理   直接返回null就行
       
    }
}