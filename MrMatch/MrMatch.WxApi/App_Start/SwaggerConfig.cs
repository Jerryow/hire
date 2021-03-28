using System.Web.Http;
using WebActivatorEx;
using MrMatch.WxApi;
using Swashbuckle.Application;

[assembly: PreApplicationStartMethod(typeof(SwaggerConfig), "Register")]

namespace MrMatch.WxApi
{
    public class SwaggerConfig
    {
        public static void Register()
        {
            var thisAssembly = typeof(SwaggerConfig).Assembly;

            GlobalConfiguration.Configuration
                .EnableSwagger(c =>
                    {
                        c.SingleApiVersion("v1", "MrMatch.WxApi");
                        c.IncludeXmlComments(GetXmlCommentsPath("MrMatch.WxApi"));
                    })
                .EnableSwaggerUi(c =>
                    {
                        
                        
                    });
        }
        protected static string GetXmlCommentsPath(string name)
        {
            return System.String.Format(@"{0}\bin\{1}.xml", System.AppDomain.CurrentDomain.BaseDirectory, name);
        }
    }
}
