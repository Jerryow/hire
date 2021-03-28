namespace MrMatch.Domain.Models
{
    using MrMatch.Domain.EntityBase;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    /// <summary>
    /// 企业用户收藏夹表
    /// </summary>
    public partial class TP_AccountFolder : Entity
    {
        /// <summary>
        /// 企业成员ID
        /// </summary>
        public long AccountID { get; set; }
        /// <summary>
        /// 收藏夹名称
        /// </summary>
        [StringLength(100)]
        public string FolderName { get; set; }
    }
}
