using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.ExceptionHandling;
using System.Web.Http.Filters;
using System.Web.Http.Results;

namespace MrMatch.Biz.Api.Base
{
    public class ExceptionFAttribute : IExceptionFilter
    {
        public bool AllowMultiple => throw new NotImplementedException();

        public Task ExecuteExceptionFilterAsync(HttpActionExecutedContext actionExecutedContext, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }


    //控制器作用域
    //public class ExceptionFAttribute : ExceptionFilterAttribute
    //{
    //    public override void OnException(HttpActionExecutedContext actionExecutedContext)
    //    {
    //        actionExecutedContext.Response = new HttpResponseMessage();//重新构造返回对象
    //        actionExecutedContext.Response.StatusCode = HttpStatusCode.InternalServerError;//设置statucode
    //        actionExecutedContext.Response.Content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(new Result() { code = -1, msg = "213" }), Encoding.UTF8, "application/json");//返回msg

    //        base.OnException(actionExecutedContext);
    //    }
    //}


    //api全局作用域
    public class ExceptionFaaAttribute : ExceptionHandler
    {
        public override bool ShouldHandle(ExceptionHandlerContext context) //判断是否需要处理
        {
            string url = context.Request.RequestUri.AbsoluteUri;
            return url.Contains("/api/");
            //return base.ShouldHandle(context);
        }
        public override void Handle(ExceptionHandlerContext context)
        {
            context.Result = new ResponseMessageResult(context.Request.CreateResponse
                (
                System.Net.HttpStatusCode.OK, new
                {
                    code = -1,
                    msg = "您当前状态为游客，请前往登录页面登录"
                }
                ));
            //base.Handle(context);
        }

        public override Task HandleAsync(ExceptionHandlerContext context, CancellationToken cancellationToken)
        {
            return base.HandleAsync(context, cancellationToken);
        }

    }
}
