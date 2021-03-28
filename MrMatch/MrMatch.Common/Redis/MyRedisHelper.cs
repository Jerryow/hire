using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrMatch.Common.Redis
{
    public class MyRedisHelper
    {
        public static PooledRedisClientManager instance;
        private static readonly object redisLock = new object();
        static MyRedisHelper()
        {
        }
        public static PooledRedisClientManager Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (redisLock)
                    {
                        if (instance == null)
                        {
                            InitClient();
                        }
                    }
                }
                return instance;
            }
        }
        public static void InitClient()
        {
            instance = new PooledRedisClientManager(
                                           new string[] { string.Format("{0}@{1}:{2}", "xxxxxx", "xxxxxxx", "6379") },
                                           new string[] { string.Format("{0}@{1}:{2}", "xxxxxx", "xxxxxxx", "6379") },
                                           new RedisClientManagerConfig
                                           {
                                               DefaultDb = 0,
                                               MaxReadPoolSize = 50,
                                               MaxWritePoolSize = 50,
                                               AutoStart = true
                                           },
                                           0,
                                           10000,
                                           10)
            {
                ConnectTimeout = 1000 * 60 * 20
            };
        }

        #region 方法

        #endregion
        public static T Get<T>(string key, int seconds, Func<T> func)
        {
            using (IRedisClient redisClient = Instance.GetClient())
            {
                T t = redisClient.Get<T>(key);
                if (t == null && func != null)
                {
                    t = func();
                    redisClient.Set<T>(key, t, DateTime.Now.AddSeconds(seconds));
                }
                return t;
            }

        }
        
        public static Boolean Set<T>(string key, int seconds, T t)
        {
            using (IRedisClient redisClient = Instance.GetClient())
            {
                return redisClient.Set<T>(key, t, DateTime.Now.AddSeconds(seconds));
            }
        }

        

        public static void ClearKey(string key)
        {
            using (IRedisClient redisClient = Instance.GetClient())
            {
                IEnumerable<string> keys = redisClient.GetAllKeys(); ;
                foreach (var item in keys)
                {
                    if (item.Contains(key))
                    {
                        redisClient.Remove(item);
                    }
                }
            }

        }
    }
}
