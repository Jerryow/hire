using MrMatch.Domain.Models;
using MrMatch.MysqlFramework.BaseContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MrMatch.Biz.Controllers
{
    public class PassportController : Controller
    {
        private readonly IDbContext _dbContext;
        public PassportController(IDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        // GET: Passport
        public ActionResult Login()
        {
            ViewBag.aa = _dbContext.Set<TP_SystemUser>().Find(1);
                return View();
        }
        public ActionResult Regist()
        {
            return View();
        }
    }
}