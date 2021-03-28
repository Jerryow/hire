using MrMatch.Common.LogHelper;
using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace MrMatch.Biz.Api.Base
{
    public class ApiFilters : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {

            //actionContext.ActionArguments

            var log = new LogService();

            //获取出现异常的controller名和action名，用于记录
            string controllerName = actionContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            string actionName = actionContext.ActionDescriptor.ActionName;
            //定义一个HandErrorInfo，用于Error视图展示异常信息
            string thisTime = DateTime.Now.ToShortDateString().Replace("/", "");
            string errorDetails = $"记录时间：{DateTime.Now.ToString()},请求在发生在{controllerName}控制器的{actionName}";
            string splitLine = "——————————————————————分割线——————————————————————";


            var url = actionContext.Request.RequestUri;
            var paras = actionContext.ActionArguments;


            log.LogDebug(errorDetails + splitLine);

        }

        //压缩
        //public override void OnActionExecuting(HttpActionContext filterContext)
        //{
        //    var request = filterContext.Request;
        //    var response = filterContext.Response;
        //    var accept = request.Headers.AcceptEncoding.Where(x=>x.Value=="gzip").FirstOrDefault();
        //    if (accept!=null)
        //    {
        //        response.Headers.Add("Content-Encoding", "gzip");
        //        response.Content = new GZipStream(, CompressionMode.Compress);
        //    }
        //}
    }


    public class AAAttribute : Attribute
    {
        private HttpActionContext context;
        public AAAttribute(HttpActionContext httpActionContext)
        {
            this.context = httpActionContext;
        }
        public bool Para()
        {
            var paras = context.ActionArguments;
            return true;  
        }
    }
}