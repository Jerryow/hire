namespace MrMatch.Domain.Models
{
    using MrMatch.Domain.EntityBase;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    /// <summary>
    /// 企业成员邀请表
    /// </summary>
    public partial class TP_AccountInvitation : Entity
    {
        /// <summary>
        /// 企业ID
        /// </summary>
        public long CompanyID { get; set; }
        /// <summary>
        /// 被邀请者邮箱
        /// </summary>
        [StringLength(100)]
        public string Email { get; set; }
        /// <summary>
        /// 域名
        /// </summary>
        [StringLength(100)]
        public string Domain { get; set; }
        /// <summary>
        /// 发起邀请者ID
        /// </summary>
        public long SenderID { get; set; }
        /// <summary>
        /// 是否已注册(默认false)
        /// </summary>
        public bool IsRegisted { get; set; }
    }
}
