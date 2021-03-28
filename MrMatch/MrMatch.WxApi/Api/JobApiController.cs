using MrMatch.Application.Config;
using MrMatch.Application.Job;
using MrMatch.Application.Job.Inp;
using MrMatch.Application.User;
using MrMatch.Common.LogHelper;
using MrMatch.WxApi.Api.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using MrMatch.Common.ReflectionHelper;
using MrMatch.Application.Wechat;
using MrMatch.Application.System;
using MrMatch.Common.Wechat;
using System.Configuration;

namespace MrMatch.WxApi.Api
{
    public class JobApiController : SecurityBaseController
    {
        #region DI
        private readonly IJobService jobService;
        private readonly IUserService userService;
        private readonly IConfigService configService;
        private readonly ISystemService systemService;
        private readonly IWechatService wechatService;
        private readonly ILogService logService;
        #endregion

        public JobApiController(
            IUserService _userService,
            IConfigService _configService,
            ILogService _logService,
            IWechatService _wechatService,
            ISystemService _systemService,
            IJobService _jobService)
        {
            userService = _userService;
            configService = _configService;
            logService = _logService;
            jobService = _jobService;
            wechatService = _wechatService;
            systemService = _systemService;
        }

        /// <summary>
        /// 分页获取职位数据
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="userID"></param>
        /// <param name="intentionID"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [System.Web.Http.HttpGet]
        public async Task<IHttpActionResult> GetJobPagenation(int pageIndex, int pageSize, long userID, long intentionID)
        {
            try
            {
                var data = await jobService.GetJobByPagenationAsync(pageIndex, pageSize, userID, intentionID);
                return Succcess("", data);
            }
            catch (Exception ex)
            {
                logService.LogError("[分页获取职位数据]" + ex.Message, ex);
                return Fail("获取失败,请刷新重试.");
            }
        }

        /// <summary>
        /// 分页获取收藏职位数据
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        [System.Web.Http.HttpGet]
        public async Task<IHttpActionResult> GetFollowJobPagenation(int pageIndex, int pageSize, long userID)
        {
            try
            {
                var data = await jobService.GetFollowJobByPagenationAsync(pageIndex, pageSize, userID);
                return Succcess("", data);
            }
            catch (Exception ex)
            {
                logService.LogError("[分页获取收藏职位数据]" + ex.Message, ex);
                return Fail("获取失败,请刷新重试.");
            }
        }

        /// <summary>
        /// 分页获取投递职位数据
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        [System.Web.Http.HttpGet]
        public async Task<IHttpActionResult> GetApplyJobPagenation(int pageIndex, int pageSize, long userID)
        {
            try
            {
                var data = await jobService.GetApplyJobByPagenationAsync(pageIndex, pageSize, userID);
                return Succcess("", data);
            }
            catch (Exception ex)
            {
                logService.LogError("[分页获取投递职位数据]" + ex.Message, ex);
                return Fail("获取失败,请刷新重试.");
            }
        }

        /// <summary>
        /// 获取职位详情数据
        /// </summary>
        /// <param name="PKID"></param>
        /// <param name="userID"></param>
        /// <param name="isLogin"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [System.Web.Http.HttpGet]
        public async Task<IHttpActionResult> GetJobDetails(int PKID, long userID, bool isLogin)
        {
            try
            {
                var data = await jobService.GetJobDetailsAsync(PKID, userID, isLogin);
                return Succcess("", data);
            }
            catch (Exception ex)
            {
                logService.LogError("[分页获取职位数据]" + ex.Message, ex);
                return Fail("获取失败,请刷新重试.");
            }
        }

        /// <summary>
        /// 投递职位
        /// </summary>
        /// <param name="jobID"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        [System.Web.Http.HttpGet]
        public async Task<IHttpActionResult> JobApply(long jobID, long userID)
        {
            try
            {
                var res = await jobService.JobApplyAsync(jobID, userID);
                if (!res.BoolResult)
                {
                    return Fail(res.Message);
                }
                return Succcess(res.Message);
            }
            catch (Exception ex)
            {
                logService.LogError("[投递职位]" + ex.Message, ex);
                return Fail("投递职位失败,请刷新重试.");
            }
        }

        /// <summary>
        /// 收藏职位
        /// </summary>
        /// <param name="jobID"></param>
        /// <param name="userID"></param>
        /// <param name="follow">true:收藏  false:取消收藏</param>
        /// <returns></returns>
        [System.Web.Http.HttpGet]
        public async Task<IHttpActionResult> FollowJob(long jobID, long userID, bool follow)
        {
            try
            {
                var res = await jobService.JobFollowOrNotAsync(jobID, userID, follow);
                if (!res.BoolResult)
                {
                    return Fail(res.Message);
                }
                return Succcess(res.Message);
            }
            catch (Exception ex)
            {
                logService.LogError("[收藏职位]" + ex.Message, ex);
                return Fail("收藏职位失败,请刷新重试.");
            }
        }

        /// <summary>
        /// 举报岗位
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        [System.Web.Http.HttpPost]
        public async Task<IHttpActionResult> ReportJob(UserReportInp form)
        {
            try
            {
                //基础数据校验
                var validate = EntityProperties.EntityValidate<UserReportInp>(form);
                if (!validate.BoolResult)
                {
                    return Fail(validate.Message);
                }

                var res = await jobService.ReportJobAsync(form);
                if (!res.BoolResult)
                {
                    return Fail(res.Message);
                }
                return Succcess(res.Message);
            }
            catch (Exception ex)
            {
                logService.LogError("[举报岗位]" + ex.Message, ex);
                return Fail("举报岗位失败,请刷新重试.");
            }
        }

        /// <summary>
        /// 获取小程序职位二维码
        /// </summary>
        /// <param name="jobID"></param>
        /// <returns></returns>
        [System.Web.Http.HttpGet]
        public async Task<IHttpActionResult> GetMiniPic(long jobID)
        {
            try
            {
                if (jobID <= 0)
                {
                    return Fail("入参错误");
                }

                var appid = await systemService.GetSiteConfigValueByCodeAsync("WxAppID");
                var secret = await systemService.GetSiteConfigValueByCodeAsync("WxAppSecret");
                var token = await wechatService.GetAccessToken(appid, secret);
                if (!token.IsOK)
                {
                    logService.LogWarning("[获取小程序职位二维码]" + token.Message);
                    return Fail("获取失败,请稍后重试.");
                }

                var setting = new Common.Wechat.MiniWeChatPic();
                setting.scene = "id=" + jobID.ToString();
                setting.page = ConfigurationManager.AppSettings["miniPic"];
                setting.width = 280;
                var url = await jobService.GetMiniPicUrlAsync(jobID, token.Token, setting);
                return Succcess("");
            }
            catch (Exception ex)
            {
                logService.LogError("[获取小程序职位二维码]" + ex.Message, ex);
                return Fail("获取小程序职位二维码失败,请刷新重试.");
            }
        }
    }
}
