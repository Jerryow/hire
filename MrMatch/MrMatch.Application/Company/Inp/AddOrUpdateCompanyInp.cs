using MrMatch.Common.AttributeHelper;
using MrMatch.Domain.EntityBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrMatch.Application.Company.Inp
{
    public class AddOrUpdateCompanyInp : EntityDto
    {
        /// <summary>
        /// 企业名称
        /// </summary>
        [Validate("string", "企业名称不能为空")]
        public string CompanyName { get; set; }
        /// <summary>
        /// 对外显示名称
        /// </summary>
        [Validate("string", "对外显示名称不能为空")]
        public string ShortName { get; set; }
        /// <summary>
        /// 企业类型
        /// </summary>
        [Validate("int", "请选择企业类型")]
        public int CompnayType { get; set; }
        /// <summary>
        /// 融资阶段
        /// </summary>
        [Validate("int", "请选择融资阶段")]
        public int FinancingStatus { get; set; }
        /// <summary>
        /// 官网
        /// </summary>
        [Validate("string", "官网不能为空")]
        public string Website { get; set; }
        /// <summary>
        /// 企业规模
        /// </summary>
        [Validate("int", "请选择企业规模")]
        public int EmployeeNum { get; set; }
        /// <summary>
        /// 注册资金
        /// </summary>
        public int RegisteredCapital { get; set; }
        /// <summary>
        /// 简介
        /// </summary>
        public string Summary { get; set; }
        /// <summary>
        /// 所在地(上海，北京)
        /// </summary>
        [Validate("string", "所在城市不能为空")]
        public string Location { get; set; }
        /// <summary>
        /// 详细地址
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 介绍
        /// </summary>
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
        [Validate("string", "请上传企业证书")]
        public string LicenseImageUrl { get; set; }
        /// <summary>
        /// 证书认证状态
        /// </summary>
        public int LicenseAuthStatus { get; set; }
        /// <summary>
        /// 企业Logo图片地址
        /// </summary>
        [Validate("string", "请上传企业Logo")]
        public string LogoImageUrl { get; set; }
        /// <summary>
        /// 企业紧急联系人姓名
        /// </summary>
        [Validate("string", "企业紧急联系人姓名不能为空")]
        public string ContactName { get; set; }
        /// <summary>
        /// 紧急联系人电话
        /// </summary>
        [Validate("string", "紧急联系人电话不能为空")]
        public string CellPhone { get; set; }
        /// <summary>
        /// 紧急联系人邮箱
        /// </summary>
        [Validate("string", "紧急联系人邮箱不能为空")]
        public string Email { get; set; }
    }
}
