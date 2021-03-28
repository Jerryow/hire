using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MrMatch.Common.AttributeHelper;

namespace MrMatch.Application.Job.Inp
{
    public class UserReportInp
    {
        /// <summary>
        /// 用户id
        /// </summary>
        [Validate("long", "UserID错误")]
        public long UserID { get; set; }
        /// <summary>
        /// 职位id
        /// </summary>
        [Validate("long", "JobID错误")]
        public long JobID { get; set; }
        /// <summary>
        /// 举报原因
        /// </summary>
        [Validate("string", "举报原因不能为空")]
        public string Reason { get; set; }
        /// <summary>
        /// 额外原因
        /// </summary>
        public string ReasonExtra { get; set; }
    }
}
