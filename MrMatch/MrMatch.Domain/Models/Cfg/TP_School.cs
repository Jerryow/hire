namespace MrMatch.Domain.Models
{
    using MrMatch.Domain.EntityBase;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    /// <summary>
    /// 学校表
    /// </summary>
    public partial class TP_School : Entity
    {
        /// <summary>
        /// 国家ID
        /// </summary>
        public long CountryID { get; set; }
        /// <summary>
        /// 地区ID
        /// </summary>
        public long DistrictID { get; set; }
        /// <summary>
        /// 城市ID
        /// </summary>
        public long CityID { get; set; }
        /// <summary>
        /// 学校类型
        /// </summary>
        public int SchoolType { get; set; }
        /// <summary>
        /// 学校级别  985.211
        /// </summary>
        [StringLength(50)]
        public string SchoolGrade { get; set; }
        /// <summary>
        /// 是否公立
        /// </summary>
        public bool IsPublic { get; set; }
        /// <summary>
        /// 学校名称
        /// </summary>
        [StringLength(200)]
        public string SchoolName { get; set; }
        /// <summary>
        /// 英文名称
        /// </summary>
        [StringLength(200)]
        public string SchoolNameEN { get; set; }
        /// <summary>
        /// 其他名称
        /// </summary>
        [StringLength(100)]
        public string AnotherName { get; set; }
        /// <summary>
        /// 简称
        /// </summary>
        [StringLength(50)]
        public string ShortName { get; set; }
    }
}
