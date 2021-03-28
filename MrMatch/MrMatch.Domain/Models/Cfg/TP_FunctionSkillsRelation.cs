namespace MrMatch.Domain.Models
{
    using MrMatch.Domain.EntityBase;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    /// <summary>
    /// 职能/技能关联表
    /// </summary>
    public partial class TP_FunctionSkillsRelation : Entity
    {
        /// <summary>
        /// 技能ID
        /// </summary>
        public long SkillID { get; set; }
        /// <summary>
        /// 职能ID
        /// </summary>
        public long FunctionID { get; set; }
    }
}
