using System.Web.Http;
using WebActivatorEx;
using MrMatch.CandidateClient;
using Swashbuckle.Application;

[assembly: PreApplicationStartMethod(typeof(SwaggerConfig), "Register")]

namespace MrMatch.CandidateClient
{
    public class SwaggerConfig
    {
        public static void Register()
        {
            var thisAssembly = typeof(SwaggerConfig).Assembly;

            GlobalConfiguration.Configuration
                .EnableSwagger(c =>
                {


                    c.SingleApiVersion("v1", "MrMatch.CandidateClient");
                    c.IncludeXmlComments(GetXmlCommentsPath("MrMatch.CandidateClient"));


                })
                .EnableSwaggerUi(c =>
                {
                    //c.InjectJavaScript(thisAssembly,"MrMatch_Swagger.js");
                });
        }

        protected static string GetXmlCommentsPath(string name)
        {
            return System.String.Format(@"{0}\bin\{1}.xml", System.AppDomain.CurrentDomain.BaseDirectory, name);
        }
    }
}
