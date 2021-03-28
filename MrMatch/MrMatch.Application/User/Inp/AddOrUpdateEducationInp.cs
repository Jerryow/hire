using MrMatch.Common.AttributeHelper;
using MrMatch.Domain.EntityBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrMatch.Application.User.Inp
{
    public class AddOrUpdateEducationInp : EntityDto
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
        /// 学校名称
        /// </summary>
        [Validate("string", "学校名称不能为空")]
        public string SchoolName { get; set; }
        /// <summary>
        /// 专业
        /// </summary>
        [Validate("string", "专业不能为空")]
        public string MajorSubject { get; set; }
        /// <summary>
        /// 学历
        /// </summary>
        [Validate("int", "学历不能为空")]
        public int Degree { get; set; }
    }
}
