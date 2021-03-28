using MrMatch.Application.Job;
using MrMatch.Application.Job.Inp;
using MrMatch.Application.System;
using MrMatch.Application.Wechat;
using MrMatch.Biz.Api.Base;
using MrMatch.Common.LogHelper;
using MrMatch.Common.Wechat;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using MrMatch.Common.ReflectionHelper;
using MrMatch.Application;
using MrMatch.Application.Company;
using MrMatch.MysqlFramework.Extensions;
using MrMatch.Domain.Models;
using MrMatch.Application.Config;
using MrMatch.Application.Config.Oup;
using MrMatch.Application.Company.Inp;

namespace MrMatch.Biz.Api
{
    public class JobDeliveryApiController : SecurityBaseController
    {
        #region DI
        private readonly IJobService jobService;
        private readonly IWechatService wechatService;
        private readonly ICompanyService companyService;
        private readonly IConfigService configService;
        private readonly ISystemService systemService;
        private readonly ILogService logService;
        #endregion

        public JobDeliveryApiController(
            ILogService _logService,
            IWechatService _wechatService,
            ISystemService _systemService,
            ICompanyService _companyService,
            IConfigService _configService,
            IJobService _jobService)
        {
            logService = _logService;
            jobService = _jobService;
            wechatService = _wechatService;
            systemService = _systemService;
            companyService = _companyService;
            configService = _configService;
        }

        /// <summary>
        /// 新增/修改职位
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        [System.Web.Http.HttpPost]
        public async Task<IHttpActionResult> SaveJob(JobPublishInp form)
        {
            try
            {
                form.BasicCompanyID = CurrUser.CompanyID;
                form.AccountID = CurrUser.PKID;
                if (!form.AgentJob)
                {
                    form.JobCompanyID = CurrUser.CompanyID;
                }
                var validate = EntityProperties.EntityValidate<JobPublishInp>(form);
                if (!validate.BoolResult)
                {
                    return Fail(validate.Message);
                }

                var res = await jobService.AddOrUpdateJobAsync(form);
                if (!res.BoolResult)
                {
                    return Fail(res.Message);
                }
                //回写生成小程序二维码

                //获取token
                var appid = await systemService.GetSiteConfigValueByCodeAsync("WxAppID");
                var secret = await systemService.GetSiteConfigValueByCodeAsync("WxAppSecret");
                var token = await wechatService.GetAccessToken(appid, secret);
                if (!token.IsOK)
                {
                    logService.LogWarning("[新增/修改职位]" + token.Message);
                    return Succcess("操作成功");
                }

                //上传oss并回写url地址
                MiniWeChatPic setting = new MiniWeChatPic();
                setting.page = "id=" + res.PKID.ToString();
                setting.page = ConfigurationManager.AppSettings["miniPic"];
                setting.width = 280;
                await jobService.GetMiniPicUrlAsync(res.PKID, token.Token, setting);
                return Succcess("操作成功");
            }
            catch (Exception ex)
            {
                logService.LogError("[新增/修改职位]" + ex.Message, ex);
                return Fail("操作失败,请重试");
            }
        }

        /// <summary>
        /// 新增/修改草稿
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        [System.Web.Http.HttpPost]
        public async Task<IHttpActionResult> SaveJobDraft(JobDraftInp form)
        {
            try
            {
                form.BasicCompanyID = CurrUser.CompanyID;
                form.AccountID = CurrUser.PKID;
                var validate = EntityProperties.EntityValidate<JobDraftInp>(form);
                if (!validate.BoolResult)
                {
                    return Fail(validate.Message);
                }

                var res = await jobService.AddOrUpdateJobDraftAsync(form, CurrUser);
                if (!res.BoolResult)
                {
                    return Fail(res.Message);
                }
                return Succcess("操作成功");
            }
            catch (Exception ex)
            {
                logService.LogError("[新增/修改草稿]" + ex.Message, ex);
                return Fail("操作失败,请重试");
            }
        }

