namespace MrMatch.Domain.Models
{
    using MrMatch.Domain.EntityBase;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    /// <summary>
    /// ְ��/���ܹ�����
    /// </summary>
    public partial class TP_FunctionSkillsRelation : Entity
    {
        /// <summary>
        /// ����ID
        /// </summary>
        public long SkillID { get; set; }
        /// <summary>
        /// ְ��ID
        /// </summary>
        public long FunctionID { get; set; }
    }
}
