using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrMatch.Common.Redis.MyHelper
{
    public static class RedisStringHelper
    {
        #region Get
        public static string Get(string key)
        {
            using (IRedisClient redisClient = MyRedisHelper.Instance.GetClient())
            {
                var t = redisClient.GetValue(key);
                return t;
            }

        }
        #endregion

        #region Set
        public static void Set(string key, string t)
        {
            using (IRedisClient redisClient = MyRedisHelper.Instance.GetClient())
            {
                redisClient.SetValue(key, t);
            }
        }
        public static void Set(string key, string t, DateTime dt)
        {
            using (IRedisClient redisClient = MyRedisHelper.Instance.GetClient())
            {
                var timeSpan = dt.Subtract(DateTime.Now);
                redisClient.SetValue(key, t, timeSpan);
            }
        }
        #endregion

        #region clear
        public static void Clear(string key)
        {
            using (IRedisClient redisClient = MyRedisHelper.Instance.GetClient())
            {
                redisClient.Remove(key);
            }
        }
        #endregion
    }
}
