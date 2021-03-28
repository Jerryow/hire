namespace MrMatch.Domain.Models
{
    using MrMatch.Domain.EntityBase;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    /// <summary>
    /// 猎头企业代理公司表
    /// </summary>
    public partial class TP_AgentCompany : Entity
    {
        /// <summary>
        /// 猎头企业ID
        /// </summary>
        public long BasicCompanyID { get; set; }
        /// <summary>
        /// 企业成员ID
        /// </summary>
        public long AccountID { get; set; }
        /// <summary>
        /// 企业名称
        /// </summary>
        [StringLength(100)]
        public string CompanyName { get; set; }
        /// <summary>
        /// 企业对外显示名称
        /// </summary>
        [StringLength(50)]
        public string ShortName { get; set; }
        /// <summary>
        /// 所在城市
        /// </summary>
        [StringLength(100)]
        public string City { get; set; }
        /// <summary>
        /// 企业类型
        /// </summary>
        public int CompnayType { get; set; }
        /// <summary>
        /// 企业规模
        /// </summary>
        public int EmployeeNum { get; set; }
        /// <summary>
        /// 简介
        /// </summary>
        [StringLength(1000)]
        public string Summary { get; set; }
        /// <summary>
        /// 介绍
        /// </summary>
        [StringLength(3000)]
        public string Introduce { get; set; }
    }
}
