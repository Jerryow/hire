using MrMatch.Domain.EntityBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrMatch.Application.User.Oup
{
    public class JobIntentionOup : EntityDto
    {
        /// <summary>
        /// 过往资历ID 用逗号隔开
        /// </summary>
        public string FunctionIDs { get; set; }
        /// <summary>
        /// 意向城市ID 用逗号隔开
        /// </summary>
        public string LocationIDs { get; set; }
        /// <summary>
        /// 年薪
        /// </summary>
        public decimal AnnualSalary { get; set; }
        /// <summary>
        /// 中文职能
        /// </summary>
        public string Functions { get; set; }
        /// <summary>
        /// 中文地点
        /// </summary>
        public string Locations { get; set; }
    }
}
