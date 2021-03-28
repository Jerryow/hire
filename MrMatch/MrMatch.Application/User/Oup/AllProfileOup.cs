using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrMatch.Application.User.Oup
{
    public class AllProfileOup
    {
        /// <summary>
        /// 基本信息
        /// </summary>
        public ProfileOup ProfileInfo { get; set; }
        /// <summary>
        /// 工作经验列表
        /// </summary>
        public List<WorkExperienceOup> WorkExList { get; set; }
        /// <summary>
        /// 教育经验列表
        /// </summary>
        public List<EducationOup> EducationList { get; set; }
        /// <summary>
        /// 屏蔽列表
        /// </summary>
        public List<AvoidOup> AvoidList { get; set; }
        /// <summary>
        /// 求职意向
        /// </summary>
        public JobIntentionOup JobIntention { get; set; }
        /// <summary>
        /// 用户标签列表
        /// </summary>
        public List<UserTagsOup> UserTagsList { get; set; }
        /// <summary>
        /// 审核状态->公开枚举
        /// </summary>
        public int ApproveStatus { get; set; }
        /// <summary>
        /// 上/下架
        /// </summary>
        public bool ActiveStatus { get; set; }
        /// <summary>
        /// 是否有快照
        /// </summary>
        public bool ProfileSnap { get; set; }
        /// <summary>
        /// 获取企业活跃信息
        /// </summary>
        public int CompanyCount { get; set; }
    }
}
