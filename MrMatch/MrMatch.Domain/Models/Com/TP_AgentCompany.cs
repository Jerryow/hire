namespace MrMatch.Domain.Models
{
    using MrMatch.Domain.EntityBase;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    /// <summary>
    /// ��ͷ��ҵ����˾��
    /// </summary>
    public partial class TP_AgentCompany : Entity
    {
        /// <summary>
        /// ��ͷ��ҵID
        /// </summary>
        public long BasicCompanyID { get; set; }
        /// <summary>
        /// ��ҵ��ԱID
        /// </summary>
        public long AccountID { get; set; }
        /// <summary>
        /// ��ҵ����
        /// </summary>
        [StringLength(100)]
        public string CompanyName { get; set; }
        /// <summary>
        /// ��ҵ������ʾ����
        /// </summary>
        [StringLength(50)]
        public string ShortName { get; set; }
        /// <summary>
        /// ���ڳ���
        /// </summary>
        [StringLength(100)]
        public string City { get; set; }
        /// <summary>
        /// ��ҵ����
        /// </summary>
        public int CompnayType { get; set; }
        /// <summary>
        /// ��ҵ��ģ
        /// </summary>
        public int EmployeeNum { get; set; }
        /// <summary>
        /// ���
        /// </summary>
        [StringLength(1000)]
        public string Summary { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        [StringLength(3000)]
        public string Introduce { get; set; }
    }
}
