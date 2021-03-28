using MrMatch.Biz.Handler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MrMatch.Biz.Controllers
{
    public class MessageController : ControllerBaseAttr
    {
        // GET: Message
        public ActionResult Index()
        {
            return View();
        }
    }
}