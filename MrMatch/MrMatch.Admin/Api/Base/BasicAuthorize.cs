
using MrMatch.Common.LogHelper;
using MrMatch.Common.Redis;
using MrMatch.Common.Redis.MyHelper;
using MrMatch.Domain.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http;

namespace MrMatch.Admin.Api.Base
{
    /// <summary>
    /// API权限验证
    /// </summary>
    public class BasicAuthorize : AuthorizeAttribute
    {
        private LogService log = new LogService();
        public override void OnAuthorization(System.Web.Http.Controllers.HttpActionContext actionContext)
        {

            if (actionContext.Request.Method == HttpMethod.Options)
            {
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Accepted);
                return;
            }

            //从http请求的头里面获取身份验证信息，验证是否是请求发起方的ticket
            var authorization = actionContext.Request.Headers.Authorization;//获取请求头验证的Ticket
            int apiKey = actionContext.Request.GetQueryNameValuePairs().Count(w => w.Key == "api_key");//获取Swagger的Ticket

            //验证是否允许匿名访问
            if (actionContext.ActionDescriptor.GetCustomAttributes<AllowAnonymousAttribute>(true).Count != 0
                || actionContext.ActionDescriptor.ControllerDescriptor.GetCustomAttributes<AllowAnonymousAttribute>(true).Count != 0)
            {
                base.OnAuthorization(actionContext);
            }
            //验证Ticket
            else if (authorization != null && authorization.Parameter != null)
            {
                //用户验证逻辑
                if (ValidateTicketNew(authorization.Parameter))
                {
                    base.IsAuthorized(actionContext);
                }
                else
                {
                    this.HandleUnauthorizedRequest(actionContext);
                }
            }
            //验证Swagger
            else if (apiKey > 0)
            {

                if (ValidateTicketNew(actionContext.Request.GetQueryNameValuePairs().First(w => w.Key == "api_key").Value))
                {
                    base.IsAuthorized(actionContext);
                }
                else
                {
                    HandleUnauthorizedRequest(actionContext);
                }
            }
            //如果取不到身份验证信息，并且不允许匿名访问，则返回未验证401
            else
            {
                this.HandleUnauthorizedRequest(actionContext);
            }
        }

        protected override void HandleUnauthorizedRequest(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            var challengeMessage = new System.Net.Http.HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized);//告诉浏览器要验证
            challengeMessage.Headers.Add("WWW-Authenticate", "Basic");//权限信息放在basic
            //throw new System.Web.Http.HttpResponseException(challengeMessage);

            base.HandleUnauthorizedRequest(actionContext);//返回没有授权

            var response = actionContext.Response = actionContext.Response ?? new HttpResponseMessage();
            response.StatusCode = HttpStatusCode.OK;
            response.Content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(new Result() { code = -1, msg = "您当前状态为游客，请前往登录页面登录" }), Encoding.UTF8, "application/json");
        }

        private bool ValidateTicketNew(string encryptTicket)
        {
            try
            {
                var strTicket = Common.Encrypt.Encryption.DecryptString(encryptTicket);

                //验证redis

                var user = Newtonsoft.Json.JsonConvert.DeserializeObject<TokenModel>(strTicket);

                if (user == null || user.PKID <= 0)
                {
                    return false;
                }
                //需要转码
                var redisToken = "";
                var client = ConfigurationManager.AppSettings["ProductionOrNot"];
                redisToken = Application.Cache.GetCacheHelper.GetAdminToken(user.PKID.ToString(), client);
                if (string.IsNullOrEmpty(redisToken.Trim()))
                {
                    return false;
                }

                if (redisToken != encryptTicket)
                {
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }


    public class Result
    {
        public int code { get; set; }
        public string msg { get; set; }
    }
}