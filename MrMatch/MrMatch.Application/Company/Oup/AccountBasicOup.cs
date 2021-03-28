using MrMatch.Domain.EntityBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrMatch.Application.Company.Oup
{
    public class AccountBasicOup : EntityDto
    {
        /// <summary>
        /// 企业ID
        /// </summary>
        public string CompanyName { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        public string CellPhone { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string AccountName { get; set; }
        /// <summary>
        /// 职位
        /// </summary>
        public string Position { get; set; }
        /// <summary>
        /// 是否管理员
        /// </summary>
        public bool IsAdmin { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedTime { get; set; }
        /// <summary>
        /// 头像
        /// </summary>
        public string AvatarUrl { get; set; }
        /// <summary>
        /// 微信号
        /// </summary>
        public string WechatAccount { get; set; }
        /// <summary>
        /// 微信二维码
        /// </summary>
        public string WechatContactUrl { get; set; }
        /// <summary>
        /// 领英链接
        /// </summary>
        public string LinkinUrl { get; set; }
        /// <summary>
        /// 专注领域
        /// </summary>
        public string FocusArea { get; set; }
        /// <summary>
        /// 个人介绍
        /// </summary>
        public string Introduction { get; set; }
    }
}
