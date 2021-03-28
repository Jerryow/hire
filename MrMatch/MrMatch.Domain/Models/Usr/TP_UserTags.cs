namespace MrMatch.Domain.Models
{
    using MrMatch.Domain.EntityBase;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    /// <summary>
    /// 用户标签
    /// </summary>
    public partial class TP_UserTags : Entity
    {
        /// <summary>
        /// 父ID
        /// </summary>
        public long TagID { get; set; }
        /// <summary>
        /// 用户ID
        /// </summary>
        public long UserID { get; set; }
        /// <summary>
        /// 标签名称
        /// </summary>
        [StringLength(200)]
        public string Tags { get; set; }
    }
}
