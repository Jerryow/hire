using MrMatch.Domain.EntityBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrMatch.Application.User.Inp
{
    public class AddOrUpdateWechatMessageInp : EntityDto
    {
        /// <summary>
        /// 接收者ID
        /// </summary>
        public long RecieverID { get; set; }
        /// <summary>
        /// 消息主题
        /// </summary>
        public string MessageSubject { get; set; }
        /// <summary>
        /// 消息内容
        /// </summary>
        public string MessageContent { get; set; }
        /// <summary>
        /// 发送者ID
        /// </summary>
        public long SenderID { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 是否已发送
        /// </summary>
        public bool IsSent { get; set; }
    }
}
