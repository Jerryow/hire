using MrMatch.Application.Config;
using MrMatch.Application.System;
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

namespace MrMatch.WxApi.Api
{
    public class BasicApiController : SecurityBaseController
    {
        #region DI
        private readonly IUserService userService;
        private readonly IConfigService configService;
        private readonly ISystemService systemService;
        private readonly ILogService logService;
        #endregion

        public BasicApiController(
            IUserService _userService,
            IConfigService _configService,
            ISystemService _systemService,
            ILogService _logService)
        {
            userService = _userService;
            configService = _configService;
            systemService = _systemService;
            logService = _logService;
        }



        /// <summary>
        /// 获取职业的三级目录
        /// </summary>
        /// <returns></returns>
        [System.Web.Http.HttpGet]
        public async Task<IHttpActionResult> GetFunctionForCascader()
        {
            try
            {
                var res = await configService.GetFunctionForCascaderAsync(true);
                return Succcess("成功", res);
            }
            catch (Exception ex)
            {
                logService.LogError("[获取职业的三级目录]" + ex.Message, ex);
                return Fail("获取失败,请刷新重试.");
            }
        }

        /// <summary>
        /// 获取城市
        /// </summary>
        /// <returns></returns>
        [System.Web.Http.HttpGet]
        public async Task<IHttpActionResult> GetAllDistrictList()
        {
            try
            {
                var hot = await configService.GetHotDistrictAsync();
                var cascader = await configService.GetProvinceListAsync(true);
                return Succcess("成功", new { hot=hot, basic = cascader });
            }
            catch (Exception ex)
            {
                logService.LogError("[获取城市]" + ex.Message, ex);
                return Fail("获取失败,请刷新重试.");
            }
        }

        /// <summary>
        /// 获取标签的目录
        /// </summary>
        /// <returns></returns>
        [System.Web.Http.HttpGet]
        public async Task<IHttpActionResult> GetTagsList()
        {
            try
            {
                var res = await configService.GetTagsListAsync();
                return Succcess("成功", res);
            }
            catch (Exception ex)
            {
                logService.LogError("[获取标签的目录]" + ex.Message, ex);
                return Fail("获取失败,请刷新重试.");
            }
        }

        /// <summary>
        /// 获取JobBoard的选择薪酬范围
        /// </summary>
        [System.Web.Http.HttpGet]
        public async Task<IHttpActionResult> GetAnnulSlaryRange()
        {
            try
            {
                var res = await systemService.GetSiteConfigValueByCodeAsync("AnnulSalaryRange");
                return Succcess("成功", res);
            }
            catch (Exception ex)
            {
                logService.LogError("[获取标签的目录]" + ex.Message, ex);
                return Fail("获取失败,请刷新重试.");
            }
        }
    }
}
