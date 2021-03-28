namespace MrMatch.Domain.Models
{
    using MrMatch.Domain.EntityBase;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    /// <summary>
    /// ��ҵ�û��ղع�����ϵ��
    /// </summary>
    public partial class TP_AccountFollowFolder : Entity
    {
        /// <summary>
        /// ��ҵ��ԱID
        /// </summary>
        public long AccountID { get; set; }
        /// <summary>
        /// �û�ID
        /// </summary>
        public long UserID { get; set; }
        /// <summary>
        /// �ղؼ�ID
        /// </summary>
        public long FolderID { get; set; }
    }
}
