using MrMatch.Application.Job.Oup;
using MrMatch.Domain.EntityBase.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MrMatch.Domain.Models;
using MrMatch.MysqlFramework;
using MrMatch.MysqlFramework.Extensions;
using MrMatch.Common.Extension;
using MrMatch.Common.Mapper;
using MrMatch.Application;
using MrMatch.Application.Job.Inp;
using MrMatch.Common.ReflectionHelper;
using MrMatch.Common.Wechat;
using System.Configuration;
using MrMatch.Common.ImageHelper;
using MrMatch.Application.Company.Oup;

namespace MrMatch.Application.Job
{
    public class JobService : IJobService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ICommunityRepository<mq_user> mqUser;
        private readonly IRepository<TP_Job> job;
        private readonly IRepository<TP_JobApply> apply;
        private readonly IRepository<TP_UserJobFollow> jobFollow;
        private readonly IRepository<TP_ProfileSnap> profileSnap;
        private readonly IRepository<TP_WorkExperienceSnap> workExperienceSnap;
        private readonly IRepository<TP_UserJobIntention> intention;
        private readonly IRepository<TP_Company> company;
        private readonly IRepository<TP_Account> account;
        private readonly IRepository<TP_JobDraft> jobDraft;
        private readonly IRepository<TP_AgentCompany> agentCompany;
        private readonly IRepository<TP_District> district;
        private readonly IRepository<TP_UserReport> userReport;
        private readonly IRepository<TP_CompanyContract> companyContract;

        public JobService(
            IUnitOfWork _unitOfWork,
            ICommunityRepository<mq_user> _mqUser,
            IRepository<TP_Job> _job,
            IRepository<TP_JobApply> _apply,
            IRepository<TP_UserJobFollow> _jobFollow,
            IRepository<TP_UserJobIntention> _intention,
            IRepository<TP_Company> _company,
            IRepository<TP_Account> _account,
            IRepository<TP_JobDraft> _jobDraft,
            IRepository<TP_AgentCompany> _agentCompany,
            IRepository<TP_District> _district,
            IRepository<TP_UserReport> _userReport,
            IRepository<TP_CompanyContract> _companyContract,
            IRepository<TP_ProfileSnap> _profileSnap,
            IRepository<TP_WorkExperienceSnap> _workExperienceSnap)
        {
            unitOfWork = _unitOfWork;
            mqUser = _mqUser;
            job = _job;
            apply = _apply;
            jobFollow = _jobFollow;
            intention = _intention;
            company = _company;
            account = _account;
            agentCompany = _agentCompany;
            district = _district;
            userReport = _userReport;
            companyContract = _companyContract;
            jobDraft = _jobDraft;
            profileSnap = _profileSnap;
            workExperienceSnap = _workExperienceSnap;
        }

        public async Task<PagenationOutput<JobListOup>> GetJobByPagenationAsync(int pageIndex, int pageSize, long userID, long intentionID)
        {
            PagenationOutput<JobListOup> data = new PagenationOutput<JobListOup>();
            var total = 0;
            var pageCount = 0;

            //意向薪资范围
            var salary = "";
            //城市id
            var locationid = "";
            //判断是否已登陆
            if (userID > 0)
            {
                var user = await mqUser.FirstOrDefaultAsync(x => x.id == userID);
                if (user.IsNullEntity())
                {
                    if (intentionID > 0)
                    {
                        if (!string.IsNullOrEmpty(user.AnnualSalary.Trim()))
                        {
                            salary = user.AnnualSalary;
                        }
                        if (!string.IsNullOrEmpty(user.LocationIDs.Trim()))
                        {
                            locationid = user.LocationIDs;
                        }
                    }
                }
            }

            var query = job.GetAll().Where(x => x.Valid == true && x.ActiveStatus == true);
            //如已登陆,返回用户精准的匹配职能项
            if (intentionID > 0)
            {
                query = query.Where(x => x.FunctionID == intentionID);
            }
            //如已登陆,返回用户精准的匹配地点
            if (!string.IsNullOrEmpty(locationid))
            {
                var idLong = Convert.ToInt64(locationid);
                query = query.Where(x => x.DistrictID == idLong);
            }
            //如已登陆,返回用户精准的匹配年薪
            if (!string.IsNullOrEmpty(salary))
            {
                query = query.AnnualSalaryMatch(salary);
            }

            var oup = query.OrderByDescending(x => x.LastModifiedTime)
                     .Skip(pageSize * (pageIndex - 1))
                     .Take(pageSize)
                     .MapToList<TP_Job, JobListOup>();
            if (oup.Count > 0)
            {
                //取主体公司id,旗下猎头公司id,城市id
                var basicCompanyIDs = oup.Select(x => x.BasicCompanyID).Distinct().ToList();
                var jobCompanyIDs = new List<long>();
                var districtIDs = oup.Select(x => x.DistrictID).Distinct().ToList();
                oup.ForEach(item =>
                {
                    if (item.AgentJob)
                    {
                        jobCompanyIDs.Add(item.JobCompanyID);
                    }
                });
                //去重
                jobCompanyIDs = jobCompanyIDs.Distinct().ToList();
                //获取分页后的主体企业
                var basicCompanies = await company.GetAllListAsync(x => x.Valid == true && basicCompanyIDs.Contains(x.PKID));
                //获取分页后的主体企业旗下的猎头企业
                var jobCompanies = await agentCompany.GetAllListAsync(x => x.Valid == true && jobCompanyIDs.Contains(x.PKID));
                //获取分页后的城市
                var districts = await district.GetAllListAsync(x => x.Valid == true && districtIDs.Contains(x.PKID));

                //填充对外显示名称,城市,logo
                oup.ForEach(item =>
                {
                    if (item.AgentJob)
                    {
                        item.JobCompanyShortName = jobCompanies.Find(x => x.PKID == item.JobCompanyID).ShortName;
                    }
                    else
                    {
                        item.JobCompanyShortName = basicCompanies.Find(x => x.PKID == item.BasicCompanyID).ShortName;
                    }
                    item.DistrictName = districts.Find(x => x.PKID == item.DistrictID).DistrictName;
                    item.LogoUrl = basicCompanies.Find(x => x.PKID == item.BasicCompanyID).LogoImageUrl;
                });

                if (userID > 0)
                {
                    var jobids = oup.Select(x => x.PKID).ToList();
                    //收藏
                    var follows = await jobFollow.GetAllListAsync(x => x.Valid == true && jobids.Contains(x.JobID));
                    //投递
                    var applies = await apply.GetAllListAsync(x => x.Valid == true && jobids.Contains(x.JobID));

                    oup.ForEach(q =>
                    {
                        q.IsApply = applies.Where(x => x.JobID == q.PKID && x.UserID == userID).Count() > 0 ? true : false;
                        q.IsFollow = follows.Where(x => x.JobID == q.PKID && x.UserID == userID).Count() > 0 ? true : false;
                    });
                }
            }

            data.PageIndex = pageIndex;
            data.PageCount = pageCount;
            data.Total = oup.Count;
            data.DataList = oup;
            return data;
        }

