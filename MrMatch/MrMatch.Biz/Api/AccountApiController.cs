using MrMatch.Application.Company;
using MrMatch.Biz.Api.Base;
using MrMatch.Common.LogHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using MrMatch.Common.Extension;
using MrMatch.MysqlFramework.Extensions;
using MrMatch.Application.Company.Inp;
using MrMatch.Common.ReflectionHelper;
using MrMatch.Application.System;
using MrMatch.Application.SendMessage;
using System.Configuration;

namespace MrMatch.Biz.Api
{
    public class AccountApiController : SecurityBaseController
    {
        #region DI
        private readonly ISendMessageService sendMessageService;
        private readonly ICompanyService companyService;
        private readonly ILogService logService;
        private readonly ISystemService systemService;
        #endregion

        public AccountApiController(
            ICompanyService _companyService,
            ISendMessageService _sendMessageService,
            ILogService _logService,
            ISystemService _systemService)
        {
            logService = _logService;
            companyService = _companyService;
            sendMessageService = _sendMessageService;
            systemService = _systemService;
        }
        #region complete
        /// <summary>
        /// 查询企业成员发送邀请
        /// </summary>
        /// <returns></returns>
        [System.Web.Http.HttpGet]
        public async Task<IHttpActionResult> GetInvitation()
        {
            try
            {
                var oup = await companyService.GetInvitationAsync(CurrUser.Email);
                if (!oup.IsNullEntity())
                {
                    return Succcess("", null);
                }
                return Succcess("", oup);
            }
            catch (Exception ex)
            {
                logService.LogError("[查询企业成员发送邀请]" + ex.Message, ex);
                return Fail("获取失败,请重试");
            }
        }

        /// <summary>
        /// 完善信息
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        [System.Web.Http.HttpPost]
        public async Task<IHttpActionResult> RegistCompany(RegistCompanyInp form)
        {
            try
            {
                //入参基础校验
                var validate = EntityProperties.EntityValidate<RegistCompanyInp>(form);
                if (!validate.BoolResult)
                {
                    return Fail(validate.Message);
                }

                var res = await companyService.RegistCompanyAsync(form, CurrUser.PKID);
                if (!res.BoolResult)
                {
                    return Fail(res.Message);
                }
                return Succcess(res.Message);
            }
            catch (Exception ex)
            {
                logService.LogError("[完善信息]" + ex.Message, ex);
                return Fail("完善信息失败,请重试");
            }
        }
        #endregion

        #region companyInfo
        /// <summary>
        /// 获取是否审核
        /// </summary>
        /// <returns></returns>
        [System.Web.Http.HttpGet]
        public async Task<IHttpActionResult> GetApproveStatus()
        {
            //企业信息
            var company = await companyService.GetCompanyAsync(CurrUser.CompanyID);
            return Succcess("成功", new { check = company.CheckedStatus, agent = company.AgentOrNot });
        }
        /// <summary>
        /// 获取企业信息
        /// </summary>
        /// <returns></returns>
        [System.Web.Http.HttpGet]
        public async Task<IHttpActionResult> GetCompanyInfo()
        {
            try
            {
                //成员信息
                var user = await companyService.GetAccountAsync(CurrUser.PKID);
                //企业信息
                var company = await companyService.GetCompanyAsync(user.CompanyID);

                //获取签约数据
                var contracts = await companyService.GetContractListAsync(user.CompanyID);

                //获取签约配置信息
                var configs = await systemService.GetSiteConfigValueByCodeAsync("ContractHelp");

                return Succcess("", new
                {
                    companyInfo = company,
                    isAdmin = user == null ? false : user.IsAdmin,
                    contractInfoList = contracts,
                    contractHelp = configs
                });
            }
            catch (Exception ex)
            {
                logService.LogError("[获取企业信息]" + ex.Message, ex);
                return Fail("获取企业信息失败,请重试");
            }
        }
        /// <summary>
        /// 修改企业信息
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        [System.Web.Http.HttpPost]
        public async Task<IHttpActionResult> UpdateCompany(UpdateCompanyInp form)
        {
            try
            {
                //入参基础校验
                var validate = EntityProperties.EntityValidate<UpdateCompanyInp>(form);
                if (!validate.BoolResult)
                {
                    return Fail(validate.Message);
                }
                var res = await companyService.UpdateCompanyInfoAsync(form);
                if (!res.BoolResult)
                {
                    return Fail(res.Message);
                }
                return Succcess(res.Message);
            }
            catch (Exception ex)
            {
                logService.LogError("[修改企业信息]" + ex.Message, ex);
                return Fail("修改企业信息失败,请重试");
            }
        }

