namespace MrMatch.Domain.Models
{
    using MrMatch.Domain.EntityBase;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    /// <summary>
    /// 邀请/面试表
    /// </summary>
    public partial class TP_JobInterview : Entity
    {
        /// <summary>
        /// 企业ID
        /// </summary>
        public long CompanyID { get; set; }
        /// <summary>
        /// 候选人ID
        /// </summary>
        public long UserID { get; set; }
        /// <summary>
        /// 企业成员ID
        /// </summary>
        public long AccountID { get; set; }
        /// <summary>
        /// 流程状态  
        /// </summary>
        public int InterviewStatus { get; set; }
        /// <summary>
        /// 面试日期
        /// </summary>
        public DateTime? InterviewDate { get; set; }
        /// <summary>
        /// 候选人备注
        /// </summary>
        [StringLength(3000)]
        public string Comment { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [StringLength(3000)]
        public string Remark { get; set; }
        /// <summary>
        /// 企业邀请的招呼语
        /// </summary>
        [StringLength(3000)]
        public string InviteLetter { get; set; }
    }
}
