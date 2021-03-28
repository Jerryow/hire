namespace MrMatch.Domain.Models
{
    using MrMatch.Domain.EntityBase;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    /// <summary>
    /// ���ܱ�
    /// </summary>
    public partial class TP_Skills : Entity
    {
        /// <summary>
        /// ��������
        /// </summary>
        [StringLength(50)]
        public string SkillName { get; set; }
        /// <summary>
        /// �Ƿ�����
        /// </summary>
        public bool OnEnabled { get; set; }
    }
}
