namespace MrMatch.Domain.Models
{
    using MrMatch.Domain.EntityBase;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    /// <summary>
    /// 小程序/B端站内信
    /// </summary>
    public partial class TP_UserMessage : Entity
    {
        /// <summary>
        /// 接收者ID
        /// </summary>
        public long RecieverID { get; set; }
        /// <summary>
        /// 消息主题
        /// </summary>
        [StringLength(256)]
        public string MessageSubject { get; set; }
        /// <summary>
        /// 消息内容
        /// </summary>
        [StringLength(3000)]
        public string MessageContent { get; set; }
        /// <summary>
        /// 是否已发送
        /// </summary>
        public bool IsSent { get; set; }
    }
}
