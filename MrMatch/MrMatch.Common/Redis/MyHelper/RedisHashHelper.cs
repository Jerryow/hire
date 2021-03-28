using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrMatch.Common.Redis.MyHelper
{
    public class RedisHashHelper
    {
        #region Get
        /// <summary>
        /// 获取所有hash的key
        /// </summary>
        /// <param name="hashID"></param>
        /// <returns></returns>
        public static List<string> GetHashKeys(string hashID)
        {
            using (IRedisClient redisClient = MyRedisHelper.Instance.GetClient())
            {
                var t = redisClient.GetHashKeys(hashID);
                return t;
            }

        }

        /// <summary>
        /// 获取所有hash的value
        /// </summary>
        /// <param name="hashID"></param>
        /// <returns></returns>
        public static List<string> GetHashValues(string hashID)
        {
            using (IRedisClient redisClient = MyRedisHelper.Instance.GetClient())
            {
                var t = redisClient.GetHashValues(hashID);
                return t;
            }

        }

        /// <summary>
        /// 获取hash的指定值的value
        /// </summary>
        /// <param name="hashID"></param>
        /// <param name="field"></param>
        /// <returns></returns>
        public static string GetHashValue(string hashID, string field)
        {
            using (IRedisClient redisClient = MyRedisHelper.Instance.GetClient())
            {
                var t = redisClient.GetValueFromHash(hashID, field);
                return t;
            }

        }
        #endregion

        #region Set
        /// <summary>
        /// 获取hash的指定值的value
        /// </summary>
        /// <param name="hashID"></param>
        /// <param name="field"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public static void SetHashValue(string hashID, string field, string val)
        {
            using (IRedisClient redisClient = MyRedisHelper.Instance.GetClient())
            {
                redisClient.SetEntryInHash(hashID, field, val);
            }
        }
        #endregion
    }
}
