using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MrMatch.CandidateClient.Handler
{
    public static class HtmlHelperExtend
    {
        /// <summary>
        /// 给CSS文件或JS文件指定版本号
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="url">CSS或JS路径</param>
        /// <returns></returns>
        public static string GetCssJsUrl(this HtmlHelper helper, string url)
        {
            string version = ConfigurationManager.AppSettings["JsVersion"];
            version = version == null ? "1.0" : version;
            return url += "?v=" + version;
        }
    }
}