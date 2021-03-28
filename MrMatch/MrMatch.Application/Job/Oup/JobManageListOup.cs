using MrMatch.Domain.EntityBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrMatch.Application.Job.Oup
{
    public class JobManageListOup : EntityDto
    {
        /// <summary>
        /// 职位名称
        /// </summary>
        public string JobName { get; set; }
        /// <summary>
        /// 职位所属企业ID
        /// </summary>
        public long AccountID { get; set; }
        /// <summary>
        /// 职位所属企业ID
        /// </summary>
        public long JobCompanyID { get; set; }
        /// <summary>
        /// 职位所属企业
        /// </summary>
        public string JobCompanyName { get; set; }
        /// <summary>
        /// 职位所属企业对外显示名称
        /// </summary>
        public string JobCompanyShortName { get; set; }
        /// <summary>
        /// 最后编辑时间
        /// </summary>
        public DateTime LastModifiedTime { get; set; }
        /// <summary>
        /// 是否猎头职位
        /// </summary>
        public bool AgentJob { get; set; }
        /// <summary>
        /// 投递数
        /// </summary>
        public int ApplyCount { get; set; }
    }
}
