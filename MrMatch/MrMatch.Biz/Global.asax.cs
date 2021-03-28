using MrMatch.Biz.Handler;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Mapping;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace MrMatch.Biz
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            ControllerBuilder.Current.SetControllerFactory(new aop.MyControllerFac());

            // 配置依赖注入(注意：这里使用的是单独类库里面的AutofacWebApiConfig类)
            AutofacWebApiConfig.RegisterDependencies();

            //EF暖机操作
            using (MrMatch.MysqlFramework.MrMatchDbContext dbContext = new MrMatch.MysqlFramework.MrMatchDbContext())
            {
                var objectContext = ((IObjectContextAdapter)dbContext).ObjectContext;

                var mappingCollection =
                            (StorageMappingItemCollection)objectContext.MetadataWorkspace.GetItemCollection(DataSpace.CSSpace);
                mappingCollection.GenerateViews(new List<EdmSchemaError>());
            }
        }

        //全局异常
        //protected void Application_Error(object sender, EventArgs e)
        //{
        //    Exception exception = Server.GetLastError();//获取系统的最后一个异常
        //    Response.Write("系统异常1111");//返回值

        //    //Response.Redirect()//重定向
        //    //base.Context.RewritePath("/home/index")//重定向
        //}
    }
}
