namespace MrMatch.Domain.Models
{
    using MrMatch.Domain.EntityBase;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    /// <summary>
    /// ������ǩ��
    /// </summary>
    public partial class TP_Tags : Entity
    {
        /// <summary>
        /// ��ID
        /// </summary>
        public long ParentID { get; set; }
        /// <summary>
        /// ��ǩ����
        /// </summary>
        [StringLength(200)]
        public string Tags { get; set; }
    }
}
