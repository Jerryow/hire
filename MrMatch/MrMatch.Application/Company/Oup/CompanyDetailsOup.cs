using MrMatch.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrMatch.Application.Company.Oup
{
    public class CompanyDetailsOup
    {
        /// <summary>
        /// 企业名称
        /// </summary>
        public string CompanyName { get; set; }
        /// <summary>
        /// 对外显示名称
        /// </summary>
        public string ShortName { get; set; }
        /// <summary>
        /// 企业类型
        /// </summary>
        public int CompnayType { get; set; }
        /// <summary>
        /// 官网
        /// </summary>
        public string Website { get; set; }
        /// <summary>
        /// 企业规模
        /// </summary>
        public int EmployeeNum { get; set; }
        /// <summary>
        /// 注册资金
        /// </summary>
        public string RegisteredCapital { get; set; }
        /// <summary>
        /// 简介
        /// </summary>
        public string Summary { get; set; }
        /// <summary>
        /// 所在地(上海，北京)
        /// </summary>
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
        public string CheckedStatus { get; set; }
        /// <summary>
        /// 企业紧急联系人姓名
        /// </summary>
        public string ContactName { get; set; }
        /// <summary>
        /// 紧急联系人电话
        /// </summary>
        public string CellPhone { get; set; }
        /// <summary>
        /// 紧急联系人邮箱
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// 是否猎头企业
        /// </summary>
        public bool AgentOrNot { get; set; }
        /// <summary>
        /// Logo
        /// </summary>
        public string LogoImageUrl { get; set; }
        /// <summary>
        /// Lisence
        /// </summary>
        public string LicenseImageUrl { get; set; }
        /// <summary>
        /// 证书认证状态
        /// </summary>
        public int LicenseAuthStatus { get; set; }
        /// <summary>
        /// 企业成员信息
        /// </summary>
        public List<AccountOup> AccountInfo { get; set; }
        /// <summary>
        /// 签约信息
        /// </summary>
        public List<TP_CompanyContract> ContractInfo { get; set; }
    }
}
