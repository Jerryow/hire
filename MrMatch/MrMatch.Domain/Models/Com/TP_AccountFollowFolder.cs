namespace MrMatch.Domain.Models
{
    using MrMatch.Domain.EntityBase;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    /// <summary>
    /// 企业用户收藏关联关系表
    /// </summary>
    public partial class TP_AccountFollowFolder : Entity
    {
        /// <summary>
        /// 企业成员ID
        /// </summary>
        public long AccountID { get; set; }
        /// <summary>
        /// 用户ID
        /// </summary>
        public long UserID { get; set; }
        /// <summary>
        /// 收藏夹ID
        /// </summary>
        public long FolderID { get; set; }
    }
}
