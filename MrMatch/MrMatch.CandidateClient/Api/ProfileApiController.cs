using MrMatch.Application.User;
using MrMatch.Application.User.Inp;
using MrMatch.Application.User.Oup;
using MrMatch.CandidateClient.Api.Base;
using MrMatch.Common.LogHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using MrMatch.Common.ReflectionHelper;
using MrMatch.Application.Config;

namespace MrMatch.CandidateClient.Api
{
    public class ProfileApiController : SecurityBaseController
    {
        #region DI
        private readonly IUserService userService;
        private readonly IConfigService configService;
        private readonly ILogService logService;
        #endregion

        public ProfileApiController(
            IUserService _userService,
            IConfigService _configService,
            ILogService _logService)
        {
            userService = _userService;
            configService = _configService;
            logService = _logService;
        }

        /// <summary>
        /// 获取基础用户信息
        /// </summary>
        /// <returns></returns>
        [System.Web.Http.HttpGet]
        public async Task<IHttpActionResult> GetUserInfo()
        {
            try
            {
                var res = await userService.GetBasicUserAsync(CurrID);
                return Succcess("成功", res);
            }
            catch (Exception ex)
            {
                logService.LogError("[获取基础用户信息]" + ex.Message, ex);
                return Fail("获取失败,请刷新重试.");
            }
        }

        #region basic
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
                return Succcess("成功", new { hot = hot, basic = cascader });
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
        /// 用户提交审核
        /// </summary>
        /// <returns></returns>
        [System.Web.Http.HttpGet]
        public async Task<IHttpActionResult> CommitApprove()
        {
            try
            {
                //判断是否有更改
                var update = await userService.SnapUpdateStatusAsync(CurrID);
                if (!update.Education && !update.JobIntention && !update.Profile && !update.WorkEx)
                {
                    return Fail("您的简历没有任何改动,无需提交审核");
                }
                var res = await userService.CommitApproveAsync(CurrID);
                return Succcess("成功", res);
            }
            catch (Exception ex)
            {
                logService.LogError("[用户提交审核]" + ex.Message, ex);
                return Fail("用户提交审核失败,请刷新重试.");
            }
        }

        /// <summary>
        /// 用户上下架
        /// </summary>
        /// <param name="status">true:上架 false:下架</param>
        /// <returns></returns>
        [System.Web.Http.HttpGet]
        public async Task<IHttpActionResult> UpdateUserActivity(bool status)
        {
            try
            {
                var res = await userService.UpdateUserActivityAsync(CurrID, status);
                return Succcess("成功", res);
            }
            catch (Exception ex)
            {
                logService.LogError("[用户上下架]" + ex.Message, ex);
                return Fail("用户上下架失败,请刷新重试.");
            }
        }

        /// <summary>
        /// 简历快照修改状态
        /// </summary>
        /// <returns></returns>
        [System.Web.Http.HttpGet]
        public async Task<IHttpActionResult> SnapUpdateStatus()
        {
            try
            {
                var res = await userService.SnapUpdateStatusAsync(CurrID);
                return Succcess("成功", res);
            }
            catch (Exception ex)
            {
                logService.LogError("[简历快照修改状态]" + ex.Message, ex);
                return Fail("获取失败,请刷新重试.");
            }
        }

        /// <summary>
        /// 修改订阅职位
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        [System.Web.Http.HttpPost]
        public async Task<IHttpActionResult> AddOrUpdateUserJobBoard(AddOrUpdateUserJobBoardInp form)
        {
            try
            {
                var validate = EntityProperties.EntityValidate<AddOrUpdateUserJobBoardInp>(form);
                if (!validate.BoolResult)
                {
                    return Fail(validate.Message);
                }
                var res = await userService.AddOrUpdateUserJobBoardAsync(form);
                return Succcess("成功", res);
            }
            catch (Exception ex)
            {
                logService.LogError("[修改订阅职位]" + ex.Message, ex);
                return Fail("修改订阅职位失败,请刷新重试.");
            }
        }

