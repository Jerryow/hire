namespace MrMatch.Domain.Models
{
    using MrMatch.Domain.EntityBase;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    /// <summary>
    /// 企业成员搜索记录表
    /// </summary>
    public partial class TP_AccountInfoSearchLog : Entity
    {
        /// <summary>
        /// 企业ID
        /// </summary>
        public long CompanyID { get; set; }
        /// <summary>
        /// 企业成员ID
        /// </summary>
        public long AccountID { get; set; }
        /// <summary>
        /// 搜索地址
        /// </summary>
        [StringLength(200)]
        public string SearchUrl { get; set; }
        /// <summary>
        /// 标记名称
        /// </summary>
        [StringLength(200)]
        public string SearchName { get; set; }
    }
}
