using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrMatch.Application.LoginOrRegist.Oup
{
    public class SignInOup
    {
        public string Cookie { get; set; }

        public bool IsOK { get; set; }

        public string Msg { get; set; }

        public string ReturnUrl { get; set; }

        public long PKID { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName { get; set; }
        /// <summary>
        /// 头像
        /// </summary>
        public string AvatarUrl { get; set; }
    }
}
