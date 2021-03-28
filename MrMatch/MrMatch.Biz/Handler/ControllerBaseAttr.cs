
using MrMatch.Biz.Handler.ActionFilter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MrMatch.Biz.Handler
{
    //[SSLFilter]
    [GlobalError]
    [LoginCheckFilter]
    public class ControllerBaseAttr : Controller
    {
    }
}