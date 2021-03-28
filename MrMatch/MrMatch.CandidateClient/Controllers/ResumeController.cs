using MrMatch.CandidateClient.Handler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MrMatch.CandidateClient.Controllers
{
    public class ResumeController : ControllerBaseAttr
    {
        // GET: Resume
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Snap()
        {
            return View();
        }
    }
}