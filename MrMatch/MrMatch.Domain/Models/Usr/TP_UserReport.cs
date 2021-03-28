namespace MrMatch.Domain.Models
{
    using MrMatch.Domain.EntityBase;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    /// <summary>
    /// �ٱ�
    /// </summary>
    public partial class TP_UserReport : Entity
    {
        /// <summary>
        /// �ٱ���ID
        /// </summary>
        public long UserID { get; set; }
        /// <summary>
        /// ְλID
        /// </summary>
        public long JobID { get; set; }
        /// <summary>
        /// �ٱ�ԭ��
        /// </summary>
        [StringLength(100)]
        public string Reason { get; set; }
        /// <summary>
        /// ����˵��
        /// </summary>
        [StringLength(3000)]
        public string ReasonExtra { get; set; }
    }
}
