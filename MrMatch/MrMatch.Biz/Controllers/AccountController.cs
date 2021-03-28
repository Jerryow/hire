using MrMatch.Biz.Handler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MrMatch.MysqlFramework;

namespace MrMatch.Biz.Controllers
{
    public class AccountController : ControllerBaseAttr
    {
        // GET: Account
        public ActionResult Complete()
        {
            return View();
        }

        public ActionResult Index()
        {
            if (Handler.ActionFilter.CurrID.CurrentUser.CompanyID <= 0)
            {
                Response.Redirect("/account/complete");
            }
            return View();
        }

        public ActionResult CompanyInfo()
        {
            return View();
        }

        public ActionResult AccountBasicInfo()
        {
            return View();
        }

        public ActionResult Logo()
        {
            return View();
        }

        public ActionResult License()
        {
            return View();
        }

        public ActionResult Account()
        {
            return View();
        }

        public ActionResult Invitation()
        {
            return View();
        }

        public ActionResult LetterTemplate()
        {
            return View();
        }

        public ActionResult NoticeMessage()
        {
            return View();
        }
    }
}