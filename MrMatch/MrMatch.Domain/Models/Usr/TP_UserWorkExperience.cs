namespace MrMatch.Domain.Models
{
    using MrMatch.Domain.EntityBase;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    /// <summary>
    /// ��ѡ�˹�������
    /// </summary>
    public partial class TP_UserWorkExperience : Entity
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
        /// ��ҵ����
        /// </summary>
        [StringLength(100)]
        public string CompanyName { get; set; }
        /// <summary>
        /// ͷ��
        /// </summary>
        [StringLength(50)]
        public string Position { get; set; }
        /// <summary>
        /// ��������
        /// </summary>
        [StringLength(3000)]
        public string Introduction { get; set; }
        /// <summary>
        /// ��������ְ��(��ʱ����)
        /// </summary>
        [StringLength(500)]
        public string FunctionIDs { get; set; }
    }
}