        public async Task<PagenationOutput<JobListOup>> GetApplyJobByPagenationAsync(int pageIndex, int pageSize, long userID)
        {
            PagenationOutput<JobListOup> data = new PagenationOutput<JobListOup>();
            var total = 0;
            var pageCount = 0;

            var user = await mqUser.FirstOrDefaultAsync(x => x.id == userID);
            if (!user.IsNullEntity())
            {
                return data;
            }

            var applyIds = (await apply.GetAllListAsync(x => x.Valid == true && x.UserID == userID))
                .Select(x => x.JobID)
                .ToList();
            if (applyIds.Count <= 0)
            {
                data.Total = 0;
                data.PageIndex = 1;
                data.DataList = new List<JobListOup>();
            }
            else
            {
                var oup = (await job.GetAllListAsync(x => x.Valid == true && applyIds.Contains(x.PKID)))
                    .OrderByDescending(x => x.LastModifiedTime)
                    .Skip(pageSize * (pageIndex - 1))
                    .Take(pageSize)
                    .MapToList<TP_Job, JobListOup>();

                //取主体公司id,旗下猎头公司id,城市id
                var basicCompanyIDs = oup.Select(x => x.BasicCompanyID).Distinct().ToList();
                var jobCompanyIDs = new List<long>();
                var districtIDs = oup.Select(x => x.DistrictID).Distinct().ToList();
                oup.ForEach(item =>
                {
                    if (item.AgentJob)
                    {
                        jobCompanyIDs.Add(item.JobCompanyID);
                    }
                });
                //去重
                jobCompanyIDs = jobCompanyIDs.Distinct().ToList();
                //获取分页后的主体企业
                var basicCompanies = await company.GetAllListAsync(x => x.Valid == true && basicCompanyIDs.Contains(x.PKID));
                //获取分页后的主体企业旗下的猎头企业
                var jobCompanies = await agentCompany.GetAllListAsync(x => x.Valid == true && jobCompanyIDs.Contains(x.PKID));
                //获取分页后的城市
                var districts = await district.GetAllListAsync(x => x.Valid == true && districtIDs.Contains(x.PKID));

                var jobids = oup.Select(x => x.PKID).ToList();
                //收藏
                var follows = await jobFollow.GetAllListAsync(x => x.Valid == true && jobids.Contains(x.JobID));
                //填充对外显示名称,城市,logo
                oup.ForEach(item =>
                {
                    if (item.AgentJob)
                    {
                        item.JobCompanyShortName = jobCompanies.Find(x => x.PKID == item.JobCompanyID).ShortName;
                    }
                    else
                    {
                        item.JobCompanyShortName = basicCompanies.Find(x => x.PKID == item.BasicCompanyID).ShortName;
                    }
                    item.DistrictName = districts.Find(x => x.PKID == item.DistrictID).DistrictName;
                    item.LogoUrl = basicCompanies.Find(x => x.PKID == item.BasicCompanyID).LogoImageUrl;
                    item.IsFollow = follows.Where(x => x.JobID == item.PKID && x.UserID == userID).Count() > 0 ? true : false;
                });


                data.Total = oup.Count;
                data.PageIndex = pageIndex;
                data.DataList = oup;
            }

            return data;
        }

