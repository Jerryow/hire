namespace MrMatch.Domain.Models
{
    using MrMatch.Domain.EntityBase;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    /// <summary>
    /// 职位主表
    /// </summary>
    public partial class TP_Job : Entity
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
        [StringLength(50)]
        public string JobName { get; set; }
        /// <summary>
        /// 汇报对象
        /// </summary>
        [StringLength(30)]
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
        [StringLength(30)]
        public string SalaryComposition { get; set; }
        /// <summary>
        /// 薪资构成-其他
        /// </summary>
        [StringLength(50)]
        public string SalaryCompositionExtra { get; set; }
        /// <summary>
        /// 社保福利
        /// </summary>
        [StringLength(30)]
        public string SocialSecurity { get; set; }
        /// <summary>
        /// 社保福利-其他
        /// </summary>
        [StringLength(50)]
        public string SocialSecurityExtra { get; set; }
        /// <summary>
        /// 居住福利
        /// </summary>
        [StringLength(30)]
        public string LiveWelfare { get; set; }
        /// <summary>
        /// 居住福利-其他
        /// </summary>
        [StringLength(50)]
        public string LiveWelfareExtra { get; set; }
        /// <summary>
        /// 年假福利
        /// </summary>
        [StringLength(30)]
        public string AnnualLeave { get; set; }
        /// <summary>
        /// 年假福利-其他
        /// </summary>
        [StringLength(50)]
        public string AnnualLeaveExtra { get; set; }
        /// <summary>
        /// 通讯补助
        /// </summary>
        [StringLength(30)]
        public string Subsidy { get; set; }
        /// <summary>
        /// 通讯补助-其他
        /// </summary>
        [StringLength(50)]
        public string SubsidyExtra { get; set; }
        /// <summary>
        /// 年龄要求
        /// </summary>
        [StringLength(30)]
        public string AgeRequirements { get; set; }
        /// <summary>
        /// 专业要求
        /// </summary>
        [StringLength(30)]
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
        [StringLength(200)]
        public string Language { get; set; }
        /// <summary>
        /// 语言要求-其他
        /// </summary>
        [StringLength(200)]
        public string LanguageExtra { get; set; }
        /// <summary>
        /// 技能要求
        /// </summary>
        [StringLength(500)]
        public string Skills { get; set; }
        /// <summary>
        /// 技能要求-其他
        /// </summary>
        [StringLength(500)]
        public string SkillsExtra { get; set; }
        /// <summary>
        /// 职位描述
        /// </summary>
        [StringLength(3000)]
        public string JobDescription { get; set; }
        /// <summary>
        /// 岗位要求
        /// </summary>
        [StringLength(3000)]
        public string JobRequirements { get; set; }
        /// <summary>
        /// 补充说明
        /// </summary>
        [StringLength(3000)]
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
        /// 搜索量
        /// </summary>
        public int SearchCount { get; set; }
        /// <summary>
        /// 小程序码
        /// </summary>
        [StringLength(200)]
        public string WechatMiniPic { get; set; }
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
