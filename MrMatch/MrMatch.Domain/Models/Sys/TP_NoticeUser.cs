namespace MrMatch.Domain.Models
{
    using MrMatch.Domain.EntityBase;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    /// <summary>
    /// ��Ϣ�������ͱ�
    /// </summary>
    public partial class TP_NoticeUser : Entity
    {
        /// <summary>
        /// ��Ϣ����
        /// </summary>
        public long MessageID { get; set; }
        /// <summary>
        /// ������,0�����û�/������û�ID
        /// </summary>
        public long UserID { get; set; }
    }
}
