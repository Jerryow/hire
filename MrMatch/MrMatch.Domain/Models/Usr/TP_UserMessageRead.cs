namespace MrMatch.Domain.Models
{
    using MrMatch.Domain.EntityBase;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    /// <summary>
    /// վ�����Ƿ��Ѷ���ϵ��
    /// </summary>
    public partial class TP_UserMessageRead : Entity
    {
        /// <summary>
        /// ��ϢID
        /// </summary>
        public long MessageID { get; set; }
        /// <summary>
        /// �û�ID
        /// </summary>
        public long UserID { get; set; }
    }
}
