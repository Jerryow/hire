namespace MrMatch.Domain.Models
{
    using MrMatch.Domain.EntityBase;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    /// <summary>
    /// 举报
    /// </summary>
    public partial class TP_UserReport : Entity
    {
        /// <summary>
        /// 举报人ID
        /// </summary>
        public long UserID { get; set; }
        /// <summary>
        /// 职位ID
        /// </summary>
        public long JobID { get; set; }
        /// <summary>
        /// 举报原因
        /// </summary>
        [StringLength(100)]
        public string Reason { get; set; }
        /// <summary>
        /// 补充说明
        /// </summary>
        [StringLength(3000)]
        public string ReasonExtra { get; set; }
    }
}
