namespace MrMatch.Domain.Models
{
    using MrMatch.Domain.EntityBase;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    /// <summary>
    /// 地区表
    /// </summary>
    public partial class TP_District : Entity
    {
        /// <summary>
        /// 国家ID
        /// </summary>
        public long CountryID { get; set; }
        /// <summary>
        /// 地区名称
        /// </summary>
        [StringLength(50)]
        public string DistrictName { get; set; }
        /// <summary>
        /// 父ID
        /// </summary>
        public long ParentID { get; set; }
        /// <summary>
        /// 首字母
        /// </summary>
        [StringLength(10)]
        public string Initial { get; set; }
        /// <summary>
        /// 缩写
        /// </summary>
        [StringLength(50)]
        public string Initials { get; set; }
        /// <summary>
        /// 拼音
        /// </summary>
        [StringLength(200)]
        public string Pinyin { get; set; }
        /// <summary>
        /// 附加信息
        /// </summary>
        [StringLength(50)]
        public string Extra { get; set; }
        /// <summary>
        /// 后缀
        /// </summary>
        [StringLength(50)]
        public string Suffix { get; set; }
        /// <summary>
        /// 邮政编码
        /// </summary>
        [StringLength(50)]
        public string PostCode { get; set; }
        /// <summary>
        /// 地区编码
        /// </summary>
        [StringLength(50)]
        public string AreaCode { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int OrderNum { get; set; }
        /// <summary>
        /// 是否显示(做职位的可检索地区)
        /// </summary>
        public bool OnShow { get; set; }
        /// <summary>
        /// 是否热门城市
        /// </summary>
        public bool HotCity { get; set; }
    }
}
