using MrMatch.Application.LoginOrRegist;
using MrMatch.Application.LoginOrRegist.Inp;
using MrMatch.Application.LoginOrRegist.Oup;
using MrMatch.Application.SendMessage;
using MrMatch.Application.SendMessage.Inp;
using MrMatch.Common.LogHelper;
using MrMatch.WxApi.Api.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace MrMatch.WxApi.Api
{
    public class PassportApiController : ApiControllerBase
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



        /// <summary>
        /// 手机号登陆
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        [System.Web.Http.HttpPost]
        public async Task<IHttpActionResult> LoginMobile(MobileLoginInp form)
        {
            try
            {
                var res = await signInOrUpService.LoginMobile(form, Application.CommonEnum.TP_Candidate_Client.小程序.GetHashCode());
                if (!res.IsOK)
                {
                    return Fail(res.Msg);
                }
                return Succcess("成功",new 
                { 
                    Ticket = res.Cookie,
                    IsOK = res.IsOK,
                    Msg = res.Msg,
                    PKID = res.PKID,
                    NickName = res.NickName,
                    AvatarUrl = res.AvatarUrl
                });
            }
            catch (Exception ex)
            {

                logService.LogError("[手机号登陆]" + ex.Message, ex); 
                return Fail(ex.Message);
            }
        }


        /// <summary>
        /// 发送手机验证码
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        [System.Web.Http.HttpPost]
        public async Task<IHttpActionResult> SendVerifyCode(SendPhoneInp form)
        {
            if (string.IsNullOrEmpty(form.UserSign))
            {
                return Fail("验证签名不能为空");
            }

            if (string.IsNullOrEmpty(form.PhoneNumber))
            {
                return Fail("手机号不能为空");
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
            var res = await sendMessageService.SendPhoneMessage(form, tempCode, result, 2);
            if (!res.BoolResult)
            {
                return Fail(res.Message);
            }
            return Succcess(res.Message);
        }

        /// <summary>
        /// 获取图片
        /// </summary>
        /// <param name="phone"></param>
        /// <returns></returns>
        [System.Web.Http.HttpGet]
        public IHttpActionResult GetPicInfo(string phone)
        {
            if (string.IsNullOrEmpty(phone))
            {
                return Json(new { code = "0", msg = "请先输入手机号." });
            }

            var oup = sendMessageService.GetVerifyImg(phone);
            return Json(new { code = "1", msg = "成功", data = oup });
        }
    }
}
