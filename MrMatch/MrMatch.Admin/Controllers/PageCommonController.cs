using MrMatch.Admin.Handler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MrMatch.Admin.Controllers
{
    public class PageCommonController : ControllerBaseAttr
    {
        // GET: PageCommon
        public ActionResult MainHeader()
        {
            return View();
        }

        public ActionResult MainSidebar()
        {
            return View();
        }
    }
}