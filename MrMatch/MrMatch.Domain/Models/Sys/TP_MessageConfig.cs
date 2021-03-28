namespace MrMatch.Domain.Models
{
    using MrMatch.Domain.EntityBase;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    /// <summary>
    /// 第三方通知消息配置表
    /// </summary>
    public partial class TP_MessageConfig : Entity
    {
        /// <summary>
        /// 类型 10:短信 20邮件
        /// </summary>
        public int ConfigType { get; set; }
        /// <summary>
        /// 供应商名称
        /// </summary>
        [Required]
        [StringLength(50)]
        public string ProviderName { get; set; }
        /// <summary>
        /// 接口名称
        /// </summary>
        [StringLength(50)]
        public string ApiName { get; set; }
        /// <summary>
        /// 接口编码
        /// </summary>
        [StringLength(50)]
        public string ApiCode { get; set; }
        /// <summary>
        /// 接口地址
        /// </summary>
        [StringLength(200)]
        public string ApiUrl { get; set; }
        /// <summary>
        /// 端口
        /// </summary>
        public int? Port { get; set; }
        /// <summary>
        /// 是否启用ssl（默认false）
        /// </summary>
        public bool? EnableSsl { get; set; }
        /// <summary>
        /// 发送签名
        /// </summary>
        [StringLength(50)]
        public string SenderName { get; set; }
        /// <summary>
        /// 登录名
        /// </summary>
        [StringLength(50)]
        public string LoginName { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        [StringLength(256)]
        public string Password { get; set; }
        /// <summary>
        /// 验证密钥
        /// </summary>
        [StringLength(256)]
        public string VerifyKey { get; set; }
        /// <summary>
        /// 签名
        /// </summary>
        [StringLength(50)]
        public string SignMark { get; set; }
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsActivated { get; set; }
        /// <summary>
        /// 次数限制
        /// </summary>
        public int? TimesLimit { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [StringLength(3000)]
        public string Remark { get; set; }
    }
}
