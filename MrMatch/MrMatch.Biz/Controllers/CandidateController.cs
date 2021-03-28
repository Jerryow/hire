using MrMatch.Biz.Handler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MrMatch.Biz.Controllers
{
    public class CandidateController : ControllerBaseAttr
    {
        // GET: Candidate
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Item()
        {
            return View();
        }

        public ActionResult Follow()
        {
            return View();
        }

        public ActionResult ManageFollow()
        {
            return View();
        }

        public ActionResult Folder()
        {
            return View();
        }

        public ActionResult Invite()
        {
            return View();
        }
    }
}