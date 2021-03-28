using MrMatch.Domain.EntityBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrMatch.Application.System.Inp
{
    public class AddOrUpdateNoticeInp : EntityDto
    {
        /// <summary>
        /// 消息主题
        /// </summary>
        public string MessageSubject { get; set; }
        /// <summary>
        /// 消息内容
        /// </summary>
        public string MessageContent { get; set; }
        /// <summary>
        /// 消息类型 1:所有用户  2:指定用户
        /// </summary>
        public int MessageType { get; set; }
        /// <summary>
        /// 接收者,0所有用户/具体的用户ID
        /// </summary>
        public string UserIDs { get; set; }
        /// <summary>
        /// 接收端,1:B端 2:C端
        /// </summary>
        public int SendClient { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }
}
