using MrMatch.Domain.EntityBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MrMatch.Common.AttributeHelper;

namespace MrMatch.Application.Job.Inp
{
    public class JobPublishInp : EntityDto
    {
        /// <summary>
        /// 所在城市ID
        /// </summary>
        [Validate("long","请选择所在城市")]
        public long DistrictID { get; set; }
        /// <summary>
        /// 职能ID
        /// </summary>
        [Validate("long", "请选择职能")]
        public long FunctionID { get; set; }
        /// <summary>
        /// 登陆账户企业ID
        /// </summary>
        [Validate("long", "企业信息不匹配")]
        public long BasicCompanyID { get; set; }
        /// <summary>
        /// 企业成员ID
        /// </summary>
        [Validate("long", "企业信息不匹配")]
        public long AccountID { get; set; }
        /// <summary>
        /// 职位所属企业ID
        /// </summary>
        [Validate("long", "企业信息不匹配")]
        public long JobCompanyID { get; set; }
        /// <summary>
        /// 职位名称
        /// </summary>
        [Validate("string", "职位名称不能为空")]
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
        /// 税前年薪下限
        /// </summary>
        public decimal MinAnnualSalary { get; set; }
        /// <summary>
        /// 税前年薪上限
        /// </summary>
        public decimal MaxAnnualSalary { get; set; }
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
        [Validate("int", "工作年限不能为空")]
        public int WorkYears { get; set; }
        /// <summary>
        /// 学历
        /// </summary>
        [Validate("int", "请选择学历")]
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
        [Validate("string", "职位描述不能为空")]
        public string JobDescription { get; set; }
        /// <summary>
        /// 岗位要求
        /// </summary>
        [Validate("string", "岗位要求不能为空")]
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
    }
}
