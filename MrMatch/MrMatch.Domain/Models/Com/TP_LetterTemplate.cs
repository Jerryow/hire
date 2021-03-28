namespace MrMatch.Domain.Models
{
    using MrMatch.Domain.EntityBase;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    /// <summary>
    /// 企业成员邀请模板表
    /// </summary>
    public partial class TP_LetterTemplate : Entity
    {
        /// <summary>
        /// 企业ID
        /// </summary>
        public long CompanyID { get; set; }
        /// <summary>
        /// 企业成员ID
        /// </summary>
        public long AccountID { get; set; }
        /// <summary>
        /// 模板名称
        /// </summary>
        [StringLength(50)]
        public string TemplateName { get; set; }
        /// <summary>
        /// 模板内容
        /// </summary>
        [StringLength(3000)]
        public string TemplateContent { get; set; }
    }
}
