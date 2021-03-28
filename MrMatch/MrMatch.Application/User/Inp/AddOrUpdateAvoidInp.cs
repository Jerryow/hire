using MrMatch.Common.AttributeHelper;
using MrMatch.Domain.EntityBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrMatch.Application.User.Inp
{
    public class AddOrUpdateAvoidInp : EntityDto
    {
        /// <summary>
        /// 候选人ID
        /// </summary>
        [Validate("long", "UserID错误")]
        public long UserID { get; set; }
        /// <summary>
        /// 屏蔽域名
        /// </summary>
        [Validate("string", "屏蔽域名不能为空")]
        public string AvoidDomain { get; set; }
    }
}
