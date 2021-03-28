using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MrMatch.Common.AttributeHelper;

namespace MrMatch.Application.SendMessage.Inp
{
    public class SendPhoneInp
    {
        [Validate("string", "手机号不能为空")]
        public string PhoneNumber { get; set; }
        public int UserOffsetX { get; set; }
        [Validate("string", "验证签名不能为空")]
        public string UserSign { get; set; }
    }
}
