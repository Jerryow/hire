namespace MrMatch.Domain.Models
{
    using MrMatch.Domain.EntityBase;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    /// <summary>
    /// 用户收藏职位
    /// </summary>
    public partial class TP_UserJobFollow : Entity
    {
        /// <summary>
        /// 职位ID
        /// </summary>
        public long JobID { get; set; }
        /// <summary>
        /// 用户ID
        /// </summary>
        public long UserID { get; set; }
    }
}
