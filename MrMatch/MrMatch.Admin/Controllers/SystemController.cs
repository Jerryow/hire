using MrMatch.Admin.Handler;
using MrMatch.Admin.Handler.ActionFilters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MrMatch.Admin.Controllers
{

   
    public class SystemController : ControllerBaseAttr
    {
        // GET: System
        public ActionResult SystemUser()
        {
            return View();
        }

        public ActionResult SiteConfig()
        {
            return View();
        }

        public ActionResult MessageConfig()
        {
            return View();
        }

        public ActionResult MessageConfigItem()
        {
            return View();
        }

        public ActionResult MessageTemplate()
        {
            return View();
        }

        public ActionResult MessageTemplateItem()
        {
            return View();
        }

        public ActionResult SystemNotice()
        {
            return View();
        }

        public ActionResult WechatSiteMessage()
        {
            return View();
        }

        public ActionResult WechatSiteMessageContent()
        {
            return View();
        }
    }
}