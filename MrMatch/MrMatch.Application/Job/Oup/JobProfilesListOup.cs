using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrMatch.Application.Job.Oup
{
    public class JobProfilesListOup
    {
        /// <summary>
        /// 候选人ID
        /// </summary>
        public long UserID { get; set; }
        /// <summary>
        /// 真实姓名
        /// </summary>
        public string RealName { get; set; }
        /// <summary>
        /// 性别 1男 2女
        /// </summary>
        public int Gender { get; set; }
        /// <summary>
        /// 工作年限
        /// </summary>
        public int WorkYears { get; set; }
        /// <summary>
        /// 当前企业
        /// </summary>
        public string CurrentCompany { get; set; }
        /// <summary>
        /// 当前职位
        /// </summary>
        public string CurrentPosition { get; set; }
        /// <summary>
        /// 投递时间
        /// </summary>
        public DateTime ApplyTime { get; set; }
    }
}
