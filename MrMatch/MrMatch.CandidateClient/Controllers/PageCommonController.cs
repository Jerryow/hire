using MrMatch.CandidateClient.Handler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MrMatch.CandidateClient.Controllers
{
    public class PageCommonController : ControllerBaseAttr
    {
        // GET: PageCommon
        public ActionResult PageHead()
        {
            return View();
        }
        public ActionResult PageFoot()
        {
            return View();
        }
    }
}