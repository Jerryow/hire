using MrMatch.Admin.Handler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MrMatch.Admin.Controllers
{
    public class ConfigController : ControllerBaseAttr
    {
        // GET: Config
        public ActionResult Country()
        {
            return View();
        }

        public ActionResult District()
        {
            return View();
        }

        public ActionResult Function()
        {
            return View();
        }

        public ActionResult TagInfo()
        {
            return View();
        }

        public ActionResult Skill()
        {
            return View();
        }
    }
}