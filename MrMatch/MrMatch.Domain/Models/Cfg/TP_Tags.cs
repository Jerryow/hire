namespace MrMatch.Domain.Models
{
    using MrMatch.Domain.EntityBase;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    /// <summary>
    /// 基础标签表
    /// </summary>
    public partial class TP_Tags : Entity
    {
        /// <summary>
        /// 父ID
        /// </summary>
        public long ParentID { get; set; }
        /// <summary>
        /// 标签名称
        /// </summary>
        [StringLength(200)]
        public string Tags { get; set; }
    }
}
