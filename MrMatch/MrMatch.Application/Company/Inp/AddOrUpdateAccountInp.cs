using MrMatch.Domain.EntityBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MrMatch.Common.AttributeHelper;

namespace MrMatch.Application.Company.Inp
{
    public class AddOrUpdateAccountInp : EntityDto
    {
        /// <summary>
        /// 企业ID
        /// </summary>
        [Validate("long", "企业id不能为空")]
        public long CompanyID { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        [Validate("string", "邮箱不能为空")]
        public string Email { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        [Validate("string", "电话不能为空")]
        public string CellPhone { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        [Validate("string", "姓名不能为空")]
        public string AccountName { get; set; }
        /// <summary>
        /// 职位
        /// </summary>
        [Validate("string", "职位不能为空")]
        public string Position { get; set; }
        /// <summary>
        /// 域名
        /// </summary>
        public string Domain { get; set; }
        /// <summary>
        /// 是否管理员
        /// </summary>
        public bool IsAdmin { get; set; }
        /// <summary>
        /// 是否激活
        /// </summary>
        public bool OnActive { get; set; }
        /// <summary>
        /// 是否免验登陆
        /// </summary>
        public bool IsVerify { get; set; }
    }
}
