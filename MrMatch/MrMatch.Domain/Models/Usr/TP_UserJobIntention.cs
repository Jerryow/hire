namespace MrMatch.Domain.Models
{
    using MrMatch.Domain.EntityBase;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    /// <summary>
    /// 用户求职意向表
    /// </summary>
    public partial class TP_UserJobIntention : Entity
    {
        /// <summary>
        /// 候选人ID
        /// </summary>
        public long UserID { get; set; }
        /// <summary>
        /// 过往资历ID 用逗号隔开
        /// </summary>
        [StringLength(100)]
        public string FunctionIDs { get; set; }
        /// <summary>
        /// 意向城市ID 用逗号隔开
        /// </summary>
        [StringLength(100)]
        public string LocationIDs { get; set; }
        /// <summary>
        /// 年薪
        /// </summary>
        public decimal AnnualSalary { get; set; }
        /// <summary>
        /// 意向年薪是否公开
        /// </summary>
        public bool OnOpen { get; set; }
        /// <summary>
        /// 和快照对比是否改变
        /// </summary>
        public bool IsUpdated { get; set; }
    }
}
