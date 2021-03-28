namespace MrMatch.Domain.Models
{
    using MrMatch.Domain.EntityBase;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    /// <summary>
    /// ��ѡ�����α�
    /// </summary>
    public partial class TP_UserAvoid : Entity
    {
        /// <summary>
        /// ��ѡ��ID
        /// </summary>
        public long UserID { get; set; }
        /// <summary>
        /// ��������
        /// </summary>
        [StringLength(200)]
        public string AvoidDomain { get; set; }
    }
}
