namespace MrMatch.Domain.Models
{
    using MrMatch.Domain.EntityBase;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    /// <summary>
    /// ��ҵ�û��ղؼб�
    /// </summary>
    public partial class TP_AccountFolder : Entity
    {
        /// <summary>
        /// ��ҵ��ԱID
        /// </summary>
        public long AccountID { get; set; }
        /// <summary>
        /// �ղؼ�����
        /// </summary>
        [StringLength(100)]
        public string FolderName { get; set; }
    }
}
