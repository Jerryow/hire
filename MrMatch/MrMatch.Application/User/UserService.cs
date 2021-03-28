using MrMatch.Application.System.Oup;
using MrMatch.Domain.Models;
using MrMatch.Domain.EntityBase.Repository;
using MrMatch.Common.Mapper;
using MrMatch.Application.System.Inp;
using System.Threading.Tasks;
using MrMatch.MysqlFramework.Extensions;
using System;
using System.Collections.Generic;
using MrMatch.Common.Encrypt;
using MrMatch.Common.Redis;
using System.Linq;
using MrMatch.Application.User.Inp;
using MrMatch.MysqlFramework;
using System.Configuration;
using MrMatch.Application.User.Oup;
using MrMatch.Common.Extension;
using MrMatch.Common.ImageHelper;

namespace MrMatch.Application.User
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ICommunityRepository<mq_user> user;
        private readonly IRepository<TP_UserProfile> profile;
        private readonly IRepository<TP_UserWorkExperience> workExperience;
        private readonly IRepository<TP_UserJobIntention> jobIntention;
        private readonly IRepository<TP_UserTags> userTags;
        private readonly IRepository<TP_UserEducation> education;
        private readonly IRepository<TP_UserAvoid> avoid;
        private readonly IRepository<TP_Function> function;
        private readonly IRepository<TP_District> district;
        private readonly IRepository<TP_Tags> tags;
        private readonly IRepository<TP_MessageTemplate> temp;
        private readonly IRepository<TP_ProfileSnap> profileSnap;
        private readonly IRepository<TP_WorkExperienceSnap> workExperienceSnap;
        private readonly IRepository<TP_EducationSnap> educationSnap;
        public UserService(
            IUnitOfWork _unitOfWork,
            ICommunityRepository<mq_user> _user,
            IRepository<TP_UserProfile> _profile,
            IRepository<TP_UserWorkExperience> _workExperience,
            IRepository<TP_UserJobIntention> _jobIntention,
            IRepository<TP_UserTags> _userTags,
            IRepository<TP_UserEducation> _education,
            IRepository<TP_UserAvoid> _avoid,
            IRepository<TP_Function> _function,
            IRepository<TP_District> _district,
            IRepository<TP_Tags> _tags,
            IRepository<TP_MessageTemplate> _temp,
            IRepository<TP_ProfileSnap> _profileSnap,
            IRepository<TP_WorkExperienceSnap> _workExperienceSnap,
            IRepository<TP_EducationSnap> _educationSnap)
        {
            unitOfWork = _unitOfWork;
            user = _user;
            profile = _profile;
            workExperience = _workExperience;
            jobIntention = _jobIntention;
            userTags = _userTags;
            education = _education;
            avoid = _avoid;
            function = _function;
            district = _district;
            tags = _tags;
            temp = _temp;
            profileSnap = _profileSnap;
            workExperienceSnap = _workExperienceSnap;
            educationSnap = _educationSnap;
        }

        #region mquser
        public PagenationOutput<UserListOup> GetUserByPagenation(PagenationInput input)
        {
            PagenationOutput<UserListOup> data = new PagenationOutput<UserListOup>();
            var total = 0;
            var pageCount = 0;
            var query = user.GetAll();
            var list = user.GetByPagenation<long>
                (input.PageIndex,
                input.PageSize,
                out total,
                out pageCount,
                query,
                x => x.id,
                input.IsAsc).ToList();
            var ids = list.Select(x => x.id).ToList();
            var profiles = profile.GetAllList(x => ids.Contains(x.UserID));
            var oupList = new List<UserListOup>();
            list.ForEach(item =>
            {
                var oup = new UserListOup();
                var currentPro = profiles.FirstOrDefault(x => x.UserID == item.id);
                oup.PKID = item.id;
                oup.RealName = currentPro.RealName;
                oup.Education = currentPro.Education;
                oup.CurrentPosition = currentPro.CurrentPosition;
                oupList.Add(oup);
            });
            data.PageIndex = input.PageIndex;
            data.PageCount = pageCount;
            data.Total = total;
            data.DataList = oupList;
            return data;
        }
        public PagenationOutput<UserListOup> GetCheckUserByPagenation(PagenationInput input)
        {
            PagenationOutput<UserListOup> data = new PagenationOutput<UserListOup>();
            var total = 0;
            var pageCount = 0;
            var query = user.GetAll(x => x.ApproveStatus == 1);
            var list = user.GetByPagenation<long>
                (input.PageIndex,
                input.PageSize,
                out total,
                out pageCount,
                query,
                x => x.id,
                input.IsAsc).ToList();
            var ids = list.Select(x => x.id).ToList();
            var profiles = profile.GetAllList(x => ids.Contains(x.UserID));
            var oupList = new List<UserListOup>();
            list.ForEach(item =>
            {
                var oup = new UserListOup();
                var currentPro = profiles.FirstOrDefault(x => x.UserID == item.id);
                oup.PKID = item.id;
                oup.RealName = currentPro.RealName;
                oup.Education = currentPro.Education;
                oup.CurrentPosition = currentPro.CurrentPosition;
                oupList.Add(oup);
            });
            data.PageIndex = input.PageIndex;
            data.PageCount = pageCount;
            data.Total = total;
            data.DataList = oupList;
            return data;
        }

        public async Task<BasicUserOup> GetBasicUserAsync(long userID)
        {
            var oup = new BasicUserOup();
            var basicUser = await user.FirstOrDefaultAsync(x => x.id == userID);
            if (!basicUser.IsNullEntity())
            {
                return null;
            }
            oup.PKID = basicUser.id;
            oup.NickName = basicUser.nick_name;
            oup.AvatarUrl = basicUser.img;
            oup.ProfileSnap = basicUser.ProfileSnap;
            oup.ApproveStatus = basicUser.ApproveStatus;
            oup.ActiveStatus = basicUser.ActiveStatus;
            oup.IntentionFunctionIDs = basicUser.IntentionFunctionIDs;
            oup.LocationIDs = basicUser.LocationIDs;
            oup.AnnualSalary = basicUser.AnnualSalary;

            if (!string.IsNullOrEmpty(oup.IntentionFunctionIDs))
            {
                var funcids = Common.Extension.StringExtension.SplitFunctionsToLongList(oup.IntentionFunctionIDs, ',');
                if (funcids.Count > 0)
                {
                    var data = await function.GetAllListAsync(x => funcids.Contains(x.PKID));
                    for (int i = 0; i < funcids.Count; i++)
                    {
                        oup.IntentionFunctionName += data.FirstOrDefault(x => x.PKID == funcids[i]).FunctionName;
                        oup.IntentionFunctionName += ",";
                    }

                    oup.IntentionFunctionName = oup.IntentionFunctionName.SubStringLast();
                }
            }

            long locaid = 0;
            long.TryParse(oup.LocationIDs, out locaid);
            if (locaid > 0)
            {
                oup.LocationName = (await district.FirstOrDefaultAsync(x => x.PKID == locaid)).DistrictName;
            }

            return oup;
        }

        public async Task<BoolMessageOup> AddOrUpdateUserJobBoardAsync(AddOrUpdateUserJobBoardInp input)
        {
            var oup = new BoolMessageOup();
            var mquser = await user.FirstOrDefaultAsync(x => x.id == input.UserID);
            if (!mquser.IsNullEntity())
            {
                oup.BoolResult = false;
                oup.Message = "操作失败,请重试";
                return oup;
            }
            if (input.WhichProperty == 1)
            {
                mquser.IntentionFunctionIDs = input.PropertyValue;
            }
            else if (input.WhichProperty == 2)
            {
                mquser.LocationIDs = input.PropertyValue;
            }
            else
            {
                mquser.AnnualSalary = input.PropertyValue;
            }
            unitOfWork.RegisterUpdate(mquser);
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

        public async Task<BoolMessageOup> UpdateUserActivityAsync(long userID, bool status)
        {
            var oup = new BoolMessageOup();
            var mquser = await user.FirstOrDefaultAsync(x => x.id == userID);
            if (!mquser.IsNullEntity())
            {
                oup.BoolResult = false;
                oup.Message = "操作失败,请重试";
                return oup;
            }

            mquser.ActiveStatus = status;
            unitOfWork.RegisterUpdate(mquser);
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

        public async Task<BoolMessageOup> CommitApproveAsync(long userID)
        {
            var oup = new BoolMessageOup();
            var mquser = await user.FirstOrDefaultAsync(x => x.id == userID);
            if (!mquser.IsNullEntity())
            {
                oup.BoolResult = false;
                oup.Message = "操作失败,请重试";
                return oup;
            }

            if (mquser.ApproveStatus == CommonEnum.TP_Profile_ApproveStatus.审核中.GetHashCode())
            {
                oup.BoolResult = false;
                oup.Message = "您的简历正在审核中,请耐心等待";
                return oup;
            }

            //验证是否符合审核条件
            var intention = await jobIntention.FirstOrDefaultAsync(x => x.Valid == true && x.UserID == userID);
            if (!intention.IsNullEntity())
            {
                oup.BoolResult = false;
                oup.Message = "请先完善基本信息";
                return oup;
            }
            var work = await workExperience.GetAllListAsync(x => x.Valid == true && x.UserID == userID);
            if (work.Count <= 0)
            {
                oup.BoolResult = false;
                oup.Message = "请先完善基本信息";
                return oup;
            }
            var edu = await education.GetAllListAsync(x => x.Valid == true && x.UserID == userID);
            if (edu.Count <= 0)
            {
                oup.BoolResult = false;
                oup.Message = "请先完善基本信息";
                return oup;
            }
            var pro = await profile.FirstOrDefaultAsync(x => x.Valid == true && x.UserID == userID);
            if (!pro.IsNullEntity())
            {
                oup.BoolResult = false;
                oup.Message = "请先完善基本信息";
                return oup;
            }


            mquser.ApproveStatus = CommonEnum.TP_Profile_ApproveStatus.审核中.GetHashCode();
            unitOfWork.RegisterUpdate(mquser);
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

        public async Task<BoolMessageOup> ApproveUserAsync(long userID, int status)
        {
            var oup = new BoolMessageOup();
            var mquser = await user.FirstOrDefaultAsync(x => x.id == userID);
            if (!mquser.IsNullEntity())
            {
                oup.BoolResult = false;
                oup.Message = "操作失败,请重试";
                return oup;
            }

            if (mquser.ApproveStatus != CommonEnum.TP_Profile_ApproveStatus.审核中.GetHashCode())
            {
                oup.BoolResult = false;
                oup.Message = "不符合审核标准";
                return oup;
            }
            var now = DateTime.Now;
            var msg = new TP_UserMessage();
            msg.RecieverID = userID;
            msg.IsSent = true;
            msg.Valid = true;
            msg.CreatedTime = now;
            msg.LastModifiedTime = now;

            //判断是通过还是驳回
            if (status == CommonEnum.TP_Profile_ApproveStatus.审核驳回.GetHashCode())
            {
                //先取缓存,后取数据库
                msg.MessageSubject = CommonEnum.PrintMessageTemplateTitle(CommonEnum.TP_MessageTemplate.审核驳回.GetHashCode());
                msg.MessageContent = Cache.GetCacheHelper.GetTemplate(CommonEnum.TP_MessageTemplate.审核驳回.GetHashCode().ToString());
                if (string.IsNullOrEmpty(msg.MessageContent))
                {
                    var code = CommonEnum.TP_MessageTemplate.审核驳回.GetHashCode().ToString();
                    msg.MessageContent = (await temp.FirstOrDefaultAsync(x => x.TemplateCode == code)).TemplateContent;
                }
            }
            else
            {
                //先取缓存,后取数据库
                msg.MessageSubject = CommonEnum.PrintMessageTemplateTitle(CommonEnum.TP_MessageTemplate.审核通过.GetHashCode());
                msg.MessageContent = Cache.GetCacheHelper.GetTemplate(CommonEnum.TP_MessageTemplate.审核通过.GetHashCode().ToString());
                if (string.IsNullOrEmpty(msg.MessageContent))
                {
                    var code = CommonEnum.TP_MessageTemplate.审核通过.GetHashCode().ToString();
                    msg.MessageContent = (await temp.FirstOrDefaultAsync(x => x.TemplateCode == code)).TemplateContent;
                }

                var intention = await jobIntention.FirstOrDefaultAsync(x => x.Valid == true && x.UserID == userID);
                var work = await workExperience.GetAllListAsync(x => x.Valid == true && x.UserID == userID);
                var edu = await education.GetAllListAsync(x => x.Valid == true && x.UserID == userID);
                var pro = await profile.FirstOrDefaultAsync(x => x.Valid == true && x.UserID == userID);

                //审核通过更新快照
                //判断是否有快照
                if (mquser.ProfileSnap)
                {
                    //替换
                    var existPro = await profileSnap.FirstOrDefaultAsync(x => x.UserID == userID && x.Valid == true);
                    existPro = LogicHelper.ReplaceProfileSnap(pro, intention, existPro);
                    unitOfWork.RegisterUpdate(existPro);
                    var existEdu = await educationSnap.GetAllListAsync(x => x.UserID == userID && x.Valid == true);
                    existEdu.ForEach(item =>
                    {
                        item.Valid = false;
                        item.LastModifiedTime = now;
                        unitOfWork.RegisterUpdate(item);
                    });
                    var existWork = await workExperienceSnap.GetAllListAsync(x => x.UserID == userID && x.Valid == true);
                    existWork.ForEach(item =>
                    {
                        item.Valid = false;
                        item.LastModifiedTime = now;
                        unitOfWork.RegisterUpdate(item);
                    });

                    var newWork = LogicHelper.AddWorkSnap(work);
                    newWork.ForEach(item =>
                    {
                        unitOfWork.RegisterNew(item);
                    });
                    var newEdu = LogicHelper.AddEducationSnap(edu);
                    newEdu.ForEach(item =>
                    {
                        unitOfWork.RegisterNew(item);
                    });

                    //回写简历和求职意向的更改状态
                    pro.IsUpdated = false;
                    unitOfWork.RegisterUpdate(pro);
                    intention.IsUpdated = false;
                    unitOfWork.RegisterUpdate(intention);
                    mquser.ActiveStatus = true;
                }
                else
                {
                    //新增
                    var newPro = LogicHelper.AddProfileSnap(pro, intention);
                    unitOfWork.RegisterNew(newPro);
                    var newWork = LogicHelper.AddWorkSnap(work);
                    newWork.ForEach(item =>
                    {
                        unitOfWork.RegisterNew(item);
                    });
                    var newEdu = LogicHelper.AddEducationSnap(edu);
                    newEdu.ForEach(item =>
                    {
                        unitOfWork.RegisterNew(item);
                    });
                    mquser.ProfileSnap = true;
                    mquser.ActiveStatus = true;
                }
            }

            unitOfWork.RegisterNew(msg);

            mquser.ApproveStatus = status;
            unitOfWork.RegisterUpdate(mquser);
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

        public async Task<SnapUpdateOup> SnapUpdateStatusAsync(long userID)
        {
            var oup = new SnapUpdateOup();
            var mquser = await user.FirstOrDefaultAsync(x => x.id == userID);
            if (!mquser.IsNullEntity())
            {
                return null;
            }
            var intention = await jobIntention.FirstOrDefaultAsync(x => x.Valid == true && x.UserID == userID);
            var work = await workExperience.GetAllListAsync(x => x.Valid == true && x.UserID == userID);
            var workSnap = await workExperienceSnap.GetAllListAsync(x => x.Valid == true && x.UserID == userID);
            var workUpdate = LogicHelper.CompareWorkExperience(work, workSnap);
            var edu = await education.GetAllListAsync(x => x.Valid == true && x.UserID == userID);
            var eduSnap = await educationSnap.GetAllListAsync(x => x.Valid == true && x.UserID == userID);
            var eduUpdate = LogicHelper.CompareEducation(edu, eduSnap);
            var pro = await profile.FirstOrDefaultAsync(x => x.Valid == true && x.UserID == userID);
            oup.JobIntention = intention.IsUpdated;
            oup.Profile = pro.IsUpdated;
            oup.WorkEx = workUpdate;
            oup.Education = eduUpdate;
            return oup;
        }

        public async Task<WechatAccountOup> GetWechatAccountAsync(long userID)
        {
            var oup = new WechatAccountOup();
            var basicUser = await user.FirstOrDefaultAsync(x => x.id == userID);
            if (!basicUser.IsNullEntity())
            {
                return null;
            }
            oup.PKID = basicUser.id;
            oup.CellPhone = basicUser.phone;
            oup.Email = basicUser.Email;
            return oup;
        }

        public async Task<BoolMessageOup> UpdateWechatAccountAsync(long userID, string email)
        {
            var oup = new BoolMessageOup();
            var basicUser = await user.FirstOrDefaultAsync(x => x.id == userID);
            if (!basicUser.IsNullEntity())
            {
                oup.BoolResult = false;
                oup.Message = "修改失败,请重试";
                return oup;
            }
            basicUser.Email = email;
            unitOfWork.RegisterUpdate(basicUser);
            oup.BoolResult = await unitOfWork.CommitAsync();
            if (!oup.BoolResult)
            {
                oup.Message = "修改失败,请重试";
            }
            else
            {
                oup.Message = "修改成功";
            }
            return oup;
        }

        public async Task<BoolMessageOup> UpdateUserAvatarAsync(long userID, byte[] img)
        {
            var oup = new BoolMessageOup();
            var basicUser = await user.FirstOrDefaultAsync(x => x.id == userID);
            if (!basicUser.IsNullEntity())
            {
                oup.BoolResult = false;
                oup.Message = "入参错误";
                return oup;
            }

            var key = ConfigurationManager.AppSettings["AccessKeyId"];
            var secret = ConfigurationManager.AppSettings["AccessKeySecret"];
            var endpoint = ConfigurationManager.AppSettings["Endpoint"];
            var bucket = ConfigurationManager.AppSettings["BucketName"];
            AliyunOssHelper oss = new AliyunOssHelper(key, secret, endpoint, bucket);

            //判断是生产还是测试
            var client = ConfigurationManager.AppSettings["ProductionOrNot"];
            var fileName = "";
            if (client.Trim().ToString() == "false")
            {
                fileName = "deve/tp/UserAvatarUrl/" + userID.ToString() + "_" + Guid.NewGuid().ToString().Replace("-", "") + ".jpg";
            }
            else
            {
                fileName = "prod/UserAvatarUrl/" + userID.ToString() + "_" + Guid.NewGuid().ToString().Replace("-", "") + ".jpg";
            }


            var res = oss.PushImg(img, fileName);
            //上传成功,回写信息
            if (res.Success)
            {
                basicUser.img = "https://oss.mrmatch.net/" + fileName;
                unitOfWork.RegisterUpdate(basicUser);
                oup.BoolResult = await unitOfWork.CommitAsync();
                if (!oup.BoolResult)
                {
                    oup.Message = "上传失败,请重试";
                }
                else
                {
                    oup.Message = "上传成功";
                }
                return oup;
            }
            oup.BoolResult = false;
            oup.Message = "上传失败,请重试";
            return oup;
        }

        public async Task<List<SystemNoticeUserOup>> GetAllUserListAsync()
        {
            var oup = (await user.GetAllListAsync()).MapToList<mq_user, SystemNoticeUserOup>();
            return oup;
        }
        #endregion

        #region profile
        public async Task<ProfileOup> GetProfileAsync(long userID)
        {
            var data = await profile.FirstOrDefaultAsync(x => x.UserID == userID);
            if (!data.IsNullEntity())
            {
                return null;
            }
            var oup = data.MapTo<TP_UserProfile, ProfileOup>();
            return oup;
        }
        public async Task<BoolMessageOup> AddOrUpdateProfileAsync(AddOrUpdateProfileInp input)
        {
            var rtn = new BoolMessageOup();

            var now = DateTime.Now;

            var works = await workExperience.GetAllListAsync(x => x.Valid == true && x.UserID == input.UserID);
            if (input.PKID <= 0)
            {
                var add = input.MapTo<AddOrUpdateProfileInp, TP_UserProfile>();
                if (works.Count > 0)
                {
                    add.WorkYears = LogicHelper.ComputeWorkYearsOne(works);
                }
                add.WorkYears = 0;
                add.OnOpen = false;
                add.IsUpdated = false;
                add.CreatedTime = now;
                add.LastModifiedTime = now;
                add.Valid = true;
                unitOfWork.RegisterNew(add);
                rtn.BoolResult = await unitOfWork.CommitAsync();
                if (rtn.BoolResult)
                {
                    rtn.Message = "新增成功";
                }
                else
                {
                    rtn.Message = "新增失败";
                }
            }
            else
            {
                var update = await profile.FirstOrDefaultAsync(x => x.PKID == input.PKID);
                var mquser = await user.FirstOrDefaultAsync(x => x.id == update.UserID);
                if (!update.IsNullEntity())
                {
                    rtn.BoolResult = false;
                    rtn.Message = "找不到目标数据";
                    return rtn;
                }
                update = input.MapTo<TP_UserProfile>(update);
                update.LastModifiedTime = now;
                //有快照 回写变化
                if (mquser.ProfileSnap)
                {
                    var snapPro = await profileSnap.FirstOrDefaultAsync(x => x.UserID == update.UserID);
                    var result = LogicHelper.CompareProfile(update, snapPro);
                    if (result)
                    {
                        update.IsUpdated = true;
                    }
                }
                unitOfWork.RegisterUpdate(update);
                rtn.BoolResult = await unitOfWork.CommitAsync();
                if (rtn.BoolResult)
                {
                    rtn.Message = "修改成功";
                }
                else
                {
                    rtn.Message = "修改失败";
                }
            }
            return rtn;
        }
        public async Task<ProfileSnapOup> GetProfileSnapAsync(long userID)
        {
            var snap = await profileSnap.FirstOrDefaultAsync(x => x.UserID == userID && x.Valid == true);
            if (!snap.IsNullEntity())
            {
                return null;
            }
            var oup = snap.MapTo<TP_ProfileSnap, ProfileSnapOup>();

            var ids = oup.FunctionIDs.SplitFunctionsToLongList(',');
            var funcs = await function.GetAllListAsync(x => ids.Contains(x.PKID));
            var name = "";
            for (int i = 0; i < funcs.Count; i++)
            {
                name += funcs[i].FunctionName;
                name += ",";
            }
            oup.FunctionIDs = name.SubStringLast();

            var locationids = oup.LocationIDs.SplitToLongList(',');
            var districts = await district.GetAllListAsync(x => locationids.Contains(x.PKID));
            var locationName = "";
            for (int i = 0; i < districts.Count; i++)
            {
                locationName += districts[i].DistrictName;
                locationName += ",";
            }
            oup.LocationIDs = locationName.SubStringLast();
            return oup;
        }

        public async Task<List<WorkExperienceSnapOup>> GetWorkExperienceSnapAsync(long userID)
        {
            var snap = await workExperienceSnap.GetAllListAsync(x => x.UserID == userID && x.Valid == true);
            if (!snap.IsNullEntity())
            {
                return null;
            }
            var oup = snap.MapToList<TP_WorkExperienceSnap, WorkExperienceSnapOup>();
            return oup;
        }

        public async Task<List<EducationSnapOup>> GetEducationSnapAsync(long userID)
        {
            var snap = await educationSnap.GetAllListAsync(x => x.UserID == userID && x.Valid == true);
            if (!snap.IsNullEntity())
            {
                return null;
            }
            var oup = snap.MapToList<TP_EducationSnap, EducationSnapOup>();
            return oup;
        }
        #endregion

        #region work
        public async Task<WorkExperienceOup> GetWorkExperienceAsync(long PKID)
        {
            var data = await workExperience.FirstOrDefaultAsync(x => x.PKID == PKID);
            if (!data.IsNullEntity())
            {
                return null;
            }
            var oup = data.MapTo<TP_UserWorkExperience, WorkExperienceOup>();
            return oup;
        }
        public async Task<List<WorkExperienceOup>> GetWorkExperienceListAsync(long userID)
        {
            var data = await workExperience.GetAllListAsync(x => x.Valid == true && x.UserID == userID);
            if (data.Count <= 0)
            {
                return null;
            }
            var res = data.MapToList<TP_UserWorkExperience, WorkExperienceOup>().OrderByDescending(x => x.StartDate).ToList();
            return res;
        }
        public async Task<BoolMessageOup> AddOrUpdateWorkExperienceAsync(AddOrUpdateWorkExperienceInp input)
        {
            var rtn = new BoolMessageOup();

            var now = DateTime.Now;

            if (input.PKID <= 0)
            {
                //排重
                var exist = await workExperience.FirstOrDefaultAsync(x => x.Valid == true && x.UserID == input.UserID && x.CompanyName == input.CompanyName);
                if (exist.IsNullEntity())
                {
                    rtn.BoolResult = false;
                    rtn.Message = "公司名为:" + input.CompanyName + "的工作经验已存在";
                    return rtn;
                }

                var add = input.MapTo<AddOrUpdateWorkExperienceInp, TP_UserWorkExperience>();
                add.CreatedTime = now;
                add.LastModifiedTime = now;
                add.Valid = true;
                unitOfWork.RegisterNew(add);

                rtn.BoolResult = await unitOfWork.CommitAsync();
                if (rtn.BoolResult)
                {
                    rtn.Message = "新增成功";
                }
                else
                {
                    rtn.Message = "新增失败";
                }
            }
            else
            {
                var update = await workExperience.FirstOrDefaultAsync(x => x.PKID == input.PKID);
                if (!update.IsNullEntity())
                {
                    rtn.BoolResult = false;
                    rtn.Message = "找不到目标数据";
                    return rtn;
                }

                update = input.MapTo<TP_UserWorkExperience>(update);
                update.LastModifiedTime = now;
                unitOfWork.RegisterUpdate(update);
                rtn.BoolResult = await unitOfWork.CommitAsync();
                if (rtn.BoolResult)
                {
                    rtn.Message = "修改成功";
                }
                else
                {
                    rtn.Message = "修改失败";
                }
            }
            //更新工作年限
            if (rtn.BoolResult)
            {
                var works = await workExperience.GetAllListAsync(x => x.Valid == true && x.UserID == input.UserID);
                var pro = await profile.FirstOrDefaultAsync(x => x.UserID == input.UserID);
                if (pro.IsNullEntity())
                {
                    pro.WorkYears = LogicHelper.ComputeWorkYearsOne(works);
                    unitOfWork.RegisterUpdate(pro);
                    await unitOfWork.CommitAsync();
                }
            }
            return rtn;
        }
        public async Task<BoolMessageOup> DestroyWorkExperienceAsync(long PKID)
        {
            var rtn = new BoolMessageOup();

            var now = DateTime.Now;
            var data = await workExperience.FirstOrDefaultAsync(x => x.PKID == PKID);
            if (!data.IsNullEntity())
            {
                rtn.BoolResult = false;
                rtn.Message = "找不到目标数据";
                return rtn;
            }

            data.Valid = false;
            data.LastModifiedTime = now;
            unitOfWork.RegisterUpdate(data);
            rtn.BoolResult = await unitOfWork.CommitAsync();
            if (rtn.BoolResult)
            {
                rtn.Message = "删除成功";
            }
            else
            {
                rtn.Message = "删除失败";
            }
            return rtn;
        }
        #endregion

        #region education
        public async Task<EducationOup> GetEducationAsync(long PKID)
        {
            var data = await education.FirstOrDefaultAsync(x => x.PKID == PKID);
            if (!data.IsNullEntity())
            {
                return null;
            }
            var oup = data.MapTo<TP_UserEducation, EducationOup>();
            return oup;
        }
        public async Task<List<EducationOup>> GetEducationListAsync(long userID)
        {
            var data = await education.GetAllListAsync(x => x.Valid == true && x.UserID == userID);
            if (data.Count <= 0)
            {
                return null;
            }
            var res = data.MapToList<TP_UserEducation, EducationOup>().OrderByDescending(x => x.StartDate).ToList();
            return res;
        }
        public async Task<BoolMessageOup> AddOrUpdateEducationAsync(AddOrUpdateEducationInp input)
        {
            var rtn = new BoolMessageOup();

            var now = DateTime.Now;

            if (input.PKID <= 0)
            {
                //排重
                var exist = await education.FirstOrDefaultAsync(x => x.Valid == true && x.UserID == input.UserID && x.SchoolName == input.SchoolName);
                if (exist.IsNullEntity())
                {
                    rtn.BoolResult = false;
                    rtn.Message = "学校名为:" + input.SchoolName + "的教育经历已存在";
                    return rtn;
                }

                var add = input.MapTo<AddOrUpdateEducationInp, TP_UserEducation>();
                add.CreatedTime = now;
                add.LastModifiedTime = now;
                add.Valid = true;
                unitOfWork.RegisterNew(add);

                rtn.BoolResult = await unitOfWork.CommitAsync();
                if (rtn.BoolResult)
                {
                    rtn.Message = "新增成功";
                }
                else
                {
                    rtn.Message = "新增失败";
                }
            }
            else
            {
                var update = await education.FirstOrDefaultAsync(x => x.PKID == input.PKID);
                if (!update.IsNullEntity())
                {
                    rtn.BoolResult = false;
                    rtn.Message = "找不到目标数据";
                    return rtn;
                }

                update = input.MapTo<TP_UserEducation>(update);
                update.LastModifiedTime = now;
                unitOfWork.RegisterUpdate(update);
                rtn.BoolResult = await unitOfWork.CommitAsync();
                if (rtn.BoolResult)
                {
                    rtn.Message = "修改成功";
                }
                else
                {
                    rtn.Message = "修改失败";
                }
            }
            return rtn;
        }
        public async Task<BoolMessageOup> DestroyEducationAsync(long PKID)
        {
            var rtn = new BoolMessageOup();

            var now = DateTime.Now;
            var data = await education.FirstOrDefaultAsync(x => x.PKID == PKID);
            if (!data.IsNullEntity())
            {
                rtn.BoolResult = false;
                rtn.Message = "找不到目标数据";
                return rtn;
            }

            data.Valid = false;
            data.LastModifiedTime = now;
            unitOfWork.RegisterUpdate(data);
            rtn.BoolResult = await unitOfWork.CommitAsync();
            if (rtn.BoolResult)
            {
                rtn.Message = "删除成功";
            }
            else
            {
                rtn.Message = "删除失败";
            }
            return rtn;
        }
        #endregion

        #region jobIntention
        public async Task<JobIntentionOup> GetJobIntentionAsync(long userID)
        {
            var data = await jobIntention.FirstOrDefaultAsync(x => x.UserID == userID);
            if (!data.IsNullEntity())
            {
                return null;
            }
            var res = data.MapTo<TP_UserJobIntention, JobIntentionOup>();

            if (res.LocationIDs.Trim().Length > 0)
            {
                var ids = res.LocationIDs.SplitToLongList(',');
                var names = await district.GetAllListAsync(x => ids.Contains(x.PKID));
                ids.ForEach(item =>
                {
                    var name = names.Find(x => x.PKID == item);
                    if (name.IsNullEntity())
                    {
                        res.Locations += name.DistrictName;
                        res.Locations += ",";
                    }
                });
                res.Locations = res.Locations.Substring(0, res.Locations.Length - 1);
            }

            if (res.FunctionIDs.Trim().Length > 0)
            {
                var ids = res.FunctionIDs.SplitFunctionsToLongList(',');
                var names = await function.GetAllListAsync(x => ids.Contains(x.PKID));
                ids.ForEach(item =>
                {
                    var name = names.Find(x => x.PKID == item);
                    if (name.IsNullEntity())
                    {
                        res.Functions += name.FunctionName;
                        res.Functions += ",";
                    }
                });
                res.Functions = res.Functions.Substring(0, res.Functions.Length - 1);
            }

            return res;
        }
        public async Task<BoolMessageOup> AddOrUpdateJobIntentionAsync(AddOrUpdateJobIntentionInp input)
        {
            var rtn = new BoolMessageOup();

            var now = DateTime.Now;
            if (input.FunctionIDs.IsSplitRepet(','))
            {
                rtn.BoolResult = false;
                rtn.Message = "资历概括不能重复";
                return rtn;
            }

            if (input.LocationIDs.IsSplitRepet(','))
            {
                rtn.BoolResult = false;
                rtn.Message = "意向城市不能重复";
                return rtn;
            }

            if (input.PKID <= 0)
            {
                var add = input.MapTo<AddOrUpdateJobIntentionInp, TP_UserJobIntention>();
                add.OnOpen = false;
                add.CreatedTime = now;
                add.LastModifiedTime = now;
                add.Valid = true;
                unitOfWork.RegisterNew(add);
                rtn.BoolResult = await unitOfWork.CommitAsync();
                if (rtn.BoolResult)
                {
                    rtn.Message = "新增成功";
                }
                else
                {
                    rtn.Message = "新增失败";
                }
            }
            else
            {
                var update = await jobIntention.FirstOrDefaultAsync(x => x.PKID == input.PKID);
                var mquser = await user.FirstOrDefaultAsync(x => x.id == update.UserID);
                if (!update.IsNullEntity())
                {
                    rtn.BoolResult = false;
                    rtn.Message = "找不到目标数据";
                    return rtn;
                }
                update = input.MapTo<TP_UserJobIntention>(update);
                update.LastModifiedTime = now;
                //有快照 回写变化
                if (mquser.ProfileSnap)
                {
                    var snapPro = await profileSnap.FirstOrDefaultAsync(x => x.UserID == update.UserID);
                    var result = LogicHelper.CompareIntention(update, snapPro);
                    if (result)
                    {
                        update.IsUpdated = true;
                    }
                }
                unitOfWork.RegisterUpdate(update);
                rtn.BoolResult = await unitOfWork.CommitAsync();
                if (rtn.BoolResult)
                {
                    rtn.Message = "修改成功";
                }
                else
                {
                    rtn.Message = "修改失败";
                }
            }
            return rtn;
        }
        #endregion

        #region avoid
        public async Task<List<AvoidOup>> GetAvoidListAsync(long userID)
        {
            var data = await avoid.GetAllListAsync(x => x.Valid == true && x.UserID == userID);
            if (data.Count <= 0)
            {
                return null;
            }
            var res = data.MapToList<TP_UserAvoid, AvoidOup>();
            return res;
        }
        public async Task<BoolMessageOup> AddOrUpdateAvoidAsync(AddOrUpdateAvoidInp input)
        {
            var rtn = new BoolMessageOup();

            var now = DateTime.Now;

            if (input.PKID <= 0)
            {
                //排重
                var exist = await avoid.FirstOrDefaultAsync(x => x.Valid == true && x.UserID == input.UserID && x.AvoidDomain == input.AvoidDomain);
                if (exist.IsNullEntity())
                {
                    rtn.BoolResult = false;
                    rtn.Message = "域名为:" + input.AvoidDomain + "已存在";
                    return rtn;
                }

                var add = input.MapTo<AddOrUpdateAvoidInp, TP_UserAvoid>();
                add.CreatedTime = now;
                add.LastModifiedTime = now;
                add.Valid = true;
                unitOfWork.RegisterNew(add);

                rtn.BoolResult = await unitOfWork.CommitAsync();
                if (rtn.BoolResult)
                {
                    rtn.Message = "新增成功";
                }
                else
                {
                    rtn.Message = "新增失败";
                }
            }
            else
            {
                var update = await avoid.FirstOrDefaultAsync(x => x.PKID == input.PKID);
                if (!update.IsNullEntity())
                {
                    rtn.BoolResult = false;
                    rtn.Message = "找不到目标数据";
                    return rtn;
                }

                update = input.MapTo<TP_UserAvoid>(update);
                update.LastModifiedTime = now;
                unitOfWork.RegisterUpdate(update);
                rtn.BoolResult = await unitOfWork.CommitAsync();
                if (rtn.BoolResult)
                {
                    rtn.Message = "修改成功";
                }
                else
                {
                    rtn.Message = "修改失败";
                }
            }
            return rtn;
        }
        public async Task<BoolMessageOup> DestroyAvoidAsync(long PKID)
        {
            var rtn = new BoolMessageOup();

            var now = DateTime.Now;
            var data = await avoid.FirstOrDefaultAsync(x => x.PKID == PKID);
            if (!data.IsNullEntity())
            {
                rtn.BoolResult = false;
                rtn.Message = "找不到目标数据";
                return rtn;
            }

            data.Valid = false;
            data.LastModifiedTime = now;
            unitOfWork.RegisterUpdate(data);
            rtn.BoolResult = await unitOfWork.CommitAsync();
            if (rtn.BoolResult)
            {
                rtn.Message = "删除成功";
            }
            else
            {
                rtn.Message = "删除失败";
            }
            return rtn;
        }
        #endregion

        #region userTags
        public async Task<List<UserTagsOup>> GetUserTagsListAsync(long userID)
        {
            var data = await userTags.GetAllListAsync(x => x.Valid == true && x.UserID == userID);
            if (data.Count <= 0)
            {
                return null;
            }
            var res = data.MapToList<TP_UserTags, UserTagsOup>();
            return res;
        }
        public async Task<BoolMessageOup> AddOrUpdateUserTagsAsync(AddOrUpdateUserTagsInp input)
        {
            var rtn = new BoolMessageOup();
            rtn.BoolResult = true;
            rtn.Message = "保存成功";
            var now = DateTime.Now;

            var cfgTags = await tags.GetAllListAsync(x => x.Valid == true);
            var userTagList = await userTags.GetAllListAsync(x => x.Valid == true && x.UserID == input.UserID);

            //全部删除
            if (string.IsNullOrEmpty(input.Tags))
            {
                if (userTagList.Count > 0)
                {
                    userTagList.ForEach(item =>
                    {
                        item.Valid = false;
                        item.LastModifiedTime = now;
                        unitOfWork.RegisterUpdate(item);
                    });
                    rtn.BoolResult = await unitOfWork.CommitAsync();
                    if (rtn.BoolResult)
                    {
                        rtn.Message = "保存成功";
                    }
                    else
                    {
                        rtn.Message = "保存失败";
                    }
                    return rtn;
                }

                return rtn;
            }

            //先删除
            if (userTagList.Count > 0)
            {
                userTagList.ForEach(item =>
                {
                    item.Valid = false;
                    item.LastModifiedTime = now;
                    unitOfWork.RegisterUpdate(item);
                });
            }

            //后新增
            var ids = input.Tags.SplitToLongList(',');
            ids.ForEach(async item =>
            {
                var cTag = await tags.FirstOrDefaultAsync(x => x.PKID == item);
                if (cTag.IsNullEntity())
                {
                    TP_UserTags add = new TP_UserTags();
                    add.TagID = cTag.PKID;
                    add.Tags = cTag.Tags;
                    add.UserID = input.UserID;
                    add.CreatedTime = now;
                    add.LastModifiedTime = now;
                    add.Valid = true;
                    unitOfWork.RegisterNew(add);
                }
            });

            rtn.BoolResult = await unitOfWork.CommitAsync();
            if (rtn.BoolResult)
            {
                rtn.Message = "保存成功";
            }
            else
            {
                rtn.Message = "保存失败";
            }
            return rtn;
        }

        #endregion
    }
}
