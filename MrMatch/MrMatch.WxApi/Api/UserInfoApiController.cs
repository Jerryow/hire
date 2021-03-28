using MrMatch.Application.User;
using MrMatch.Application.User.Inp;
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
    public class UserInfoApiController : SecurityBaseController
    {
        #region DI
        private readonly IUserService userService;
        private readonly ILogService logService;
        #endregion

        public UserInfoApiController(
            IUserService _userService,
            ILogService _logService)
        {
            userService = _userService;
            logService = _logService;
        }

        /// <summary>
        /// 获取账户信息
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        [System.Web.Http.HttpGet]
        public async Task<IHttpActionResult> GetAccountInfo(long userID)
        {
            try
            {
                if (userID <= 0)
                {
                    return Fail("userID入参错误");
                }
                var res = await userService.GetWechatAccountAsync(userID);
                return Succcess("成功", res);
            }
            catch (Exception ex)
            {
                logService.LogError("[获取账户信息]" + ex.Message, ex);
                return Fail("获取失败,请刷新重试.");
            }
        }

        /// <summary>
        /// 修改账户信息
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="email"></param>
        /// <returns></returns>
        [System.Web.Http.HttpGet]
        public async Task<IHttpActionResult> GetAccountInfo1(long userID, string email)
        {
            try
            {
                if (userID <= 0)
                {
                    return Fail("userID入参错误");
                }
                var res = await userService.UpdateWechatAccountAsync(userID,email);
                if (!res.BoolResult)
                {
                    return Fail(res.Message);
                }
                return Succcess(res.Message);
            }
            catch (Exception ex)
            {
                logService.LogError("[修改账户信息]" + ex.Message, ex);
                return Fail("修改账户信息失败,请刷新重试.");
            }
        }

        /// <summary>
        /// 上传用户头像
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        [System.Web.Http.HttpPost]
        public async Task<IHttpActionResult> UploadAvatar(UploadUserAvatarInp form)
        {
            try
            {
                if (form.UserID <= 0)
                {
                    return Fail("入参错误");
                }

                var res = await userService.UpdateUserAvatarAsync(form.UserID, form.AvatarBytes);
                if (!res.BoolResult)
                {
                    return Fail(res.Message);
                }
                return Succcess(res.Message);
            }
            catch (Exception ex)
            {
                logService.LogError("[上传用户头像]" + ex.Message, ex);
                return Fail("上传用户头像失败,请刷新重试.");
            }
        }
    }
}
