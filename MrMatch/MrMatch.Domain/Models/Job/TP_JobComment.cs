namespace MrMatch.Domain.Models
{
    using MrMatch.Domain.EntityBase;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    /// <summary>
    /// 面试流程评价/备注表
    /// </summary>
    public partial class TP_JobComment : Entity
    {
        /// <summary>
        /// 候选人ID
        /// </summary>
        public long UserID { get; set; }
        /// <summary>
        /// 企业ID
        /// </summary>
        public long CompanyID { get; set; }
        /// <summary>
        /// 面试ID
        /// </summary>
        public long InterviewID { get; set; }
        /// <summary>
        /// 发起邀约的企业成员ID
        /// </summary>
        public long AccountID { get; set; }
        /// <summary>
        /// 是否对团队可见
        /// </summary>
        public bool IsOpen { get; set; }
        /// <summary>
        /// 评价内容
        /// </summary>
        [StringLength(3000)]
        public string Comment { get; set; }
    }
}
