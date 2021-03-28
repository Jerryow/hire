using MrMatch.Application.Config;
using MrMatch.Biz.Api.Base;
using MrMatch.Common.LogHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace MrMatch.Biz.Api
{
    public class CandidateApiController : SecurityBaseController
    {
        #region DI
        private readonly IConfigService configService;
        private readonly ILogService logService;
        #endregion

        public CandidateApiController(
            IConfigService _configService,
            ILogService _logService)
        {
            configService = _configService;
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
        public async Task<IHttpActionResult> GetDistrictForCascader()
        {
            try
            {
                //var hot = await configService.GetHotDistrictAsync();
                var cascader = await configService.GetProvinceListAsync(true);
                return Succcess("成功", cascader);
            }
            catch (Exception ex)
            {
                logService.LogError("[获取城市]" + ex.Message, ex);
                return Fail("获取失败,请刷新重试.");
            }
        }
    }
}
