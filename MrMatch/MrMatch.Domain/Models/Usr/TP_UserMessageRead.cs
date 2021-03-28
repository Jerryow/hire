namespace MrMatch.Domain.Models
{
    using MrMatch.Domain.EntityBase;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    /// <summary>
    /// 站内信是否已读关系表
    /// </summary>
    public partial class TP_UserMessageRead : Entity
    {
        /// <summary>
        /// 消息ID
        /// </summary>
        public long MessageID { get; set; }
        /// <summary>
        /// 用户ID
        /// </summary>
        public long UserID { get; set; }
    }
}
