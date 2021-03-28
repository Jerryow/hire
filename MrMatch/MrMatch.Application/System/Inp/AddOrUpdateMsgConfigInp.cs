using MrMatch.Domain.EntityBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrMatch.Application.System.Inp
{
    public class AddOrUpdateMsgConfigInp : EntityDto
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
        /// 接口编码
        /// </summary>
        public string ApiCode { get; set; }
        /// <summary>
        /// 接口地址
        /// </summary>
        public string ApiUrl { get; set; }
        /// <summary>
        /// 端口
        /// </summary>
        public int Port { get; set; }
        /// <summary>
        /// 是否启用ssl（默认false）
        /// </summary>
        public bool EnableSsl { get; set; }
        /// <summary>
        /// 发送签名
        /// </summary>
        public string SenderName { get; set; }
        /// <summary>
        /// 登录名
        /// </summary>
        public string LoginName { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// 验证密钥
        /// </summary>
        public string VerifyKey { get; set; }
        /// <summary>
        /// 签名
        /// </summary>
        public string SignMark { get; set; }
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsActivated { get; set; }
        /// <summary>
        /// 次数限制
        /// </summary>
        public int TimesLimit { get; set; }
    }
}
