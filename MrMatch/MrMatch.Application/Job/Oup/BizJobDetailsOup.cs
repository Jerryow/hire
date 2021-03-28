using MrMatch.Application.Company.Oup;
using MrMatch.Domain.EntityBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrMatch.Application.Job.Oup
{
    public class BizJobDetailsOup : EntityDto
    {
        /// <summary>
        /// 所在城市ID
        /// </summary>
        public long DistrictID { get; set; }
        /// <summary>
        /// 职能ID
        /// </summary>
        public long FunctionID { get; set; }
        /// <summary>
        /// 登陆账户企业ID
        /// </summary>
        public long BasicCompanyID { get; set; }
        /// <summary>
        /// 企业成员ID
        /// </summary>
        public long AccountID { get; set; }
        /// <summary>
        /// 职位所属企业ID
        /// </summary>
        public long JobCompanyID { get; set; }
        /// <summary>
        /// 职位名称
        /// </summary>
        public string JobName { get; set; }
        /// <summary>
        /// 汇报对象
        /// </summary>
        public string Reporter { get; set; }
        /// <summary>
        /// 下属人数
        /// </summary>
        public int SubordinateCount { get; set; }
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
        /// 薪资构成
        /// </summary>
        public string SalaryComposition { get; set; }
        /// <summary>
        /// 薪资构成-其他
        /// </summary>
        public string SalaryCompositionExtra { get; set; }
        /// <summary>
        /// 社保福利
        /// </summary>
        public string SocialSecurity { get; set; }
        /// <summary>
        /// 社保福利-其他
        /// </summary>
        public string SocialSecurityExtra { get; set; }
        /// <summary>
        /// 居住福利
        /// </summary>
        public string LiveWelfare { get; set; }
        /// <summary>
        /// 居住福利-其他
        /// </summary>
        public string LiveWelfareExtra { get; set; }
        /// <summary>
        /// 年假福利
        /// </summary>
        public string AnnualLeave { get; set; }
        /// <summary>
        /// 年假福利-其他
        /// </summary>
        public string AnnualLeaveExtra { get; set; }
        /// <summary>
        /// 通讯补助
        /// </summary>
        public string Subsidy { get; set; }
        /// <summary>
        /// 通讯补助-其他
        /// </summary>
        public string SubsidyExtra { get; set; }
        /// <summary>
        /// 年龄要求
        /// </summary>
        public string AgeRequirements { get; set; }
        /// <summary>
        /// 专业要求
        /// </summary>
        public string MajorRequirements { get; set; }
        /// <summary>
        /// 工作年限
        /// </summary>
        public int WorkYears { get; set; }
        /// <summary>
        /// 学历
        /// </summary>
        public int Degree { get; set; }
        /// <summary>
        /// 是否统招全日制
        /// </summary>
        public bool FullTime { get; set; }
        /// <summary>
        /// 语言要求
        /// </summary>
        public string Language { get; set; }
        /// <summary>
        /// 语言要求-其他
        /// </summary>
        public string LanguageExtra { get; set; }
        /// <summary>
        /// 技能要求
        /// </summary>
        public string Skills { get; set; }
        /// <summary>
        /// 技能要求-其他
        /// </summary>
        public string SkillsExtra { get; set; }
        /// <summary>
        /// 职位描述
        /// </summary>
        public string JobDescription { get; set; }
        /// <summary>
        /// 岗位要求
        /// </summary>
        public string JobRequirements { get; set; }
        /// <summary>
        /// 补充说明
        /// </summary>
        public string ExtraInfo { get; set; }
        /// <summary>
        /// 上下架
        /// </summary>
        public bool ActiveStatus { get; set; }
        /// <summary>
        /// 紧急程度
        /// </summary>
        public int EmergencyLevel { get; set; }
        /// <summary>
        /// 是否猎头岗位
        /// </summary>
        public bool AgentJob { get; set; }
        /// <summary>
        /// 是否展示个人信息
        /// </summary>
        public bool ShowPersonal { get; set; }
        /// <summary>
        /// 个人信息展示项
        /// </summary>
        public string ShowItems { get; set; }
        /// <summary>
        /// 最后修改时间
        /// </summary>
        public DateTime LastModifiedTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string CompanyName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string CompanyShortName { get; set; }
    }
}
