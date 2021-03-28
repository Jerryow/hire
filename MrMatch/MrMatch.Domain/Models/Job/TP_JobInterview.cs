namespace MrMatch.Domain.Models
{
    using MrMatch.Domain.EntityBase;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    /// <summary>
    /// ����/���Ա�
    /// </summary>
    public partial class TP_JobInterview : Entity
    {
        /// <summary>
        /// ��ҵID
        /// </summary>
        public long CompanyID { get; set; }
        /// <summary>
        /// ��ѡ��ID
        /// </summary>
        public long UserID { get; set; }
        /// <summary>
        /// ��ҵ��ԱID
        /// </summary>
        public long AccountID { get; set; }
        /// <summary>
        /// ����״̬  
        /// </summary>
        public int InterviewStatus { get; set; }
        /// <summary>
        /// ��������
        /// </summary>
        public DateTime? InterviewDate { get; set; }
        /// <summary>
        /// ��ѡ�˱�ע
        /// </summary>
        [StringLength(3000)]
        public string Comment { get; set; }
        /// <summary>
        /// ��ע
        /// </summary>
        [StringLength(3000)]
        public string Remark { get; set; }
        /// <summary>
        /// ��ҵ������к���
        /// </summary>
        [StringLength(3000)]
        public string InviteLetter { get; set; }
    }
}