        /// <summary>
        /// 获取首页基本信息
        /// </summary>
        /// <returns></returns>
        [System.Web.Http.HttpGet]
        public async Task<IHttpActionResult> GetMaintainInfo()
        {
            try
            {
                //企业信息
                var company = await companyService.GetCompanyAsync(CurrUser.CompanyID);

                //获取签约数据
                var contracts = await companyService.GetContractListAsync(CurrUser.CompanyID);

                //获取签约配置信息
                var configs = await systemService.GetSiteConfigValueByCodeAsync("ContractHelp");

                return Succcess("", new
                {
                    companyInfo = company,
                    contractInfoList = contracts,
                    contractHelp = configs
                });
            }
            catch (Exception ex)
            {
                logService.LogError("[获取首页基本信息]" + ex.Message, ex);
                return Fail("获取首页基本信息失败,请重试");
            }
        }
        #endregion

        #region logo/license
        /// <summary>
        /// 获取logo信息
        /// </summary>
        /// <returns></returns>
        [System.Web.Http.HttpGet]
        public async Task<IHttpActionResult> GetCompanyLogo()
        {
            try
            {
                var logo = await companyService.GetCompanyLogoAsync(CurrUser.CompanyID);

                return Succcess("成功", new { url = logo });
            }
            catch (Exception ex)
            {
                logService.LogError("[获取logo信息]" + ex.Message, ex);
                return Fail("获取logo信息失败,请重试");
            }
        }
        /// <summary>
        /// 上传logo
        /// </summary>
        /// <param name="form">base64</param>
        /// <returns></returns>
        [System.Web.Http.HttpPost]
        public async Task<IHttpActionResult> UploadLogo(UploadLogoInp form)
        {
            try
            {
                var res = await companyService.UpdateLogoAsync(CurrUser.CompanyID, form.ImgBytes);
                if (res.BoolResult)
                {
                    return Succcess("上传成功");
                }
                return Fail(res.Message);
            }
            catch (Exception ex)
            {
                logService.LogError("[上传logo]" + ex.Message, ex);
                return Fail("上传logo失败,请重试");
            }
        }
        /// <summary>
        /// 获取license信息
        /// </summary>
        /// <returns></returns>
        [System.Web.Http.HttpGet]
        public async Task<IHttpActionResult> GetCompanyLicense()
        {
            try
            {
                var license = await companyService.GetCompanyLicenseAsync(CurrUser.CompanyID);

                return Succcess("成功", new { url = license });
            }
            catch (Exception ex)
            {
                logService.LogError("[获取license信息]" + ex.Message, ex);
                return Fail("获取license信息失败,请重试");
            }
        }
        /// <summary>
        /// 上传lisence
        /// </summary>
        /// <param name="form">base64</param>
        /// <returns></returns>
        [System.Web.Http.HttpPost]
        public async Task<IHttpActionResult> UploadLisence(UploadLisenceInp form)
        {
            try
            {
                var res = await companyService.UpdateLisenceAsync(CurrUser.CompanyID, form.ImgBytes);
                if (res.BoolResult)
                {
                    return Succcess("上传成功");
                }
                return Fail(res.Message);
            }
            catch (Exception ex)
            {
                logService.LogError("[上传lisence]" + ex.Message, ex);
                return Fail("上传lisence失败,请重试");
            }
        }
        #endregion

