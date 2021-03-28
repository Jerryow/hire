using MrMatch.Domain.EntityBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrMatch.Application.Job.Oup
{
    public class JobListOup : EntityDto
    {
        /// <summary>
        /// 所在城市ID
        /// </summary>
        public long DistrictID { get; set; }
        /// <summary>
        /// 登陆账户企业ID
        /// </summary>
        public long BasicCompanyID { get; set; }
        /// <summary>
        /// 职位所属企业ID
        /// </summary>
        public long JobCompanyID { get; set; }
        /// <summary>
        /// 职位名称
        /// </summary>
        public string JobName { get; set; }
        /// <summary>
        /// 税前年薪
        /// </summary>
        public string MaxAnnualSalary { get; set; }
        /// <summary>
        /// 税前年薪
        /// </summary>
        public string MinAnnualSalary { get; set; }
        /// <summary>
        /// 薪资是否公开
        /// </summary>
        public bool SalaryOpen { get; set; }
        /// <summary>
        /// 上下架
        /// </summary>
        public bool ActiveStatus { get; set; }
        /// <summary>
        /// 是否猎头岗位
        /// </summary>
        public bool AgentJob { get; set; }
        /// <summary>
        /// 对外显示名称
        /// </summary>
        public string JobCompanyShortName { get; set; }
        /// <summary>
        /// 所在城市
        /// </summary>
        public string DistrictName { get; set; }
        /// <summary>
        /// logo
        /// </summary>
        public string LogoUrl { get; set; }
        /// <summary>
        /// 是否收藏
        /// </summary>
        public bool IsFollow { get; set; }
        /// <summary>
        /// 是否已投递
        /// </summary>
        public bool IsApply { get; set; }
    }
}
