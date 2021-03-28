using MrMatch.Common.AttributeHelper;
using MrMatch.Domain.EntityBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrMatch.Application.User.Inp
{
    public class AddOrUpdateWorkExperienceInp : EntityDto
    {
        /// <summary>
        /// 候选人ID
        /// </summary>
        [Validate("long", "UserID错误")]
        public long UserID { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StartDate { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public string ExpirationDate { get; set; }
        /// <summary>
        /// 企业名称
        /// </summary>
        [Validate("string", "企业名称不能为空")]
        public string CompanyName { get; set; }
        /// <summary>
        /// 头衔
        /// </summary>
        [Validate("string", "头衔不能为空")]
        public string Position { get; set; }
        /// <summary>
        /// 工作介绍
        /// </summary>
        public string Introduction { get; set; }
    }
}
