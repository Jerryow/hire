using MrMatch.Application.LoginOrRegist.Inp;
using MrMatch.Application.System.Oup;
using MrMatch.Application.LoginOrRegist;
using MrMatch.Application.User.Inp;
using MrMatch.CandidateClient.Api.Base;
using MrMatch.CandidateClient.Handler;
using MrMatch.Common.LogHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;
using ThoughtWorks.QRCode.Codec;
using MrMatch.Application.LoginOrRegist.Oup;
using MrMatch.Application.SendMessage.Inp;
using MrMatch.Application.SendMessage;
using MrMatch.Common.Extension;
using MrMatch.Common.ReflectionHelper;
using System.Configuration;

namespace MrMatch.CandidateClient.Api
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

        /// <summary>
        /// 获取二维码
        /// </summary>
        /// <param name="KeyCode">websocket的加密字符串</param>
        /// <returns></returns>
        [AllowAnonymous]
        [System.Web.Http.HttpGet]
        public HttpResponseMessage LoginQRCode(string KeyCode)
        {
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);

            if (!string.IsNullOrEmpty(KeyCode))
            {
                QRCodeEncoder qrCodeEncoder = new QRCodeEncoder();
                qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
                qrCodeEncoder.QRCodeScale = 4;
                qrCodeEncoder.QRCodeVersion = 6;
                qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.M;
                System.Drawing.Image image = qrCodeEncoder.Encode(KeyCode);
                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);

                response.Content = new ByteArrayContent(ms.ToArray());
                response.Content.Headers.ContentType = new MediaTypeHeaderValue("image/jpeg");
            }

            return response;
        }

        /// <summary>
        /// 小程序扫码登陆
        /// </summary>
        /// <param name="userLoginForm"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [System.Web.Http.HttpPost]
        public async Task<HttpResponseMessage> LoginQR(QRLoginInp form)
        {
            try
            {
                var res = await signInOrUpService.LoginQR(form);
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
                    ReturnUrl = res.ReturnUrl,
                    NickName = res.NickName,
                    AvatarUrl = res.AvatarUrl,
                    PKID = res.PKID
                }.ToJson());
                //存入cookie
                CookiesManager.SetCookie("candidate_user", res.Cookie, Request, resp, 720);

                return resp;
            }
            catch (Exception ex)
            {

                logService.LogError("[小程序扫码登陆]" + ex.Message, ex);
                return Request.CreateResponse(HttpStatusCode.OK, new
                {
                    IsOK = false,
                    Msg = "登陆失败,请重试"
                });
            }
            
        }

        /// <summary>
        /// 手机号登陆
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [System.Web.Http.HttpPost]
        public async Task<HttpResponseMessage> LoginMobile(MobileLoginInp form)
        {
            try
            {
                //入参基础校验
                var validate = EntityProperties.EntityValidate<MobileLoginInp>(form);
                if (!validate.BoolResult)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, new
                    {
                        IsOK = validate.BoolResult,
                        Msg = validate.Message
                    }.ToJson());
                }

                var res = await signInOrUpService.LoginMobile(form, Application.CommonEnum.TP_Candidate_Client.PC简历.GetHashCode());
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
                    ReturnUrl = res.ReturnUrl,
                    NickName = res.NickName,
                    AvatarUrl = res.AvatarUrl,
                    PKID = res.PKID
                }.ToJson());

                //存入cookie
                if (form.AutoLogin)
                {
                    CookiesManager.SetCookie("candidate_user", res.Cookie, Request, resp, 1800);
                }
                else
                {
                    CookiesManager.SetCookie("candidate_user", res.Cookie, Request, resp, 720);
                }

                return resp;
            }
            catch (Exception ex)
            {

                logService.LogError("[手机号登陆]" + ex.Message, ex);
                return Request.CreateResponse(HttpStatusCode.OK, new
                {
                    IsOK = false,
                    Msg = "登陆失败,请重试"
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
        [AllowAnonymous]
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

        /// <summary>
        /// 退出登陆
        /// </summary>
        /// <returns></returns>
        [System.Web.Http.HttpGet]
        public HttpResponseMessage Logout()
        {
            var resp = Request.CreateResponse(HttpStatusCode.OK, "退出登录成功。");
            CookiesManager.SetCookie("candidate_user", "fjoidsnlk13mdkl2", Request, resp, -60);
            var client = ConfigurationManager.AppSettings["ProductionOrNot"];
            MrMatch.Application.Cache.ClearCacheHelper.ClearCandidateLoginToken(CurrID.ToString(), client);
            return resp;

        }
    }
}
