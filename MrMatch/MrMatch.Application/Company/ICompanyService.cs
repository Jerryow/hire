using MrMatch.Application.Company.Inp;
using MrMatch.Application.Company.Oup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MrMatch.Domain.Models;

namespace MrMatch.Application.Company
{
    public interface ICompanyService
    {
        /// <summary>
        /// 完善企业信息
        /// </summary>
        /// <param name="input"></param>
        /// <param name="currentID">企业成员id</param>
        /// <returns></returns>
        Task<BoolMessageOup> RegistCompanyAsync(RegistCompanyInp input, long currentID);

        /// <summary>
        /// 获取当前登陆用户的成员邀请数据
        /// </summary>
        /// <param name="email">邮箱</param>
        /// <returns></returns>
        Task<List<InviteCompaniesOup>> GetInvitationAsync(string email);

        /// <summary>
        /// 获取企业信息
        /// </summary>
        /// <param name="PKID"></param>
        /// <returns></returns>
        Task<CompanyOup> GetCompanyAsync(long PKID);

        /// <summary>
        /// 获取企业Logo信息
        /// </summary>
        /// <param name="PKID"></param>
        /// <returns></returns>
        Task<string> GetCompanyLogoAsync(long PKID);

        /// <summary>
        /// 获取企业执照信息
        /// </summary>
        /// <param name="PKID"></param>
        /// <returns></returns>
        Task<string> GetCompanyLicenseAsync(long PKID);

        /// <summary>
        /// 获取企业成员信息
        /// </summary>
        /// <param name="PKID"></param>
        /// <returns></returns>
        Task<AccountOup> GetAccountAsync(long PKID);

        /// <summary>
        /// 获取企业所有成员信息
        /// </summary>
        /// <param name="companyID">企业id</param>
        /// <returns></returns>
        Task<List<AccountOup>> GetAccountListAsync(long companyID);

        /// <summary>
        /// 获取企业签约数据
        /// </summary>
        /// <param name="companyID"></param>
        /// <returns></returns>
        Task<List<TP_CompanyContract>> GetContractListAsync(long companyID);

        /// <summary>
        /// 修改企业信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<BoolMessageOup> UpdateCompanyInfoAsync(UpdateCompanyInp input);

        /// <summary>
        /// 发送成员邀请
        /// </summary>
        /// <param name="currentAccount"></param>
        /// <param name="email"></param>
        /// <returns></returns>
        Task<BoolMessageOup> SendInviteAsync(TP_Account currentAccount,string email);

        /// <summary>
        /// 成员激活/停用
        /// </summary>
        /// <param name="PKID"></param>
        /// <param name="onActive"></param>
        /// <returns></returns>
        Task<BoolMessageOup> AccountActivityAsync(long PKID, bool onActive);

        /// <summary>
        /// 修改个人信息
        /// </summary>
        /// <param name="PKID"></param>
        /// <param name="AccountName"></param>
        /// <param name="Position"></param>
        /// <param name="wechatAccount"></param>
        /// <param name="linkin"></param>
        /// <param name="focus"></param>
        /// <param name="introduction"></param>
        /// <returns></returns>
        Task<BoolMessageOup> UpdateAccountBasicAsync(long PKID, string AccountName, string Position, string wechatAccount, string linkin, string focus, string introduction);

        /// <summary>
        /// 获取个人信息
        /// </summary>
        /// <param name="PKID"></param>
        /// <returns></returns>
        Task<AccountBasicOup> GetAccountBasicAsync(long PKID);

        /// <summary>
        /// 上传企业成员的头像
        /// </summary>
        /// <param name="accountID"></param>
        /// <param name="img"></param>
        /// <returns></returns>
        Task<BoolMessageOup> UpdateAccountAvatarAsync(long accountID, byte[] img);

        /// <summary>
        /// 上传企业成员的微信二维码
        /// </summary>
        /// <param name="accountID"></param>
        /// <param name="img"></param>
        /// <returns></returns>
        Task<BoolMessageOup> UpdateAccountWechatAsync(long accountID, byte[] img);

