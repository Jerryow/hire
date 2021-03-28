namespace MrMatch.Domain.Models
{
    using MrMatch.Domain.EntityBase;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    /// <summary>
    /// �û���ǩ
    /// </summary>
    public partial class TP_UserTags : Entity
    {
        /// <summary>
        /// ��ID
        /// </summary>
        public long TagID { get; set; }
        /// <summary>
        /// �û�ID
        /// </summary>
        public long UserID { get; set; }
        /// <summary>
        /// ��ǩ����
        /// </summary>
        [StringLength(200)]
        public string Tags { get; set; }
    }
}
