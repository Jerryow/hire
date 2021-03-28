using MrMatch.Admin.Api.Base;
using MrMatch.Application;
using MrMatch.Application.User;
using MrMatch.Common.LogHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace MrMatch.Admin.Api
{
    public class UserApiController : SecurityBaseController
    {
        #region DI
        private readonly ILogService log;
        private readonly IUserService userService;
        public UserApiController(
            ILogService _log,
            IUserService _userService)
        {
            log = _log;
            userService = _userService;
        }
        #endregion

        /// <summary>
        /// 获取用户信息(pkid+phone)
        /// </summary>
        /// <returns></returns>
        [System.Web.Http.HttpGet]
        public async Task<IHttpActionResult> GetUserIDPhone()
        {
            try
            {
                var data = await userService.GetAllUserListAsync();
                //log.LogDebug(Newtonsoft.Json.JsonConvert.SerializeObject(data));
                return Succcess("成功.", data);
            }
            catch (Exception ex)
            {
                log.LogError("[获取用户信息(pkid+phone)]" + ex.Message, ex);
                return Fail("获取失败,请刷新重试.");
            }
        }

        /// <summary>
        /// 分页获取待审核用户的数据
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="keyWords">搜索关键字</param>
        /// <returns></returns>
        [System.Web.Http.HttpGet]
        public IHttpActionResult GetCheckUserInfoPagenation(int pageIndex, int pageSize, string keyWords)
        {
            try
            {
                var pagenation = new PagenationInput(pageIndex, pageSize, keyWords, false);

                var data = userService.GetCheckUserByPagenation(pagenation);
                //log.LogDebug(Newtonsoft.Json.JsonConvert.SerializeObject(data));
                return Succcess("成功.", data);
            }
            catch (Exception ex)
            {
                log.LogError("[分页获取待审核用户的数据]" + ex.Message, ex);
                return Fail("获取失败,请刷新重试.");
            }
        }

        /// <summary>
        /// 分页获取用户的数据
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="keyWords">搜索关键字</param>
        /// <returns></returns>
        [System.Web.Http.HttpGet]
        public IHttpActionResult GetUserInfoPagenation(int pageIndex, int pageSize, string keyWords)
        {
            try
            {
                var pagenation = new PagenationInput(pageIndex, pageSize, keyWords, false);

                var data = userService.GetUserByPagenation(pagenation);
                //log.LogDebug(Newtonsoft.Json.JsonConvert.SerializeObject(data));
                return Succcess("成功.", data);
            }
            catch (Exception ex)
            {
                log.LogError("[分页获取用户的数据]" + ex.Message, ex);
                return Fail("获取失败,请刷新重试.");
            }
        }

        /// <summary>
        /// 审核用户
        /// </summary>
        /// <param name="PKID"></param>
        /// <param name="status">CommonEnum->TP_Profile_ApproveStatus</param>
        /// <returns></returns>
        [System.Web.Http.HttpGet]
        public async Task<IHttpActionResult> UpdateApproveStatus(long PKID, int status)
        {
            try
            {
                if (PKID <= 0)
                {
                    return Fail("入参错误");
                }
                var ids = new List<int>{2,3};
                if (!ids.Contains(status))
                {
                    return Fail("入参错误");
                }

                var res = await userService.ApproveUserAsync(PKID, status);
                if (!res.BoolResult)
                {
                    return Fail(res.Message);
                }
                return Succcess(res.Message);
            }
            catch (Exception ex)
            {
                log.LogError("[审核用户]" + ex.Message, ex);
                return Fail("审核用户失败,请刷新重试.");
            }
            
        }
    }
}
