using MrMatch.Domain.EntityBase;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrMatch.Domain.Models
{
    public class TP_WorkExperienceSnap : Entity
    {
        /// <summary>
        /// 候选人ID
        /// </summary>
        public long UserID { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StartDate { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        [StringLength(100)]
        public string ExpirationDate { get; set; }
        /// <summary>
        /// 企业名称
        /// </summary>
        [StringLength(100)]
        public string CompanyName { get; set; }
        /// <summary>
        /// 头衔
        /// </summary>
        [StringLength(50)]
        public string Position { get; set; }
        /// <summary>
        /// 工作介绍
        /// </summary>
        [StringLength(3000)]
        public string Introduction { get; set; }
        /// <summary>
        /// 工作经验职能(暂时不用)
        /// </summary>
        [StringLength(500)]
        public string FunctionIDs { get; set; }
    }
}
