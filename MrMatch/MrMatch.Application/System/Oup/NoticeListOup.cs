using MrMatch.Domain.EntityBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrMatch.Application.System.Oup
{
    public class NoticeListOup : EntityDto
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
        /// 接收端,1:B端 2:C端
        /// </summary>
        public int SendClient { get; set; }
        /// <summary>
        /// 是否发送
        /// </summary>
        public bool IsFinish { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedTime { get; set; }
    }
}
