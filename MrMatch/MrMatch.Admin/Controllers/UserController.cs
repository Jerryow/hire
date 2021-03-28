using MrMatch.Admin.Handler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MrMatch.Admin.Controllers
{
    public class UserController : ControllerBaseAttr
    {
        // GET: User
        public ActionResult User()
        {
            return View();
        }
        public ActionResult CheckUser()
        {
            return View();
        }
    }
}