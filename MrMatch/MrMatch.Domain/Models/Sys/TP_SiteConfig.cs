namespace MrMatch.Domain.Models
{
    using MrMatch.Domain.EntityBase;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class TP_SiteConfig : Entity
    {
        /// <summary>
        /// ��������
        /// </summary>
        [StringLength(50)]
        public string ConfigName { get; set; }
        /// <summary>
        /// ���ñ���
        /// </summary>
        [StringLength(50)]
        public string ConfigCode { get; set; }
        /// <summary>
        /// ����ֵ
        /// </summary>
        [StringLength(1000)]
        public string ConfigValue { get; set; }
        /// <summary>
        /// �Ƿ��ڲ�
        /// </summary>
        public bool OnInternal { get; set; }
    }
}
