namespace MrMatch.Domain.Models
{
    using MrMatch.Domain.EntityBase;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    /// <summary>
    /// ��ѡ�˽���������
    /// </summary>
    public partial class TP_UserEducation : Entity
    {
        /// <summary>
        /// ��ѡ��ID
        /// </summary>
        public long UserID { get; set; }
        /// <summary>
        /// ��ʼʱ��
        /// </summary>
        public DateTime StartDate { get; set; }
        /// <summary>
        /// ����ʱ��
        /// </summary>
        [StringLength(100)]
        public string ExpirationDate { get; set; }
        /// <summary>
        /// ѧУ����
        /// </summary>
        [StringLength(100)]
        public string SchoolName { get; set; }
        /// <summary>
        /// רҵ
        /// </summary>
        [StringLength(100)]
        public string MajorSubject { get; set; }
        /// <summary>
        /// ѧ��
        /// </summary>
        public int Degree { get; set; }
    }
}
