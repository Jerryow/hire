using MrMatch.Common.Encrypt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;

namespace MrMatch.Admin.Handler
{
    /// <summary>
    /// 会话管理
    /// </summary>
    public class CookiesManager
    {


        /// <summary>
        /// 获取cookie()
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetCookie(string key)
        {
            HttpCookie cookies = HttpContext.Current.Request.Cookies[key];
            if (cookies == null || string.IsNullOrEmpty(cookies.Value)) return "";
            var change = HttpUtility.UrlDecode(cookies.Value);
            string deVal = Encryption.DecryptString(change);
            //string deVal = Encryption.DecryptString(cookies.Value);
            return deVal;
        }

        /// <summary>
        /// 获取指定Cookie值
        /// </summary>
        /// <param name="cookiename">cookiename</param>
        /// <returns></returns>
        public static string GetCookieValue(string cookiename, HttpRequestMessage request)
        {
            CookieHeaderValue cookieHeader = request.Headers.GetCookies().FirstOrDefault();
            if (cookieHeader == null)
                return string.Empty;

            var cookie = cookieHeader.Cookies.FirstOrDefault(w => w.Name == cookiename);
            if (cookie == null)
                return string.Empty;

            return DecryptCookieValue(cookie.Value);

        }
        /// <summary>
        /// 设置cookie
        /// </summary>
        /// <param name="cookiename"></param>
        /// <param name="cookievalue"></param>
        /// <param name="request"></param>
        /// <param name="resp"></param>
        public static void SetCookie(string cookiename, string cookievalue, HttpRequestMessage request, HttpResponseMessage resp, int minutes)
        {
            var cookie = new CookieHeaderValue(cookiename, EncryptionCookieValue(cookievalue));
            cookie.Expires = DateTimeOffset.Now.AddMinutes(minutes);
            cookie.Domain = request.RequestUri.Host;
            cookie.HttpOnly = true;
            cookie.Path = "/";

            resp.Headers.AddCookies(new CookieHeaderValue[] { cookie });
        }

        /// <summary>
        /// Cookie加密
        /// </summary>
        /// <param name="cookieValue"></param>
        public static string EncryptionCookieValue(string cookieValue)
        {
            //调用加密算法进行解密
            return Encryption.EncryptString(cookieValue);
        }

        /// <summary>
        ///  Cookie解密
        /// </summary>
        /// <param name="cookieValue"></param>
        /// <returns></returns>
        public static string DecryptCookieValue(string cookieValue)
        {
            //调用解密算法实施解密
            string dstr = Encryption.DecryptString(cookieValue);
            return dstr == "-100" ? "" : dstr;
        }
    }
}