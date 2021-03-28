using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrMatch.Common.Redis
{
    /// <summary>
    /// redis配置文件信息
    /// </summary>
    public sealed class RedisConfigInfo
    {
        public RedisConfigInfo()
        {
            this.Port = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["RedisPort"]);
            this.Host = System.Configuration.ConfigurationManager.AppSettings["RedisHost"];
            this.ServerPassword = System.Configuration.ConfigurationManager.AppSettings["RedisPassword"];
            this.WriteServerList = this.ServerPassword + "@" + this.Host + ":" + this.Port;
            this.ReadServerList = this.ServerPassword + "@" + this.Host + ":" + this.Port;
        }
        /// <summary>
        /// 可写的Redis链接地址
        /// format:ip1,ip2
        /// </summary>
        public string WriteServerList = "";
        /// <summary>
        /// 可读的Redis链接地址
        /// format:ip1,ip2
        /// </summary>
        public string ReadServerList = "";
        /// <summary>
        /// Redis的IP
        /// format:ip1,ip2
        /// </summary>
        public string Host = System.Configuration.ConfigurationManager.AppSettings["RedisHost"];
        /// <summary>
        /// Redis的端口
        /// format:ip1,ip2
        /// </summary>
        public int Port = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["RedisPort"]);
        /// <summary>
        /// Redis的密码
        /// </summary>
        public string ServerPassword = System.Configuration.ConfigurationManager.AppSettings["RedisPassword"];
        /// <summary>
        /// 最大写链接数
        /// </summary>
        public static int MaxWritePoolSize = 50;
        /// <summary>
        /// 最大读链接数
        /// </summary>
        public static int MaxReadPoolSize = 20;
        /// <summary>
        /// 本地缓存到期时间，单位:秒
        /// </summary>
        public int LocalCacheTime = 180;
        /// <summary>
        /// 自动重启
        /// </summary>
        public bool AutoStart = true;
        /// <summary>
        /// 是否记录日志,该设置仅用于排查redis运行时出现的问题,
        /// 如redis工作正常,请关闭该项
        /// </summary>
        //public bool RecordeLog = false;
    }

}
