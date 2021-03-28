using MrMatch.Common.LogHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace MrMatch.WxApi.Api.Base
{
    public class ApiFilters : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            var log = new LogService();

            //获取出现异常的controller名和action名，用于记录
            string controllerName = actionContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            string actionName = actionContext.ActionDescriptor.ActionName;
            //定义一个HandErrorInfo，用于Error视图展示异常信息
            string thisTime = DateTime.Now.ToShortDateString().Replace("/", "");
            string errorDetails = $"记录时间：{DateTime.Now.ToString()},请求在发生在{controllerName}控制器的{actionName}";
            string splitLine = "——————————————————————分割线——————————————————————";

            log.LogDebug(errorDetails + splitLine);

        }
    }
}