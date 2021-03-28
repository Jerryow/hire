using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MrMatch.Common.AttributeHelper;

namespace MrMatch.Application.LoginOrRegist.Inp
{
    public class MobileLoginInp
    {
        public string CountryCode { get; set; }
        [Validate("string","手机号不能为空")]
        public string CellPhone { get; set; }
        [Validate("string", "验证码不能为空")]
        public string VerifyCode { get; set; }

        public bool AutoLogin { get; set; }
    }
}
