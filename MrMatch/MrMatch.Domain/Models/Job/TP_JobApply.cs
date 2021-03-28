namespace MrMatch.Domain.Models
{
    using MrMatch.Domain.EntityBase;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    /// <summary>
    /// ְλͶ�ݱ�
    /// </summary>
    public partial class TP_JobApply : Entity
    {
        /// <summary>
        /// �û�ID
        /// </summary>
        public long UserID { get; set; }
        /// <summary>
        /// Ͷ��״̬ 1:δ�鿴  2:�Ѳ鿴  3:���� 4:�ܾ�
        /// </summary>
        public int ApplyStatus { get; set; }
        /// <summary>
        /// ����ʱ��
        /// </summary>
        public DateTime ExpirationDate { get; set; }
        /// <summary>
        /// ְλ��������ҵ��ԱID
        /// </summary>
        public long AccountID { get; set; }
        /// <summary>
        /// ְλID
        /// </summary>
        public long JobID { get; set; }
    }
}
