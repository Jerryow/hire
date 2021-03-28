using MrMatch.Domain.EntityBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MrMatch.Common.AttributeHelper;

namespace MrMatch.Application.User.Inp
{
    public class AddOrUpdateProfileInp : EntityDto
    {
        /// <summary>
        /// 候选人ID
        /// </summary>
        [Validate("long", "UserID错误")]
        public long UserID { get; set; }
        /// <summary>
        /// 真实姓名
        /// </summary>
        [Validate("string", "真实姓名不能为空")]
        public string RealName { get; set; }
        /// <summary>
        /// 性别 1男 2女
        /// </summary>
        [Validate("int", "请选择性别")]
        public int Gender { get; set; }
        /// <summary>
        /// 生日
        /// </summary>
        public DateTime Birthday { get; set; }
        /// <summary>
        /// 学历
        /// </summary>
        [Validate("string", "学历不能为空")]
        public string Education { get; set; }
        /// <summary>
        /// 出生地
        /// </summary>
        public string BirthPlace { get; set; }
        /// <summary>
        /// 居住地
        /// </summary>
        [Validate("string", "居住地不能为空")]
        public string Residence { get; set; }
        /// <summary>
        /// 户籍所在地
        /// </summary>
        public string CensusRegister { get; set; }
        /// <summary>
        /// 当前企业
        /// </summary>
        [Validate("string", "当前企业不能为空")]
        public string CurrentCompany { get; set; }
        /// <summary>
        /// 当前职位
        /// </summary>
        [Validate("string", "当前职位不能为空")]
        public string CurrentPosition { get; set; }
        /// <summary>
        /// 当前年薪
        /// </summary>
        public decimal AnnualSalary { get; set; }
        /// <summary>
        /// 求职状态
        /// </summary>
        [Validate("int", "请选择求职状态")]
        public int JobStatus { get; set; }
        /// <summary>
        /// 工作介绍
        /// </summary>
        public string Introduction { get; set; }
    }
}
