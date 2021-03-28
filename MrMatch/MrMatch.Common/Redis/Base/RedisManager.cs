using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrMatch.Common.Redis
{
    public class RedisManager
    {
        /// <summary>
        /// redis配置文件信息
        /// </summary>
        //private static RedisConfigInfo redisConfigInfo = new RedisConfigInfo();

        //private static PooledRedisClientManager prcManager;
        //private static RedisClient redisClient;
        //private static readonly object redisLock = new object();

        /// <summary>
        /// 静态构造方法，初始化链接池管理对象
        /// </summary>
        //private RedisManager()
        //{
        //    CreateManager();
        //}

        /// <summary>
        /// 创建链接池管理对象// 创建redis对象
        /// </summary>
        //private static void CreateManager()
        //{
        //    string[] WriteServerConStr = redisConfigInfo.WriteServerList.Split(',');
        //    string[] ReadServerConStr = redisConfigInfo.ReadServerList.Split(',');
        //    prcManager = new PooledRedisClientManager(ReadServerConStr, WriteServerConStr,
        //                     new RedisClientManagerConfig
        //                     {
        //                         MaxWritePoolSize = RedisConfigInfo.MaxWritePoolSize,
        //                         MaxReadPoolSize = RedisConfigInfo.MaxReadPoolSize,
        //                         AutoStart = true,
        //                         DefaultDb = 0
        //                     }, 0, 10000, 10);
        //    //redisClient = new RedisClient(redisConfigInfo.Host, redisConfigInfo.Port, redisConfigInfo.ServerPassword);
        //}

        /// <summary>
        /// 客户端缓存操作对象
        /// </summary>
        //public static RedisClient GetClient()
        //{
        //    //return prcManager.GetClient();
        //    if (prcManager == null)
        //    {
        //        lock (redisLock)
        //        {
        //            if (prcManager == null)
        //            {
        //                CreateManager();
        //            }
        //        }
        //    }
        //    return prcManager.GetClient() as RedisClient;
        //}
    }
}
