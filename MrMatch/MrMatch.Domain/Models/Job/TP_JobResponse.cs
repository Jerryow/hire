namespace MrMatch.Domain.Models
{
    using MrMatch.Domain.EntityBase;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    /// <summary>
    /// 面试流程响应记录表
    /// </summary>
    public partial class TP_JobResponse : Entity
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
        /// 面试ID
        /// </summary>
        public long RequestID { get; set; }
        /// <summary>
        /// 操作值
        /// </summary>
        [StringLength(200)]
        public string ResponseValue { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [StringLength(3000)]
        public string Remark { get; set; }
    }
}
