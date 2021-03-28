namespace MrMatch.Domain.Models
{
    using MrMatch.Domain.EntityBase;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    /// <summary>
    /// 企业用户表
    /// </summary>
    public partial class TP_Account : Entity
    {
        /// <summary>
        /// 企业ID
        /// </summary>
        public long CompanyID { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        [StringLength(100)]
        public string Email { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        [StringLength(20)]
        public string CellPhone { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        [StringLength(256)]
        public string Password { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        [StringLength(50)]
        public string AccountName { get; set; }
        /// <summary>
        /// 职位
        /// </summary>
        [StringLength(50)]
        public string Position { get; set; }
        /// <summary>
        /// 域名
        /// </summary>
        [StringLength(100)]
        public string Domain { get; set; }
        /// <summary>
        /// 是否管理员
        /// </summary>
        public bool IsAdmin { get; set; }
        /// <summary>
        /// 是否激活
        /// </summary>
        public bool OnActive { get; set; }
        /// <summary>
        /// 上次登陆时间
        /// </summary>
        public DateTime LastLoginTime { get; set; }
        /// <summary>
        /// 草稿限制数量
        /// </summary>
        public int DraftLimiteCount { get; set; }
        /// <summary>
        /// 邀请模板限制数量
        /// </summary>
        public int TemplateLimiteCount { get; set; }
        /// <summary>
        /// 是否免验登陆
        /// </summary>
        public bool IsVerify { get; set; }
        /// <summary>
        /// 头像
        /// </summary>
        [StringLength(500)]
        public string AvatarUrl { get; set; }
        /// <summary>
        /// 微信号
        /// </summary>
        [StringLength(300)]
        public string WechatAccount { get; set; }
        /// <summary>
        /// 微信二维码
        /// </summary>
        [StringLength(500)]
        public string WechatContactUrl { get; set; }
        /// <summary>
        /// 领英链接
        /// </summary>
        [StringLength(500)]
        public string LinkinUrl { get; set; }
        /// <summary>
        /// 专注领域
        /// </summary>
        [StringLength(500)]
        public string FocusArea { get; set; }
        /// <summary>
        /// 个人介绍
        /// </summary>
        [StringLength(2000)]
        public string Introduction { get; set; }

    }
}
