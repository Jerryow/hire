namespace MrMatch.Domain.Models
{
    using MrMatch.Domain.EntityBase;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    /// <summary>
    /// ��ҵ��Ա������¼��
    /// </summary>
    public partial class TP_AccountInfoSearchLog : Entity
    {
        /// <summary>
        /// ��ҵID
        /// </summary>
        public long CompanyID { get; set; }
        /// <summary>
        /// ��ҵ��ԱID
        /// </summary>
        public long AccountID { get; set; }
        /// <summary>
        /// ������ַ
        /// </summary>
        [StringLength(200)]
        public string SearchUrl { get; set; }
        /// <summary>
        /// �������
        /// </summary>
        [StringLength(200)]
        public string SearchName { get; set; }
    }
}
