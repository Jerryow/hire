using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrMatch.Common.Extension
{
    public static class ClassExtension
    {
        /// <summary>
        /// 实体序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static string ToJson<T>(this T entity) where T : class
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(entity);
        }
    }
}