        public async Task<PagenationOutput<JobListOup>> GetFollowJobByPagenationAsync(int pageIndex, int pageSize, long userID)
        {
            PagenationOutput<JobListOup> data = new PagenationOutput<JobListOup>();
            var total = 0;
            var pageCount = 0;

            var user = await mqUser.FirstOrDefaultAsync(x => x.id == userID);
            if (!user.IsNullEntity())
            {
                return data;
            }

            var followIds = (await jobFollow.GetAllListAsync(x => x.Valid == true && x.UserID == userID))
                .Select(x => x.JobID)
                .ToList();
            if (followIds.Count <= 0)
            {
                data.Total = 0;
                data.PageIndex = 1;
                data.DataList = new List<JobListOup>();
            }
            else
            {
                var oup = (await job.GetAllListAsync(x => x.Valid == true && followIds.Contains(x.PKID)))
                    .OrderByDescending(x => x.LastModifiedTime)
                    .Skip(pageSize * (pageIndex - 1))
                    .Take(pageSize)
                    .MapToList<TP_Job, JobListOup>();

                //取主体公司id,旗下猎头公司id,城市id
                var basicCompanyIDs = oup.Select(x => x.BasicCompanyID).Distinct().ToList();
                var jobCompanyIDs = new List<long>();
                var districtIDs = oup.Select(x => x.DistrictID).Distinct().ToList();
                oup.ForEach(item =>
                {
                    if (item.AgentJob)
                    {
                        jobCompanyIDs.Add(item.JobCompanyID);
                    }
                });
                //去重
                jobCompanyIDs = jobCompanyIDs.Distinct().ToList();
                //获取分页后的主体企业
                var basicCompanies = await company.GetAllListAsync(x => x.Valid == true && basicCompanyIDs.Contains(x.PKID));
                //获取分页后的主体企业旗下的猎头企业
                var jobCompanies = await agentCompany.GetAllListAsync(x => x.Valid == true && jobCompanyIDs.Contains(x.PKID));
                //获取分页后的城市
                var districts = await district.GetAllListAsync(x => x.Valid == true && districtIDs.Contains(x.PKID));

                var jobids = oup.Select(x => x.PKID).ToList();
                //投递
                var applies = await apply.GetAllListAsync(x => x.Valid == true && jobids.Contains(x.JobID));
                //填充对外显示名称,城市,logo
                oup.ForEach(item =>
                {
                    if (item.AgentJob)
                    {
                        item.JobCompanyShortName = jobCompanies.Find(x => x.PKID == item.JobCompanyID).ShortName;
                    }
                    else
                    {
                        item.JobCompanyShortName = basicCompanies.Find(x => x.PKID == item.BasicCompanyID).ShortName;
                    }
                    item.DistrictName = districts.Find(x => x.PKID == item.DistrictID).DistrictName;
                    item.LogoUrl = basicCompanies.Find(x => x.PKID == item.BasicCompanyID).LogoImageUrl;
                    item.IsApply = applies.Where(x => x.JobID == item.PKID && x.UserID == userID).Count() > 0 ? true : false;
                });


                data.Total = oup.Count;
                data.PageIndex = pageIndex;
                data.DataList = oup;
            }

            return data;
        }

        public async Task<BoolMessageOup> JobFollowOrNotAsync(long PKID, long userID, bool isFollow)
        {
            var rtn = new BoolMessageOup();
            rtn.BoolResult = true;
            rtn.Message = "";

            //验证用户/岗位是否存在
            var user = await mqUser.FirstOrDefaultAsync(x => x.id == userID);
            if (!user.IsNullEntity())
            {
                rtn.BoolResult = false;
                rtn.Message = "userID入参错误";
                return rtn;
            }
            var jobInfo = await job.FirstOrDefaultAsync(x => x.PKID == PKID);
            if (!jobInfo.IsNullEntity())
            {
                rtn.BoolResult = false;
                rtn.Message = "PKID入参错误";
                return rtn;
            }

            var now = DateTime.Now;
            var existData = await jobFollow.FirstOrDefaultAsync(x => x.UserID == userID && x.JobID == PKID);

            //收藏
            if (isFollow)
            {
                if (!existData.IsNullEntity())
                {
                    //从未收藏过新数据
                    var follow = new TP_UserJobFollow();
                    follow.JobID = jobInfo.PKID;
                    follow.UserID = user.id;
                    follow.CreatedTime = now;
                    follow.LastModifiedTime = now;
                    follow.Valid = true;
                    follow = unitOfWork.RegisterNewEntity(follow);
                    rtn.BoolResult = await unitOfWork.CommitAsync();
                    if (!rtn.BoolResult)
                    {
                        rtn.Message = "收藏失败,请刷新重试";
                    }
                    else
                    {
                        rtn.Message = "收藏成功";
                    }
                    return rtn;
                }
                //曾收藏过,后取消收藏
                if (existData.Valid)
                {
                    rtn.BoolResult = false;
                    rtn.Message = "此岗位已收藏,请勿重复提交";
                    return rtn;
                }

                existData.Valid = true;
                existData.LastModifiedTime = now;
                unitOfWork.RegisterUpdate(existData);
                rtn.BoolResult = await unitOfWork.CommitAsync();
                if (rtn.BoolResult)
                {
                    rtn.Message = "收藏成功";
                }
                else
                {
                    rtn.Message = "收藏失败,请刷新重试";
                }
                return rtn;
            }
            else
            {
                //取消收藏
                if (!existData.IsNullEntity())
                {
                    rtn.BoolResult = false;
                    rtn.Message = "该用户未收藏此职位";
                    return rtn;
                }
                existData.Valid = false;
                existData.LastModifiedTime = now;
                unitOfWork.RegisterUpdate(existData);
                rtn.BoolResult = await unitOfWork.CommitAsync();
                if (rtn.BoolResult)
                {
                    rtn.Message = "取消收藏成功";
                }
                else
                {
                    rtn.Message = "取消收藏失败,请刷新重试";
                }
                return rtn;
            }
        }

