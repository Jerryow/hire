using MrMatch.Application.LoginOrRegist;
using MrMatch.Application.SendMessage;
using MrMatch.Biz.Handler;
using MrMatch.Common.LogHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using MrMatch.Application.LoginOrRegist.Inp;
using MrMatch.Application.LoginOrRegist.Oup;
using MrMatch.Biz.Api.Base;
using MrMatch.Application.SendMessage.Inp;
using MrMatch.Common.ReflectionHelper;
using MrMatch.Common.Extension;
using System.Configuration;

namespace MrMatch.Biz.Api
{
    public class PassportApiController : SecurityBaseController
    {
        #region DI
        private readonly ISendMessageService sendMessageService;
        private readonly ISignInOrUpService signInOrUpService;
        private readonly ILogService logService;
        #endregion

        public PassportApiController(
            ISendMessageService _sendMessageService,
            ISignInOrUpService _signInOrUpService,
            ILogService _logService)
        {
            signInOrUpService = _signInOrUpService;
            logService = _logService;
            sendMessageService = _sendMessageService;
        }


        [AllowAnonymous]
        [System.Web.Http.HttpGet]
        //[AA(this.ActionContext)]
        public IHttpActionResult te(string name, int a)
        {
            var aa = this.ActionContext.ActionArguments;
            return Succcess("das");
        }

        [AllowAnonymous]
        [System.Web.Http.HttpGet]
        //[AA(this.ActionContext)]
        public IHttpActionResult ex()
        {
            throw new Exception();
        }



        /// <summary>
        /// 手机号登陆
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [System.Web.Http.HttpPost]
        public async Task<HttpResponseMessage> LoginMobile(BizLoginInp form)
        {
            try
            {
                //入参基础校验
                var validate = EntityProperties.EntityValidate<BizLoginInp>(form);
                if (!validate.BoolResult)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, new
                    {
                        IsOK = validate.BoolResult,
                        Msg = validate.Message
                    }.ToJson());
                }