        /// <summary>
        /// 获取用户简历快照
        /// </summary>
        /// <returns></returns>
        [System.Web.Http.HttpGet]
        public async Task<IHttpActionResult> GetProfileSnap()
        {
            try
            {
                var pro = await userService.GetProfileSnapAsync(CurrID);
                var edu = await userService.GetEducationSnapAsync(CurrID);
                var work = await userService.GetWorkExperienceSnapAsync(CurrID);
                return Succcess("成功", new { profile = pro, workEx = work, education = edu });
            }
            catch (Exception ex)
            {
                logService.LogError("[获取用户简历快照]" + ex.Message, ex);
                return Fail("获取失败,请刷新重试.");
            }
        }
        #endregion

        #region  profile
        /// <summary>
        /// 获取所有简历信息
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        [System.Web.Http.HttpGet]
        public async Task<IHttpActionResult> GetAllProfile(long userID)
        {
            try
            {
                if (userID <= 0)
                {
                    return Fail("userID入参错误");
                }
                var res = new AllProfileOup();
                res.AvoidList = await userService.GetAvoidListAsync(userID);
                res.WorkExList = await userService.GetWorkExperienceListAsync(userID);
                res.EducationList = await userService.GetEducationListAsync(userID);
                res.UserTagsList = await userService.GetUserTagsListAsync(userID);
                res.JobIntention = await userService.GetJobIntentionAsync(userID);
                res.ProfileInfo = await userService.GetProfileAsync(userID); 
                var mquser = await userService.GetBasicUserAsync(userID);
                res.ApproveStatus = mquser.ApproveStatus;
                res.ActiveStatus = mquser.ActiveStatus;
                res.ProfileSnap = mquser.ProfileSnap;
                res.CompanyCount = 100;
                return Succcess("成功", res);
            }
            catch (Exception ex)
            {
                logService.LogError("[获取所有简历信息]" + ex.Message, ex);
                return Fail("获取失败,请刷新重试.");
            }
        }

        /// <summary>
        /// 获取用户简历信息
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        [System.Web.Http.HttpGet]
        public async Task<IHttpActionResult> GetProfileInfo(long userID)
        {
            try
            {
                if (userID <= 0)
                {
                    return Fail("userID入参错误");
                }
                var res = await userService.GetProfileAsync(userID);
                return Succcess("成功", res);
            }
            catch (Exception ex)
            {
                logService.LogError("[获取用户简历信息]" + ex.Message, ex);
                return Fail("获取失败,请刷新重试.");
            }
        }

        /// <summary>
        /// 新增简历数据
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        [System.Web.Http.HttpPost]
        public async Task<IHttpActionResult> AddProfile(AddOrUpdateProfileInp form)
        {
            try
            {
                var validate = EntityProperties.EntityValidate(form);
                if (!validate.BoolResult)
                {
                    return Fail(validate.Message);
                }
                var res = await userService.AddOrUpdateProfileAsync(form);
                if (res.BoolResult)
                {
                    return Succcess(res.Message);
                }
                return Fail(res.Message);
            }
            catch (Exception ex)
            {
                logService.LogError("[新增简历数据]" + ex.Message, ex);
                return Fail("新增简历数据失败,请刷新重试.");
            }
        }

        /// <summary>
        /// 修改简历数据
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        [System.Web.Http.HttpPost]
        public async Task<IHttpActionResult> UpdateProfile(AddOrUpdateProfileInp form)
        {
            try
            {
                var validate = EntityProperties.EntityValidate(form);
                if (!validate.BoolResult)
                {
                    return Fail(validate.Message);
                }
                var res = await userService.AddOrUpdateProfileAsync(form);
                if (res.BoolResult)
                {
                    return Succcess(res.Message);
                }
                return Fail(res.Message);
            }
            catch (Exception ex)
            {
                logService.LogError("[修改简历数据]" + ex.Message, ex);
                return Fail("修改简历数据失败,请刷新重试.");
            }
        }
        #endregion

        #region work
        /// <summary>
        /// 获取用户工作经验列表
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        [System.Web.Http.HttpGet]
        public async Task<IHttpActionResult> GetWorkExperienceList(long userID)
        {
            try
            {
                if (userID <= 0)
                {
                    return Fail("userID入参错误");
                }
                var res = await userService.GetWorkExperienceListAsync(userID);
                return Succcess("成功", res);
            }
            catch (Exception ex)
            {
                logService.LogError("[获取用户工作经验列表]" + ex.Message, ex);
                return Fail("获取失败,请刷新重试.");
            }
        }

