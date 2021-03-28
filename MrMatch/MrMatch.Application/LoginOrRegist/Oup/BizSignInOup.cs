using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrMatch.Application.LoginOrRegist.Oup
{
    public class BizSignInOup
    {
        public string Cookie { get; set; }

        public bool IsOK { get; set; }

        public string Msg { get; set; }

        public string ReturnUrl { get; set; }
    }
}