        public async Task<JobDetailsOup> GetJobDetailsAsync(long PKID, long userID, bool isLogin)
        {
            var existJob = await job.FirstOrDefaultAsync(x => x.PKID == PKID && x.Valid == true);
            var jobInfo = existJob.MapTo<TP_Job, JobDetailsOup>();
            jobInfo.AnnualSalary = existJob.MinAnnualSalary.ToString().Split('.')[0] + "," + existJob.MaxAnnualSalary.ToString().Split('.')[0];
            //空对象
            if (!jobInfo.IsNullEntity())
            {
                return null;
            }

            var basicCompany = await company.FirstOrDefaultAsync(x => x.PKID == jobInfo.BasicCompanyID);
            if (!basicCompany.IsNullEntity())
            {
                return null;
            }
            jobInfo.CompanyEmployeeNum = CommonEnum.PrintEmployeeNumToString(basicCompany.EmployeeNum);
            jobInfo.BasicCompanyShortName = basicCompany.ShortName;
            jobInfo.BasicCompanySummary = basicCompany.Summary;
            jobInfo.BasicCompanyDescription = basicCompany.Introduce;
            jobInfo.Summary = basicCompany.Summary;

            jobInfo.IsFollow = false;
            jobInfo.IsApply = false;
            jobInfo.IsReport = false;

            //是否猎头岗位
            if (jobInfo.AgentJob)
            {
                jobInfo.JobCompanyShortName = (await agentCompany.FirstOrDefaultAsync(x => x.PKID == jobInfo.JobCompanyID)).ShortName;
            }
            else
            {
                jobInfo.JobCompanyShortName = basicCompany.ShortName;
            }

            var accountInfo = await account.FirstOrDefaultAsync(x => x.PKID == jobInfo.AccountID);
            jobInfo.AccountInfo = new JobAccountOup();
            jobInfo.AccountInfo.AccountName = accountInfo.AccountName;
            jobInfo.AccountInfo.AvatarUrl = accountInfo.AvatarUrl;
            jobInfo.AccountInfo.Position = accountInfo.Position;
            if (jobInfo.ShowPersonal)
            {
                jobInfo.AccountInfo = LogicHelper.GetJobAccountInfo(accountInfo, jobInfo.ShowItems);
            }

            jobInfo.LogoUrl = basicCompany.LogoImageUrl;
            jobInfo.DistrictName = (await district.FirstOrDefaultAsync(x => x.PKID == jobInfo.DistrictID)).DistrictName;

            if (isLogin)
            {
                //判断用户id是否准确
                var user = await mqUser.FirstOrDefaultAsync(x => x.id == userID);
                if (user.IsNullEntity())
                {
                    //是否收藏
                    var followCount = await jobFollow.CountAsync(x => x.Valid == true
                    && x.JobID == jobInfo.PKID
                    && x.UserID == userID);
                    if (followCount > 0)
                    {
                        jobInfo.IsFollow = true;
                    }
                    //是否投递
                    var applyCount = await apply.CountAsync(x => x.Valid == true
                    && x.UserID == userID
                    && x.JobID == jobInfo.PKID
                    && x.AccountID == jobInfo.AccountID);
                    if (applyCount > 0)
                    {
                        jobInfo.IsApply = true;
                    }
                    //是否举报
                    var reportCount = await userReport.CountAsync(x => x.Valid == true
                        && x.UserID == userID
                        && x.JobID == jobInfo.PKID);
                    if (reportCount > 0)
                    {
                        jobInfo.IsReport = true;
                    }
                }
            }

            return jobInfo;
        }

        public async Task<BoolMessageOup> JobApplyAsync(long PKID, long userID)
        {
            var oup = new BoolMessageOup();
            //验证用户是否审核通过并上架
            var user = await mqUser.FirstOrDefaultAsync(x => x.id == userID);
            if (!user.IsNullEntity())
            {
                oup.BoolResult = false;
                oup.Message = "入参错误,请重试";
                return oup;
            }

            if (!user.ProfileSnap)
            {
                oup.BoolResult = false;
                oup.Message = "请先完成基本信息并提交审核后投递";
                return oup;
            }

            //验证job是否存在
            var jobInfo = await job.FirstOrDefaultAsync(x => x.PKID == PKID);
            if (!jobInfo.IsNullEntity())
            {
                oup.BoolResult = false;
                oup.Message = "入参错误,请重试";
                return oup;
            }

            //判断是否投递过
            var existApply = await apply.FirstOrDefaultAsync(x => x.Valid == true && x.UserID == userID && x.JobID == PKID);
            if (existApply.IsNullEntity())
            {
                oup.BoolResult = false;
                oup.Message = "此岗位您已投递过了,请勿重复投递";
                return oup;
            }

            var now = DateTime.Now;
            var newData = new TP_JobApply();
            newData.UserID = userID;
            newData.ApplyStatus = CommonEnum.TP_JobApply_ApplyStatus.未查看.GetHashCode();
            newData.ExpirationDate = now;
            newData.AccountID = jobInfo.AccountID;
            newData.JobID = PKID;
            newData.CreatedTime = now;
            newData.LastModifiedTime = now;
            newData.Valid = true;
            unitOfWork.RegisterNew(newData);
            oup.BoolResult = await unitOfWork.CommitAsync();
            if (!oup.BoolResult)
            {
                oup.Message = "投递失败";
            }
            else
            {
                oup.Message = "投递成功";
            }
            return oup;
        }

        public async Task<BoolMessageOup> ReportJobAsync(UserReportInp input)
        {
            var rtn = new BoolMessageOup();
            rtn.BoolResult = true;
            rtn.Message = "";

            //验证用户/岗位是否存在
            var user = await mqUser.FirstOrDefaultAsync(x => x.id == input.UserID);
            if (!user.IsNullEntity())
            {
                rtn.BoolResult = false;
                rtn.Message = "userID入参错误";
                return rtn;
            }
            var jobInfo = await job.FirstOrDefaultAsync(x => x.PKID == input.JobID);
            if (!jobInfo.IsNullEntity())
            {
                rtn.BoolResult = false;
                rtn.Message = "PKID入参错误";
                return rtn;
            }
            //举报
            var now = DateTime.Now;
            var existData = await userReport.FirstOrDefaultAsync(x =>
            x.Valid == true
            && x.UserID == user.id
            && x.JobID == jobInfo.PKID);

            if (existData.IsNullEntity())
            {
                rtn.BoolResult = false;
                rtn.Message = "已经举报过了";
                return rtn;
            }

            var newData = new TP_UserReport();
            newData.UserID = user.id;
            newData.JobID = jobInfo.PKID;
            newData.Reason = input.Reason;
            newData.ReasonExtra = input.ReasonExtra;
            newData.CreatedTime = DateTime.Now;
            newData.Valid = true;
            newData = unitOfWork.RegisterNewEntity(newData);
            rtn.BoolResult = await unitOfWork.CommitAsync();
            if (rtn.BoolResult)
            {
                rtn.Message = "举报成功";
            }
            else
            {
                rtn.Message = "举报失败";
            }
            return rtn;
        }

