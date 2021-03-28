namespace MrMatch.Domain.Models
{
    using MrMatch.Domain.EntityBase;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    /// <summary>
    /// ���������¼��
    /// </summary>
    public partial class TP_ProfileView : Entity
    {
        /// <summary>
        /// ��ѡ��ID
        /// </summary>
        public long UserID { get; set; }
        /// <summary>
        /// ��ҵ��ԱID
        /// </summary>
        public long AgentID { get; set; }
        /// <summary>
        /// ��ҵID
        /// </summary>
        public long CompanyID { get; set; }
    }
}
