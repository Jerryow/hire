namespace MrMatch.Domain.Models
{
    using MrMatch.Domain.EntityBase;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    /// <summary>
    /// 候选人教育经历表
    /// </summary>
    public partial class TP_UserEducation : Entity
    {
        /// <summary>
        /// 候选人ID
        /// </summary>
        public long UserID { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StartDate { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        [StringLength(100)]
        public string ExpirationDate { get; set; }
        /// <summary>
        /// 学校名称
        /// </summary>
        [StringLength(100)]
        public string SchoolName { get; set; }
        /// <summary>
        /// 专业
        /// </summary>
        [StringLength(100)]
        public string MajorSubject { get; set; }
        /// <summary>
        /// 学历
        /// </summary>
        public int Degree { get; set; }
    }
}
