namespace MrMatch.Domain.Models
{
    using MrMatch.Domain.EntityBase;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    /// <summary>
    /// 消息模板表
    /// </summary>
    public partial class TP_MessageTemplate : Entity
    {
        /// <summary>
        /// 模板类型 10小程序模板  20邮件模板
        /// </summary>
        public int TemplateType { get; set; }
        /// <summary>
        /// 模板编码
        /// </summary>
        [StringLength(50)]
        public string TemplateCode { get; set; }
        /// <summary>
        /// 模板主题
        /// </summary>
        [StringLength(50)]
        public string TemplateTitle { get; set; }
        /// <summary>
        /// 模板内容
        /// </summary>
        [StringLength(3000)]
        public string TemplateContent { get; set; }
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsActivated { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [StringLength(3000)]
        public string Remark { get; set; }
    }
}