                var res = await signInOrUpService.BizLoginMobile(form);
                if (!res.IsOK)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, new
                    {
                        IsOK = res.IsOK,
                        Msg = res.Msg
                    }.ToJson());
                }

                var resp = Request.CreateResponse(HttpStatusCode.OK, new
                {
                    IsOK = res.IsOK,
                    Msg = res.Msg,
                    ReturnUrl = res.ReturnUrl
                }.ToJson());

                //存入cookie
                if (form.AutoLogin)
                {
                    CookiesManager.SetCookie("biz_user", res.Cookie, Request, resp, 1800);
                }
                else
                {
                    CookiesManager.SetCookie("biz_user", res.Cookie, Request, resp, 720);
                }

                return resp;
            }
            catch (Exception ex)
            {

                logService.LogError("[手机号登陆]" + ex.Message, ex);
                return Request.CreateResponse(HttpStatusCode.OK,new
                {
                    IsOK = false,
                    Msg = "登陆失败,请重试"
                });
            }
        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [System.Web.Http.HttpPost]
        public async Task<HttpResponseMessage> Regist(BizRegistInp form)
        {
            try
            {
                //入参基础校验
                var validate = EntityProperties.EntityValidate<BizRegistInp>(form);
                if (!validate.BoolResult)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, new
                    {
                        IsOK = validate.BoolResult,
                        Msg = validate.Message
                    }.ToJson());
                }

                var res = await signInOrUpService.BizRegist(form);
                if (!res.IsOK)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, new
                    {
                        IsOK = res.IsOK,
                        Msg = res.Msg
                    }.ToJson());
                }

                var resp = Request.CreateResponse(HttpStatusCode.OK, new
                {
                    IsOK = res.IsOK,
                    Msg = res.Msg,
                    ReturnUrl = res.ReturnUrl
                }.ToJson());


                CookiesManager.SetCookie("biz_user", res.Cookie, Request, resp, 720);

                return resp;
            }
            catch (Exception ex)
            {

                logService.LogError("[注册]" + ex.Message, ex);
                return Request.CreateResponse(HttpStatusCode.OK, new
                {
                    IsOK = false,
                    Msg = "注册失败,请重试"
                });
            }
        }

        /// <summary>
        /// 发送手机验证码
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [System.Web.Http.HttpPost]
        public async Task<IHttpActionResult> SendVerifyCode(SendPhoneInp form)
        {

            try
            {
                //入参基础校验
                var validate = EntityProperties.EntityValidate<SendPhoneInp>(form);
                if (!validate.BoolResult)
                {
                    return Fail(validate.Message);
                }


                //判断生产或测试环境
                var isProduction = System.Configuration.ConfigurationManager.AppSettings["ProductionOrNot"];
                Random rd = new Random();
                string str = "0123456789";
                string result = "";
                for (int i = 0; i < 4; i++)
                {
                    result += str[rd.Next(str.Length)];
                }

                if (isProduction.Trim().ToLower() == "false")
                {
                    return Succcess(result);
                }
                var tempCode = System.Configuration.ConfigurationManager.AppSettings["LoginTempCode"];
                var res = await sendMessageService.SendPhoneMessage(form, tempCode, result, 1);
                if (!res.BoolResult)
                {
                    return Fail(res.Message);
                }
                return Succcess(res.Message);
            }
            catch (Exception ex)
            {
                logService.LogError("[发送手机验证码]" + ex.Message, ex);
                return Fail("发送失败,请重试");
            }
        }

        /// <summary>
        /// 发送邮箱验证码
        /// </summary>
        /// <param name="accountEmail">邮箱</param>
        /// <returns></returns>
        [AllowAnonymous]
        [System.Web.Http.HttpGet]
        public async Task<IHttpActionResult> SendEmailVerifyCode(string accountEmail)
        {

            try
            {
                //入参基础校验
                if (string.IsNullOrEmpty(accountEmail))
                {
                    return Fail("邮箱不能为空");
                }


                //判断生产或测试环境
                var isProduction = System.Configuration.ConfigurationManager.AppSettings["ProductionOrNot"];
                Random rd = new Random();
                string str = "0123456789";
                string result = "";
                for (int i = 0; i < 4; i++)
                {
                    result += str[rd.Next(str.Length)];
                }

                if (isProduction.Trim().ToLower() == "false")
                {
                    return Succcess(result);
                }
                var res = await sendMessageService.SendEmailMessage(accountEmail, result);
                if (!res.BoolResult)
                {
                    return Fail(res.Message);
                }
                return Succcess(res.Message);
            }
            catch (Exception ex)
            {
                logService.LogError("[发送邮箱验证码]" + ex.Message, ex);
                return Fail("发送邮箱验证码失败,请重试");
            }
        }

        /// <summary>
        /// 获取图片
        /// </summary>
        /// <param name="phone"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [System.Web.Http.HttpGet]
        public IHttpActionResult GetPicInfo(string phone)
        {
            try
            {
                if (string.IsNullOrEmpty(phone))
                {
                    return Fail("请先输入手机号");
                }

                var oup = sendMessageService.GetVerifyImg(phone);
                return Succcess("成功", oup);
            }
            catch (Exception ex)
            {
                logService.LogError("[获取图片]" + ex.Message, ex);
                return Fail("获取图片失败,请重试");
            }
        }

        /// <summary>
        /// 退出登陆
        /// </summary>
        /// <returns></returns>
        [System.Web.Http.HttpGet]
        public HttpResponseMessage Logout()
        {
            try
            {
                var resp = Request.CreateResponse(HttpStatusCode.OK, "退出登录成功");
                CookiesManager.SetCookie("biz_user", "fjoidsnlk13mdkl2", Request, resp, -60);
                var client = ConfigurationManager.AppSettings["ProductionOrNot"];
                MrMatch.Application.Cache.ClearCacheHelper.ClearBizLoginToken(CurrUser.PKID.ToString(), client);
                return resp;
            }
            catch (Exception ex)
            {
                logService.LogError("[退出登陆]" + ex.Message, ex);
                return Request.CreateResponse(HttpStatusCode.OK, "退出登陆失败,请刷新重试");
            }
        }
    }
}
