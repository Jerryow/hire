namespace MrMatch.Domain.Models
{
    using MrMatch.Domain.EntityBase;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    /// <summary>
    /// ��ҵǩԼ��
    /// </summary>
    public partial class TP_CompanyContract : Entity
    {
        /// <summary>
        /// ��ҵID
        /// </summary>
        public long CompanyID { get; set; }
        /// <summary>
        /// ǩԼ����
        /// </summary>
        public DateTime ContractDate { get; set; }
        /// <summary>
        /// ǩԼ��Ч��
        /// </summary>
        public DateTime StartDate { get; set; }
        /// <summary>
        /// ǩԼ������
        /// </summary>
        public DateTime ExpireDate { get; set; }
        /// <summary>
        /// ���λ����
        /// </summary>
        public int AdvertiseCount { get; set; }
        /// <summary>
        /// ÿ�¿���������
        /// </summary>
        public int InviteCount { get; set; }
        /// <summary>
        /// ÿ�������ۼ�����
        /// </summary>
        public int AccumulateCount { get; set; }
        /// <summary>
        /// �Ƿ�����
        /// </summary>
        public bool OnEnabled { get; set; }
    }
}
