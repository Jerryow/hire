namespace MrMatch.Domain.Models
{
    using MrMatch.Domain.EntityBase;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    /// <summary>
    /// 企业主表
    /// </summary>
    public partial class TP_Company : Entity
    {
        /// <summary>
        /// 企业名称
        /// </summary>
        [StringLength(100)]
        public string CompanyName { get; set; }
        /// <summary>
        /// 对外显示名称
        /// </summary>
        [StringLength(50)]
        public string ShortName { get; set; }
        /// <summary>
        /// 企业类型
        /// </summary>
        public int CompnayType { get; set; }
        /// <summary>
        /// 融资阶段
        /// </summary>
        public int FinancingStatus { get; set; }
        /// <summary>
        /// 官网
        /// </summary>
        [StringLength(200)]
        public string Website { get; set; }
        /// <summary>
        /// 企业规模
        /// </summary>
        public int EmployeeNum { get; set; }
        /// <summary>
        /// 注册资金
        /// </summary>
        public int RegisteredCapital { get; set; }
        /// <summary>
        /// 简介
        /// </summary>
        [StringLength(200)]
        public string Summary { get; set; }
        /// <summary>
        /// 所在地(上海，北京)
        /// </summary>
        [StringLength(200)]
        public string Location { get; set; }
        /// <summary>
        /// 详细地址
        /// </summary>
        [StringLength(500)]
        public string Address { get; set; }
        /// <summary>
        /// 介绍
        /// </summary>
        [StringLength(1000)]
        public string Introduce { get; set; }
        /// <summary>
        /// 审核状态
        /// </summary>
        public int CheckedStatus { get; set; }
        /// <summary>
        /// 是否猎头企业
        /// </summary>
        public bool AgentOrNot { get; set; }
        /// <summary>
        /// 企业证书图片地址
        /// </summary>
        [StringLength(200)]
        public string LicenseImageUrl { get; set; }
        /// <summary>
        /// 证书认证状态
        /// </summary>
        public int LicenseAuthStatus { get; set; }
        /// <summary>
        /// 企业Logo图片地址
        /// </summary>
        [StringLength(200)]
        public string LogoImageUrl { get; set; }
        /// <summary>
        /// 企业紧急联系人姓名
        /// </summary>
        [StringLength(50)]
        public string ContactName { get; set; }
        /// <summary>
        /// 紧急联系人电话
        /// </summary>
        [StringLength(50)]
        public string CellPhone { get; set; }
        /// <summary>
        /// 紧急联系人邮箱
        /// </summary>
        [StringLength(100)]
        public string Email { get; set; }
    }
}
