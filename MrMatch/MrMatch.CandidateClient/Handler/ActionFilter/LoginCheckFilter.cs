using MrMatch.CandidateClient.Handler.ActionFilters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MrMatch.CandidateClient.Handler.ActionFilter
{
    public class LoginCheckFilter : FilterAttribute, IAuthorizationFilter
    {
        /// <summary>
        /// 是否检查请求
        /// </summary>
        public bool IsCheck { get; set; } = true;
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            if (!IsCheck)
                return;

            if (CurrID.PKID <= 0)
            {
                filterContext.Result = new RedirectResult($"/passport/login?fromurl={filterContext.RequestContext.HttpContext.Request.Url}");
            }
        }
    }
}