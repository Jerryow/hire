using MrMatch.Domain.EntityBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrMatch.Application.User.Oup
{
    public class WechatAccountOup : EntityDto
    {
        /// <summary>
        /// 手机号码
        /// </summary>
        public string CellPhone { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }
    }
}
