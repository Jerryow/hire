namespace MrMatch.Domain.Models
{
    using MrMatch.Domain.EntityBase;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    /// <summary>
    /// 候选人屏蔽表
    /// </summary>
    public partial class TP_UserAvoid : Entity
    {
        /// <summary>
        /// 候选人ID
        /// </summary>
        public long UserID { get; set; }
        /// <summary>
        /// 屏蔽域名
        /// </summary>
        [StringLength(200)]
        public string AvoidDomain { get; set; }
    }
}
