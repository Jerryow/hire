namespace MrMatch.Domain.Models
{
    using MrMatch.Domain.EntityBase;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    /// <summary>
    /// ְλ����鿴��¼��
    /// </summary>
    public partial class TP_JobView : Entity
    {
        /// <summary>
        /// ְλID
        /// </summary>
        public long JobID { get; set; }
        /// <summary>
        /// �û�ID
        /// </summary>
        public long UserID { get; set; }
    }
}
