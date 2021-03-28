namespace MrMatch.Domain.Models
{
    using MrMatch.Domain.EntityBase;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    /// <summary>
    /// ��ҵ��Ա����ģ���
    /// </summary>
    public partial class TP_LetterTemplate : Entity
    {
        /// <summary>
        /// ��ҵID
        /// </summary>
        public long CompanyID { get; set; }
        /// <summary>
        /// ��ҵ��ԱID
        /// </summary>
        public long AccountID { get; set; }
        /// <summary>
        /// ģ������
        /// </summary>
        [StringLength(50)]
        public string TemplateName { get; set; }
        /// <summary>
        /// ģ������
        /// </summary>
        [StringLength(3000)]
        public string TemplateContent { get; set; }
    }
}