        public async Task<JobMiniPicUrlOup> GetMiniPicUrlAsync(long PKID, string token, MiniWeChatPic setting)
        {
            var oup = new JobMiniPicUrlOup();
            var existJob = await job.FirstOrDefaultAsync(x => x.Valid == true && x.PKID == PKID);
            if (!existJob.IsNullEntity())
            {
                oup.IsOK = false;
                return oup;
            }
            if (!string.IsNullOrEmpty(existJob.WechatMiniPic))
            {
                oup.IsOK = false;
                return oup;
            }

            var res = Common.Wechat.WechatHelper.GetMiniPic(setting, token);
            if (!res.IsOK)
            {
                oup.IsOK = false;
                return oup;
            }

            var key = ConfigurationManager.AppSettings["AccessKeyId"];
            var keySecret = ConfigurationManager.AppSettings["AccessKeySecret"];
            var endpoint = ConfigurationManager.AppSettings["Endpoint"];
            var bucket = ConfigurationManager.AppSettings["BucketName"];
            AliyunOssHelper oss = new AliyunOssHelper(key, keySecret, endpoint, bucket);

            //判断是生产还是测试
            var client = ConfigurationManager.AppSettings["ProductionOrNot"];
            var fileName = "";
            if (client.Trim().ToString() == "false")
            {
                fileName = "deve/tp/WechatMiniPicUrl/" + existJob.JobCompanyID.ToString() + "_" + existJob.PKID.ToString() + "_" + Guid.NewGuid().ToString().Replace("-", "") + ".jpg";
            }
            else
            {
                fileName = "prod/WechatMiniPicUrl/" + existJob.JobCompanyID.ToString() + "_" + existJob.PKID.ToString() + "_" + Guid.NewGuid().ToString().Replace("-", "") + ".jpg";
            }

            var imgRes = oss.PushImg(res.Buffer, fileName);
            //上传成功,回写信息
            if (imgRes.Success)
            {
                oup.IsOK = true;
                oup.ImgUrl = "https://oss.mrmatch.net/" + fileName;
                existJob.WechatMiniPic = oup.ImgUrl;
                existJob.LastModifiedTime = DateTime.Now;
                unitOfWork.RegisterUpdate(existJob);
                await unitOfWork.CommitAsync();
            }
            else
            {
                oup.IsOK = false;
                oup.ImgUrl = "";
            }
            return oup;
        }

        public async Task<BoolMessageOup> GetAllJobMiniPicUrlAsync(string token)
        {
            var oup = new BoolMessageOup();
            var jobList = await job.GetAllListAsync(x => x.Valid == true && x.WechatMiniPic.Length < 10);

            jobList.ForEach(item =>
            {
                var setting = new MiniWeChatPic();
                setting.scene = "id=" + item.PKID.ToString();
                setting.page = ConfigurationManager.AppSettings["miniPic"];
                setting.width = 280;
                var res = Common.Wechat.WechatHelper.GetMiniPic(setting, token);
                if (res.IsOK)
                {
                    var key = ConfigurationManager.AppSettings["AccessKeyId"];
                    var keySecret = ConfigurationManager.AppSettings["AccessKeySecret"];
                    var endpoint = ConfigurationManager.AppSettings["Endpoint"];
                    var bucket = ConfigurationManager.AppSettings["BucketName"];
                    AliyunOssHelper oss = new AliyunOssHelper(key, keySecret, endpoint, bucket);

                    //判断是生产还是测试
                    var client = ConfigurationManager.AppSettings["ProductionOrNot"];
                    var fileName = "";
                    if (client.Trim().ToString() == "false")
                    {
                        fileName = "deve/tp/WechatMiniPicUrl/" + item.JobCompanyID.ToString() + "_" + item.PKID.ToString() + "_" + Guid.NewGuid().ToString().Replace("-", "") + ".jpg";
                    }
                    else
                    {
                        fileName = "prod/WechatMiniPicUrl/" + item.JobCompanyID.ToString() + "_" + item.PKID.ToString() + "_" + Guid.NewGuid().ToString().Replace("-", "") + ".jpg";
                    }

                    var imgRes = oss.PushImg(res.Buffer, fileName);
                    if (imgRes.Success)
                    {
                        item.WechatMiniPic = "https://oss.mrmatch.net/" + fileName;
                        item.LastModifiedTime = DateTime.Now;
                        unitOfWork.RegisterUpdate(item);
                    }
                }
            });

            oup.BoolResult = await unitOfWork.CommitAsync();

            if (!oup.BoolResult)
            {
                oup.Message = "操作失败,请重试";
            }
            else
            {
                oup.Message = "操作成功";
            }

            return oup;
        }

