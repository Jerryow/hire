using MrMatch.Domain.EntityBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrMatch.Application.User.Oup
{
    public class ProfileOup : EntityDto
    {
        /// <summary>
        /// 候选人ID
        /// </summary>
        public long UserID { get; set; }
        /// <summary>
        /// 真实姓名
        /// </summary>
        public string RealName { get; set; }
        /// <summary>
        /// 性别 1男 2女
        /// </summary>
        public int Gender { get; set; }
        /// <summary>
        /// 生日
        /// </summary>
        public DateTime Birthday { get; set; }
        /// <summary>
        /// 学历
        /// </summary>
        public string Education { get; set; }
        /// <summary>
        /// 出生地
        /// </summary>
        public string BirthPlace { get; set; }
        /// <summary>
        /// 居住地
        /// </summary>
        public string Residence { get; set; }
        /// <summary>
        /// 户籍所在地
        /// </summary>
        public string CensusRegister { get; set; }
        /// <summary>
        /// 当前企业
        /// </summary>
        public string CurrentCompany { get; set; }
        /// <summary>
        /// 当前职位
        /// </summary>
        public string CurrentPosition { get; set; }
        /// <summary>
        /// 当前年薪
        /// </summary>
        public decimal AnnualSalary { get; set; }
        /// <summary>
        /// 求职状态
        /// </summary>
        public int JobStatus { get; set; }
        /// <summary>
        /// 工作介绍
        /// </summary>
        public string Introduction { get; set; }
    }
}
