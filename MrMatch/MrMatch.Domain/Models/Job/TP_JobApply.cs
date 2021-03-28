namespace MrMatch.Domain.Models
{
    using MrMatch.Domain.EntityBase;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    /// <summary>
    /// 职位投递表
    /// </summary>
    public partial class TP_JobApply : Entity
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public long UserID { get; set; }
        /// <summary>
        /// 投递状态 1:未查看  2:已查看  3:接收 4:拒绝
        /// </summary>
        public int ApplyStatus { get; set; }
        /// <summary>
        /// 过期时间
        /// </summary>
        public DateTime ExpirationDate { get; set; }
        /// <summary>
        /// 职位发布的企业成员ID
        /// </summary>
        public long AccountID { get; set; }
        /// <summary>
        /// 职位ID
        /// </summary>
        public long JobID { get; set; }
    }
}