        public async Task<AddOrUpdateJobOup> AddOrUpdateJobAsync(JobPublishInp input)
        {
            var oup = new AddOrUpdateJobOup();
            var now = DateTime.Now;
            //上架验证
            if (input.ActiveStatus)
            {
                //签约数据
                var contract = await companyContract.FirstOrDefaultAsync(
                    x => x.CompanyID == input.BasicCompanyID
                    && x.StartDate <= now
                    && x.ExpireDate >= now
                    && x.OnEnabled == true);
                if (!contract.IsNullEntity())
                {
                    oup.BoolResult = false;
                    oup.Message = "未签约或签约过期,不可发布职位";
                    oup.PKID = 0;
                    return oup;
                }

                //查询已上架数量
                var jobCount = await job.CountAsync(
                    x => x.Valid == true
                    && x.AccountID == input.AccountID
                    && x.ActiveStatus == true);
                if (jobCount >= contract.AdvertiseCount)
                {
                    oup.BoolResult = false;
                    oup.Message = "您的上架职位数量已经满了,请下架其他的职位或联系客服人员更新签约.";
                    oup.PKID = 0;
                    return oup;
                }
            }

            if (input.PKID > 0)
            {
                //修改
                var existData = await job.FirstOrDefaultAsync(x => x.PKID == input.PKID);
                existData = input.MapTo<TP_Job>(existData);
                existData.LastModifiedTime = now;
                unitOfWork.RegisterUpdate(existData);
                oup.BoolResult = await unitOfWork.CommitAsync();
                if (!oup.BoolResult)
                {
                    oup.Message = "修改失败,请重试";
                }
                else
                {
                    oup.Message = "修改成功";
                    oup.PKID = existData.PKID;
                }
                return oup;
            }

            var newData = input.MapTo<JobPublishInp, TP_Job>();
            newData.EmergencyLevel = 0;
            newData.SearchCount = 0;
            newData.WechatMiniPic = "";
            newData.CreatedTime = now;
            newData.LastModifiedTime = now;
            newData.Valid = true;
            unitOfWork.RegisterNew(newData);
            oup.BoolResult = await unitOfWork.CommitAsync();
            if (!oup.BoolResult)
            {
                oup.Message = "新增失败,请重试";
            }
            else
            {
                oup.Message = "新增成功";
                oup.PKID = newData.PKID;
            }
            return oup;
        }

        public async Task<PagenationOutput<JobManageListOup>> GetActivityJobListAsync(PagenationInput input, bool type, long jobID, string jobName, long companyID, long accountID)
        {
            PagenationOutput<JobManageListOup> data = new PagenationOutput<JobManageListOup>();
            var total = 0;
            var pageCount = 0;

            var companyInfo = await company.FirstOrDefaultAsync(x => x.PKID == companyID);
            if (!companyInfo.IsNullEntity())
            {
                return null;
            }
            var jobs = job.GetAll(
                x => x.Valid == true
                && x.AccountID == accountID
                && x.BasicCompanyID == companyID
                && x.ActiveStatus == type)
                .WhereIf(x => x.PKID == jobID, jobID > 0)
                .WhereIf(x => x.JobName.Contains(jobName), !string.IsNullOrEmpty(jobName))
                .OrderByDescending(x => x.LastModifiedTime)
                    .Skip(input.PageSize * (input.PageIndex - 1))
                    .Take(input.PageSize)
                    .ToList();

            if (jobs.Count > 0)
            {
                //取主体公司id,旗下猎头公司id
                var jobCompanyIDs = new List<long>();
                jobs.ForEach(item =>
                {
                    if (item.AgentJob)
                    {
                        jobCompanyIDs.Add(item.JobCompanyID);
                    }
                });
                //去重
                jobCompanyIDs = jobCompanyIDs.Distinct().ToList();
                //获取分页后的主体企业旗下的猎头企业
                var jobCompanies = await agentCompany.GetAllListAsync(x => x.Valid == true && jobCompanyIDs.Contains(x.PKID));


                var oup = jobs.MapToList<TP_Job, JobManageListOup>();

                var applyIDs = oup.Select(x => x.PKID);
                var applies = await apply.GetAllListAsync(x => x.Valid == true && applyIDs.Contains(x.JobID));

                oup.ForEach(item =>
                {
                    if (item.AgentJob)
                    {
                        var com = jobCompanies.FirstOrDefault(x => x.PKID == item.JobCompanyID);
                        item.JobCompanyName = com.CompanyName;
                        item.JobCompanyShortName = com.ShortName;
                    }
                    else
                    {
                        item.JobCompanyName = companyInfo.CompanyName;
                        item.JobCompanyShortName = companyInfo.ShortName;
                    }
                    item.ApplyCount = applies.Count(x => x.JobID == item.PKID);
                });
                data.Total = jobs.Count;
                data.PageIndex = input.PageIndex;
                data.DataList = oup;
                return data;
            }
            return null;
        }

        public async Task<PagenationOutput<JobManageListOup>> GetJobDraftListAsync(PagenationInput input, string jobName, long companyID, long accountID)
        {
            PagenationOutput<JobManageListOup> data = new PagenationOutput<JobManageListOup>();
            var total = 0;
            var pageCount = 0;

            var companyInfo = await company.FirstOrDefaultAsync(x => x.PKID == companyID);
            if (!companyInfo.IsNullEntity())
            {
                return null;
            }
            var jobs = jobDraft.GetAll(
                x => x.Valid == true
                && x.AccountID == accountID
                && x.BasicCompanyID == companyID)
                .WhereIf(x => x.JobName.Contains(jobName), !string.IsNullOrEmpty(jobName))
                .OrderByDescending(x => x.LastModifiedTime)
                    .Skip(input.PageSize * (input.PageIndex - 1))
                    .Take(input.PageSize)
                    .ToList();

            if (jobs.Count > 0)
            {
                //取主体公司id,旗下猎头公司id
                var jobCompanyIDs = new List<long>();
                jobs.ForEach(item =>
                {
                    if (item.AgentJob)
                    {
                        jobCompanyIDs.Add(item.JobCompanyID);
                    }
                });
                //去重
                jobCompanyIDs = jobCompanyIDs.Distinct().ToList();
                //获取分页后的主体企业旗下的猎头企业
                var jobCompanies = await agentCompany.GetAllListAsync(x => x.Valid == true && jobCompanyIDs.Contains(x.PKID));


                var oup = jobs.MapToList<TP_JobDraft, JobManageListOup>();

                var applyIDs = oup.Select(x => x.PKID);
                var applies = await apply.GetAllListAsync(x => x.Valid == true && applyIDs.Contains(x.JobID));

                oup.ForEach(item =>
                {
                    if (item.AgentJob)
                    {
                        var com = jobCompanies.FirstOrDefault(x => x.PKID == item.JobCompanyID);
                        item.JobCompanyName = com.CompanyName;
                        item.JobCompanyShortName = com.ShortName;
                    }
                    else
                    {
                        item.JobCompanyName = companyInfo.CompanyName;
                        item.JobCompanyShortName = companyInfo.ShortName;
                    }
                    item.ApplyCount = applies.Count(x => x.JobID == item.PKID);
                });
                data.Total = jobs.Count;
                data.PageIndex = input.PageIndex;
                data.DataList = oup;
                return data;
            }
            return null;
        }

