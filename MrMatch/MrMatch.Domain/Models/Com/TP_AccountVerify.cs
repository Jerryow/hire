namespace MrMatch.Domain.Models
{
    using MrMatch.Domain.EntityBase;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    /// <summary>
    /// 登陆/验证的验证码生成表
    /// </summary>
    public partial class TP_AccountVerify : Entity
    {
        /// <summary>
        /// 类型  1:短信 2:邮箱
        /// </summary>
        public int VerifyType { get; set; }
        /// <summary>
        /// 接收者   手机号/邮箱
        /// </summary>
        [Required]
        [StringLength(100)]
        public string SendTo { get; set; }
        /// <summary>
        /// 验证码
        /// </summary>
        [Required]
        [StringLength(20)]
        public string VerifyCode { get; set; }
        /// <summary>
        /// 过期时间
        /// </summary>
        public DateTime ExpiredTime { get; set; }
        /// <summary>
        /// 是否使用
        /// </summary>
        public bool IsUsed { get; set; }
    }
}