        #region account
        /// <summary>
        /// 获取团队成员信息
        /// </summary>
        /// <returns></returns>
        [System.Web.Http.HttpGet]
        public async Task<IHttpActionResult> GetAccountsInfo()
        {
            try
            {
                var account = await companyService.GetAccountAsync(CurrUser.PKID);

                var accounts = await companyService.GetAccountListAsync(CurrUser.CompanyID);

                return Succcess("成功", new { account = account, accountList = accounts });
            }
            catch (Exception ex)
            {
                logService.LogError("[获取团队成员信息]" + ex.Message, ex);
                return Fail("获取团队成员信息失败,请重试");
            }
        }

        /// <summary>
        /// 发送成员邀请
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        [System.Web.Http.HttpGet]
        public async Task<IHttpActionResult> SendInvite(string email)
        {
            try
            {
                if (string.IsNullOrEmpty(email))
                {
                    return Fail("入参错误");
                }

                if (!CurrUser.IsAdmin)
                {
                    return Fail("只有管理员才有权限发送邀请邮件");
                }

                //先完善邀请信息
                var inviteEmail = email + "@" + CurrUser.Domain;
                var companyName = (await companyService.GetCompanyAsync(CurrUser.CompanyID)).CompanyName;
                var invite = await companyService.SendInviteAsync(CurrUser, email);
                if (invite.BoolResult)
                {
                    var send = await sendMessageService.SendInviteEmailMessage(CurrUser.Email, inviteEmail, companyName);
                    if (!send.BoolResult)
                    {
                        return Fail(send.Message);
                    }
                    return Succcess(send.Message);
                }
                return Fail(invite.Message);
            }
            catch (Exception ex)
            {
                logService.LogError("[发送成员邀请]" + ex.Message, ex);
                return Fail("发送成员邀请失败,请重试");
            }
        }

        /// <summary>
        /// 成员激活/停用
        /// </summary>
        /// <param name="PKID"></param>
        /// <param name="onActive"></param>
        /// <returns></returns>
        [System.Web.Http.HttpGet]
        public async Task<IHttpActionResult> AccountActivity(long PKID, bool onActive)
        {
            try
            {
                if (PKID <= 0)
                {
                    return Fail("入参错误");
                }
                if (!CurrUser.IsAdmin)
                {
                    return Fail("只有管理员才有权限");
                }

                var res = await companyService.AccountActivityAsync(PKID, onActive);
                if (!res.BoolResult)
                {
                    return Fail(res.Message);
                }
                return Succcess(res.Message);
            }
            catch (Exception ex)
            {
                logService.LogError("[成员激活/停用]" + ex.Message, ex);
                return Fail("操作失败,请重试");
            }
        }
        #endregion

        #region basicinfo
        /// <summary>
        /// 获取基础信息
        /// </summary>
        /// <returns></returns>
        [System.Web.Http.HttpGet]
        public async Task<IHttpActionResult> GetBasicInfo()
        {
            try
            {
                var data = await companyService.GetAccountBasicAsync(CurrUser.PKID);
                if (!data.IsNullEntity())
                {
                    return Fail("获取失败");
                }
                return Succcess("", data);
            }
            catch (Exception ex)
            {
                logService.LogError("[获取基础信息]" + ex.Message, ex);
                return Fail("获取基础信息失败,请重试");
            }
        }

