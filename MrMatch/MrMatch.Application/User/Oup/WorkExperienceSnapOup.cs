using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrMatch.Application.User.Oup
{
    public class WorkExperienceSnapOup
    {
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StartDate { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public string ExpirationDate { get; set; }
        /// <summary>
        /// 企业名称
        /// </summary>
        public string CompanyName { get; set; }
        /// <summary>
        /// 头衔
        /// </summary>
        public string Position { get; set; }
        /// <summary>
        /// 工作介绍
        /// </summary>
        public string Introduction { get; set; }
    }
}
