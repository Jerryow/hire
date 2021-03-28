using MrMatch.Biz.Api.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;

namespace MrMatch.Biz
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {


            //容器替换
            //config.DependencyResolver = new aop.ApiControllerFac(container.build()); 

            //api全局的异常
            //config.Services.Replace(typeof(IExceptionHandler), new ExceptionFaaAttribute());

            // Web API 配置和服务

            // Web API 路由
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
