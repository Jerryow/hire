using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MrMatch.Application.Config.Oup;
using MrMatch.Domain.Models;
using MrMatch.Common.Mapper;
using MrMatch.MysqlFramework.Extensions;
using MrMatch.Application.Company.Oup;

namespace MrMatch.Application
{
    public static class LogicHelper
    {
        //只计算一次  最早的~最晚
        public static int ComputeWorkYearsOne(List<TP_UserWorkExperience> works)
        {
            if (works.Count > 0)
            {
                var now = DateTime.Now;
                int workMonth = 0;
                var untilnow = works.Where(x => x.ExpirationDate == "至今").ToList();
                if (untilnow.Count > 0)
                {
                    var start = works.OrderBy(x => x.StartDate).FirstOrDefault();
                    workMonth = (now.Year - start.StartDate.Year) * 12 + (now.Month - start.StartDate.Month);
                    var year = workMonth / 12;
                    if ((workMonth % 12) > 0)
                    {
                        year = year + 1;
                    }
                    return year;
                }
                else
                {
                    var start = works.OrderBy(x => x.StartDate).FirstOrDefault().StartDate;
                    var end = Convert.ToDateTime(works.OrderByDescending(x => x.ExpirationDate).FirstOrDefault().ExpirationDate);
                    workMonth = (end.Year - start.Year) * 12 + (end.Month - start.Month);
                    var year = workMonth / 12;
                    if ((workMonth % 12) > 0)
                    {
                        year = year + 1;
                    }
                    return year;
                }
            }
            return 0;
        }

        #region 职位的三级目录
        public static List<FunctionFirstCascaderOup> Cascader(List<TP_Function> firstFuncs, List<TP_Function> secondFuncs, List<TP_Function> thirdFuncs)
        {
            var first = new List<FunctionFirstCascaderOup>();
            var firstData = new FunctionFirstCascaderOup();
            firstFuncs.ForEach(x =>
            {
                firstData = new FunctionFirstCascaderOup();
                firstData.value = x.PKID;
                firstData.label = x.FunctionName;
                firstData.IsPopular = x.IsPopular;
                firstData.children = CascaderSecond(secondFuncs, thirdFuncs, x.PKID);
                first.Add(firstData);
            });
            return first;
        }

        public static object CascaderSecond(List<TP_Function> secondFuncs, List<TP_Function> thirdFuncs, long parentId)
        {
            var data = secondFuncs.FindAll(x => x.ParentID == parentId);

            var obj = new object();
            var objList = new List<object>();

            data.ForEach(x =>
            {
                var count = thirdFuncs.FindAll(y => y.ParentID == x.PKID);
                if (count.Count > 0)
                {
                    var child = CascaderThird(thirdFuncs, x.PKID);
                    obj = new { value = x.PKID, label = x.FunctionName, children = child };
                }
                else
                {
                    obj = new { value = x.PKID, label = x.FunctionName };
                }
                objList.Add(obj);
            });
            return objList;
        }

        public static object CascaderThird(List<TP_Function> thirdFuncs, long parentId)
        {
            var obj = new object();
            var objList = new List<object>();
            var data = thirdFuncs.FindAll(x => x.ParentID == parentId);
            data.ForEach(x =>
            {
                obj = new { value = x.PKID, label = x.FunctionName };
                objList.Add(obj);
            });
            return objList;
        }
        #endregion

        #region 省->市目录结构
        public static List<ProvinceListOup> ProvinceList(List<TP_District> province, List<TP_District> city)
        {
            var first = new List<ProvinceListOup>();
            var firstData = new ProvinceListOup();
            province.ForEach(x =>
            {
                firstData = new ProvinceListOup();
                firstData.PKID = x.PKID;
                firstData.DistrictName = x.DistrictName;
                firstData.Children = ChildrenDistrict(x.PKID, city);
                first.Add(firstData);
            });
            return first;
        }

