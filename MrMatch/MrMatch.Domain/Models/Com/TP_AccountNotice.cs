namespace MrMatch.Domain.Models
{
    using MrMatch.Domain.EntityBase;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    /// <summary>
    /// ��ҵ��Ա��Ϣ���͹�ϵ��
    /// </summary>
    public partial class TP_AccountNotice : Entity
    {
        /// <summary>
        /// ��ԱID
        /// </summary>
        public long AccountID { get; set; }
        /// <summary>
        /// ��ϢID
        /// </summary>
        public long MessageID { get; set; }
    }
}
