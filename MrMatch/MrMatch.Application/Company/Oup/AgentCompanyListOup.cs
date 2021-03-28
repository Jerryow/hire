using MrMatch.Domain.EntityBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrMatch.Application.Company.Oup
{
    public class AgentCompanyListOup : EntityDto
    {
        
        /// <summary>
        /// 企业名称
        /// </summary>
        public string CompanyName { get; set; }
        /// <summary>
        /// 企业对外显示名称
        /// </summary>
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
        /// <summary>
        /// 职位数量
        /// </summary>
        public int JobNumber { get; set; }
        /// <summary>
        /// 最后修改时间
        /// </summary>
        public DateTime LastModifiedTime { get; set; }
    }
}
