namespace MrMatch.Domain.Models
{
    using MrMatch.Domain.EntityBase;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    /// <summary>
    /// 简历浏览记录表
    /// </summary>
    public partial class TP_ProfileView : Entity
    {
        /// <summary>
        /// 候选人ID
        /// </summary>
        public long UserID { get; set; }
        /// <summary>
        /// 企业成员ID
        /// </summary>
        public long AgentID { get; set; }
        /// <summary>
        /// 企业ID
        /// </summary>
        public long CompanyID { get; set; }
    }
}
