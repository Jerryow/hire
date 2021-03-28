namespace MrMatch.Domain.Models
{
    using MrMatch.Domain.EntityBase;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    /// <summary>
    /// ���ұ�
    /// </summary>
    public partial class TP_Country : Entity
    {
        /// <summary>
        /// ��������
        /// </summary>
        [StringLength(50)]
        public string CountryName { get; set; }
        /// <summary>
        /// Ӣ������
        /// </summary>
        [StringLength(100)]
        public string EngName { get; set; }
        /// <summary>
        /// ����ĸ
        /// </summary>
        [StringLength(5)]
        public string Initial { get; set; }
        /// <summary>
        /// ��д
        /// </summary>
        [StringLength(10)]
        public string Initials { get; set; }
        /// <summary>
        /// ������Ϣ
        /// </summary>
        [StringLength(50)]
        public string Extra { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        public int OrderNum { get; set; }
    }
}
