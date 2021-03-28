namespace MrMatch.Domain.Models
{
    using MrMatch.Domain.EntityBase;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    /// <summary>
    /// ְ�ܱ�
    /// </summary>
    public partial class TP_Function : Entity
    {
        /// <summary>
        /// ��ID
        /// </summary>
        public long ParentID { get; set; }
        /// <summary>
        /// ְ������
        /// </summary>
        [StringLength(50)]
        public string FunctionName { get; set; }
        /// <summary>
        /// �Ƿ�����
        /// </summary>
        public bool OnEnabled { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        public int SortNum { get; set; }
        /// <summary>
        /// �Ƿ�����ְλ
        /// </summary>
        public bool IsPopular { get; set; }
    }
}
