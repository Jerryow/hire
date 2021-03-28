using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrMatch.Common.ImageHelper
{
    public class AliyunOssConfig
    {
        /// <summary>
        /// 地址
        /// </summary>
        public string Endpoint { get; set; }
        /// <summary>
        /// 密钥id
        /// </summary>
        public string AccessKeyId { get; set; }
        /// <summary>
        /// 密钥密码
        /// </summary>
        public string AccessKeySecret { get; set; }
        /// <summary>
        /// 存储对象名称
        /// </summary>
        public string BucketName { get; set; }
        /// <summary>
        /// 命中前缀
        /// </summary>
        public string Prefix { get; set; }
        /// <summary>
        /// 命中从哪读起
        /// </summary>
        public string Marker { get; set; }
    }
}
