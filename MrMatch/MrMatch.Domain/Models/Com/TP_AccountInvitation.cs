namespace MrMatch.Domain.Models
{
    using MrMatch.Domain.EntityBase;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    /// <summary>
    /// ��ҵ��Ա�����
    /// </summary>
    public partial class TP_AccountInvitation : Entity
    {
        /// <summary>
        /// ��ҵID
        /// </summary>
        public long CompanyID { get; set; }
        /// <summary>
        /// ������������
        /// </summary>
        [StringLength(100)]
        public string Email { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        [StringLength(100)]
        public string Domain { get; set; }
        /// <summary>
        /// ����������ID
        /// </summary>
        public long SenderID { get; set; }
        /// <summary>
        /// �Ƿ���ע��(Ĭ��false)
        /// </summary>
        public bool IsRegisted { get; set; }
    }
}
