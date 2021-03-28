using MrMatch.Domain.EntityBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrMatch.Application.System.Oup
{
    public class MessageConfigListOup : EntityDto
    {
        /// <summary>
        /// 类型 10:短信 20邮件
        /// </summary>
        public int ConfigType { get; set; }
        /// <summary>
        /// 供应商名称
        /// </summary>
        public string ProviderName { get; set; }
        /// <summary>
        /// 接口名称
        /// </summary>
        public string ApiName { get; set; }
        /// <summary>
        /// 接口地址
        /// </summary>
        public string ApiUrl { get; set; }
        /// <summary>
        /// 端口
        /// </summary>
        public int Port { get; set; }
        /// <summary>
        /// 发送签名
        /// </summary>
        public string SenderName { get; set; }
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsActivated { get; set; }
    }
}