        /// <summary>
        /// 修改个人信息
        /// </summary>
        /// <param name="accountName">称呼</param>
        /// <param name="position">职位</param>
        /// <param name="wechatAccount">微信账号</param>
        /// <param name="linkin">领英账号</param>
        /// <param name="focus">专注领域</param>
        /// <param name="introduction">个人介绍</param>
        /// <returns></returns>
        [System.Web.Http.HttpPost]
        public async Task<IHttpActionResult> UpdateBasicInfo(UpdateAccountBasicInp form)
        {
            try
            {
                if (string.IsNullOrEmpty(form.AccountName))
                {
                    return Fail("称呼不能为空");
                }
                if (string.IsNullOrEmpty(form.Position))
                {
                    return Fail("职位不能为空");
                }

                if (form.AvatarBytes.Length > 0)
                {
                    await companyService.UpdateAccountAvatarAsync(CurrUser.PKID, form.AvatarBytes);
                }

                if (form.WechatContactBytes.Length > 0)
                {
                    await companyService.UpdateAccountWechatAsync(CurrUser.PKID, form.WechatContactBytes);
                }

                var res = await companyService.UpdateAccountBasicAsync(CurrUser.PKID, form.AccountName, form.Position, form.WechatAccount, form.LinkinUrl, form.FocusArea, form.Introduction);
                if (!res.BoolResult)
                {
                    return Fail(res.Message);
                }
                return Succcess(res.Message);
            }
            catch (Exception ex)
            {
                logService.LogError("[修改个人信息]" + ex.Message, ex);
                return Fail("修改个人信息失败,请重试");
            }
        }
        #endregion

        #region letter
        /// <summary>
        /// 获取邀请模板的列表
        /// </summary>
        /// <returns></returns>
        [System.Web.Http.HttpGet]
        public async Task<IHttpActionResult> GetLetterList()
        {
            try
            {
                var data = await companyService.GetLetterListAsync(CurrUser.PKID);
                return Succcess("", new { letterList = data, existingLetter = data == null ? 0 : data.Count, limiteLetter = CurrUser.TemplateLimiteCount });
            }
            catch (Exception ex)
            {
                logService.LogError("[获取邀请模板的列表]" + ex.Message, ex);
                return Fail("获取邀请模板的列表失败,请重试");
            }
        }

        /// <summary>
        /// 获取邀请模板详情
        /// </summary>
        /// <param name="PKID"></param>
        /// <returns></returns>
        [System.Web.Http.HttpGet]
        public async Task<IHttpActionResult> GetLetterTemplateDetail(long PKID)
        {
            try
            {
                var data = await companyService.GetLetterAsync(PKID);
                return Succcess("", data);
            }
            catch (Exception ex)
            {
                logService.LogError("[获取邀请模板详情]" + ex.Message, ex);
                return Fail("获取邀请模板详情失败,请重试");
            }
        }

        /// <summary>
        /// 删除邀请模板
        /// </summary>
        /// <param name="PKID"></param>
        /// <returns></returns>
        [System.Web.Http.HttpGet]
        public async Task<IHttpActionResult> DestroyLetter(long PKID)
        {
            try
            {
                if (PKID <= 0)
                {
                    return Fail("入参错误");
                }
                var data = await companyService.DestroyLetterAsync(PKID);
                return Succcess("", data);
            }
            catch (Exception ex)
            {
                logService.LogError("[获取邀请模板详情]" + ex.Message, ex);
                return Fail("获取邀请模板详情失败,请重试");
            }
        }

        /// <summary>
        /// 新增模板
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        [System.Web.Http.HttpPost]
        public async Task<IHttpActionResult> SaveLetterTemplate(AddOrUpdateLetterInp form)
        {
            try
            {
                var validate = EntityProperties.EntityValidate<AddOrUpdateLetterInp>(form);
                if (!validate.BoolResult)
                {
                    return Fail(validate.Message);
                }
                var oup = await companyService.AddOrUpdateLetterAsync(form, CurrUser);
                if (!oup.BoolResult)
                {
                    return Fail(oup.Message);
                }
                return Succcess(oup.Message);
            }
            catch (Exception ex)
            {
                logService.LogError("[新增模板]" + ex.Message, ex);
                return Fail("新增模板失败,请重试");
            }
        }
        #endregion
    }

}