        /// <summary>
        /// 根据id获取用户工作经验
        /// </summary>
        /// <param name="PKID"></param>
        /// <returns></returns>
        [System.Web.Http.HttpGet]
        public async Task<IHttpActionResult> GetWorkExperienceByID(long PKID)
        {
            try
            {
                if (PKID <= 0)
                {
                    return Fail("PKID入参错误");
                }
                var res = await userService.GetWorkExperienceAsync(PKID);
                return Succcess("成功", res);
            }
            catch (Exception ex)
            {
                logService.LogError("[根据id获取用户工作经验]" + ex.Message, ex);
                return Fail("获取失败,请刷新重试.");
            }
        }

        /// <summary>
        /// 新增工作经验
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        [System.Web.Http.HttpPost]
        public async Task<IHttpActionResult> AddWorkExperience(AddOrUpdateWorkExperienceInp form)
        {
            try
            {
                var validate = EntityProperties.EntityValidate(form);
                if (!validate.BoolResult)
                {
                    return Fail(validate.Message);
                }
                var res = await userService.AddOrUpdateWorkExperienceAsync(form);
                if (res.BoolResult)
                {
                    return Succcess(res.Message);
                }
                return Fail(res.Message);
            }
            catch (Exception ex)
            {
                logService.LogError("[新增工作经验]" + ex.Message, ex);
                return Fail("新增工作经验失败,请刷新重试.");
            }
        }

        /// <summary>
        /// 修改工作经验
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        [System.Web.Http.HttpPost]
        public async Task<IHttpActionResult> UpdateWorkExperience(AddOrUpdateWorkExperienceInp form)
        {
            try
            {
                var validate = EntityProperties.EntityValidate(form);
                if (!validate.BoolResult)
                {
                    return Fail(validate.Message);
                }
                var res = await userService.AddOrUpdateWorkExperienceAsync(form);
                if (res.BoolResult)
                {
                    return Succcess(res.Message);
                }
                return Fail(res.Message);
            }
            catch (Exception ex)
            {
                logService.LogError("[修改工作经验]" + ex.Message, ex);
                return Fail("修改工作经验失败,请刷新重试.");
            }
        }

        /// <summary>
        /// 删除工作经验
        /// </summary>
        /// <param name="PKID"></param>
        /// <returns></returns>
        [System.Web.Http.HttpGet]
        public async Task<IHttpActionResult> DestroyWorkExperience(long PKID)
        {
            try
            {
                if (PKID <= 0)
                {
                    return Fail("PKID入参错误");
                }
                var res = await userService.DestroyWorkExperienceAsync(PKID);
                if (res.BoolResult)
                {
                    return Succcess(res.Message);
                }
                return Fail(res.Message);
            }
            catch (Exception ex)
            {
                logService.LogError("[删除工作经验]" + ex.Message, ex);
                return Fail("删除工作经验失败,请刷新重试.");
            }
        }
        #endregion

        #region education
        /// <summary>
        /// 获取用户教育经验列表
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        [System.Web.Http.HttpGet]
        public async Task<IHttpActionResult> GetEducationList(long userID)
        {
            try
            {
                if (userID <= 0)
                {
                    return Fail("userID入参错误");
                }
                var res = await userService.GetEducationListAsync(userID);
                return Succcess("成功", res);
            }
            catch (Exception ex)
            {
                logService.LogError("[获取用户教育经验列表]" + ex.Message, ex);
                return Fail("获取失败,请刷新重试.");
            }
        }

        /// <summary>
        /// 根据id获取用户教育经验
        /// </summary>
        /// <param name="PKID"></param>
        /// <returns></returns>
        [System.Web.Http.HttpGet]
        public async Task<IHttpActionResult> GetEducationByID(long PKID)
        {
            try
            {
                if (PKID <= 0)
                {
                    return Fail("PKID入参错误");
                }
                var res = await userService.GetEducationAsync(PKID);
                return Succcess("成功", res);
            }
            catch (Exception ex)
            {
                logService.LogError("[根据id获取用户教育经验]" + ex.Message, ex);
                return Fail("获取失败,请刷新重试.");
            }
        }