        /// <summary>
        /// 上传Logo
        /// </summary>
        /// <param name="companyID"></param>
        /// <param name="img"></param>
        /// <returns></returns>
        Task<BoolMessageOup> UpdateLogoAsync(long companyID ,byte[] img);

        /// <summary>
        /// 上传Lisence
        /// </summary>
        /// <param name="companyID"></param>
        /// <param name="img"></param>
        /// <returns></returns>
        Task<BoolMessageOup> UpdateLisenceAsync(long companyID, byte[] img);

        /// <summary>
        /// 获取邀请模板列表
        /// </summary>
        /// <param name="accountID"></param>
        /// <returns></returns>
        Task<List<LetterOup>> GetLetterListAsync(long accountID);

        /// <summary>
        /// 获取邀请模板详情
        /// </summary>
        /// <param name="PKID"></param>
        /// <returns></returns>
        Task<LetterOup> GetLetterAsync(long PKID);

        /// <summary>
        /// 新增/修改邀请模板
        /// </summary>
        /// <param name="input"></param>
        /// <param name="account"></param>
        /// <returns></returns>
        Task<BoolMessageOup> AddOrUpdateLetterAsync(AddOrUpdateLetterInp input, TP_Account account);

        /// <summary>
        /// 删除模板
        /// </summary>
        /// <param name="PKID"></param>
        /// <returns></returns>
        Task<BoolMessageOup> DestroyLetterAsync(long PKID);

        /// <summary>
        /// 分页获取企业数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        PagenationOutput<CompanyPagenationOup> GetAllCompanyByPagenation(PagenationInput input);

        /// <summary>
        /// 分页获取待审核企业数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        PagenationOutput<CompanyPagenationOup> GetCheckCompanyByPagenation(PagenationInput input);

        /// <summary>
        /// 分页获取最近n天新企业数据
        /// </summary>
        /// <param name="input"></param>
        /// <param name="days"></param>
        /// <returns></returns>
        PagenationOutput<CompanyPagenationOup> GetNewCompanyByPagenation(PagenationInput input, int days);

        /// <summary>
        /// 获取企业详情信息
        /// </summary>
        /// <param name="PKID"></param>
        /// <returns></returns>
        Task<CompanyDetailsOup> GetCompanyDetailsAsync(long PKID);

        /// <summary>
        /// 新增/编辑企业信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<BoolMessageOup> AddOrUpdateCompanyAsync(AddOrUpdateCompanyInp input);

        /// <summary>
        /// 新增/修改企业成员
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<BoolMessageOup> AddOrUpdateAccountAsync(AddOrUpdateAccountInp input);

        /// <summary>
        /// 新增/修改企业签约信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<BoolMessageOup> AddOrUpdateContractAsync(AddOrUpdateContractInp input);

        /// <summary>
        /// 获取猎头企业列表PKID+Name
        /// </summary>
        /// <param name="companyID"></param>
        /// <returns></returns>
        Task<List<AgentCompanyOup>> GetAgentCompanyListAsync(long companyID);

        /// <summary>
        /// 获取猎头企业列表PKID+Name
        /// </summary>
        /// <param name="PKID"></param>
        /// <returns></returns>
        Task<AgentCompanySingleOup> GetAgentCompanyByIDAsync(long PKID);

        /// <summary>
        /// 分页获取猎头企业列表
        /// </summary>
        /// <param name="input"></param>
        /// <param name="companyID"></param>
        /// <param name="accountID"></param>
        /// <returns></returns>
        PagenationOutput<AgentCompanyListOup> GetAgentCompanyByPagenation(PagenationInput input, long companyID, long accountID);

        /// <summary>
        /// 新增/编辑猎头企业
        /// </summary>
        /// <param name="input"></param>
        /// <param name="companyID"></param>
        /// <param name="accountID"></param>
        /// <returns></returns>
        Task<BoolMessageOup> AddOrUpdateAgentCompanyAsync(AddOrUpdateAgentCompanyInp input, long companyID, long accountID);
    }
}
