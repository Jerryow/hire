using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MrMatch.Common.AttributeHelper;
using MrMatch.Domain.EntityBase;

namespace MrMatch.Application.Company.Inp
{
    public class UpdateCompanyInp : EntityDto
    {
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
        [Validate("string", 0, 60, "简介不能为空")]
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
