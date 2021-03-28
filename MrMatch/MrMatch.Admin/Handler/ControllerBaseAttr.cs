using MrMatch.Admin.Handler.ActionFilters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MrMatch.Admin.Handler
{
    //[SSLFilter]
    [GlobalError]
    [LoginCheckFilter]
    public class ControllerBaseAttr : Controller
    {
    }
}