namespace MrMatch.Domain.Models
{
    using MrMatch.Domain.EntityBase;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    /// <summary>
    /// 生成候选人手机验证码
    /// </summary>
    public partial class TP_UserVerify : Entity
    {
        /// <summary>
        /// 手机号
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
