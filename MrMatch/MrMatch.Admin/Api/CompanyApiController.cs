using MrMatch.Admin.Api.Base;
using MrMatch.Application;
using MrMatch.Application.Company;
using MrMatch.Common.LogHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using MrMatch.Common.ReflectionHelper;
using MrMatch.Application.Company.Inp;

namespace MrMatch.Admin.Api
{
    public class CompanyApiController : SecurityBaseController
    {
        #region DI
        private readonly ILogService log;
        private readonly ICompanyService companyService;
        #endregion

        public CompanyApiController(
            ILogService _log,
            ICompanyService _companyService)
        {
            log = _log;
            companyService = _companyService;
        }

        #region company
        /// <summary>
        /// 分页获取企业数据
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="keyWords"></param>
        /// <returns></returns>
        [System.Web.Http.HttpGet]
        public IHttpActionResult GetCompanyInfoPagenation(int pageIndex, int pageSize, string keyWords)
        {
            try
            {
                var inp = new PagenationInput(pageIndex, pageSize, keyWords, true);
                var data = companyService.GetAllCompanyByPagenation(inp);
                return Succcess("", data);
            }
            catch (Exception ex)
            {
                log.LogError("[分页获取企业数据]" + ex.Message, ex);
                return Fail("获取失败,请刷新重试.");
            }
        }

        /// <summary>
        /// 分页获取待审核企业数据
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="keyWords"></param>
        /// <returns></returns>
        [System.Web.Http.HttpGet]
        public IHttpActionResult GetCheckCompanyByPagenation(int pageIndex, int pageSize, string keyWords)
        {
            try
            {
                var inp = new PagenationInput(pageIndex, pageSize, keyWords, true);
                var data = companyService.GetCheckCompanyByPagenation(inp);
                return Succcess("", data);
            }
            catch (Exception ex)
            {
                log.LogError("[分页获取待审核企业数据]" + ex.Message, ex);
                return Fail("获取失败,请刷新重试.");
            }
        }

        /// <summary>
        /// 分页获取最近n天新企业数据
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="keyWords"></param>
        /// <param name="days"></param>
        /// <returns></returns>
        [System.Web.Http.HttpGet]
        public IHttpActionResult GetNewCompanyByPagenation(int pageIndex, int pageSize, string keyWords, int days)
        {
            try
            {
                var inp = new PagenationInput(pageIndex, pageSize, keyWords, true);
                var data = companyService.GetNewCompanyByPagenation(inp, days);
                return Succcess("",data);
            }
            catch (Exception ex)
            {
                log.LogError("[分页获取最近n天新企业数据]" + ex.Message, ex);
                return Fail("获取失败,请刷新重试.");
            }
        }

        /// <summary>
        /// 获取企业详情
        /// </summary>
        /// <param name="PKID"></param>
        /// <returns></returns>
        [System.Web.Http.HttpGet]
        public async Task<IHttpActionResult> GetCompanyDetails(long PKID)
        {
            try
            {
                if (PKID <= 0)
                {
                    return Fail("入参错误");
                }
                var data = await companyService.GetCompanyDetailsAsync(PKID);
                return Succcess("", data);
            }
            catch (Exception ex)
            {
                log.LogError("[获取企业详情]" + ex.Message, ex);
                return Fail("获取失败,请刷新重试.");
            }
        }

        /// <summary>
        /// 新增/编辑企业信息
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        [System.Web.Http.HttpPost]
        public async Task<IHttpActionResult> SaveCompanyInfo(AddOrUpdateCompanyInp form)
        {
            try
            {
                var validate = EntityProperties.EntityValidate<AddOrUpdateCompanyInp>(form);
                if (!validate.BoolResult)
                {
                    return Fail(validate.Message);
                }
                var data = await companyService.AddOrUpdateCompanyAsync(form);
                return Succcess("", data);
            }
            catch (Exception ex)
            {
                log.LogError("[新增/编辑企业信息]" + ex.Message, ex);
                return Fail("操作失败,请刷新重试.");
            }
        }
        #endregion

        #region account
        /// <summary>
        /// 新增/编辑企业成员信息
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        [System.Web.Http.HttpPost]
        public async Task<IHttpActionResult> SaveAccountInfo(AddOrUpdateAccountInp form)
        {
            try
            {
                var validate = EntityProperties.EntityValidate<AddOrUpdateAccountInp>(form);
                if (!validate.BoolResult)
                {
                    return Fail(validate.Message);
                }
                var data = await companyService.AddOrUpdateAccountAsync(form);
                return Succcess("", data);
            }
            catch (Exception ex)
            {
                log.LogError("[新增/编辑企业成员信息]" + ex.Message, ex);
                return Fail("操作失败,请刷新重试.");
            }
        }
        #endregion

        #region contract
        /// <summary>
        /// 新增/编辑企业签约信息
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        [System.Web.Http.HttpPost]
        public async Task<IHttpActionResult> SaveContractInfo(AddOrUpdateContractInp form)
        {
            try
            {
                var validate = EntityProperties.EntityValidate<AddOrUpdateContractInp>(form);
                if (!validate.BoolResult)
                {
                    return Fail(validate.Message);
                }
                var data = await companyService.AddOrUpdateContractAsync(form);
                return Succcess("", data);
            }
            catch (Exception ex)
            {
                log.LogError("[新增/编辑企业签约信息]" + ex.Message, ex);
                return Fail("操作失败,请刷新重试.");
            }
        }
        #endregion
    }
}
