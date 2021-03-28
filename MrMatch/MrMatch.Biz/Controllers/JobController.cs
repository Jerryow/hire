using MrMatch.Biz.Handler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MrMatch.Biz.Controllers
{
    public class JobController : ControllerBaseAttr
    {
        // GET: Job
        public ActionResult Invite()
        {
            return View();
        }

        public ActionResult Interview()
        {
            return View();
        }

        public ActionResult InterviewItem()
        {
            return View();
        }
    }
}