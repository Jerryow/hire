using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MrMatch.Common.AttributeHelper;

namespace MrMatch.Application.LoginOrRegist.Inp
{
    public class BizRegistInp
    {
        [Validate("string","邮箱不能为空")]
        public string Email { get; set; }
        [Validate("string","手机号码不能为空")]
        public string CellPhone { get; set; }
        [Validate("string","邮箱验证码不能为空")]
        public string EmailVerifyCode { get; set; }
        [Validate("string","验证码错误")]
        public string PhoneVerifyCode { get; set; }
    }
}