        /// <summary>
        /// 新增教育经验
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        [System.Web.Http.HttpPost]
        public async Task<IHttpActionResult> AddEducation(AddOrUpdateEducationInp form)
        {
            try
            {
                var validate = EntityProperties.EntityValidate(form);
                if (!validate.BoolResult)
                {
                    return Fail(validate.Message);
                }
                var res = await userService.AddOrUpdateEducationAsync(form);
                if (res.BoolResult)
                {
                    return Succcess(res.Message);
                }
                return Fail(res.Message);
            }
            catch (Exception ex)
            {
                logService.LogError("[新增教育经验]" + ex.Message, ex);
                return Fail("新增教育经验失败,请刷新重试.");
            }
        }

        /// <summary>
        /// 修改教育经验
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        [System.Web.Http.HttpPost]
        public async Task<IHttpActionResult> UpdateEducation(AddOrUpdateEducationInp form)
        {
            try
            {
                var validate = EntityProperties.EntityValidate(form);
                if (!validate.BoolResult)
                {
                    return Fail(validate.Message);
                }
                var res = await userService.AddOrUpdateEducationAsync(form);
                if (res.BoolResult)
                {
                    return Succcess(res.Message);
                }
                return Fail(res.Message);
            }
            catch (Exception ex)
            {
                logService.LogError("[修改教育经验]" + ex.Message, ex);
                return Fail("修改教育经验失败,请刷新重试.");
            }
        }

        /// <summary>
        /// 删除教育经验
        /// </summary>
        /// <param name="PKID"></param>
        /// <returns></returns>
        [System.Web.Http.HttpGet]
        public async Task<IHttpActionResult> DestroyEducation(long PKID)
        {
            try
            {
                if (PKID <= 0)
                {
                    return Fail("PKID入参错误");
                }
                var res = await userService.DestroyEducationAsync(PKID);
                if (res.BoolResult)
                {
                    return Succcess(res.Message);
                }
                return Fail(res.Message);
            }
            catch (Exception ex)
            {
                logService.LogError("[删除教育经验]" + ex.Message, ex);
                return Fail("删除教育经验失败,请刷新重试.");
            }
        }
        #endregion

        #region avoid
        /// <summary>
        /// 获取用户屏蔽列表
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        [System.Web.Http.HttpGet]
        public async Task<IHttpActionResult> GetAvoidList(long userID)
        {
            try
            {
                if (userID <= 0)
                {
                    return Fail("userID入参错误");
                }
                var res = await userService.GetAvoidListAsync(userID);
                return Succcess("成功", res);
            }
            catch (Exception ex)
            {
                logService.LogError("[获取用户屏蔽列表]" + ex.Message, ex);
                return Fail("获取失败,请刷新重试.");
            }
        }

        /// <summary>
        /// 新增屏蔽
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        [System.Web.Http.HttpPost]
        public async Task<IHttpActionResult> AddAvoid(AddOrUpdateAvoidInp form)
        {
            try
            {
                var validate = EntityProperties.EntityValidate(form);
                if (!validate.BoolResult)
                {
                    return Fail(validate.Message);
                }
                var res = await userService.AddOrUpdateAvoidAsync(form);
                if (res.BoolResult)
                {
                    return Succcess(res.Message);
                }
                return Fail(res.Message);
            }
            catch (Exception ex)
            {
                logService.LogError("[新增屏蔽]" + ex.Message, ex);
                return Fail("新增屏蔽失败,请刷新重试.");
            }
        }

        /// <summary>
        /// 修改屏蔽
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        [System.Web.Http.HttpPost]
        public async Task<IHttpActionResult> UpdateAvoid(AddOrUpdateAvoidInp form)
        {
            try
            {
                var validate = EntityProperties.EntityValidate(form);
                if (!validate.BoolResult)
                {
                    return Fail(validate.Message);
                }
                var res = await userService.AddOrUpdateAvoidAsync(form);
                if (res.BoolResult)
                {
                    return Succcess(res.Message);
                }
                return Fail(res.Message);
            }
            catch (Exception ex)
            {
                logService.LogError("[修改屏蔽]" + ex.Message, ex);
                return Fail("修改屏蔽失败,请刷新重试.");
            }
        }

