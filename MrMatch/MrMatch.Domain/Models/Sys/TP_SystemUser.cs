namespace MrMatch.Domain.Models
{
    using MrMatch.Domain.EntityBase;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    /// <summary>
    /// 系统用户表
    /// </summary>
    public partial class TP_SystemUser : Entity
    {
        /// <summary>
        /// 登录名
        /// </summary>
        [Required]
        [StringLength(50)]
        public string LoginName { get; set; }
        /// <summary>
        /// 账户名
        /// </summary>
        [Required]
        [StringLength(50)]
        public string UserName { get; set; }
        /// <summary>
        /// 联系邮箱
        /// </summary>
        [StringLength(100)]
        public string Email { get; set; }
        /// <summary>
        /// 登陆密码
        /// </summary>
        [Required]
        [StringLength(256)]
        public string Password { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [StringLength(200)]
        public string Remark { get; set; }
        /// <summary>
        /// 是否内部用户
        /// </summary>
        public bool OnInternal { get; set; }
        /// <summary>
        /// 是否激活
        /// </summary>
        public bool OnActive { get; set; }
        /// <summary>
        /// 激活时间
        /// </summary>
        public DateTime ActiveTime { get; set; }
    }
}
