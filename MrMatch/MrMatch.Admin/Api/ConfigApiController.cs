using MrMatch.Admin.Api.Base;
using MrMatch.Application;
using MrMatch.Application.Config;
using MrMatch.Application.Config.Inp;
using MrMatch.Application.System;
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
    public class ConfigApiController : SecurityBaseController
    {
        #region DI
        private readonly ILogService log;
        private readonly ISystemService systemService;
        private readonly IConfigService configService;


        public ConfigApiController(
            ILogService _log,
            ISystemService _systemService,
            IConfigService _configService)
        {
            log = _log;
            systemService = _systemService;
            configService = _configService;
        }
        #endregion


        #region country
        /// <summary>
        /// 分页获取国家数据
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="keyWords">搜索关键字</param>
        /// <returns></returns>
        [System.Web.Http.HttpGet]
        public IHttpActionResult GetCountryPagenation(int pageIndex, int pageSize, string keyWords)
        {
            try
            {
                var pagenation = new PagenationInput(pageIndex, pageSize, keyWords, false);

                var data = configService.GetCountryByPagenation(pagenation);
                //log.LogDebug(Newtonsoft.Json.JsonConvert.SerializeObject(data));
                return Succcess("成功.", data);
            }
            catch (Exception ex)
            {
                log.LogError("[分页获取国家数据]" + ex.Message, ex);
                return Fail("获取失败,请刷新重试.");
            }
        }

        /// <summary>
        /// 获取单个国家数据
        /// </summary>
        /// <param name="PKID"></param>
        /// <returns></returns>
        [System.Web.Http.HttpGet]
        public async Task<IHttpActionResult> GetCountryByID(long PKID)
        {
            try
            {
                if (PKID <= 0)
                {
                    return Fail("入参错误");
                }
                var country = await configService.GetCountryByIDAsync(PKID);
                return Succcess("成功", country);
            }
            catch (Exception ex)
            {
                log.LogError("[获取单个国家数据]" + ex.Message, ex);
                return Fail("获取失败,请刷新重试.");
            }

        }

        /// <summary>
        /// 新增国家数据
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        [System.Web.Http.HttpPost]
        public async Task<IHttpActionResult> AddCountry(AddOrUpdateCountryInp form)
        {
            try
            {
                
                var res = await configService.AddOrUpdateCountryAsync(form);
                if (res.BoolResult)
                {
                    return Succcess(res.Message);
                }
                return Fail(res.Message);
            }
            catch (Exception ex)
            {
                log.LogError("[新增国家数据]" + ex.Message, ex);
                return Fail("新增国家数据失败,请刷新重试.");
            }
        }

        /// <summary>
        /// 修改国家数据
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        [System.Web.Http.HttpPost]
        public async Task<IHttpActionResult> UpdateCountry(AddOrUpdateCountryInp form)
        {
            try
            {
                var res = await configService.AddOrUpdateCountryAsync(form);
                if (res.BoolResult)
                {
                    return Succcess(res.Message);
                }
                return Fail(res.Message);
            }
            catch (Exception ex)
            {
                log.LogError("[修改国家数据]" + ex.Message, ex);
                return Fail("修改国家数据失败,请刷新重试.");
            }
        }

        /// <summary>
        /// 获取所有国家数据(PKID+Name)
        /// </summary>
        /// <returns></returns>
        [System.Web.Http.HttpGet]
        public async Task<IHttpActionResult> GetAllCountry()
        {
            try
            {
                var country = await configService.GetAllCountryAsync();
                return Succcess("成功", country);
            }
            catch (Exception ex)
            {
                log.LogError("[获取所有国家数据(PKID+Name)]" + ex.Message, ex);
                return Fail("获取失败,请刷新重试.");
            }

        }
        #endregion

        #region district
        /// <summary>
        /// 分页获取地区数据
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="keyWords">搜索关键字</param>
        /// <returns></returns>
        [System.Web.Http.HttpGet]
        public IHttpActionResult GetDistrictPagenation(int pageIndex, int pageSize, string keyWords)
        {
            try
            {
                var pagenation = new PagenationInput(pageIndex, pageSize, keyWords, false);

                var data = configService.GetDistrictByPagenation(pagenation);
                //log.LogDebug(Newtonsoft.Json.JsonConvert.SerializeObject(data));
                return Succcess("成功.", data);
            }
            catch (Exception ex)
            {
                log.LogError("[分页获取地区数据]" + ex.Message, ex);
                return Fail("获取失败,请刷新重试.");
            }
        }

        /// <summary>
        /// 获取单个地区数据
        /// </summary>
        /// <param name="PKID"></param>
        /// <returns></returns>
        [System.Web.Http.HttpGet]
        public async Task<IHttpActionResult> GetDistrictById(long PKID)
        {
            try
            {
                var country = await configService.GetDistrictByIDAsync(PKID);
                return Succcess("成功", country);
            }
            catch (Exception ex)
            {
                log.LogError("[地区数据]" + ex.Message, ex);
                return Fail("获取失败,请刷新重试.");
            }

        }

        /// <summary>
        /// 新增地区数据
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        [System.Web.Http.HttpPost]
        public async Task<IHttpActionResult> AddDistrict(AddOrUpdateDistrictInp form)
        {
            try
            {
                var res = await configService.AddOrUpdateDistrictAsync(form);
                if (res.BoolResult)
                {
                    return Succcess(res.Message);
                }
                return Fail(res.Message);
            }
            catch (Exception ex)
            {
                log.LogError("[新增地区数据]" + ex.Message, ex);
                return Fail("新增地区数据失败,请刷新重试.");
            }
        }

        /// <summary>
        /// 修改地区数据
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        [System.Web.Http.HttpPost]
        public async Task<IHttpActionResult> UpdateDistrict(AddOrUpdateDistrictInp form)
        {
            try
            {
                var res = await configService.AddOrUpdateDistrictAsync(form);
                if (res.BoolResult)
                {
                    return Succcess(res.Message);
                }
                return Fail(res.Message);
            }
            catch (Exception ex)
            {
                log.LogError("[修改地区数据]" + ex.Message, ex);
                return Fail("修改地区数据失败,请刷新重试.");
            }
        }

        /// <summary>
        /// 获取所有地区数据(PKID+Name)
        /// </summary>
        /// <returns></returns>
        [System.Web.Http.HttpGet]
        public async Task<IHttpActionResult> GetAllDistrict()
        {
            try
            {
                var country = await configService.GetAllDistrictAsync();
                return Succcess("成功", country);
            }
            catch (Exception ex)
            {
                log.LogError("[获取所有地区数据(PKID+Name)]" + ex.Message, ex);
                return Fail("获取失败,请刷新重试.");
            }

        }

        /// <summary>
        /// 设置地区二级目录缓存
        /// </summary>
        /// <returns></returns>
        [System.Web.Http.HttpGet]
        public async Task<IHttpActionResult> SetBasicCityCache()
        {
            try
            {
                var data = await configService.GetProvinceListAsync(false);
                Application.Cache.SetCacheHelper.SetBasicCity(data);
                return Succcess("成功");
            }
            catch (Exception ex)
            {
                log.LogError("[设置地区二级目录缓存]" + ex.Message, ex);
                return Fail("获取失败,请刷新重试.");
            }
        }
        #endregion

        #region tag
        /// <summary>
        /// 分页获取标签数据
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="keyWords">搜索关键字</param>
        /// <returns></returns>
        [System.Web.Http.HttpGet]
        public IHttpActionResult GetTagPagenation(int pageIndex, int pageSize, string keyWords)
        {
            try
            {
                var pagenation = new PagenationInput(pageIndex, pageSize, keyWords, false);

                var data = configService.GetTagsByPagenation(pagenation);
                //log.LogDebug(Newtonsoft.Json.JsonConvert.SerializeObject(data));
                return Succcess("成功.", data);
            }
            catch (Exception ex)
            {
                log.LogError("[分页获取标签数据]" + ex.Message, ex);
                return Fail("获取失败,请刷新重试.");
            }
        }

        /// <summary>
        /// 获取单个标签数据
        /// </summary>
        /// <param name="PKID"></param>
        /// <returns></returns>
        [System.Web.Http.HttpGet]
        public async Task<IHttpActionResult> GetTagById(long PKID)
        {
            try
            {
                var res = await configService.GetTagsByIDAsync(PKID);
                return Succcess("成功", res);
            }
            catch (Exception ex)
            {
                log.LogError("[获取单个标签数据]" + ex.Message, ex);
                return Fail("获取失败,请刷新重试.");
            }

        }

        /// <summary>
        /// 新增标签数据
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        [System.Web.Http.HttpPost]
        public async Task<IHttpActionResult> AddTags(AddOrUpdateTagsInp form)
        {
            try
            {
                var res = await configService.AddOrUpdateTagsAsync(form);
                if (res.BoolResult)
                {
                    return Succcess(res.Message);
                }
                return Fail(res.Message);
            }
            catch (Exception ex)
            {
                log.LogError("[新增标签数据]" + ex.Message, ex);
                return Fail("新增标签数据失败,请刷新重试.");
            }
        }

        /// <summary>
        /// 修改标签数据
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        [System.Web.Http.HttpPost]
        public async Task<IHttpActionResult> UpdateTags(AddOrUpdateTagsInp form)
        {
            try
            {
                var res = await configService.AddOrUpdateTagsAsync(form);
                if (res.BoolResult)
                {
                    return Succcess(res.Message);
                }
                return Fail(res.Message);
            }
            catch (Exception ex)
            {
                log.LogError("[修改标签数据]" + ex.Message, ex);
                return Fail("修改标签数据失败,请刷新重试.");
            }
        }

        /// <summary>
        /// 获取所有父标签数据
        /// </summary>
        /// <returns></returns>
        [System.Web.Http.HttpGet]
        public async Task<IHttpActionResult> GetAllParentTag()
        {
            try
            {
                var res = await configService.GetAllParentTagsListAsync();
                return Succcess("成功", res);
            }
            catch (Exception ex)
            {
                log.LogError("[获取所有父标签数据]" + ex.Message, ex);
                return Fail("获取失败,请刷新重试.");
            }

        }

        /// <summary>
        /// 获取所有标签数据(PKID+Name)
        /// </summary>
        /// <returns></returns>
        [System.Web.Http.HttpGet]
        public async Task<IHttpActionResult> GetAllTag()
        {
            try
            {
                var res = await configService.GetAllTagsListAsync();
                return Succcess("成功", res);
            }
            catch (Exception ex)
            {
                log.LogError("[获取所有标签数据(PKID+Name)]" + ex.Message, ex);
                return Fail("获取失败,请刷新重试.");
            }

        }
        #endregion

        #region skill
        /// <summary>
        /// 分页获取技能数据
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="keyWords">搜索关键字</param>
        /// <returns></returns>
        [System.Web.Http.HttpGet]
        public IHttpActionResult GetSkillPagenation(int pageIndex, int pageSize, string keyWords)
        {
            try
            {
                var pagenation = new PagenationInput(pageIndex, pageSize, keyWords, false);

                var data = configService.GetSkillsByPagenation(pagenation);
                //log.LogDebug(Newtonsoft.Json.JsonConvert.SerializeObject(data));
                return Succcess("成功.", data);
            }
            catch (Exception ex)
            {
                log.LogError("[分页获取技能数据]" + ex.Message, ex);
                return Fail("获取失败,请刷新重试.");
            }
        }

        /// <summary>
        /// 获取单个技能数据
        /// </summary>
        /// <param name="PKID"></param>
        /// <returns></returns>
        [System.Web.Http.HttpGet]
        public async Task<IHttpActionResult> GetSkillById(long PKID)
        {
            try
            {
                var country = await configService.GetSkillsByIDAsync(PKID);
                return Succcess("成功", country);
            }
            catch (Exception ex)
            {
                log.LogError("[获取单个技能数据]" + ex.Message, ex);
                return Fail("获取失败,请刷新重试.");
            }

        }

        /// <summary>
        /// 新增技能数据
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        [System.Web.Http.HttpPost]
        public async Task<IHttpActionResult> AddSkill(AddOrUpdateSkillsInp form)
        {
            try
            {
                var res = await configService.AddOrUpdateSkillsAsync(form);
                if (res.BoolResult)
                {
                    return Succcess(res.Message);
                }
                return Fail(res.Message);
            }
            catch (Exception ex)
            {
                log.LogError("[新增技能数据]" + ex.Message, ex);
                return Fail("新增国技能数据,请刷新重试.");
            }
        }

        /// <summary>
        /// 修改技能数据
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        [System.Web.Http.HttpPost]
        public async Task<IHttpActionResult> UpdateSkill(AddOrUpdateSkillsInp form)
        {
            try
            {
                var res = await configService.AddOrUpdateSkillsAsync(form);
                if (res.BoolResult)
                {
                    return Succcess(res.Message);
                }
                return Fail(res.Message);
            }
            catch (Exception ex)
            {
                log.LogError("[修改技能数据]" + ex.Message, ex);
                return Fail("修改技能数据失败,请刷新重试.");
            }
        }

        /// <summary>
        /// 获取所有技能数据(PKID+Name)
        /// </summary>
        /// <returns></returns>
        [System.Web.Http.HttpGet]
        public async Task<IHttpActionResult> GetAllSkill()
        {
            try
            {
                var res = await configService.GetAllSkillsListAsync();
                return Succcess("成功", res);
            }
            catch (Exception ex)
            {
                log.LogError("[获取所有技能数据(PKID+Name)]" + ex.Message, ex);
                return Fail("获取失败,请刷新重试.");
            }

        }
        #endregion

        #region function
        /// <summary>
        /// 分页获取职能数据
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="keyWords">搜索关键字</param>
        /// <returns></returns>
        [System.Web.Http.HttpGet]
        public IHttpActionResult GetFunctionPagenation(int pageIndex, int pageSize, string keyWords)
        {
            try
            {
                var pagenation = new PagenationInput(pageIndex, pageSize, keyWords, false);

                var data = configService.GetFunctionByPagenation(pagenation);
                //log.LogDebug(Newtonsoft.Json.JsonConvert.SerializeObject(data));
                return Succcess("成功.", data);
            }
            catch (Exception ex)
            {
                log.LogError("[分页获取职能数据]" + ex.Message, ex);
                return Fail("获取失败,请刷新重试.");
            }
        }

        /// <summary>
        /// 获取单个职能数据
        /// </summary>
        /// <param name="PKID"></param>
        /// <returns></returns>
        [System.Web.Http.HttpGet]
        public async Task<IHttpActionResult> GetFunctionById(long PKID)
        {
            try
            {
                var country = await configService.GetFunctionByIDAsync(PKID);
                return Succcess("成功", country);
            }
            catch (Exception ex)
            {
                log.LogError("[获取单个职能数据]" + ex.Message, ex);
                return Fail("获取失败,请刷新重试.");
            }

        }

        /// <summary>
        /// 新增职能数据
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        [System.Web.Http.HttpPost]
        public async Task<IHttpActionResult> AddFunction(AddOrUpdateFunctionInp form)
        {
            try
            {
                var res = await configService.AddOrUpdateFunctionAsync(form);
                if (res.BoolResult)
                {
                    return Succcess(res.Message);
                }
                return Fail(res.Message);
            }
            catch (Exception ex)
            {
                log.LogError("[新增职能数据]" + ex.Message, ex);
                return Fail("新增职能数据,请刷新重试.");
            }
        }

        /// <summary>
        /// 修改职能数据
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        [System.Web.Http.HttpPost]
        public async Task<IHttpActionResult> UpdateFunction(AddOrUpdateFunctionInp form)
        {
            try
            {
                var res = await configService.AddOrUpdateFunctionAsync(form);
                if (res.BoolResult)
                {
                    return Succcess(res.Message);
                }
                return Fail(res.Message);
            }
            catch (Exception ex)
            {
                log.LogError("[修改职能数据]" + ex.Message, ex);
                return Fail("修改职能数据失败,请刷新重试.");
            }
        }

        /// <summary>
        /// 获取所有职能数据(PKID+Name)
        /// </summary>
        /// <returns></returns>
        [System.Web.Http.HttpGet]
        public async Task<IHttpActionResult> GetAllFunction()
        {
            try
            {
                var res = await configService.GetAllFunctionListAsync();
                return Succcess("成功", res);
            }
            catch (Exception ex)
            {
                log.LogError("[获取所有职能数据(PKID+Name)]" + ex.Message, ex);
                return Fail("获取失败,请刷新重试.");
            }

        }

        /// <summary>
        /// 增加职能技能关联关系
        /// </summary>
        /// <returns></returns>
        [System.Web.Http.HttpGet]
        public async Task<IHttpActionResult> AddFunctionSkill(long functionID, string skillIDs)
        {
            try
            {
                var res = await configService.AddFunctionSkillsAsync(functionID, skillIDs);
                if (res.BoolResult)
                {
                    return Succcess(res.Message);
                }
                return Fail(res.Message);
            }
            catch (Exception ex)
            {
                log.LogError("[获取所有职能数据(PKID+Name)]" + ex.Message, ex);
                return Fail("获取失败,请刷新重试.");
            }
        }

        /// <summary>
        /// 获取职能技能关联关系
        /// </summary>
        /// <returns></returns>
        [System.Web.Http.HttpGet]
        public async Task<IHttpActionResult> GetFunctionSkill(long functionID)
        {
            try
            {
                var res = await configService.GetFunctionSkillAsync(functionID);
                
                return Succcess("成功",res);
            }
            catch (Exception ex)
            {
                log.LogError("[获取所有职能数据(PKID+Name)]" + ex.Message, ex);
                return Fail("获取失败,请刷新重试.");
            }
        }

        /// <summary>
        /// 设置职能三级目录缓存
        /// </summary>
        /// <returns></returns>
        [System.Web.Http.HttpGet]
        public async Task<IHttpActionResult> SetBasicFunctionCache()
        {
            try
            {
                var data = await configService.GetFunctionForCascaderAsync(false);
                Application.Cache.SetCacheHelper.SetFunctionCascader(data);
                return Succcess("成功");
            }
            catch (Exception ex)
            {
                log.LogError("[设置地区二级目录缓存]" + ex.Message, ex);
                return Fail("获取失败,请刷新重试.");
            }
        }
        #endregion

        #region test
        /// <summary>
        /// 分页获取国家数据
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="keyWords">搜索关键字</param>
        /// <returns></returns>
        [System.Web.Http.HttpGet]
        public IHttpActionResult test1()
        {
            try
            {
                
                return Succcess("成功.", "1111111111111");
            }
            catch (Exception ex)
            {
                log.LogError("[分页获取国家数据]" + ex.Message, ex);
                return Fail("获取失败,请刷新重试.");
            }
        }
        /// <summary>
        /// 分页获取国家数据
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="keyWords">搜索关键字</param>
        /// <returns></returns>
        [System.Web.Http.HttpGet]
        public IHttpActionResult test2()
        {
            try
            {
                return Succcess("成功.", "222222222");
            }
            catch (Exception ex)
            {
                log.LogError("[分页获取国家数据]" + ex.Message, ex);
                return Fail("获取失败,请刷新重试.");
            }
        }
        #endregion
    }
}