        /// <summary>
        /// 删除屏蔽
        /// </summary>
        /// <param name="PKID"></param>
        /// <returns></returns>
        [System.Web.Http.HttpGet]
        public async Task<IHttpActionResult> DestroyAvoid(long PKID)
        {
            try
            {
                if (PKID <= 0)
                {
                    return Fail("PKID入参错误");
                }
                var res = await userService.DestroyAvoidAsync(PKID);
                if (res.BoolResult)
                {
                    return Succcess(res.Message);
                }
                return Fail(res.Message);
            }
            catch (Exception ex)
            {
                logService.LogError("[删除屏蔽]" + ex.Message, ex);
                return Fail("删除屏蔽失败,请刷新重试.");
            }
        }
        #endregion

        #region jobIntention
        /// <summary>
        /// 获取用户求职意向
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        [System.Web.Http.HttpGet]
        public async Task<IHttpActionResult> GetJobIntention(long userID)
        {
            try
            {
                if (userID <= 0)
                {
                    return Fail("userID入参错误");
                }
                var res = await userService.GetJobIntentionAsync(userID);
                return Succcess("成功", res);
            }
            catch (Exception ex)
            {
                logService.LogError("[获取用户求职意向]" + ex.Message, ex);
                return Fail("获取失败,请刷新重试.");
            }
        }

        /// <summary>
        /// 新增用户求职意向
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        [System.Web.Http.HttpPost]
        public async Task<IHttpActionResult> AddJobIntention(AddOrUpdateJobIntentionInp form)
        {
            try
            {
                var validate = EntityProperties.EntityValidate(form);
                if (!validate.BoolResult)
                {
                    return Fail(validate.Message);
                }
                var res = await userService.AddOrUpdateJobIntentionAsync(form);
                if (res.BoolResult)
                {
                    return Succcess(res.Message);
                }
                return Fail(res.Message);
            }
            catch (Exception ex)
            {
                logService.LogError("[新增用户求职意向]" + ex.Message, ex);
                return Fail("新增用户求职意向失败,请刷新重试.");
            }
        }

        /// <summary>
        /// 修改用户求职意向
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        [System.Web.Http.HttpPost]
        public async Task<IHttpActionResult> UpdateJobIntention(AddOrUpdateJobIntentionInp form)
        {
            try
            {
                var validate = EntityProperties.EntityValidate(form);
                if (!validate.BoolResult)
                {
                    return Fail(validate.Message);
                }
                var res = await userService.AddOrUpdateJobIntentionAsync(form);
                if (res.BoolResult)
                {
                    return Succcess(res.Message);
                }
                return Fail(res.Message);
            }
            catch (Exception ex)
            {
                logService.LogError("[修改用户求职意向]" + ex.Message, ex);
                return Fail("修改用户求职意向失败,请刷新重试.");
            }
        }
        #endregion

        #region userTags
        /// <summary>
        /// 获取用户标签列表
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        [System.Web.Http.HttpGet]
        public async Task<IHttpActionResult> GetUserTagsList(long userID)
        {
            try
            {
                if (userID <= 0)
                {
                    return Fail("userID入参错误");
                }
                var res = await userService.GetUserTagsListAsync(userID);
                return Succcess("成功", res);
            }
            catch (Exception ex)
            {
                logService.LogError("[获取用户标签列表]" + ex.Message, ex);
                return Fail("获取失败,请刷新重试.");
            }
        }

        /// <summary>
        /// 保存用户标签
        /// <param name="form"></param>
        /// </summary>
        [System.Web.Http.HttpPost]
        public async Task<IHttpActionResult> SaveUserTags(AddOrUpdateUserTagsInp form)
        {
            try
            {
                var validate = EntityProperties.EntityValidate(form);
                if (!validate.BoolResult)
                {
                    return Fail(validate.Message);
                }
                var res = await userService.AddOrUpdateUserTagsAsync(form);
                if (res.BoolResult)
                {
                    return Succcess(res.Message);
                }
                return Fail(res.Message);
            }
            catch (Exception ex)
            {
                logService.LogError("[保存用户标签]" + ex.Message, ex);
                return Fail("保存用户标签失败,请刷新重试.");
            }
        }
        #endregion
    }
}
