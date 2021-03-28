using MrMatch.Domain.EntityBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrMatch.Application.Job.Oup
{
    public class JobDraftDetailsOup : EntityDto
    {
        public long BasicCompanyID { get; set; }

        public long AccountID { get; set; }

        public long JobCompanyID { get; set; }
        public string JobName { get; set; }

        public long FunctionID { get; set; }

        public long DistrictID { get; set; }
        public string Reporter { get; set; }

        public int SubordinateCount { get; set; }

        public decimal MaxAnnualSalary { get; set; }
        public decimal MinAnnualSalary { get; set; }

        public bool SalaryOpen { get; set; }
        public string SalaryComposition { get; set; }
        public string SalaryCompositionExtra { get; set; }
        public string SocialSecurity { get; set; }
        public string SocialSecurityExtra { get; set; }
        public string LiveWelfare { get; set; }
        public string LiveWelfareExtra { get; set; }
        public string AnnualLeave { get; set; }
        public string AnnualLeaveExtra { get; set; }
        public string Subsidy { get; set; }
        public string SubsidyExtra { get; set; }
        public string AgeRequirements { get; set; }
        public string MajorRequirements { get; set; }

        public int WorkYears { get; set; }

        public int Degree { get; set; }

        public bool FullTime { get; set; }
        public string Language { get; set; }
        public string LanguageExtra { get; set; }

        public string Skills { get; set; }
        public string SkillsExtra { get; set; }
        public string JobDescription { get; set; }
        public string JobRequirements { get; set; }
        public string ExtraInfo { get; set; }

        public bool ActiveStatus { get; set; }

        public bool AgentJob { get; set; }
        public bool ShowPersonal { get; set; }
        public string ShowItems { get; set; }
        public DateTime LastModifiedTime { get; set; }
    }
}
