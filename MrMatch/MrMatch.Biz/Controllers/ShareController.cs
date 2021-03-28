using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MrMatch.Biz.Controllers
{
    public class ShareController : Controller
    {
        // GET: Share
        public ActionResult ProfileShare(string code, string cid)
        {
            //LogHelper.LogInfo("uuid :id：" + code);
            //LogHelper.LogInfo("企业id：" + cid);
            //var comid = Convert.ToInt32(cid);

            ////签约验证
            //var now = DateTime.Now;
            //var contract = contractInfo.GetByWherLambda(x => x.Valid == true && x.CompanyID == comid && x.StartDate <= now && x.ExpireDate >= now).ToList();
            //var isContract = contract.Count > 0 ? true : false;

            ////获取签约配置信息
            //var configs = sysConfig.GetByWherLambda(x => x.Valid == true && x.ConfigCode == "ContractHelp").ToList();

            ////处理简历信息
            //var profile = userProfile.GetByWherLambda(x => x.Valid == true && x.UniCode.ToString() == code).FirstOrDefault();
            //var oup = new ProfileOup();
            //if (profile != null)
            //{

            //    var intention = userJobIntention.GetByWherLambda(x => x.Valid == true && x.UserID == profile.UserID).ToList();


            //    oup.ProfilePKID = profile.PKID;
            //    oup.RealName = profile.RealName;
            //    oup.Introduction = profile.Introduction;
            //    oup.Residence = profile.Residence;
            //    oup.UniCode = profile.UniCode.Value;
            //    oup.UserID = profile.UserID.Value;
            //    oup.AnnualSalary = profile.AnnualSalary.Value;
            //    oup.Gender = profile.Gender.Value;
            //    oup.WorkYears = profile.WorkYears.Value;
            //    oup.LastModifiedTime = profile.LastModifiedTime.Value;
            //    oup.WorkExperienceList = userWorkExperience.GetByWherLambda(x => x.Valid == true && x.UserID == profile.UserID).OrderByDescending(x => x.CreatedTime).ToList();
            //    oup.EducationList = userEducation.GetByWherLambda(x => x.Valid == true && x.UserID == profile.UserID).OrderByDescending(x => x.CreatedTime).ToList();
            //    oup.UserBasicInfo = userInfo.GetById(profile.UserID.Value);
            //    oup.JobIntention = intention.Count > 0 ? intention[0] : null;
            //}

            //ViewBag.data = oup;
            //ViewBag.accountInfo = CurrUser;
            //ViewBag.contract = isContract;
            //ViewBag.contractHelp = configs[0].ConfigValue;
            return View();
        }
    }
}