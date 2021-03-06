
using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrMatch.Common.Redis
{


    //******************注意：所有的set方法不要使用Set<T>这个,因为底层会重新序列化一下.请使用setvalue
    /// <summary>
    /// key-value 键值对:value可以是序列化的数据
    /// </summary>
    public class RedisStringService : RedisBase
    {
        //#region 赋值
        ///// <summary>
        ///// 设置key的value
        ///// </summary>
        //public static void SetValue(string key, string value)
        //{
        //    RedisBase.iClient.SetValue(key, value);
        //}

        ///// <summary>
        ///// 设置key的value
        ///// </summary>
        //public static void SetValue(string key, string value, DateTime dt)
        //{
        //    var timeSpan = dt.Subtract(DateTime.Now);
        //    RedisBase.iClient.SetValue(key, value, timeSpan);
        //}
        ///// <summary>
        ///// 设置key的value
        ///// </summary>
        //public static bool Set(string key, string value)
        //{
        //    return RedisBase.iClient.Set<string>(key, value);
        //}
        ///// <summary>
        ///// 设置key的value并设置过期时间
        ///// </summary>
        //public static bool Set(string key, string value, DateTime dt)
        //{
        //    return RedisBase.iClient.Set<string>(key, value, dt);
        //}
        ///// <summary>
        ///// 设置key的value并设置过期时间
        ///// </summary>
        //public static bool Set(string key, string value, TimeSpan sp)
        //{
        //    return RedisBase.iClient.Set<string>(key, value, sp);
        //}
        ///// <summary>
        ///// 设置多个key/value
        ///// </summary>
        //public static void Set(Dictionary<string, string> dic)
        //{
        //    RedisBase.iClient.SetAll(dic);
        //}

        //#endregion

        //#region 追加
        ///// <summary>
        ///// 在原有key的value值之后追加value
        ///// </summary>
        //public static long Append(string key, string value)
        //{
        //    return RedisBase.iClient.AppendToValue(key, value);
        //}
        //#endregion

        //#region 获取值
        ///// <summary>
        ///// 获取key的value值
        ///// </summary>
        ////public static string Get(string key)
        ////{
        ////    return RedisBase.iClient.GetValue(key);
        ////}
        //public static string Get(string key)
        //{
        //    using (IRedisClient redisClient = RedisManager.GetClient())
        //    {
        //        return RedisBase.iClient.GetValue(key);
        //    }
        //}
        ///// <summary>
        ///// 获取多个key的value值
        ///// </summary>
        //public static List<string> Get(List<string> keys)
        //{
        //    return RedisBase.iClient.GetValues(keys);
        //}
        ///// <summary>
        ///// 获取多个key的value值
        ///// </summary>
        //public static List<T> Get<T>(List<string> keys)
        //{
        //    return RedisBase.iClient.GetValues<T>(keys);
        //}
        //#endregion

        //#region 移除
        ///// <summary>
        ///// 设置key的value
        ///// </summary>
        //public static bool Remove(string key)
        //{
        //    return RedisBase.iClient.Remove(key);
        //}
        //#endregion

        //#region 获取旧值赋上新值
        ///// <summary>
        ///// 获取旧值赋上新值
        ///// </summary>
        ////public static string GetAndSetValue(string key, string value)
        ////{
        ////    return RedisBase.iClient.GetAndSetValue(key, value);
        ////}
        //#endregion

        //#region 辅助方法
        ///// <summary>
        ///// 获取值的长度
        ///// </summary>
        ////public static long GetLength(string key)
        ////{
        ////    return RedisBase.iClient.GetStringCount(key);
        ////}
        ///// <summary>
        ///// 自增1，返回自增后的值
        ///// </summary>
        //public static long Incr(string key)
        //{
        //    return RedisBase.iClient.IncrementValue(key);
        //}
        ///// <summary>
        ///// 自增count，返回自增后的值
        ///// </summary>
        ////public static double IncrBy(string key, double count)
        ////{
        ////    return RedisBase.iClient.IncrementValueBy(key, count);
        ////}
        ///// <summary>
        ///// 自减1，返回自减后的值
        ///// </summary>
        //public static long Decr(string key)
        //{
        //    return RedisBase.iClient.DecrementValue(key);
        //}
        ///// <summary>
        ///// 自减count ，返回自减后的值
        ///// </summary>
        ///// <param name="key"></param>
        ///// <param name="count"></param>
        ///// <returns></returns>
        //public static long DecrBy(string key, int count)
        //{
        //    return RedisBase.iClient.DecrementValueBy(key, count);
        //}
        //#endregion
    }
}
