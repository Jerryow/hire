namespace MrMatch.Domain.Models
{
    using MrMatch.Domain.EntityBase;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    /// <summary>
    /// ������������/��ע��
    /// </summary>
    public partial class TP_JobComment : Entity
    {
        /// <summary>
        /// ��ѡ��ID
        /// </summary>
        public long UserID { get; set; }
        /// <summary>
        /// ��ҵID
        /// </summary>
        public long CompanyID { get; set; }
        /// <summary>
        /// ����ID
        /// </summary>
        public long InterviewID { get; set; }
        /// <summary>
        /// ������Լ����ҵ��ԱID
        /// </summary>
        public long AccountID { get; set; }
        /// <summary>
        /// �Ƿ���Ŷӿɼ�
        /// </summary>
        public bool IsOpen { get; set; }
        /// <summary>
        /// ��������
        /// </summary>
        [StringLength(3000)]
        public string Comment { get; set; }
    }
}
