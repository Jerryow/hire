namespace MrMatch.Domain.Models
{
    using MrMatch.Domain.EntityBase;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    /// <summary>
    /// 技能表
    /// </summary>
    public partial class TP_Skills : Entity
    {
        /// <summary>
        /// 技能名称
        /// </summary>
        [StringLength(50)]
        public string SkillName { get; set; }
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool OnEnabled { get; set; }
    }
}
