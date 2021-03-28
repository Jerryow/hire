namespace MrMatch.Domain.Models
{
    using MrMatch.Domain.EntityBase;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    /// <summary>
    /// ��ҵ����
    /// </summary>
    public partial class TP_Company : Entity
    {
        /// <summary>
        /// ��ҵ����
        /// </summary>
        [StringLength(100)]
        public string CompanyName { get; set; }
        /// <summary>
        /// ������ʾ����
        /// </summary>
        [StringLength(50)]
        public string ShortName { get; set; }
        /// <summary>
        /// ��ҵ����
        /// </summary>
        public int CompnayType { get; set; }
        /// <summary>
        /// ���ʽ׶�
        /// </summary>
        public int FinancingStatus { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        [StringLength(200)]
        public string Website { get; set; }
        /// <summary>
        /// ��ҵ��ģ
        /// </summary>
        public int EmployeeNum { get; set; }
        /// <summary>
        /// ע���ʽ�
        /// </summary>
        public int RegisteredCapital { get; set; }
        /// <summary>
        /// ���
        /// </summary>
        [StringLength(200)]
        public string Summary { get; set; }
        /// <summary>
        /// ���ڵ�(�Ϻ�������)
        /// </summary>
        [StringLength(200)]
        public string Location { get; set; }
        /// <summary>
        /// ��ϸ��ַ
        /// </summary>
        [StringLength(500)]
        public string Address { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        [StringLength(1000)]
        public string Introduce { get; set; }
        /// <summary>
        /// ���״̬
        /// </summary>
        public int CheckedStatus { get; set; }
        /// <summary>
        /// �Ƿ���ͷ��ҵ
        /// </summary>
        public bool AgentOrNot { get; set; }
        /// <summary>
        /// ��ҵ֤��ͼƬ��ַ
        /// </summary>
        [StringLength(200)]
        public string LicenseImageUrl { get; set; }
        /// <summary>
        /// ֤����֤״̬
        /// </summary>
        public int LicenseAuthStatus { get; set; }
        /// <summary>
        /// ��ҵLogoͼƬ��ַ
        /// </summary>
        [StringLength(200)]
        public string LogoImageUrl { get; set; }
        /// <summary>
        /// ��ҵ������ϵ������
        /// </summary>
        [StringLength(50)]
        public string ContactName { get; set; }
        /// <summary>
        /// ������ϵ�˵绰
        /// </summary>
        [StringLength(50)]
        public string CellPhone { get; set; }
        /// <summary>
        /// ������ϵ������
        /// </summary>
        [StringLength(100)]
        public string Email { get; set; }
    }
}
