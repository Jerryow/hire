using MrMatch.Domain.EntityBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrMatch.MysqlFramework.Extensions
{
    public static class EntityVerifyExtension
    {
        /// <summary>
        /// 验证实体null
        /// 是实体  返回  true
        /// 不是实体 返回 false
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static bool IsNullEntity(this Entity source)
        {
            if (source == null || source.PKID <= 0)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 验证实体null
        /// 是实体  返回  true
        /// 不是实体 返回 false
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static bool IsNullEntity(this CommunityEntity source)
        {
            if (source == null || source.id <= 0)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 验证实体null
        /// 是实体  返回  true
        /// 不是实体 返回 false
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static bool IsNullEntity(this object source)
        {
            if (source == null)
            {
                return false;
            }
            return true;
        }
    }
}
