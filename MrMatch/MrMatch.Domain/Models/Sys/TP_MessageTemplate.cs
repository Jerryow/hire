namespace MrMatch.Domain.Models
{
    using MrMatch.Domain.EntityBase;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    /// <summary>
    /// ��Ϣģ���
    /// </summary>
    public partial class TP_MessageTemplate : Entity
    {
        /// <summary>
        /// ģ������ 10С����ģ��  20�ʼ�ģ��
        /// </summary>
        public int TemplateType { get; set; }
        /// <summary>
        /// ģ�����
        /// </summary>
        [StringLength(50)]
        public string TemplateCode { get; set; }
        /// <summary>
        /// ģ������
        /// </summary>
        [StringLength(50)]
        public string TemplateTitle { get; set; }
        /// <summary>
        /// ģ������
        /// </summary>
        [StringLength(3000)]
        public string TemplateContent { get; set; }
        /// <summary>
        /// �Ƿ�����
        /// </summary>
        public bool IsActivated { get; set; }
        /// <summary>
        /// ��ע
        /// </summary>
        [StringLength(3000)]
        public string Remark { get; set; }
    }
}
