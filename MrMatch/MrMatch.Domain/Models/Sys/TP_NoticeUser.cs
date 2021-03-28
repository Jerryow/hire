namespace MrMatch.Domain.Models
{
    using MrMatch.Domain.EntityBase;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    /// <summary>
    /// 消息主动推送表
    /// </summary>
    public partial class TP_NoticeUser : Entity
    {
        /// <summary>
        /// 消息主题
        /// </summary>
        public long MessageID { get; set; }
        /// <summary>
        /// 接收者,0所有用户/具体的用户ID
        /// </summary>
        public long UserID { get; set; }
    }
}
