using MrMatch.Common.LogHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MrMatch.Biz.Handler.ActionFilter
{
    public class GlobalError : HandleErrorAttribute
    {
        /// <summary>
        /// 在发生异常时触发调用
        /// </summary>
        public override void OnException(ExceptionContext filterContext)
        {
            //if (!filterContext.ExceptionHandled && filterContext.Exception is NullReferenceException)
            if (!filterContext.ExceptionHandled)
            {
                var log = new LogService();

                //获取出现异常的controller名和action名，用于记录
                string controllerName = (string)filterContext.RouteData.Values["controller"];
                string actionName = (string)filterContext.RouteData.Values["action"];
                //定义一个HandErrorInfo，用于Error视图展示异常信息
                HandleErrorInfo model = new HandleErrorInfo(filterContext.Exception, controllerName, actionName);
                string thisTime = DateTime.Now.ToShortDateString().Replace("/", "");
                string errorDetails = $"出错时间：{DateTime.Now.ToString()},错误发生在{model.ControllerName}控制器的{model.ActionName},错误类型：{model.Exception.Message}";
                string splitLine = "——————————————————————分割线——————————————————————";

                log.LogError(errorDetails + splitLine, filterContext.Exception);

                ViewResult result = new ViewResult
                {
                    ViewName = "/Views/Shared/Error.cshtml",//设置异常时跳转的404页面
                    ViewData = new ViewDataDictionary<HandleErrorInfo>(model)  //定义ViewData，泛型
                };
                filterContext.Result = result;
                filterContext.ExceptionHandled = true;//设置异常已处理
            }

        }
    }
}