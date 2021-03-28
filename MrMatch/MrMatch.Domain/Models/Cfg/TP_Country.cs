namespace MrMatch.Domain.Models
{
    using MrMatch.Domain.EntityBase;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    /// <summary>
    /// 国家表
    /// </summary>
    public partial class TP_Country : Entity
    {
        /// <summary>
        /// 国家名称
        /// </summary>
        [StringLength(50)]
        public string CountryName { get; set; }
        /// <summary>
        /// 英文名称
        /// </summary>
        [StringLength(100)]
        public string EngName { get; set; }
        /// <summary>
        /// 首字母
        /// </summary>
        [StringLength(5)]
        public string Initial { get; set; }
        /// <summary>
        /// 缩写
        /// </summary>
        [StringLength(10)]
        public string Initials { get; set; }
        /// <summary>
        /// 附加信息
        /// </summary>
        [StringLength(50)]
        public string Extra { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int OrderNum { get; set; }
    }
}