        public static List<ChildrenDistrictListOup> ChildrenDistrict(long parentID, List<TP_District> city)
        {
            var childrenList = new List<ChildrenDistrictListOup>();
            var children = new ChildrenDistrictListOup();
            var data = city.FindAll(x => x.ParentID == parentID);
            data.ForEach(x =>
            {
                children = new ChildrenDistrictListOup();
                children.PKID = x.PKID;
                children.DistrictName = x.DistrictName;
                childrenList.Add(children);
            });
            return childrenList;
        }
        #endregion

        #region 简历快照

        /// <summary>
        /// 新增profile快照
        /// </summary>
        /// <param name="profile"></param>
        /// <param name="intention"></param>
        /// <returns></returns>
        public static TP_ProfileSnap AddProfileSnap(TP_UserProfile profile, TP_UserJobIntention intention)
        {
            var oup = new TP_ProfileSnap();
            var now = DateTime.Now;
            //新增
            oup.UserID = profile.UserID;
            oup.RealName = profile.RealName;
            oup.Gender = profile.Gender;
            oup.Birthday = profile.Birthday;
            oup.Education = profile.Education;
            oup.BirthPlace = profile.BirthPlace;
            oup.Residence = profile.Residence;
            oup.CensusRegister = profile.CensusRegister;
            oup.CurrentCompany = profile.CurrentCompany;
            oup.CurrentPosition = profile.CurrentPosition;
            oup.WorkYears = profile.WorkYears;
            oup.CurrentAnnualSalary = profile.AnnualSalary;
            oup.CurrentOnOpen = profile.OnOpen;
            oup.JobStatus = profile.JobStatus;
            oup.Introduction = profile.Introduction;
            oup.FunctionIDs = intention.FunctionIDs;
            oup.LocationIDs = intention.LocationIDs;
            oup.IntentionAnnualSalary = intention.AnnualSalary;
            oup.IntentionOnOpen = intention.OnOpen;
            oup.CreatedTime = now;
            oup.LastModifiedTime = now;
            oup.Valid = true;
            return oup;
        }

        /// <summary>
        /// 替换简历快照
        /// </summary>
        /// <param name="profile"></param>
        /// <param name="intention"></param>
        /// <param name="snap"></param>
        /// <returns></returns>
        public static TP_ProfileSnap ReplaceProfileSnap(TP_UserProfile profile, TP_UserJobIntention intention, TP_ProfileSnap snap)
        {
            var oup = new TP_ProfileSnap();
            oup = snap;
            //替换
            if (profile.IsUpdated)
            {
                oup.UserID = profile.UserID;
                oup.RealName = profile.RealName;
                oup.Gender = profile.Gender;
                oup.Birthday = profile.Birthday;
                oup.Education = profile.Education;
                oup.BirthPlace = profile.BirthPlace;
                oup.Residence = profile.Residence;
                oup.CensusRegister = profile.CensusRegister;
                oup.CurrentCompany = profile.CurrentCompany;
                oup.CurrentPosition = profile.CurrentPosition;
                oup.WorkYears = profile.WorkYears;
                oup.CurrentAnnualSalary = profile.AnnualSalary;
                oup.CurrentOnOpen = profile.OnOpen;
                oup.JobStatus = profile.JobStatus;
                oup.Introduction = profile.Introduction;
                oup.LastModifiedTime = DateTime.Now;
            }
            if (intention.IsUpdated)
            {
                oup.FunctionIDs = intention.FunctionIDs;
                oup.LocationIDs = intention.LocationIDs;
                oup.IntentionAnnualSalary = intention.AnnualSalary;
                oup.IntentionOnOpen = intention.OnOpen;
                oup.LastModifiedTime = DateTime.Now;
            }
            return oup;
        }

