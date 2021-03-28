using MrMatch.Application.Config.Oup;
using MrMatch.Common.Redis.MyHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrMatch.Application.Cache
{
    public class SetCacheHelper
    {
        #region Admin
        /// <summary>
        /// 设置后台token
        /// </summary>
        /// <param name="pkid"></param>
        /// <param name="token"></param>
        /// <param name="env">生产/测试环境</param>
        /// <param name="autoLogin">是否自动登陆</param>
        public static void SetAdminLoginToken(string pkid, string token, string env)
        {
            if (env.Trim().ToLower() == "true")
            {
                RedisStringHelper.Set("AdminLogin:A_" + pkid, token, DateTime.Now.AddMinutes(720));
            }
            else
            {
                RedisStringHelper.Set("AdminLogin:ATest_" + pkid, token, DateTime.Now.AddMinutes(720));
            }
        }
        #endregion

        #region Candidate
        /// <summary>
        /// 设置PC简历token
        /// </summary>
        /// <param name="pkid"></param>
        /// <param name="token"></param>
        /// <param name="env">生产/测试环境</param>
        /// <param name="autoLogin">是否自动登陆</param>
        public static void SetCandidateLoginToken(string pkid, string token, string env, bool autoLogin)
        {
            if (env.Trim().ToLower() == "true")
            {
                if (autoLogin)
                {
                    RedisStringHelper.Set("CandidateLogin:C_" + pkid, token, DateTime.Now.AddMinutes(1800));
                }
                else
                {
                    RedisStringHelper.Set("CandidateLogin:C_" + pkid, token, DateTime.Now.AddMinutes(720));
                }
            }
            else
            {
                if (autoLogin)
                {
                    RedisStringHelper.Set("CandidateLogin:CTest_" + pkid, token, DateTime.Now.AddMinutes(1800));
                }
                else
                {
                    RedisStringHelper.Set("CandidateLogin:CTest_" + pkid, token, DateTime.Now.AddMinutes(720));
                }
            }
        }
        /// <summary>
        /// 设置小程序Api Token
        /// </summary>
        /// <param name="pkid"></param>
        /// <param name="token"></param>
        /// <param name="env">生产/测试环境</param>
        /// <param name="autoLogin">是否自动登陆</param>
        public static void SetCandidateApiLoginToken(string pkid, string token, string env, bool autoLogin)
        {
            if (env.Trim().ToLower() == "true")
            {
                if (autoLogin)
                {
                    RedisStringHelper.Set("CandidateLogin:CApi_" + pkid, token);
                }
                else
                {
                    RedisStringHelper.Set("CandidateLogin:CApi_" + pkid, token);
                }
            }
            else
            {
                if (autoLogin)
                {
                    RedisStringHelper.Set("CandidateLogin:CApiTest_" + pkid, token);
                }
                else
                {
                    RedisStringHelper.Set("CandidateLogin:CApiTest_" + pkid, token);
                }
            }
        }
        #endregion

        #region Biz
        /// <summary>
        /// 设置B端登陆token缓存
        /// </summary>
        /// <param name="pkid"></param>
        /// <param name="token"></param>
        /// <param name="env">生产/测试环境</param>
        /// <param name="autoLogin">是否自动登陆</param>
        public static void SetBizLoginToken(string pkid, string token, string env, bool autoLogin)
        {
            if (env.Trim().ToLower() == "true")
            {
                if (autoLogin)
                {
                    RedisStringHelper.Set("BizLogin:B_" + pkid, token, DateTime.Now.AddMinutes(1800));
                }
                else
                {
                    RedisStringHelper.Set("BizLogin:B_" + pkid, token, DateTime.Now.AddMinutes(720));
                }
            }
            else
            {
                if (autoLogin)
                {
                    RedisStringHelper.Set("BizLogin:BTest_" + pkid, token, DateTime.Now.AddMinutes(1800));
                }
                else
                {
                    RedisStringHelper.Set("BizLogin:BTest_" + pkid, token, DateTime.Now.AddMinutes(720));
                }
            }
        }


        #endregion

        #region 验证码缓存

        /// <summary>
        /// 设置验证码缓存
        /// </summary>
        /// <param name="phone"></param>
        /// <param name="result">验证码</param>
        /// <param name="type">1:B端  2:C端</param>
        public static void SetVerifyCode(string phone, string result, int type)
        {
            if (type == 1)
            {
                RedisStringHelper.Set("VerifyCode:BPhone_" + phone, result, DateTime.Now.AddMinutes(15));
            }
            else
            {
                RedisStringHelper.Set("VerifyCode:CPhone_" + phone, result, DateTime.Now.AddMinutes(15));
            }
        }

        /// <summary>
        /// 设置注册验证码缓存
        /// </summary>
        /// <param name="email">邮箱</param>
        /// <param name="code">验证码</param>
        public static void SetBizEmailCode(string email, string code)
        {
            RedisStringHelper.Set("BizEmailCode:" + email, code, DateTime.Now.AddMinutes(15));
        }
        #endregion

        #region 基础数据设置
        /// <summary>
        /// 城市2级目录
        /// </summary>
        /// <param name="list"></param>
        public static void SetBasicCity(List<ProvinceListOup> list)
        {
            var str = Newtonsoft.Json.JsonConvert.SerializeObject(list);
            RedisStringHelper.Set("BasicData:BasicCity", str);
        }

        /// <summary>
        /// 职能三级目录
        /// </summary>
        /// <param name="list"></param>
        public static void SetFunctionCascader(List<FunctionFirstCascaderOup> list)
        {
            var str = Newtonsoft.Json.JsonConvert.SerializeObject(list);
            RedisStringHelper.Set("BasicData:Function", str);
        }

        /// <summary>
        /// 设置系统配置缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="val"></param>
        public static void SetSiteConfig(string key, string val)
        {
            RedisHashHelper.SetHashValue("SiteConfig", key, val);
        }

        /// <summary>
        /// 设置消息模板缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="val"></param>
        public static void SetMessageTemplate(string key, string val)
        {
            RedisHashHelper.SetHashValue("MessageTemplate", key, val);
        }
        #endregion
    }
}
