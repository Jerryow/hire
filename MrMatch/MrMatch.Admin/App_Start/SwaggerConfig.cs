using System.Web.Http;
using WebActivatorEx;
using MrMatch.Admin;
using Swashbuckle.Application;

[assembly: PreApplicationStartMethod(typeof(SwaggerConfig), "Register")]

namespace MrMatch.Admin
{
    public class SwaggerConfig
    {
        public static void Register()
        {
            var thisAssembly = typeof(SwaggerConfig).Assembly;

            GlobalConfiguration.Configuration
                .EnableSwagger(c =>
                    {


                        c.SingleApiVersion("v1", "MrMatch.Admin");


                    })
                .EnableSwaggerUi(c =>
                    {
                        GetXmlCommentsPath();
                        //c.InjectJavaScript(thisAssembly,"MrMatch_Swagger.js");
                    });
        }

        private static string GetXmlCommentsPath()
        {
            return $@"{System.AppDomain.CurrentDomain.BaseDirectory}\bin\WebApi.XML";
        }
    }
}