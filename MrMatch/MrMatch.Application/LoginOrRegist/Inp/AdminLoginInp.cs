using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrMatch.Application.LoginOrRegist.Inp
{
    public class AdminLoginInp
    {
        public string LoginName { get; set; }

        public string Password { get; set; }

        public string ReturnUrl { get; set; }

        public string EncryptPassword { get; set; }

        public bool CheckNRE()
        {
            var rtn = true;
            if (string.IsNullOrEmpty(this.LoginName) || string.IsNullOrEmpty(this.Password))
            {
                rtn = false;
            }
            if (string.IsNullOrEmpty(this.ReturnUrl))
            {
                this.ReturnUrl = "/system/systemuser";
            }
            return rtn;
        }

        public string EncryptPwd()
        {
            var pwd = this.LoginName + "-" + this.Password;
            this.EncryptPassword = MrMatch.Common.Encrypt.Encryption.EncryptString(pwd);

            return this.EncryptPassword;
        }
    }
}
