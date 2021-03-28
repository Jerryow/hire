using MrMatch.Common.Redis.MyHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrMatch.Application.Cache
{
    public static class ClearCacheHelper
    {
        #region admin
        /// <summary>
        /// 清除后台token
        /// </summary>
        /// <param name="pkid"></param>
        /// <param name="env"></param>
        public static void ClearAdminLoginToken(string pkid, string env)
        {
            if (env.Trim().ToLower() == "true")
            {
                RedisStringHelper.Clear("AdminLogin:A_" + pkid);
            }
            else
            {
                RedisStringHelper.Clear("AdminLogin:ATest_" + pkid);
            }
        }
        #endregion

        #region B端
        /// <summary>
        /// 清除B端token
        /// </summary>
        /// <param name="pkid"></param>
        /// <param name="env"></param>
        public static void ClearBizLoginToken(string pkid, string env)
        {
            if (env.Trim().ToLower() == "true")
            {
                RedisStringHelper.Clear("BizLogin:B_" + pkid);
            }
            else
            {
                RedisStringHelper.Clear("BizLogin:BTest_" + pkid);
            }
        }
        #endregion

        #region C端
        /// <summary>
        /// 清除PC简历token
        /// </summary>
        /// <param name="pkid"></param>
        /// <param name="env"></param>
        public static void ClearCandidateLoginToken(string pkid, string env)
        {
            if (env.Trim().ToLower() == "true")
            {
                RedisStringHelper.Clear("CandidateLogin:C_" + pkid);
            }
            else
            {
                RedisStringHelper.Clear("CandidateLogin:CTest_" + pkid);
            }
        }

        /// <summary>
        /// 清除小程序token
        /// </summary>
        /// <param name="pkid"></param>
        /// <param name="env"></param>
        public static void ClearCandidateApiLoginToken(string pkid, string env)
        {
            if (env.Trim().ToLower() == "true")
            {
                RedisStringHelper.Clear("CandidateLogin:CApi_" + pkid);
            }
            else
            {
                RedisStringHelper.Clear("CandidateLogin:CApiTest_" + pkid);
            }
        }
        #endregion

        #region 验证码
        /// <summary>
        /// 清除手机验证码token
        /// </summary>
        /// <param name="phone"></param>
        /// <param name="type">1:B端  2:C端</param>
        public static void ClearVerifyCode(string phone, int type)
        {
            if (type == 1)
            {
                RedisStringHelper.Clear("VerifyCode:BPhone_" + phone);
            }
            else
            {
                RedisStringHelper.Clear("VerifyCode:CPhone_" + phone);
            }
        }

        /// <summary>
        /// 清除邮箱注册验证码
        /// </summary>
        /// <param name="email"></param>
        public static void ClearBizEmailVerifyCode(string email)
        {
            RedisStringHelper.Clear("BizEmailCode:" + email);
        }
        #endregion
    }
}
