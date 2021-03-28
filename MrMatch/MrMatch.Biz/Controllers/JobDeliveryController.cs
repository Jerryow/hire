using MrMatch.Biz.Handler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MrMatch.Biz.Controllers
{
    public class JobDeliveryController : ControllerBaseAttr
    {
        // GET: JobDelivery
        public ActionResult Publish()
        {
            return View();
        }

        public ActionResult JobManage()
        {
            return View();
        }

        public ActionResult CompanyManage()
        {
            return View();
        }

        public ActionResult AgentCompany()
        {
            return View();
        }

        public ActionResult JobDetails()
        {
            return View();
        }
    }
}