namespace MrMatch.Domain.Models
{
    using MrMatch.Domain.EntityBase;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    /// <summary>
    /// �û���ְ�����
    /// </summary>
    public partial class TP_UserJobIntention : Entity
    {
        /// <summary>
        /// ��ѡ��ID
        /// </summary>
        public long UserID { get; set; }
        /// <summary>
        /// ��������ID �ö��Ÿ���
        /// </summary>
        [StringLength(100)]
        public string FunctionIDs { get; set; }
        /// <summary>
        /// �������ID �ö��Ÿ���
        /// </summary>
        [StringLength(100)]
        public string LocationIDs { get; set; }
        /// <summary>
        /// ��н
        /// </summary>
        public decimal AnnualSalary { get; set; }
        /// <summary>
        /// ������н�Ƿ񹫿�
        /// </summary>
        public bool OnOpen { get; set; }
        /// <summary>
        /// �Ϳ��նԱ��Ƿ�ı�
        /// </summary>
        public bool IsUpdated { get; set; }
    }
}
