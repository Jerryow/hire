namespace MrMatch.Domain.Models
{
    using MrMatch.Domain.EntityBase;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    /// <summary>
    /// 企业成员消息推送关系表
    /// </summary>
    public partial class TP_AccountNotice : Entity
    {
        /// <summary>
        /// 成员ID
        /// </summary>
        public long AccountID { get; set; }
        /// <summary>
        /// 消息ID
        /// </summary>
        public long MessageID { get; set; }
    }
}
