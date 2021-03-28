using MrMatch.Domain.EntityBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrMatch.Application.User.Oup
{
    public class BasicUserOup : EntityDto
    {
        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName { get; set; }    
        /// <summary>
        /// 头像
        /// </summary>
        public string AvatarUrl { get; set; }
        /// <summary>
        /// 审核状态
        /// </summary>
        public int ApproveStatus { get; set; }
        /// <summary>
        /// 上架状态
        /// </summary>
        public bool ActiveStatus { get; set; }
        /// <summary>
        /// 是否有简历快照
        /// </summary>
        public bool ProfileSnap { get; set; }
        /// <summary>
        /// 意向职能ID 用逗号隔开
        /// </summary>
        public string IntentionFunctionIDs { get; set; }
        /// <summary>
        /// 意向职能
        /// </summary>
        public string IntentionFunctionName { get; set; }
        /// <summary>
        /// 意向城市ID 用逗号隔开
        /// </summary>
        public string LocationIDs { get; set; }
        /// <summary>
        /// 意向城市
        /// </summary>
        public string LocationName { get; set; }
        /// <summary>
        /// 意向年薪
        /// </summary>
        public string AnnualSalary { get; set; }
    }
}
