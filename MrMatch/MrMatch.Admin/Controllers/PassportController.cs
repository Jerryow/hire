using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MrMatch.Admin.Controllers
{
    public class PassportController : Controller
    {
        // GET: Passport
        public ActionResult Login()
        {
            return View();
        }
    }
}