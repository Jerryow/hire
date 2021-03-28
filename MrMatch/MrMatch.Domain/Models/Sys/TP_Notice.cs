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
    public partial class TP_Notice : Entity
    {
        /// <summary>
        /// 消息主题
        /// </summary>
        [StringLength(100)]
        public string MessageSubject { get; set; }
        /// <summary>
        /// 消息内容
        /// </summary>
        [StringLength(3000)]
        public string MessageContent { get; set; }
        /// <summary>
        /// 消息类型 1:所有用户  2:指定用户
        /// </summary>
        public int MessageType { get; set; }
        /// <summary>
        /// 接收者,0所有用户/具体的用户ID
        /// </summary>
        [StringLength(3000)]
        public string UserIDs { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [StringLength(3000)]
        public string Remark { get; set; }
        /// <summary>
        /// 接收端,1:B端 2:C端
        /// </summary>
        public int SendClient { get; set; }
        /// <summary>
        /// 是否发送
        /// </summary>
        public bool IsFinish { get; set; }
    }
}
