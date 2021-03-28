namespace MrMatch.Domain.Models
{
    using MrMatch.Domain.EntityBase;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    /// <summary>
    /// 职位草稿表(字段注释见主表)
    /// </summary>
    public partial class TP_JobDraft : Entity
    {

        public long BasicCompanyID { get; set; }

        public long AccountID { get; set; }

        public long JobCompanyID { get; set; }

        [StringLength(50)]
        public string JobName { get; set; }

        public long FunctionID { get; set; }

        public long DistrictID { get; set; }

        [StringLength(30)]
        public string Reporter { get; set; }

        public int SubordinateCount { get; set; }

        public decimal MaxAnnualSalary { get; set; }
        public decimal MinAnnualSalary { get; set; }

        public bool SalaryOpen { get; set; }

        [StringLength(30)]
        public string SalaryComposition { get; set; }

        [StringLength(50)]
        public string SalaryCompositionExtra { get; set; }

        [StringLength(30)]
        public string SocialSecurity { get; set; }

        [StringLength(50)]
        public string SocialSecurityExtra { get; set; }

        [StringLength(30)]
        public string LiveWelfare { get; set; }

        [StringLength(50)]
        public string LiveWelfareExtra { get; set; }

        [StringLength(30)]
        public string AnnualLeave { get; set; }

        [StringLength(50)]
        public string AnnualLeaveExtra { get; set; }

        [StringLength(30)]
        public string Subsidy { get; set; }

        [StringLength(50)]
        public string SubsidyExtra { get; set; }

        [StringLength(30)]
        public string AgeRequirements { get; set; }

        [StringLength(30)]
        public string MajorRequirements { get; set; }

        public int WorkYears { get; set; }

        public int Degree { get; set; }

        public bool FullTime { get; set; }

        [StringLength(200)]
        public string Language { get; set; }

        [StringLength(200)]
        public string LanguageExtra { get; set; }

        [StringLength(200)]
        public string Skills { get; set; }

        [StringLength(200)]
        public string SkillsExtra { get; set; }

        [StringLength(3000)]
        public string JobDescription { get; set; }

        [StringLength(3000)]
        public string JobRequirements { get; set; }

        [StringLength(3000)]
        public string ExtraInfo { get; set; }

        public bool ActiveStatus { get; set; }

        public int EmergencyLevel { get; set; }

        public bool AgentJob { get; set; }
        public bool ShowPersonal { get; set; }
        public string ShowItems { get; set; }
    }
}
