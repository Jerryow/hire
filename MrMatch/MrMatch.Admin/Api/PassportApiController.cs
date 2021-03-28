using MrMatch.Admin.Api.Base;
using MrMatch.Admin.Handler;
using MrMatch.Application.LoginOrRegist;
using MrMatch.Application.LoginOrRegist.Inp;
using MrMatch.Application.LoginOrRegist.Oup;
using MrMatch.Application.System.Inp;
using MrMatch.Application.System.Oup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Configuration;

namespace MrMatch.Admin.Api
{
    public class PassportApiController : SecurityBaseController
    {
        #region DI
        //private readonly ISystemService systemService = Handler.AutoFacContainer.Resolve<ISystemService>();
        private readonly ISignInOrUpService signInOrUpService;
        #endregion

        public PassportApiController(ISignInOrUpService _signInOrUpService)
        {
            signInOrUpService = _signInOrUpService;
        }

        /// <summary>
        /// 登陆
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [System.Web.Http.HttpPost]
        public async Task<HttpResponseMessage> Login(AdminLoginInp input)
        {
            var res = await signInOrUpService.AdminSignIn(input);

            if (!res.IsOK)
            {
                return Request.CreateResponse(HttpStatusCode.OK, Serialize<SignInOup>(res));
            }

            //FormsAuthenticationTicket ticketObject = new FormsAuthenticationTicket(0, name, DateTime.Now,
            //            DateTime.Now.AddHours(1), true, string.Format("{0}&{1}", name, pwd),
            //            FormsAuthentication.FormsCookiePath);


            //存入cookie
            var resp = Request.CreateResponse(HttpStatusCode.OK, Serialize<SignInOup>(res));
            CookiesManager.SetCookie("admin_user", res.Cookie, Request, resp, 720);

            return resp;
        }

        /// <summary>
        /// 退出登陆
        /// </summary>
        /// <returns></returns>
        [System.Web.Http.HttpGet]
        public HttpResponseMessage Logout()
        {
            var resp = Request.CreateResponse(HttpStatusCode.OK, "退出登录成功。");
            CookiesManager.SetCookie("admin_user", "fjoidsnlk13mdkl2", Request, resp, -60);
            var client = ConfigurationManager.AppSettings["ProductionOrNot"];
            MrMatch.Application.Cache.ClearCacheHelper.ClearAdminLoginToken(CurrID.ToString(), client);
            return resp;

        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="loginName">登录名</param>
        /// <param name="oldPwd">旧密码</param>
        /// <param name="newPwd">新密码</param>
        /// <returns></returns>
        [System.Web.Http.HttpGet]
        public async Task<HttpResponseMessage> UpdatePwd(string loginName, string oldPwd, string newPwd)
        {

            var res = await signInOrUpService.AdminUpdatePwd(loginName, oldPwd, newPwd);

            if (!res.BoolResult)
            {
                return Request.CreateResponse(HttpStatusCode.OK, res);
            }

            var resp = Request.CreateResponse(HttpStatusCode.OK, res);
            CookiesManager.SetCookie("admin_user", "fjoidsnlk13mdkl2", Request, resp, -60);
            var client = ConfigurationManager.AppSettings["ProductionOrNot"];
            MrMatch.Application.Cache.ClearCacheHelper.ClearAdminLoginToken(CurrID.ToString(), client);
            return resp;

        }
    }
}
