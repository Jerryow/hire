using MrMatch.Admin.Handler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MrMatch.Admin.Controllers
{
    public class CompanyController : ControllerBaseAttr
    {
        // GET: Company
        public ActionResult Index()
        {
            return View();
        }
    }
}