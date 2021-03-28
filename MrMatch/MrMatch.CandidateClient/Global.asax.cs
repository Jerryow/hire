using MrMatch.CandidateClient.Handler;
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

namespace MrMatch.CandidateClient
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

            // ��������ע��(ע�⣺����ʹ�õ��ǵ�����������AutofacWebApiConfig��)
            AutofacWebApiConfig.RegisterDependencies();

            //Common.Redis.MyRedisHelper.InitClient();
            //EFů������
            using (MrMatch.MysqlFramework.MrMatchDbContext dbContext = new MrMatch.MysqlFramework.MrMatchDbContext())
            {
                var objectContext = ((IObjectContextAdapter)dbContext).ObjectContext;
            
                var mappingCollection =
                            (StorageMappingItemCollection)objectContext.MetadataWorkspace.GetItemCollection(DataSpace.CSSpace);
                mappingCollection.GenerateViews(new List<EdmSchemaError>());
            }
        }
    }
}