        /// <summary>
        /// 对比快照基本信息
        /// true:有更改
        /// false:无更改
        /// </summary>
        /// <param name="profile"></param>
        /// <param name="snap"></param>
        /// <returns></returns>
        public static bool CompareProfile(TP_UserProfile profile, TP_ProfileSnap snap)
        {
            if (profile.RealName != snap.RealName)
            {
                return true;
            }
            if (profile.Gender != snap.Gender)
            {
                return true;
            }
            if (profile.Birthday != snap.Birthday)
            {
                return true;
            }
            if (profile.Education != snap.Education)
            {
                return true;
            }
            if (profile.BirthPlace != snap.BirthPlace)
            {
                return true;
            }
            if (profile.Residence != snap.Residence)
            {
                return true;
            }
            if (profile.CensusRegister != snap.CensusRegister)
            {
                return true;
            }
            if (profile.CurrentCompany != snap.CurrentCompany)
            {
                return true;
            }
            if (profile.CurrentPosition != snap.CurrentPosition)
            {
                return true;
            }
            if (profile.WorkYears != snap.WorkYears)
            {
                return true;
            }
            if (profile.AnnualSalary != snap.CurrentAnnualSalary)
            {
                return true;
            }
            if (profile.OnOpen != snap.CurrentOnOpen)
            {
                return true;
            }
            if (profile.JobStatus != snap.JobStatus)
            {
                return true;
            }
            if (profile.Introduction != snap.Introduction)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 对比快照求职意向
        /// </summary>
        /// <param name="intention"></param>
        /// <param name="snap"></param>
        /// <returns></returns>
        public static bool CompareIntention(TP_UserJobIntention intention, TP_ProfileSnap snap)
        {
            if (intention.FunctionIDs != snap.FunctionIDs)
            {
                return true;
            }
            if (intention.LocationIDs != snap.LocationIDs)
            {
                return true;
            }
            if (intention.AnnualSalary != snap.IntentionAnnualSalary)
            {
                return true;
            }
            if (intention.OnOpen != snap.IntentionOnOpen)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 新增教育快照
        /// </summary>
        /// <param name="work"></param>
        /// <returns></returns>
        public static List<TP_EducationSnap> AddEducationSnap(List<TP_UserEducation> education)
        {
            var oup = new List<TP_EducationSnap>();
            var now = DateTime.Now;
            education.ForEach(x =>
            {
                var data = new TP_EducationSnap();
                data.UserID = x.UserID;
                data.StartDate = x.StartDate;
                data.ExpirationDate = x.ExpirationDate;
                data.SchoolName = x.SchoolName;
                data.MajorSubject = x.MajorSubject;
                data.Degree = x.Degree;
                data.CreatedTime = now;
                data.LastModifiedTime = now;
                data.Valid = true;
                oup.Add(data);
            });
            return oup;
        }

        /// <summary>
        /// 新增工作快照
        /// </summary>
        /// <param name="work"></param>
        /// <returns></returns>
        public static List<TP_WorkExperienceSnap> AddWorkSnap(List<TP_UserWorkExperience> work)
        {
            var oup = new List<TP_WorkExperienceSnap>();
            var now = DateTime.Now;
            work.ForEach(x =>
            {
                var data = new TP_WorkExperienceSnap();
                data.UserID = x.UserID;
                data.StartDate = x.StartDate;
                data.ExpirationDate = x.ExpirationDate;
                data.CompanyName = x.CompanyName;
                data.Position = x.Position;
                data.Introduction = x.Introduction;
                data.CreatedTime = now;
                data.LastModifiedTime = now;
                data.Valid = true;
                oup.Add(data);
            });
            return oup;
        }

        /// <summary>
        /// 对比快照工作经验
        /// true:有更改
        /// false:无更改
        /// </summary>
        /// <param name="work"></param>
        /// <param name="snap"></param>
        /// <returns></returns>
        public static bool CompareWorkExperience(List<TP_UserWorkExperience> work, List<TP_WorkExperienceSnap> snap)
        {
            var rtn = false;
            if (work.Count != snap.Count)
            {
                return true;
            }
            work.ForEach(item =>
            {
                if (!rtn)
                {
                    var existSnap = snap.Where(x => x.CompanyName == item.CompanyName).FirstOrDefault();
                    if (!existSnap.IsNullEntity())
                    {
                        rtn = true;
                    }
                    else
                    {
                        if (existSnap.StartDate != item.StartDate)
                        {
                            rtn = true;
                        }
                        if (existSnap.ExpirationDate != item.ExpirationDate)
                        {
                            rtn = true;
                        }
                        if (existSnap.Position != item.Position)
                        {
                            rtn = true;
                        }
                        if (existSnap.Introduction != item.Introduction)
                        {
                            rtn = true;
                        }
                    }
                }
            });
            return rtn;
        }

        /// <summary>
        /// 对比快照教育
        /// true:有更改
        /// false:无更改
        /// </summary>
        /// <param name="education"></param>
        /// <param name="snap"></param>
        /// <returns></returns>
        public static bool CompareEducation(List<TP_UserEducation> education, List<TP_EducationSnap> snap)
        {
            var rtn = false;
            education.ForEach(item =>
            {
                if (!rtn)
                {
                    var existSnap = snap.Where(x => x.SchoolName == item.SchoolName).FirstOrDefault();
                    if (!existSnap.IsNullEntity())
                    {
                        rtn = true;
                    }
                    else
                    {
                        if (existSnap.StartDate != item.StartDate)
                        {
                            rtn = true;
                        }
                        if (existSnap.ExpirationDate != item.ExpirationDate)
                        {
                            rtn = true;
                        }
                        if (existSnap.MajorSubject != item.MajorSubject)
                        {
                            rtn = true;
                        }
                        if (existSnap.Degree != item.Degree)
                        {
                            rtn = true;
                        }
                    }
                }
            });
            return rtn;
        }

        #endregion

        #region 职位的年薪字符串截取匹配
        public static IQueryable<TP_Job> AnnualSalaryMatch(this IQueryable<TP_Job> query, string salary)
        {
            //以下
            if (salary.Contains("下"))
            {
                var val = Convert.ToInt64(salary.Split('K')[0]);
                query = query.Where(x => x.MinAnnualSalary < val);
                return query;
            }
            //以上
            if (salary.Contains("上"))
            {
                var val = Convert.ToInt64(salary.Split('K')[0]);
                query = query.Where(x => x.MaxAnnualSalary > val);
                return query;
            }
            //其他
            var min = Convert.ToInt64(salary.Split('-')[0].Replace("K", ""));
            var max = Convert.ToInt64(salary.Split('-')[1].Replace("K", ""));
            query = query.Where(x => x.MinAnnualSalary <= max && x.MaxAnnualSalary >= min);
            return query;
        }
        #endregion

        #region 职位联系人信息匹配
        /// <summary>
        /// 职位联系人信息匹配
        /// </summary>
        /// <param name="account"></param>
        /// <param name="showItems"></param>
        /// <returns></returns>
        public static JobAccountOup GetJobAccountInfo(TP_Account account, string showItems)
        {
            var oup = new JobAccountOup();
            oup.AccountName = account.AccountName;
            oup.Position = account.Position;
            oup.AvatarUrl = account.AvatarUrl;
            if (showItems.Contains("phone"))
            {
                oup.CellPhone = account.CellPhone;
            }
            if (showItems.Contains("wechat"))
            {
                oup.WechatAccount = account.WechatAccount;
            }
            if (showItems.Contains("wechatpic"))
            {
                oup.WechatContactUrl = account.WechatContactUrl;
            }
            if (showItems.Contains("linkin"))
            {
                oup.LinkinUrl = account.LinkinUrl;
            }
            if (showItems.Contains("focus"))
            {
                oup.FocusArea = account.FocusArea;
            }
            if (showItems.Contains("introduction"))
            {
                oup.Introduction = account.Introduction;
            }
            return oup;
        }
        #endregion
    }
}
