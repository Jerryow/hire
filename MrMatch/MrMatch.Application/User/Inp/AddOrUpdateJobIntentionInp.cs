using MrMatch.Common.AttributeHelper;
using MrMatch.Domain.EntityBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrMatch.Application.User.Inp
{
    public class AddOrUpdateJobIntentionInp : EntityDto
    {
        /// <summary>
        /// 候选人ID
        /// </summary>
        [Validate("long", "UserID错误")]
        public long UserID { get; set; }
        /// <summary>
        /// 过往资历ID 用逗号隔开
        /// </summary>
        [Validate("string", "请选择过往资历")]
        public string FunctionIDs { get; set; }
        /// <summary>
        /// 意向城市ID 用逗号隔开
        /// </summary>
        [Validate("string", "请选择意向城市")]
        public string LocationIDs { get; set; }
        /// <summary>
        /// 年薪
        /// </summary>
        public decimal AnnualSalary { get; set; }
    }
}
