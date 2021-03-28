using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MrMatch.Application.Job.Inp;
using MrMatch.Application.Job.Oup;
using MrMatch.Common.Wechat;
using MrMatch.Domain.Models;

namespace MrMatch.Application.Job
{
    public interface IJobService
    {
        #region 小程序
        /// <summary>
        /// 分页获取职位广告数据
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="userID"></param>
        /// <param name="intentionID"></param>
        /// <returns></returns>
        Task<PagenationOutput<JobListOup>> GetJobByPagenationAsync(int pageIndex, int pageSize, long userID, long intentionID);

        /// <summary>
        /// 分页获取收藏职位数据
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        Task<PagenationOutput<JobListOup>> GetFollowJobByPagenationAsync(int pageIndex, int pageSize, long userID);

        /// <summary>
        /// 分页获取投递职位数据
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        Task<PagenationOutput<JobListOup>> GetApplyJobByPagenationAsync(int pageIndex, int pageSize, long userID);

        /// <summary>
        /// 获取职位详情
        /// </summary>
        /// <param name="PKID">职位id</param>
        /// <param name="userID">用户id</param>
        /// <param name="isLogin">是否登陆</param>
        /// <returns></returns>
        Task<JobDetailsOup> GetJobDetailsAsync(long PKID, long userID, bool isLogin);

        /// <summary>
        /// 投递职位
        /// </summary>
        /// <param name="PKID">职位id</param>
        /// <param name="userID">用户id</param>
        /// <returns></returns>
        Task<BoolMessageOup> JobApplyAsync(long PKID, long userID);

        /// <summary>
        /// 收藏/取消收藏职位
        /// </summary>
        /// <param name="PKID"></param>
        /// <param name="userID"></param>
        /// <param name="isFollow">收藏/取消收藏</param>
        /// <returns></returns>
        Task<BoolMessageOup> JobFollowOrNotAsync(long PKID, long userID, bool isFollow);

        /// <summary>
        /// 举报职位
        /// </summary>
        /// <param name="PKID"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        Task<BoolMessageOup> ReportJobAsync(UserReportInp input);
        #endregion

        #region B端
        /// <summary>
        /// 获取职位的小程序二维码
        /// </summary>
        /// <param name="PKID"></param>
        /// <param name="token"></param>
        /// <param name="setting"></param>
        /// <returns></returns>
        Task<JobMiniPicUrlOup> GetMiniPicUrlAsync(long PKID, string token, MiniWeChatPic setting);

        /// <summary>
        /// 生成所有职位的小程序二维码
        /// </summary>
        /// <param name="token"></param>
        /// <param name="setting"></param>
        /// <returns></returns>
        Task<BoolMessageOup> GetAllJobMiniPicUrlAsync(string token);

        /// <summary>
        /// 创建职位
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<AddOrUpdateJobOup> AddOrUpdateJobAsync(JobPublishInp input);

        /// <summary>
        /// 新增/修改草稿
        /// </summary>
        /// <param name="input"></param>
        /// <param name="account">限制数量</param>
        /// <returns></returns>
        Task<BoolMessageOup> AddOrUpdateJobDraftAsync(JobDraftInp input, TP_Account account);

        /// <summary>
        /// 获取上架/下架职位列表
        /// </summary>
        /// <param name="input"></param>
        /// <param name="type">true:下架 false:上架</param>
        /// <param name="jobID"></param>
        /// <param name="jobName"></param>
        /// <param name="companyID"></param>
        /// <param name="accountID"></param>
        /// <returns></returns>
        Task<PagenationOutput<JobManageListOup>> GetActivityJobListAsync(PagenationInput input, bool type, long jobID, string jobName, long companyID, long accountID);

        /// <summary>
        /// 获取草稿箱列表
        /// </summary>
        /// <param name="input"></param>
        /// <param name="jobName"></param>
        /// <param name="companyID"></param>
        /// <param name="accountID"></param>
        /// <returns></returns>
        Task<PagenationOutput<JobManageListOup>> GetJobDraftListAsync(PagenationInput input, string jobName, long companyID, long accountID);

        /// <summary>
        /// 职位上架/下架
        /// </summary>
        /// <param name="jobID"></param>
        /// <param name="status">true:上架  false:下架</param>
        /// <param name="accountID"></param>
        /// <returns></returns>
        Task<BoolMessageOup> UpdateJobStatusAsync(long jobID, bool status, long accountID);

        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="PKID"></param>
        /// <returns></returns>
        Task<JobDraftDetailsOup> GetJobDraftDetailsAsync(long PKID);

        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="PKID"></param>
        /// <returns></returns>
        Task<BizJobDetailsOup> GetJobDetailsAsync(long PKID);

        /// <summary>
        /// 删除草稿
        /// </summary>
        /// <param name="PKID"></param>
        /// <returns></returns>
        Task<BoolMessageOup> DestroyJobDraftAsync(long PKID);

        /// <summary>
        /// 获取职位投递的简历
        /// </summary>
        /// <param name="input"></param>
        /// <param name="jobID"></param>
        /// <param name="accountID"></param>
        /// <returns></returns>
        Task<PagenationOutput<JobProfilesListOup>> GetJobProfilesByPagenationAsync(PagenationInput input, long jobID, long accountID);
        #endregion


    }
}
