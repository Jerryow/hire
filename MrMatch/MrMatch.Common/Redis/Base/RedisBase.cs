using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrMatch.Common.Redis
{
    /// <summary>
    /// RedisBase类，是redis操作的基类，继承自IDisposable接口，主要用于释放内存
    /// </summary>
    public abstract class RedisBase /*: IDisposable*/
    {
        //public static RedisClient iClient { get; private set; }
        //private bool _disposed = false;
        //static RedisBase()
        //{
        //    iClient = RedisManager.GetClient();
        //}

        ////清除所有缓存
        //public virtual void FlushAll()
        //{
        //    iClient.FlushAll();
        //}

        ////关闭  释放
        //protected virtual void Dispose(bool disposing)
        //{
        //    if (!this._disposed)
        //    {
        //        if (disposing)
        //        {
        //            iClient.Dispose();
        //            iClient = null;
        //        }
        //    }
        //    this._disposed = true;
        //}
        //public void Dispose()
        //{
        //    Dispose(true);
        //    GC.SuppressFinalize(this);
        //}
        ///// <summary>
        ///// 保存数据DB文件到硬盘
        ///// </summary>
        //public void Save()
        //{
        //    iClient.Save();
        //}
        ///// <summary>
        ///// 异步保存数据DB文件到硬盘
        ///// </summary>
        //public void SaveAsync()
        //{
        //    iClient.SaveAsync();
        //}
    }
}
