using MrMatch.Application.Config.Oup;
using MrMatch.Common.Redis.MyHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrMatch.Application.Cache
{
    public static class GetCacheHelper
    {
        #region string
        /// <summary>
        /// 获取验证码缓存
        /// </summary>
        /// <param name="phone">手机号</param>
        /// <param name="type">1:B端  2:C端</param>
        /// <returns></returns>
        public static string GetVerifyCode(string phone, int type)
        {
            if (type == 1)
            {
                return RedisStringHelper.Get("VerifyCode:BPhone_" + phone);
            }
            else
            {
                return RedisStringHelper.Get("VerifyCode:CPhone_" + phone);
            }
        }

        /// <summary>
        /// 获取A端的token
        /// </summary>
        /// <param name="pkid"></param>
        /// <param name="env"></param>
        /// <returns></returns>
        public static string GetAdminToken(string pkid, string env)
        {
            if (env.Trim().ToString() == "false")
            {
                return RedisStringHelper.Get("AdminLogin:ATest_" + pkid);
            }
            else
            {
                return RedisStringHelper.Get("AdminLogin:A_" + pkid);
            }
        }

        /// <summary>
        /// 获取B端的token
        /// </summary>
        /// <param name="pkid"></param>
        /// <param name="env"></param>
        /// <returns></returns>
        public static string GetBizToken(string pkid, string env)
        {
            if (env.Trim().ToString() == "true")
            {
                return RedisStringHelper.Get("BizLogin:B_" + pkid);
            }
            else
            {
                return RedisStringHelper.Get("BizLogin:BTest_" + pkid);
            }
        }

        /// <summary>
        /// 获取注册验证码缓存
        /// </summary>
        /// <param name="email">邮箱</param>
        public static string GetBizEmailCode(string email)
        {
            return RedisStringHelper.Get("BizEmailCode:" + email);
        }

        /// <summary>
        /// 获取PC简历token'
        /// </summary>
        /// <param name="pkid"></param>
        /// <param name="env"></param>
        /// <returns></returns>
        public static string GetCandidateToken(string pkid, string env)
        {
            if (env.Trim().ToString() == "true")
            {
                return RedisStringHelper.Get("CandidateLogin:C_" + pkid);
            }
            else
            {
                return RedisStringHelper.Get("CandidateLogin:CTest_" + pkid);
            }
        }

        /// <summary>
        /// 获取小程序token
        /// </summary>
        /// <param name="pkid"></param>
        /// <param name="env"></param>
        /// <returns></returns>
        public static string GetCandidateApiToken(string pkid, string env)
        {
            if (env.Trim().ToString() == "true")
            {
                return RedisStringHelper.Get("CandidateLogin:CApi_" + pkid);
            }
            else
            {
                return RedisStringHelper.Get("CandidateLogin:CApiTest_" + pkid);
            }
        }
        /// <summary>
        /// 获取职能意向目录
        /// </summary>
        public static List<FunctionFirstCascaderOup> GetFunctionForCascader()
        {
            try
            {
                var str = RedisStringHelper.Get("BasicData:Function");
                if (string.IsNullOrEmpty(str))
                {
                    return null;
                }
                var res = Newtonsoft.Json.JsonConvert.DeserializeObject<List<FunctionFirstCascaderOup>>(str);
                return res;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// 获取意向城市
        /// </summary>
        public static List<ProvinceListOup> GetBasicCity()
        {
            try
            {
                var str = RedisStringHelper.Get("BasicData:BasicCity");
                if (string.IsNullOrEmpty(str))
                {
                    return null;
                }
                var res = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ProvinceListOup>>(str);
                return res;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region hash
        /// <summary>
        /// 获取邮件发送模板
        /// </summary>
        /// <param name="key">CommonEnum->TP_MessageTemplate</param>
        /// <returns></returns>
        public static string GetTemplate(string key)
        {
            return RedisHashHelper.GetHashValue("MessageTemplate", key);
        }

        public static string GetSiteConfig(string code)
        {
            return RedisHashHelper.GetHashValue("SiteConfig", code);
        }
        #endregion


    }
}
