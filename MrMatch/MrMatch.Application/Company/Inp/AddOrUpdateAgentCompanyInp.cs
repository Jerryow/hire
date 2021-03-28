using MrMatch.Domain.EntityBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MrMatch.Common.AttributeHelper;

namespace MrMatch.Application.Company.Inp
{
    public class AddOrUpdateAgentCompanyInp : EntityDto
    {
        /// <summary>
        /// 企业名称
        /// </summary>
        [Validate("string","企业名称不能为空")]
        public string CompanyName { get; set; }
        /// <summary>
        /// 企业对外显示名称
        /// </summary>
        [Validate("string", "企业对外显示名称不能为空")]
        public string ShortName { get; set; }
        /// <summary>
        /// 所在城市
        /// </summary>
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
        public string Summary { get; set; }
        /// <summary>
        /// 介绍
        /// </summary>
        public string Introduce { get; set; }
    }
}