        public async Task<BoolMessageOup> UpdateJobStatusAsync(long jobID, bool status, long accountID)
        {
            var oup = new BoolMessageOup();
            var now = DateTime.Now;
            var jobInfo = await job.FirstOrDefaultAsync(x => x.PKID == jobID);
            if (!jobInfo.IsNullEntity())
            {
                oup.BoolResult = false;
                oup.Message = "入参错误,请重试";
                return oup;
            }

            if (status)
            {
                //签约数据
                var contract = await companyContract.FirstOrDefaultAsync(
                    x => x.CompanyID == jobInfo.BasicCompanyID
                    && x.StartDate <= now
                    && x.ExpireDate >= now
                    && x.OnEnabled == true);
                if (!contract.IsNullEntity())
                {
                    oup.BoolResult = false;
                    oup.Message = "未签约或签约过期,不可发布职位";
                    return oup;
                }

                //查询已上架数量
                var jobCount = await job.CountAsync(
                    x => x.Valid == true
                    && x.AccountID == accountID
                    && x.ActiveStatus == true);
                if (jobCount >= contract.AdvertiseCount)
                {
                    oup.BoolResult = false;
                    oup.Message = "您的上架职位数量已经满了,请下架其他的职位或联系客服人员更新签约.";
                    return oup;
                }
            }

            jobInfo.ActiveStatus = status;
            jobInfo.LastModifiedTime = now;
            unitOfWork.RegisterUpdate(jobInfo);
            oup.BoolResult = await unitOfWork.CommitAsync();
            if (!oup.BoolResult)
            {
                oup.Message = "操作失败,请重试";
            }
            else
            {
                oup.Message = "操作成功";
            }
            return oup;
        }

        public async Task<BoolMessageOup> AddOrUpdateJobDraftAsync(JobDraftInp input, TP_Account account)
        {
            var oup = new BoolMessageOup();
            var now = DateTime.Now;
            if (input.PKID > 0)
            {
                var draft = await jobDraft.FirstOrDefaultAsync(x => x.PKID == input.PKID);
                if (!draft.IsNullEntity())
                {
                    oup.BoolResult = false;
                    oup.Message = "入参错误,找不到数据";
                    return oup;
                }

                draft.FunctionID = input.FunctionID;
                draft.DistrictID = input.DistrictID;
                draft.Reporter = input.Reporter;
                draft.SubordinateCount = input.SubordinateCount;
                draft.MaxAnnualSalary = input.MaxAnnualSalary;
                draft.MinAnnualSalary = input.MinAnnualSalary;
                draft.SalaryOpen = input.SalaryOpen;
                draft.SalaryComposition = input.SalaryComposition;
                draft.SalaryCompositionExtra = input.SalaryCompositionExtra;
                draft.SocialSecurity = input.SocialSecurity;
                draft.SocialSecurityExtra = input.SocialSecurityExtra;
                draft.LiveWelfare = input.LiveWelfare;
                draft.LiveWelfareExtra = input.LiveWelfareExtra;
                draft.AnnualLeave = input.AnnualLeave;
                draft.AnnualLeaveExtra = input.AnnualLeaveExtra;
                draft.Subsidy = input.Subsidy;
                draft.SubsidyExtra = input.SubsidyExtra;
                draft.AgeRequirements = input.AgeRequirements;
                draft.MajorRequirements = input.MajorRequirements;
                draft.WorkYears = input.WorkYears;
                draft.Degree = input.Degree;
                draft.FullTime = input.FullTime;
                draft.Language = input.Language;
                draft.LanguageExtra = input.LanguageExtra;
                draft.Skills = input.Skills;
                draft.SkillsExtra = input.SkillsExtra;
                draft.JobDescription = input.JobDescription;
                draft.JobRequirements = input.JobRequirements;
                draft.ExtraInfo = input.ExtraInfo;
                draft.ActiveStatus = input.ActiveStatus;
                draft.AgentJob = input.AgentJob;
                draft.ShowItems = input.ShowItems;
                draft.ShowPersonal = input.ShowPersonal;
                draft.LastModifiedTime = now;
                unitOfWork.RegisterUpdate(draft);
                oup.BoolResult = await unitOfWork.CommitAsync();
                if (!oup.BoolResult)
                {
                    oup.Message = "操作失败";
                }
                else
                {
                    oup.Message = "操作成功";
                }
                return oup;
            }

            //当前已有的草稿数量
            var currentDraftCount = await jobDraft.CountAsync(x => x.Valid == true && x.BasicCompanyID == account.CompanyID && x.AccountID == account.PKID);
            if (currentDraftCount >= account.DraftLimiteCount)
            {
                oup.BoolResult = false;
                oup.Message = "当前您的草稿数量已经达到上线.请删除您不需要的草稿或直接选择一个草稿编辑";
                return oup;
            }

            //新增
            var newData = new TP_JobDraft();
            newData.BasicCompanyID = account.CompanyID;
            newData.AccountID = account.PKID;
            newData.JobCompanyID = input.JobCompanyID;
            newData.JobName = input.JobName;
            newData.FunctionID = input.FunctionID;
            newData.DistrictID = input.DistrictID;
            newData.Reporter = input.Reporter;
            newData.SubordinateCount = input.SubordinateCount;
            newData.MaxAnnualSalary = input.MaxAnnualSalary;
            newData.MinAnnualSalary = input.MinAnnualSalary;
            newData.SalaryOpen = input.SalaryOpen;
            newData.SalaryComposition = input.SalaryComposition;
            newData.SalaryCompositionExtra = input.SalaryCompositionExtra;
            newData.SocialSecurity = input.SocialSecurity;
            newData.SocialSecurityExtra = input.SocialSecurityExtra;
            newData.LiveWelfare = input.LiveWelfare;
            newData.LiveWelfareExtra = input.LiveWelfareExtra;
            newData.AnnualLeave = input.AnnualLeave;
            newData.AnnualLeaveExtra = input.AnnualLeaveExtra;
            newData.Subsidy = input.Subsidy;
            newData.SubsidyExtra = input.SubsidyExtra;
            newData.AgeRequirements = input.AgeRequirements;
            newData.MajorRequirements = input.MajorRequirements;
            newData.WorkYears = input.WorkYears;
            newData.Degree = input.Degree;
            newData.FullTime = input.FullTime;
            newData.Language = input.Language;
            newData.LanguageExtra = input.LanguageExtra;
            newData.Skills = input.Skills;
            newData.SkillsExtra = input.SkillsExtra;
            newData.JobDescription = input.JobDescription;
            newData.JobRequirements = input.JobRequirements;
            newData.ExtraInfo = input.ExtraInfo;
            newData.ActiveStatus = input.ActiveStatus;
            newData.AgentJob = input.AgentJob;
            newData.ShowItems = input.ShowItems;
            newData.ShowPersonal = input.ShowPersonal;
            //紧急程度先默认为0
            newData.EmergencyLevel = 0;
            newData.CreatedTime = now;
            newData.LastModifiedTime = now;
            newData.Valid = true;
            unitOfWork.RegisterNew(newData);
            oup.BoolResult = await unitOfWork.CommitAsync();
            if (!oup.BoolResult)
            {
                oup.Message = "创建草稿失败,请重试";
            }
            else
            {
                oup.Message = "创建草稿成功";
            }
            return oup;
        }