        /// <summary>
        /// 获取岗位列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="type">true:下架 false:上架</param>
        /// <param name="jobID"></param>
        /// <param name="jobName"></param>
        /// <returns></returns>
        [System.Web.Http.HttpGet]
        public async Task<IHttpActionResult> GetActivityJobList(int pageIndex, int pageSize, bool type, string jobID, string jobName)
        {
            try
            {
                var pagenation = new PagenationInput(pageIndex, pageSize, "", false);
                long id = 0;
                long.TryParse(jobID, out id);
                var data = await jobService.GetActivityJobListAsync(pagenation, type, id, jobName, CurrUser.CompanyID, CurrUser.PKID);
                return Succcess("", data);
            }
            catch (Exception ex)
            {

                logService.LogError("[获取岗位列表]" + ex.Message, ex);
                return Fail("获取失败,请重试");
            }
        }

        /// <summary>
        /// 获取草稿箱列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="jobName"></param>
        /// <returns></returns>
        public async Task<IHttpActionResult> GetJobDraftList(int pageIndex, int pageSize, string jobName)
        {
            try
            {
                var pagenation = new PagenationInput(pageIndex, pageSize, jobName, false);
                var data = await jobService.GetJobDraftListAsync(pagenation, jobName, CurrUser.CompanyID, CurrUser.PKID);
                return Succcess("", data);
            }
            catch (Exception ex)
            {
                logService.LogError("[获取草稿箱列表]" + ex.Message, ex);
                return Fail("获取失败,请重试");
            }
        }

        /// <summary>
        /// 修改上架/下架
        /// </summary>
        /// <param name="type">true:下架 false:上架</param>
        /// <param name="jobID"></param>
        /// <returns></returns>
        [System.Web.Http.HttpGet]
        public async Task<IHttpActionResult> UpdateActivityJobStatus(bool type, long jobID)
        {
            try
            {
                var res = await jobService.UpdateJobStatusAsync(jobID, type, CurrUser.PKID);
                if (!res.BoolResult)
                {
                    return Fail(res.Message);
                }
                return Succcess(res.Message);
            }
            catch (Exception ex)
            {

                logService.LogError("[修改上架/下架]" + ex.Message, ex);
                return Fail("操作失败,请重试");
            }
        }

        /// <summary>
        /// 获取发布职位初始化信息
        /// </summary>
        /// <returns></returns>
        [System.Web.Http.HttpGet]
        public async Task<IHttpActionResult> GetBasicInfo()
        {
            try
            {
                var basicCom = await companyService.GetCompanyAsync(CurrUser.CompanyID);

                var accountInfo = await companyService.GetAccountBasicAsync(CurrUser.PKID);

                if (basicCom.AgentOrNot)
                {
                    var agentComs = await companyService.GetAgentCompanyListAsync(CurrUser.CompanyID);
                    if (!agentComs.IsNullEntity())
                    {
                        return Succcess("成功", new
                        {
                            type = basicCom.AgentOrNot,
                            accountID = CurrUser.PKID,
                            basicId = CurrUser.CompanyID,
                            basicCom = basicCom.CompanyName,
                            basicShortName = basicCom.ShortName,
                            agentCom = new List<TP_AgentCompany>(),
                            phone = accountInfo.CellPhone,
                            wechat = accountInfo.WechatAccount,
                            linkin = accountInfo.LinkinUrl,
                            focus = accountInfo.FocusArea,
                            introduction = accountInfo.Introduction
                        });
                    }

                    var oupAgent = agentComs.Select(x => new
                    {
                        ID = x.PKID,
                        Name = x.CompanyName,
                        ShortName = x.ShortName
                    });
                    return Succcess("成功", new
                    {
                        type = basicCom.AgentOrNot,
                        basicId = CurrUser.CompanyID,
                        accountID = CurrUser.PKID,
                        basicCom = basicCom.CompanyName,
                        basicShortName = basicCom.ShortName,
                        agentCom = oupAgent,
                        phone = accountInfo.CellPhone,
                        wechat = accountInfo.WechatAccount,
                        linkin = accountInfo.LinkinUrl,
                        focus = accountInfo.FocusArea,
                        introduction = accountInfo.Introduction
                    });
                }

                return Succcess("成功", new
                {
                    type = basicCom.AgentOrNot,
                    basicId = CurrUser.CompanyID,
                    accountID = CurrUser.PKID,
                    basicCom = basicCom.CompanyName,
                    basicShortName = basicCom.ShortName,
                    agentCom = new List<TP_AgentCompany>(),
                    phone = accountInfo.CellPhone,
                    wechat = accountInfo.WechatAccount,
                    linkin = accountInfo.LinkinUrl,
                    focus = accountInfo.FocusArea,
                    introduction = accountInfo.Introduction
                });
            }
            catch (Exception ex)
            {

                logService.LogError("[获取发布职位初始化信息]" + ex.Message, ex);
                return Fail("获取失败,请重试");
            }
        }

