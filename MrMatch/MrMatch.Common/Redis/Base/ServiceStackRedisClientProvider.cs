using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MrMatch.Common.Redis.Base
{
    /// <summary>
    /// 通过ServiceStack.Redis实现的Redis的客户端操作类型
    /// </summary>
    //public sealed class ServiceStackRedisClientProvider
    //{
    //    #region 私有变量

    //    //线程同步变量
    //    private static readonly object lockObject = new object();

    //    //redis链接池管理对象
    //    private static volatile PooledRedisClientManager _instance = null;

    //    //配置文件里面的ServiceStack详细配置设置
    //    private static ServiceStackDetails _serviceStackDetails;

    //    //可以自行配置ServiceStack的配置对象
    //    private static ServiceStackConfigEntry _serviceStackConfigEntry;

    //    #endregion

    //    #region 私有构造函数

    //    /// <summary>
    //    /// 私有构造函数，禁止外部通过new关键字来创建该对象实例
    //    /// </summary>
    //    private ServiceStackRedisClientProvider() { }

    //    #endregion

    //    #region 获取PooledRedisClientManager实例的方法

    //    /// <summary>
    //    /// 获取redis链接池管理对象实例
    //    /// 实例发生变化的集中情况：
    //    /// 1.实例为空
    //    /// 2.配置文件发生变化
    //    /// </summary>
    //    /// <param name="startByConfigFile">这是一个布尔值，true表示根据配置文件的配置启动，false表示是根据配置对象启动</param>
    //    /// <returns>返回PooledRedisClientManager类型的对象实例</returns>
    //    private static PooledRedisClientManager GetInstance(bool startByConfigFile)
    //    {
    //        if (_instance == null)
    //        {
    //            lock (lockObject)
    //            {
    //                if (_instance == null)
    //                {
    //                    string[] readWriteServerList = null;
    //                    string[] readOnlyServerList = null;
    //                    RedisClientManagerConfig managerConfig = null;

    //                    //根据我们配置文件中数据来设置启动信息（app.config或者web.config）
    //                    if (startByConfigFile && (_serviceStackDetails != null))
    //                    {
    //                        managerConfig = new RedisClientManagerConfig()
    //                        {
    //                            AutoStart = _serviceStackDetails.AutoStart,
    //                            MaxReadPoolSize = _serviceStackDetails.MaxReadPoolSize,
    //                            MaxWritePoolSize = _serviceStackDetails.MaxWritePoolSize,
    //                        };

    //                        readWriteServerList = GetRedisHosts(_serviceStackDetails.ReadWriteHosts);
    //                        readOnlyServerList = GetRedisHosts(_serviceStackDetails.ReadOnlyHosts);
    //                    }
    //                    else if (!startByConfigFile && (_serviceStackConfigEntry != null))//根据配置对象来设置启动信息(ServiceStackConfigEntry)
    //                    {
    //                        managerConfig = new RedisClientManagerConfig()
    //                        {
    //                            AutoStart = _serviceStackConfigEntry.AutoStart,
    //                            MaxReadPoolSize = _serviceStackConfigEntry.MaxReadPoolSize,
    //                            MaxWritePoolSize = _serviceStackConfigEntry.MaxWritePoolSize,
    //                        };

    //                        readWriteServerList = GetRedisHosts(_serviceStackConfigEntry.ReadWriteHosts);
    //                        readOnlyServerList = GetRedisHosts(_serviceStackConfigEntry.ReadOnlyHosts);
    //                    }
    //                    else
    //                    {
    //                        throw new InvalidOperationException("Redis客户端初始化配置失败！");
    //                    }

    //                    if ((readWriteServerList != null && readWriteServerList.Length > 0) && (readOnlyServerList != null && readOnlyServerList.Length <= 0))
    //                    {
    //                        _instance = new PooledRedisClientManager(readWriteServerList);
    //                    }

    //                    if ((readWriteServerList != null && readWriteServerList.Length > 0) && (readOnlyServerList != null && readOnlyServerList.Length > 0))
    //                    {
    //                        _instance = new PooledRedisClientManager(readWriteServerList, readOnlyServerList, managerConfig);
    //                    }
    //                }
    //            }
    //        }
    //        return _instance;
    //    }

    //    /// <summary>
    //    /// 解析Redis服务器列表，该列表格式IP[:Port]
    //    /// </summary>
    //    /// <param name="redisHosts">包含一个或者多个Redis服务器地址的字符串列表，以逗号做为分隔符</param>
    //    /// <returns>返回Redis服务器地址列表</returns>
    //    private static string[] GetRedisHosts(string redisHosts)
    //    {
    //        if (string.IsNullOrWhiteSpace(redisHosts) || string.IsNullOrEmpty(redisHosts))
    //        {
    //            return new string[] { };
    //        }
    //        var hosts = redisHosts.Split(',');
    //        foreach (var host in hosts)
    //        {
    //            if (!Regex.IsMatch(host, @"^(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9])\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[0-9]):\d{3,4}$"))
    //            {
    //                throw new InvalidOperationException("Redis服务器地址格式不正确！");
    //            }
    //        }
    //        return hosts;
    //    }

    //    #endregion

    //    #region 提供对外访问接口

    //    /// <summary>
    //    /// 获取Redis客户端对象实例
    //    /// </summary>
    //    /// <param name="redisClientName">在配置文件中，Redis客户端的名称</param>
    //    /// <param name="databaseIndex">redis逻辑分为16个数据库，排序为：0-15，我们默认使用的是0号数据库，数据库当前的索引值</param>
    //    /// <returns>返回IRedisClient对象实例</returns>
    //    public static IRedisClient GetRedisClient(string redisClientName, int databaseIndex = 0)
    //    {
    //        //获取配置数据
    //        ParameterValidityChecker.RequiredParameterStringNotNullOrWhiteSpace(redisClientName, "Redis客户端的名称不能为空！");
    //        var _configurationManager = (ConfigurationFrameworkManager)ConfigurationManager.GetSection("Framework");
    //        if (_configurationManager != null)
    //        {
    //            _serviceStackDetails = _configurationManager.RedisClientConfiguration.GetServiceStackDetails(redisClientName);
    //            if (_serviceStackDetails == null)
    //            {
    //                throw new InvalidOperationException("以ServiceStack.Redis为实现技术的Redis客户端的配置有误！");
    //            }
    //        }
    //        else
    //        {
    //            throw new InvalidOperationException("以ServiceStack.Redis为实现技术的Redis客户端的配置有误！");
    //        }

    //        //实例化Redis客户端实例对象
    //        var pooledRedisClientManager = GetInstance(true);
    //        var redisClient = pooledRedisClientManager.GetClient();
    //        if (!string.IsNullOrEmpty(_serviceStackDetails.Password))
    //        {
    //            redisClient.Password = _serviceStackDetails.Password;
    //        }
    //        redisClient.Db = databaseIndex;
    //        return redisClient;
    //    }

    //    /// <summary>
    //    /// 获取Redis客户端对象实例
    //    /// </summary>
    //    /// <param name="serviceStackConfigEntry">在配置文件中，Redis客户端的名称</param>
    //    /// <param name="databaseIndex">redis逻辑分为16个数据库，排序为：0-15，我们默认使用的是0号数据库，数据库当前的索引值</param>
    //    /// <returns>返回IRedisClient对象实例</returns>
    //    public static IRedisClient GetRedisClient(ServiceStackConfigEntry serviceStackConfigEntry, int databaseIndex = 0)
    //    {
    //        //获取配置数据
    //        if (serviceStackConfigEntry == null)
    //        {
    //            throw new ArgumentNullException("以ServiceStack.Redis为实现技术的Redis客户端的配置对象不能为空！");
    //        }
    //        else
    //        {
    //            _serviceStackConfigEntry = serviceStackConfigEntry;
    //        }

    //        if (string.IsNullOrEmpty(_serviceStackConfigEntry.ReadWriteHosts) || string.IsNullOrWhiteSpace(_serviceStackConfigEntry.ReadWriteHosts))
    //        {
    //            throw new InvalidOperationException("【ReadWriteHosts】必须设置其值！");
    //        }

    //        //实例化Redis客户端实例对象
    //        var pooledRedisClientManager = GetInstance(false);
    //        var redisClient = pooledRedisClientManager.GetClient();
    //        if (!string.IsNullOrEmpty(_serviceStackConfigEntry.Password) && !string.IsNullOrWhiteSpace(_serviceStackConfigEntry.Password))
    //        {
    //            redisClient.Password = _serviceStackConfigEntry.Password;
    //        }
    //        redisClient.Db = databaseIndex;
    //        return redisClient;
    //    }

    //    #endregion
    //}
}