        public async Task<JobDraftDetailsOup> GetJobDraftDetailsAsync(long PKID)
        {
            var existJob = (await jobDraft.FirstOrDefaultAsync(x => x.PKID == PKID && x.Valid == true)).MapTo<TP_JobDraft, JobDraftDetailsOup>();
            //空对象
            if (!existJob.IsNullEntity())
            {
                return null;
            }

            return existJob;
        }

        public async Task<BizJobDetailsOup> GetJobDetailsAsync(long PKID)
        {
            var existJob = (await job.FirstOrDefaultAsync(x => x.PKID == PKID && x.Valid == true)).MapTo<TP_Job, BizJobDetailsOup>();
            //空对象
            if (!existJob.IsNullEntity())
            {
                return null;
            }
            if (existJob.AgentJob)
            {
                existJob.CompanyName = (await agentCompany.FirstOrDefaultAsync(x => x.PKID == existJob.JobCompanyID)).CompanyName;
                existJob.CompanyShortName = (await agentCompany.FirstOrDefaultAsync(x => x.PKID == existJob.JobCompanyID)).ShortName;
            }
            else
            {
                existJob.CompanyName = (await company.FirstOrDefaultAsync(x => x.PKID == existJob.BasicCompanyID)).CompanyName;
                existJob.CompanyShortName = (await company.FirstOrDefaultAsync(x => x.PKID == existJob.BasicCompanyID)).ShortName;
            }

            return existJob;
        }

        public async Task<BoolMessageOup> DestroyJobDraftAsync(long PKID)
        {
            var oup = new BoolMessageOup();
            var existJob = await jobDraft.FirstOrDefaultAsync(x => x.PKID == PKID && x.Valid == true);
            //空对象
            if (!existJob.IsNullEntity())
            {
                oup.BoolResult = false;
                oup.Message = "入参错误,操作失败";
                return oup;
            }

            existJob.Valid = false;
            existJob.LastModifiedTime = DateTime.Now;
            unitOfWork.RegisterUpdate(existJob);
            oup.BoolResult = await unitOfWork.CommitAsync();
            if (!oup.BoolResult)
            {
                oup.Message = "删除失败,请重试";
            }
            else
            {
                oup.Message = "删除草稿成功";
            }
            return oup;
        }

        public async Task<PagenationOutput<JobProfilesListOup>> GetJobProfilesByPagenationAsync(PagenationInput input, long jobID, long accountID)
        {
            PagenationOutput<JobProfilesListOup> data = new PagenationOutput<JobProfilesListOup>();
            var total = 0;
            var pageCount = 0;

            var applies = await apply.GetAllListAsync(
                x => x.Valid == true
                && x.AccountID == accountID
                && x.JobID == jobID);
            if (applies.Count <= 0)
            {
                return new PagenationOutput<JobProfilesListOup>();
            }

            var userids = applies.Select(x => x.UserID).ToList();
            var profiles = (await profileSnap.GetAllListAsync(x => x.Valid == true && userids.Contains(x.UserID)))
                .Skip(input.PageSize * (input.PageIndex - 1))
                .Take(input.PageSize)
                .MapToList<TP_ProfileSnap, JobProfilesListOup>();
            var workUserIDs = profiles.Select(x => x.UserID).ToList();
            var works = await workExperienceSnap.GetAllListAsync(x => x.Valid == true && workUserIDs.Contains(x.UserID));
            profiles.ForEach(item =>
            {
                var coms = works.Where(x => x.UserID == item.UserID).OrderByDescending(x => x.StartDate).FirstOrDefault();
                item.CurrentCompany = coms.CompanyName;
                item.CurrentPosition = coms.Position;
                item.ApplyTime = applies.FirstOrDefault(x => x.UserID == item.UserID).CreatedTime;
            });
            data.Total = profiles.Count;
            data.PageIndex = input.PageIndex;
            data.DataList = profiles;
            return data;
        }
    }
}