        /// <summary>
        /// 获取技能
        /// </summary>
        /// <param name="PKID"></param>
        /// <returns></returns>
        [System.Web.Http.HttpGet]
        public async Task<IHttpActionResult> GetRelationSkills(long PKID)
        {
            try
            {
                if (PKID <= 0)
                {
                    return Fail("入参错误");
                }

                var ids = await configService.GetFunctionSkillAsync(PKID);
                var allSkills = await configService.GetAllSkillsListAsync();
                var skills = new List<AllSkillListOup>();
                if (ids.Count <= 0)
                {
                    return Succcess("", skills);
                }
                skills = allSkills.Where(x => x.OnEnabled == true && ids.Contains(x.PKID)).ToList();
                return Succcess("", skills);
            }
            catch (Exception ex)
            {

                logService.LogError("[获取技能]" + ex.Message, ex);
                return Fail("获取失败,请重试");
            }
        }

        /// <summary>
        /// 获取职位信息
        /// </summary>
        /// <param name="PKID"></param>
        /// <returns></returns>
        [System.Web.Http.HttpGet]
        public async Task<IHttpActionResult> GetJobInfo(long PKID, long accountID)
        {
            try
            {
                if (PKID <= 0 || accountID == 0)
                {
                    return Fail("入参错误");
                }
                //验证是否本人操作
                if (accountID != CurrUser.PKID)
                {
                    return Fail("只能查看自己的数据");
                }
                var data = await jobService.GetJobDetailsAsync(PKID);
                return Succcess("", new { companyName = data.CompanyName, jobData = data });
            }
            catch (Exception ex)
            {
                logService.LogError("[获取职位信息]" + ex.Message, ex);
                return Fail("获取失败,请重试");
            }
        }

        /// <summary>
        /// 获取草稿信息
        /// </summary>
        /// <param name="PKID"></param>
        /// <returns></returns>
        [System.Web.Http.HttpGet]
        public async Task<IHttpActionResult> GetJobDraftInfo(long PKID, long accountID)
        {
            try
            {
                if (PKID <= 0 || accountID == 0)
                {
                    return Fail("入参错误");
                }
                //验证是否本人操作
                if (accountID != CurrUser.PKID)
                {
                    return Fail("只能查看自己的数据");
                }
                var data = await jobService.GetJobDraftDetailsAsync(PKID);
                return Succcess("", data);
            }
            catch (Exception ex)
            {
                logService.LogError("[获取草稿信息]" + ex.Message, ex);
                return Fail("获取失败,请重试");
            }
        }

