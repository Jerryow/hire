namespace MrMatch.Domain.Models
{
    using MrMatch.Domain.EntityBase;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    /// <summary>
    /// ��ѡ�˼���������Ϣ
    /// </summary>
    public partial class TP_UserProfile : Entity
    {
        /// <summary>
        /// ��ѡ��ID
        /// </summary>
        public long UserID { get; set; }
        /// <summary>
        /// ��ʵ����
        /// </summary>
        [StringLength(50)]
        public string RealName { get; set; }
        /// <summary>
        /// �Ա� 1�� 2Ů
        /// </summary>
        public int Gender { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        public DateTime Birthday { get; set; }
        /// <summary>
        /// ѧ��
        /// </summary>
        [StringLength(50)]
        public string Education { get; set; }
        /// <summary>
        /// ������
        /// </summary>
        [StringLength(100)]
        public string BirthPlace { get; set; }
        /// <summary>
        /// ��ס��
        /// </summary>
        [StringLength(100)]
        public string Residence { get; set; }
        /// <summary>
        /// �������ڵ�
        /// </summary>
        [StringLength(100)]
        public string CensusRegister { get; set; }
        /// <summary>
        /// ��ǰ��ҵ
        /// </summary>
        [StringLength(100)]
        public string CurrentCompany { get; set; }
        /// <summary>
        /// ��ǰְλ
        /// </summary>
        [StringLength(50)]
        public string CurrentPosition { get; set; }
        /// <summary>
        /// ��������
        /// </summary>
        public int WorkYears { get; set; }
        /// <summary>
        /// ��ǰ��н
        /// </summary>
        public decimal AnnualSalary { get; set; }
        /// <summary>
        /// н���Ƿ񹫿�
        /// </summary>
        public bool OnOpen { get; set; }
        /// <summary>
        /// ��ְ״̬
        /// </summary>
        public int JobStatus { get; set; }
        /// <summary>
        /// ��������
        /// </summary>
        [StringLength(1000)]
        public string Introduction { get; set; }
        /// <summary>
        /// �Ϳ��նԱ��Ƿ�ı�
        /// </summary>
        public bool IsUpdated { get; set; }
    }
}
