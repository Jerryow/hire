using MrMatch.Application.System.Inp;
using MrMatch.Application.System.Oup;
using MrMatch.Application.User.Inp;
using MrMatch.Application.User.Oup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrMatch.Application.User
{
    public interface IUserService
    {

        #region mquser

        /// <summary>
        /// 分页获取待审核用户列表
        /// </summary>
        /// <param name="pagenationInput"></param>
        /// <returns></returns>
        PagenationOutput<UserListOup> GetUserByPagenation(PagenationInput input);

        /// <summary>
        /// 分页获取待审核用户列表
        /// </summary>
        /// <param name="pagenationInput"></param>
        /// <returns></returns>
        PagenationOutput<UserListOup> GetCheckUserByPagenation(PagenationInput input);

        /// <summary>
        /// 获取基础用户信息
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        Task<BasicUserOup> GetBasicUserAsync(long userID);

        /// <summary>
        /// 获取小程序用户账号信息
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        Task<WechatAccountOup> GetWechatAccountAsync(long userID);

        /// <summary>
        /// 修改小程序账户信息
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="email"></param>
        /// <returns></returns>
        Task<BoolMessageOup> UpdateWechatAccountAsync(long userID, string email);

        /// <summary>
        /// 修改用户的订阅职位
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        Task<BoolMessageOup> AddOrUpdateUserJobBoardAsync(AddOrUpdateUserJobBoardInp input);

        /// <summary>
        /// 上架/下架
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        Task<BoolMessageOup> UpdateUserActivityAsync(long userID, bool status);

        /// <summary>
        /// 用户提交审核
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        Task<BoolMessageOup> CommitApproveAsync(long userID);

        /// <summary>
        /// 后台审核简历
        /// </summary>
        /// <param name="userID"></param>
        /// /// <param name="status">CommonEnum->TP_Profile_ApproveStatus</param>
        /// <returns></returns>
        Task<BoolMessageOup> ApproveUserAsync(long userID, int status);

        /// <summary>
        /// 快照对比是否有修改
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        Task<SnapUpdateOup> SnapUpdateStatusAsync(long userID);

        /// <summary>
        /// 上传用户头像
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="img"></param>
        /// <returns></returns>
        Task<BoolMessageOup> UpdateUserAvatarAsync(long userID, byte[] img);

        /// <summary>
        /// 获取用户列表(PKID+Phone)
        /// </summary>
        /// <returns></returns>
        Task<List<SystemNoticeUserOup>> GetAllUserListAsync(); 
        #endregion

        #region profile
        /// <summary>
        /// 根据id获取简历信息
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        Task<ProfileOup> GetProfileAsync(long userID);

        /// <summary>
        /// 新增/修改简历信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<BoolMessageOup> AddOrUpdateProfileAsync(AddOrUpdateProfileInp input);

        /// <summary>
        /// 获取简历基本信息快照
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        Task<ProfileSnapOup> GetProfileSnapAsync(long userID);

        /// <summary>
        /// 获取简历工作经验快照
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        Task<List<WorkExperienceSnapOup>> GetWorkExperienceSnapAsync(long userID);

        /// <summary>
        /// 获取简历教育经验快照
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        Task<List<EducationSnapOup>> GetEducationSnapAsync(long userID);
        #endregion

        #region work
        /// <summary>
        /// 根据id获取工作信息
        /// </summary>
        /// <param name="PKID"></param>
        /// <returns></returns>
        Task<WorkExperienceOup> GetWorkExperienceAsync(long PKID);

        /// <summary>
        /// 获取工作信息列表
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        Task<List<WorkExperienceOup>> GetWorkExperienceListAsync(long userID);

        /// <summary>
        /// 新增/修改工作信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<BoolMessageOup> AddOrUpdateWorkExperienceAsync(AddOrUpdateWorkExperienceInp input);

        /// <summary>
        /// 删除工作信息
        /// </summary>
        /// <param name="PKID"></param>
        /// <returns></returns>
        Task<BoolMessageOup> DestroyWorkExperienceAsync(long PKID);
        #endregion

        #region education
        /// <summary>
        /// 根据id获取教育信息
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        Task<EducationOup> GetEducationAsync(long PKID);

        /// <summary>
        /// 获取教育信息列表
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        Task<List<EducationOup>> GetEducationListAsync(long userID);

        /// <summary>
        /// 新增/修改教育信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<BoolMessageOup> AddOrUpdateEducationAsync(AddOrUpdateEducationInp input);

        /// <summary>
        /// 删除教育信息
        /// </summary>
        /// <param name="PKID"></param>
        /// <returns></returns>
        Task<BoolMessageOup> DestroyEducationAsync(long PKID);
        #endregion

        #region jobIntention
        /// <summary>
        /// 根据id获取求职意向
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        Task<JobIntentionOup> GetJobIntentionAsync(long userID);

        /// <summary>
        /// 新增/修改求职意向
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<BoolMessageOup> AddOrUpdateJobIntentionAsync(AddOrUpdateJobIntentionInp input);
        #endregion

        #region avoid
        /// <summary>
        /// 获取屏蔽信息列表
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        Task<List<AvoidOup>> GetAvoidListAsync(long userID);

        /// <summary>
        /// 新增/修改屏蔽信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<BoolMessageOup> AddOrUpdateAvoidAsync(AddOrUpdateAvoidInp input);

        /// <summary>
        /// 删除屏蔽信息
        /// </summary>
        /// <param name="PKID"></param>
        /// <returns></returns>
        Task<BoolMessageOup> DestroyAvoidAsync(long PKID);
        #endregion

        #region userTags
        /// <summary>
        /// 获取标签列表
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        Task<List<UserTagsOup>> GetUserTagsListAsync(long userID);

        /// <summary>
        /// 新增/修改标签信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<BoolMessageOup> AddOrUpdateUserTagsAsync(AddOrUpdateUserTagsInp input);
        #endregion
    }
}