        /// <summary>
        /// 删除草稿
        /// </summary>
        /// <param name="PKID"></param>
        /// <returns></returns>
        [System.Web.Http.HttpGet]
        public async Task<IHttpActionResult> DestroyJobDraft(long PKID, long accountID)
        {
            try
            {
                if (PKID <= 0 || accountID == 0)
                {
                    return Fail("入参错误");
                }
                //验证是否本人操作
                if (accountID != CurrUser.PKID)
                {
                    return Fail("只能本人操作");
                }

                var res = await jobService.DestroyJobDraftAsync(PKID);
                if (!res.BoolResult)
                {
                    return Fail(res.Message);
                }
                return Succcess(res.Message);
            }
            catch (Exception ex)
            {
                logService.LogError("[删除草稿]" + ex.Message, ex);
                return Fail("删除草稿失败,请重试");
            }
        }

        /// <summary>
        /// 猎头企业列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="companyName"></param>
        /// <returns></returns>
        [System.Web.Http.HttpGet]
        public IHttpActionResult GetAgentCompanyList(int pageIndex, int pageSize, string companyName)
        {
            try
            {
                var pagenation = new PagenationInput(pageIndex, pageSize, companyName, false);
                var res = companyService.GetAgentCompanyByPagenation(pagenation, CurrUser.CompanyID, CurrUser.PKID);
                return Succcess("", res);
            }
            catch (Exception ex)
            {
                logService.LogError("[猎头企业列表]" + ex.Message, ex);
                return Fail("获取失败,请重试");
            }
        }

        /// <summary>
        /// 根据id获取猎头企业
        /// </summary>
        /// <param name="PKID"></param>
        /// <returns></returns>
        [System.Web.Http.HttpGet]
        public async Task<IHttpActionResult> GetAgentCompanyByID(long PKID)
        {
            try
            {
                if (PKID <= 0)
                {
                    return Fail("入参错误");
                }
                var res = await companyService.GetAgentCompanyByIDAsync(PKID);
                return Succcess("", res);
            }
            catch (Exception ex)
            {
                logService.LogError("[根据id获取猎头企业]" + ex.Message, ex);
                return Fail("获取失败,请重试");
            }
        }

        /// <summary>
        /// 新增/编辑猎头企业
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        [System.Web.Http.HttpPost]
        public async Task<IHttpActionResult> SaveAgentCompany(AddOrUpdateAgentCompanyInp form)
        {
            try
            {
                var validate = EntityProperties.EntityValidate<AddOrUpdateAgentCompanyInp>(form);
                if (!validate.BoolResult)
                {
                    return Fail(validate.Message);
                }

                var res = await companyService.AddOrUpdateAgentCompanyAsync(form, CurrUser.CompanyID, CurrUser.PKID);
                if (!res.BoolResult)
                {
                    return Fail(res.Message);
                }
                return Succcess(res.Message);
            }
            catch (Exception ex)
            {
                logService.LogError("[新增/编辑猎头企业]" + ex.Message, ex);
                return Fail("新增/编辑猎头企业失败,请重试");
            }
        }

        /// <summary>
        /// 获取职位投递简历列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="PKID"></param>
        /// <returns></returns>
        [System.Web.Http.HttpGet]
        public async Task<IHttpActionResult> GetJobProfilesPagenation(int pageIndex, int pageSize, long PKID)
        {
            try
            {
                if (PKID <= 0)
                {
                    return Fail("入参错误");
                }

                var job = await jobService.GetJobDetailsAsync(PKID);

                var pagenation = new PagenationInput(pageIndex, pageSize, "", false);
                var data = await jobService.GetJobProfilesByPagenationAsync(pagenation, PKID, CurrUser.PKID);
                return Succcess("", new
                {
                    jobName = job.JobName,
                    lastModifiedTime = job.LastModifiedTime,
                    companyName = job.CompanyName,
                    status = job.ActiveStatus,
                    profiles = data
                });
            }
            catch (Exception ex)
            {
                logService.LogError("[获取职位投递简历列表]" + ex.Message, ex);
                return Fail("获取失败,请重试");
            }
        }
    }
}
