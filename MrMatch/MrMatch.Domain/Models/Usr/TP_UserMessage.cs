namespace MrMatch.Domain.Models
{
    using MrMatch.Domain.EntityBase;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    /// <summary>
    /// С����/B��վ����
    /// </summary>
    public partial class TP_UserMessage : Entity
    {
        /// <summary>
        /// ������ID
        /// </summary>
        public long RecieverID { get; set; }
        /// <summary>
        /// ��Ϣ����
        /// </summary>
        [StringLength(256)]
        public string MessageSubject { get; set; }
        /// <summary>
        /// ��Ϣ����
        /// </summary>
        [StringLength(3000)]
        public string MessageContent { get; set; }
        /// <summary>
        /// �Ƿ��ѷ���
        /// </summary>
        public bool IsSent { get; set; }
    }
}
