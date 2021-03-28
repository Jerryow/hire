using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MrMatch.Common.AttributeHelper;

namespace MrMatch.Application.Company.Inp
{
    public class RegistCompanyInp
    {
        public int CompanyID { get; set; }
        [Validate("string","企业名称不能为空")]
        public string CompanyName { get; set; }
        [Validate("string", "职位不能为空")]
        public string Position { get; set; }
        [Validate("string", "姓名不能为空")]
        public string AccountName { get; set; }

        public bool AgentOrNot { get; set; }
    }
}
